using System.IO;
using System.Text;

namespace Nameless.Skeleton.IO {

    /// <summary>
    /// Defines methods for work with file system.
    /// </summary>
    public interface IFileSystemService {

        #region Methods

        /// <summary>
        /// Retrieves a <see cref="Stream"/> for the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileAccess">The file access type. Default is <see cref="FileAccess.Read"/>.</param>
        /// <param name="fileMode">The file mode type. Default is <see cref="FileMode.Open"/>.</param>
        /// <returns>An instance of <see cref="Stream"/>.</returns>
        Stream GetFileStream(string filePath, FileAccess fileAccess = FileAccess.Read, FileMode fileMode = FileMode.Open);

        /// <summary>
        /// Retrieves a <see cref="TextReader"/> for the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>An instance of <see cref="TextReader"/>.</returns>
        TextReader GetFileReader(string filePath);

        /// <summary>
        /// Retrieves a <see cref="TextReader"/> for the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="encoding">The file encoding.</param>
        /// <returns>An instance of <see cref="TextReader"/>.</returns>
        TextReader GetFileReader(string filePath, Encoding encoding);

        /// <summary>
        /// Reads all text from a text file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>An instance of <see cref="string"/> representing the file content.</returns>
        string ReadAllText(string filePath);

        /// <summary>
        /// Reads all text from a text file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="encoding">The file encoding.</param>
        /// <returns>An instance of <see cref="string"/> representing the file content.</returns>
        string ReadAllText(string filePath, Encoding encoding);

        /// <summary>
        /// Reads a file using a buffer.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileAccess">The file access type. Default is <see cref="FileAccess.Read"/>.</param>
        /// <param name="fileMode">The file mode type. Default is <see cref="FileMode.Open"/>.</param>
        /// <param name="bufferSize">Size of the buffer. Default is 4Kb.</param>
        /// <returns>An array of <see cref="byte"/> representing the file.</returns>
        byte[] ReadAllBytes(string filePath, FileAccess fileAccess = FileAccess.Read, FileMode fileMode = FileMode.Open, int bufferSize = 1024 * 4);

        /// <summary>
        /// Writes all content to the specified file. If the file not exists, creates it. If exists, overrides.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="content">The file content.</param>
        void WriteAllText(string filePath, string content);

        /// <summary>
        /// Writes all content to the specified file. If the file not exists, creates it. If exists, overrides.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="content">The file content.</param>
        /// <param name="encoding">The file encoding.</param>
        void WriteAllText(string filePath, string content, Encoding encoding);

        /// <summary>
        /// Checks if a specified file exists.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns><c>true</c> if file exists; otherwise, <c>false</c>.</returns>
        bool FileExists(string filePath);

        #endregion Methods
    }
}