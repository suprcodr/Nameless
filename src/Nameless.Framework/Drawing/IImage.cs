using System.IO;

namespace Nameless.Framework.Drawing {

    public interface IImage {

        #region Properties
        
        int Height { get; }
        int Width { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        IImage Crop(int x, int y, int width, int height);
        IImage Resize(int width, int height);
        IImage RotateFlip(RotateFlipType type);
        void Save(Stream outputStream, ImageFormat format = ImageFormat.Bitmap);
        void Save(string outputPath, ImageFormat format = ImageFormat.Bitmap);

        #endregion Methods
    }
}