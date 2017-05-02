using System.Text.RegularExpressions;

namespace Nameless.Skeleton.Helpers {

    /// <summary>
    /// Validation/Format functions for CNPJ and CPF (brazilian documents).
    /// </summary>
    public static class Brazil {

        #region Public Inner Classes

        /// <summary>
        /// Format.
        /// </summary>
        public static class Formatter {

            #region Public Static Methods

            /// <summary>
            /// Formats a CNPJ number (<see cref="double"/>).
            /// </summary>
            /// <param name="cnpj">The CNPJ numbers.</param>
            /// <returns>The formatted <see cref="string"/> representation.</returns>
            public static string Cnpj(double cnpj) {
                return string.Format(@"{0:00\.000\.000\/0000\-00}", cnpj);
            }

            /// <summary>
            /// Formats a CPF number (<see cref="double"/>).
            /// </summary>
            /// <param name="cpf">The CPF numbers.</param>
            /// <returns>The formatted <see cref="string"/> representation.</returns>
            public static string Cpf(double cpf) {
                return string.Format(@"{0:000\.000\.000\-00}", cpf);
            }

            #endregion Public Static Methods
        }

        /// <summary>
        /// Validation.
        /// </summary>
        public static class Validator {

            #region Public Static Methods

            /// <summary>
            /// Validates a CNPJ number (<see cref="double"/>).
            /// </summary>
            /// <param name="cnpj">The CNPJ number.</param>
            /// <returns><c>true</c> if is valid, otherwise, <c>false</c>.</returns>
            public static bool Cnpj(double cnpj) {
                return Cnpj(Formatter.Cnpj(cnpj));
            }

            /// <summary>
            /// Validates a CNPJ document (<see cref="string"/>).
            /// </summary>
            /// <param name="cnpj">The CNPJ document.</param>
            /// <returns><c>true</c> if is valid, otherwise, <c>false</c>.</returns>
            public static bool Cnpj(string cnpj) {
                if (string.IsNullOrWhiteSpace(cnpj)) { return false; }

                return CnpjFormatValidator(cnpj) && CnpjValueValidator(cnpj);
            }

            /// <summary>
            /// Validates a CPF number (<see cref="double"/>).
            /// </summary>
            /// <param name="cpf">The CPF number.</param>
            /// <returns><c>true</c> if is valid, otherwise, <c>false</c>.</returns>
            public static bool Cpf(double cpf) {
                return Cpf(Formatter.Cpf(cpf));
            }

            /// <summary>
            /// Validates a CPF document (<see cref="string"/>).
            /// </summary>
            /// <param name="cpf">The CPF document.</param>
            /// <returns><c>true</c> if is valid, otherwise, <c>false</c>.</returns>
            public static bool Cpf(string cpf) {
                if (string.IsNullOrWhiteSpace(cpf)) { return false; }

                return CpfFormatValidator(cpf) && CpfValueValidator(cpf);
            }

            #endregion Public Static Methods

            #region Private Static Methods

            private static bool CnpjFormatValidator(string cnpj) {
                return Regex.IsMatch(cnpj, @"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)");
            }

            private static bool CnpjValueValidator(string cnpj) {
                cnpj = cnpj.Trim();
                cnpj = Regex.Replace(cnpj, "[.-\\]", string.Empty);

                if (cnpj.Length != 14) { return false; }

                var temp = cnpj.Substring(0, 12);
                var sum = 0;
                var multiplierA = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                for (var idx = 0; idx < 12; idx++) {
                    sum += int.Parse(temp[idx].ToString()) * multiplierA[idx];
                }

                var rest = (sum % 11);
                rest = (rest < 2 ? 0 : (11 - rest));
                var digit = rest.ToString();
                temp = temp + digit;
                sum = 0;

                var multiplierB = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                for (var idx = 0; idx < 13; idx++) {
                    sum += int.Parse(temp[idx].ToString()) * multiplierB[idx];
                }

                rest = (sum % 11);
                rest = (rest < 2 ? 0 : (11 - rest));
                digit = digit + rest.ToString();

                return cnpj.EndsWith(digit);
            }

            private static bool CpfFormatValidator(string cpf) {
                return Regex.IsMatch(cpf, @"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)");
            }

            private static bool CpfValueValidator(string cpf) {
                cpf = cpf.Trim();
                cpf = Regex.Replace(cpf, "[.-]", string.Empty);

                if (cpf.Length != 11) { return false; }

                var temp = cpf.Substring(0, 9);
                var sum = 0;
                var multiplierA = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                for (var idx = 0; idx < 9; idx++) {
                    sum += int.Parse(temp[idx].ToString()) * multiplierA[idx];
                }

                var rest = sum % 11;
                rest = (rest < 2 ? 0 : (11 - rest));
                var digit = rest.ToString();
                temp = temp + digit;
                sum = 0;

                var multiplierB = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                for (var idx = 0; idx < 10; idx++) {
                    sum += int.Parse(temp[idx].ToString()) * multiplierB[idx];
                }

                rest = sum % 11;
                rest = (rest < 2 ? 0 : (11 - rest));
                digit = digit + rest.ToString();

                return cpf.EndsWith(digit);
            }

            #endregion Private Static Methods
        }

        #endregion Public Inner Classes
    }
}