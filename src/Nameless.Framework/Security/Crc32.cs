using System;
using System.Security.Cryptography;

namespace Nameless.Framework.Security {

    /// <summary>
    /// Implements a 32-bit CRC hash algorithm.
    /// See: http://github.com/damieng/DamienGKit/blob/master/CSharp/DamienG.Library/Security/Cryptography/Crc32.cs
    /// </summary>
    /// <remarks>
    /// <see cref="Crc32"/> should only be used for backward compatibility with older
    /// file formats and algorithms. It is not secure enough for new applications. If
    /// you need to call multiple times for the same data either use the
    /// HashAlgorithm interface or remember that the result of one Compute call needs
    /// to be ~ (XOR) before being passed in as the seed for the next Compute call.
    /// </remarks>
    public sealed class Crc32 : HashAlgorithm {

        #region Private Constants

        private const int TableSize = 256;
        private const int ByteSize = 8;

        #endregion Private Constants

        #region Public Constants

        /// <summary>
        /// Gets the default polynomial.
        /// </summary>
        public const uint DefaultPolynomial = 0xEDB88320U;

        /// <summary>
        /// Gets the default seed.
        /// </summary>
		public const uint DefaultSeed = 0xFFFFFFFFU;

        #endregion Public Constants

        #region Private Static Fields

        private static uint[] _defaultTable;

        #endregion Private Static Fields

        #region Private Read-Only Fields

        private readonly uint _seed;
        private readonly uint[] _table;

        #endregion Private Read-Only Fields

        #region Private Fields

        private uint _hash;

        #endregion Private Fields

        #region Public Override Properties

        /// <inheritdoc />
        public override int HashSize {
            get { return 32; }
        }

        #endregion Public Override Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Crc32"/>.
        /// </summary>
        /// <param name="polynomial">The polynomial value.</param>
        /// <param name="seed">The seed value.</param>
        public Crc32(uint polynomial = DefaultPolynomial, uint seed = DefaultSeed) {
            _table = InitializeTable(polynomial);
            _seed = seed;
        }

        #endregion Public Constructors

        #region Public Static Methods

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="buffer">The buffer to compute.</param>
        /// <param name="polynomial">The polynomial value.</param>
        /// <param name="seed">The seed value.</param>
        /// <returns>The hash of the computation.</returns>
        public static uint Compute(byte[] buffer, uint polynomial = DefaultPolynomial, uint seed = DefaultSeed) {
            var table = InitializeTable(polynomial);

            return ~CalculateHash(table, seed, buffer, 0, buffer.Length);
        }

        #endregion Public Static Methods

        #region Public Override Methods

        /// <inheritdoc />
        public override void Initialize() {
            _hash = _seed;
        }

        #endregion Public Override Methods

        #region Protected Override Methods

        /// <inheritdoc />
        protected override void HashCore(byte[] buffer, int start, int length) {
            _hash = CalculateHash(_table, _hash, buffer, start, length);
        }

        /// <inheritdoc />
		protected override byte[] HashFinal() {
            return UintToBigEndianBytes(~_hash);
        }

        #endregion Protected Override Methods

        #region Private Static Methods

        private static uint[] InitializeTable(uint polynomial) {
            if (polynomial == DefaultPolynomial && _defaultTable != null) {
                return _defaultTable;
            }

            var table = new uint[TableSize];

            for (var idx = 0; idx < TableSize; idx++) {
                var entry = (uint)idx;

                for (var bit = 0; bit < ByteSize; bit++) {
                    entry = ((entry & 1) == 1)
                        ? ((entry >> 1) ^ polynomial)
                        : (entry >> 1);
                }

                table[idx] = entry;
            }

            if (polynomial == DefaultPolynomial) {
                _defaultTable = table;
            }

            return table;
        }

        private static byte[] UintToBigEndianBytes(uint value) {
            var result = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian) {
                Array.Reverse(result);
            }

            return result;
        }

        private static uint CalculateHash(uint[] table, uint seed, byte[] buffer, int start, int size) {
            var crc = seed;

            for (var idx = start; idx < size - start; idx++) {
                crc = (crc >> 8) ^ table[buffer[idx] ^ crc & 0xff];
            }

            return crc;
        }

        #endregion Private Static Methods
    }
}