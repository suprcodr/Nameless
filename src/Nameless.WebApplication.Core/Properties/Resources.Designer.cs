﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nameless.WebApplication.Core.Properties {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Nameless.WebApplication.Core.Properties.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Algorithm must be &quot;{0}&quot;..
        /// </summary>
        internal static string Assert_TokenAlgorithm {
            get {
                return ResourceManager.GetString("Assert_TokenAlgorithm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bad request..
        /// </summary>
        internal static string BadRequest {
            get {
                return ResourceManager.GetString("BadRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Task was canceled..
        /// </summary>
        internal static string TaskCanceled {
            get {
                return ResourceManager.GetString("TaskCanceled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is already a role with the same name..
        /// </summary>
        internal static string Validation_DuplicateRoleName {
            get {
                return ResourceManager.GetString("Validation_DuplicateRoleName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid JSON web token..
        /// </summary>
        internal static string Validation_InvalidJWT {
            get {
                return ResourceManager.GetString("Validation_InvalidJWT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The role name is invalid..
        /// </summary>
        internal static string Validation_InvalidRoleName {
            get {
                return ResourceManager.GetString("Validation_InvalidRoleName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User name or password invalid..
        /// </summary>
        internal static string Validation_InvalidUserNameOrPassword {
            get {
                return ResourceManager.GetString("Validation_InvalidUserNameOrPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Must be a non-zero TimeSpan..
        /// </summary>
        internal static string Validation_NonZeroTimeSpan {
            get {
                return ResourceManager.GetString("Validation_NonZeroTimeSpan", resourceCulture);
            }
        }
    }
}
