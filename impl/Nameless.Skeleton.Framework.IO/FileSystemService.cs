using System.IO;
using System.Text;

namespace Nameless.Skeleton.IO {

    /// <summary>
    /// Default implementation of <see cref="IFileSystemService"/>
    /// </summary>
    public class FileSystemService : IFileSystemService {

        #region Public Static Read-Only Fields

        public static readonly IFileSystemService Instance = new FileSystemService();

        #endregion Public Static Read-Only Fields

        #region Private Constructors

        private FileSystemService() {
        }

        #endregion Private Constructors

        #region IFileSystemService Members

        /// <inheritdoc />
        public Stream GetFileStream(string filePath, FileAccess fileAccess = FileAccess.Read, FileMode fileMode = FileMode.Open) {
            Prevent.ParameterNullOrWhiteSpace(filePath, nameof(filePath));

            if (!FileExists(filePath)) {
                throw new FileNotFoundException(Path.GetFileName(filePath));
            }

            return new FileStream(filePath, fileMode, fileAccess);
        }

        /// <inheritdoc />
        public TextReader GetFileReader(string filePath) {
            return GetFileReader(filePath, Encoding.UTF8);
        }

        /// <inheritdoc />
        public TextReader GetFileReader(string filePath, Encoding encoding) {
            Prevent.ParameterNullOrWhiteSpace(filePath, nameof(filePath));
            Prevent.ParameterNull(encoding, nameof(encoding));

            if (!FileExists(filePath)) {
                throw new FileNotFoundException(Path.GetFileName(filePath));
            }

            return new StreamReader(File.Open(filePath, FileMode.Open), encoding);
        }

        /// <inheritdoc />
        public string ReadAllText(string filePath) {
            return ReadAllText(filePath, Encoding.UTF8);
        }

        /// <inheritdoc />
        public string ReadAllText(string filePath, Encoding encoding) {
            Prevent.ParameterNullOrWhiteSpace(filePath, nameof(filePath));
            Prevent.ParameterNull(encoding, nameof(encoding));

            if (!FileExists(filePath)) {
                throw new FileNotFoundException(Path.GetFileName(filePath));
            }

            return File.ReadAllText(filePath, encoding);
        }

        /// <inheritdoc />
        public bool FileExists(string filePath) {
            return File.Exists(filePath);
        }

        /// <inheritdoc />
        public byte[] ReadAllBytes(string filePath, FileAccess fileAccess = FileAccess.Read, FileMode fileMode = FileMode.Open, int bufferSize = 1024 * 4) {
            Prevent.ParameterNullOrWhiteSpace(filePath, nameof(filePath));

            if (!FileExists(filePath)) {
                throw new FileNotFoundException(Path.GetFileName(filePath));
            }

            using (var memoryStream = new MemoryStream())
            using (var fileStream = new FileStream(filePath, fileMode, fileAccess)) {
                var buffer = new byte[bufferSize];
                var count = 0;

                while ((count = fileStream.Read(buffer, 0, bufferSize)) > 0) {
                    memoryStream.Write(buffer, 0, count);
                }

                return memoryStream.ToArray();
            }
        }

        /// <inheritdoc />
        public void WriteAllText(string filePath, string content) {
            WriteAllText(filePath, content, Encoding.UTF8);
        }

        /// <inheritdoc />
        public void WriteAllText(string filePath, string content, Encoding encoding) {
            Prevent.ParameterNullOrWhiteSpace(filePath, nameof(filePath));

            File.WriteAllText(filePath, content, encoding);
        }

        #endregion IFileSystemService Members
    }
}