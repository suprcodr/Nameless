using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nameless.Skeleton.Properties;

namespace Nameless.Skeleton.Helpers {

    /// <summary>
    /// An <see cref="object"/> helper class.
    /// </summary>
    public static class ObjectHelper {

        #region Copyright (c) 2009, Andre Loker <mail@andreloker.de>

        // Permission to use, copy, modify, and/or distribute this software for any
        // purpose with or without fee is hereby granted, provided that the above
        // copyright notice and this permission notice appear in all copies.
        //
        // THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
        // WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
        // MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
        // ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
        // WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
        // ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
        // OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.

        #endregion Copyright (c) 2009, Andre Loker <mail@andreloker.de>

        #region Public Static Methods

        /// <summary>
        /// Invokes a method on the <paramref name="target"/>
        /// </summary>
        /// <param name="target">The target, must not be <c>null</c></param>
        /// <param name="methodName">Name of the method, must not be <c>null</c></param>
        /// <param name="args">The arguments passed to the method.</param>
        /// <remarks>
        /// If the target type contains multiple overload of the given <paramref name="methodName"/>
        /// <see cref="Send"/> tries to find the best match.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// No method with the given <paramref name="methodName"/> was found or the invocation
        /// is ambiguous, ie. multiple methods match.
        /// </exception>
        public static object Send(object target, string methodName, params object[] args) {
            return Send<object>(target, methodName, args);
        }

        /// <summary>
        /// Invokes a method on the <paramref name="target"/>
        /// </summary>
        /// <param name="target">The target, must not be <c>null</c></param>
        /// <param name="methodName">Name of the method, must not be <c>null</c></param>
        /// <param name="args">The arguments passed to the method.</param>
        /// <remarks>
        /// If the target type contains multiple overload of the given <paramref name="methodName"/>
        /// <see cref="Send"/> tries to find the best match.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// No method with the given <paramref name="methodName"/> was found or the invocation
        /// is ambiguous, ie. multiple methods match.
        /// </exception>
        /// <returns>The value returned from the invoked method cast to a
        /// <typeparamref name="T"/>
        /// </returns>
        public static T Send<T>(object target, string methodName, params object[] args) {
            Prevent.ParameterNull(target, nameof(target));
            Prevent.ParameterNullOrWhiteSpace(methodName, nameof(methodName));

            var type = target.GetType();
            var methods = GetMethodCandidates(methodName, type);
            var methodToInvoke = FindBestFittingMethod(methods, args);

            return InvokeFunction<T>(target, methodToInvoke, args);
        }

        #endregion Public Static Methods

        #region Private Static Methods

        private static IEnumerable<MethodInfo> GetMethodCandidates(string methodName, Type type) {
            return type
                .GetTypeInfo()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.Name == methodName);
        }

        private static MethodInfo FindBestFittingMethod(IEnumerable<MethodInfo> methods, object[] args) {
            var highestScore = -1;
            var matchingMethodCount = 0;
            MethodInfo selectedMethod = null;

            foreach (var method in methods) {
                var methodScore = RateMethodMatch(method.GetParameters(), args);

                if (methodScore > highestScore) {
                    matchingMethodCount = 1;
                    highestScore = methodScore;
                    selectedMethod = method;
                } else if (methodScore == highestScore) {
                    // count the number of matches, match count > 1 => ambiguous call
                    matchingMethodCount++;
                }
            }

            if (matchingMethodCount > 1) {
                throw new ArgumentException(Resources.AmbiguousMethodInvocation);
            }

            return selectedMethod;
        }

        private static int RateMethodMatch(ParameterInfo[] parameters, object[] args) {
            var argsLength = args != null ? args.Length : 0;

            if (parameters.Length == argsLength) {
                return argsLength != 0
                    ? RateParameterMatches(parameters, args)
                    : 1;
            }

            return 0;
        }

        private static int RateParameterMatches(ParameterInfo[] parameters, object[] args) {
            var score = 0;

            for (var idx = 0; idx < args.Length; ++idx) {
                var typeMatchScore = RateParameterMatch(parameters[idx], args[idx]);

                if (typeMatchScore == 0) { return 0; }

                score += typeMatchScore;
            }

            return score;
        }

        private static int RateParameterMatch(ParameterInfo parameter, object arg) {
            var parameterType = parameter.ParameterType;

            return arg == null
                ? RateNullArgument(parameterType)
                : RateNonNullArgument(arg, parameterType);
        }

        private static int RateNullArgument(Type parameterType) {
            return CanBeNull(parameterType) ? 1 : 0;
        }

        private static bool CanBeNull(Type type) {
            return !type.GetTypeInfo().IsValueType || type.IsNullable();
        }

        private static int RateNonNullArgument(object arg, Type parameterType) {
            var argType = arg.GetType();

            // perfect match!
            if (argType == parameterType) { return 2; }

            // at least convertible to parameter type
            if (parameterType.GetTypeInfo().IsAssignableFrom(argType)) { return 1; }

            return 0;
        }

        private static T InvokeFunction<T>(object target, MethodInfo method, object[] args) {
            return (T)method.Invoke(target, args);
        }

        #endregion Private Static Methods
    }
}