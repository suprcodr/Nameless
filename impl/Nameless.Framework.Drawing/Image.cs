using System;
using System.IO;
using ImageSharp;
using ImageSharp.Formats;
using ImageSharp.Processing;
using SixLabors.Primitives;

namespace Nameless.Framework.Drawing {

    public sealed class Image : IImage, IDisposable {

        #region Private Fields

        private Image<Rgba32> _image;
        private bool _disposed;

        #endregion Private Fields

        #region Public Constructors

        public Image(string filePath) {
            Prevent.ParameterNullOrWhiteSpace(filePath, nameof(filePath));

            _image = ImageSharp.Image.Load(filePath);
        }

        public Image(Stream stream) {
            Prevent.ParameterNull(stream, nameof(stream));

            _image = ImageSharp.Image.Load(stream);
        }

        #endregion Public Constructors

        #region Private Constructors

        private Image(Image<Rgba32> image) {
            _image = image;
        }

        #endregion Private Constructors

        #region Destructor

        ~Image() {
            Dispose(disposing: false);
        }

        #endregion Destructor

        #region Private Static Methods

        private static void ParseRotateFlip(RotateFlipType type, out RotateType rotate, out FlipType flip) {
            rotate = RotateType.None;
            flip = FlipType.None;
            switch (type) {
                case RotateFlipType.Rotate180FlipNone:
                    rotate = RotateType.Rotate180;
                    break;

                case RotateFlipType.Rotate180FlipX:
                    rotate = RotateType.Rotate180;
                    flip = FlipType.Horizontal;
                    break;

                case RotateFlipType.Rotate180FlipXY:
                    rotate = RotateType.Rotate180;
                    flip = FlipType.Horizontal | FlipType.Vertical;
                    break;

                case RotateFlipType.Rotate180FlipY:
                    rotate = RotateType.Rotate180;
                    flip = FlipType.Vertical;
                    break;

                case RotateFlipType.Rotate270FlipNone:
                    rotate = RotateType.Rotate270;
                    break;

                case RotateFlipType.Rotate270FlipX:
                    rotate = RotateType.Rotate270;
                    flip = FlipType.Horizontal;
                    break;

                case RotateFlipType.Rotate270FlipXY:
                    rotate = RotateType.Rotate270;
                    flip = FlipType.Horizontal | FlipType.Vertical;
                    break;

                case RotateFlipType.Rotate270FlipY:
                    rotate = RotateType.Rotate270;
                    flip = FlipType.Vertical;
                    break;

                case RotateFlipType.Rotate90FlipNone:
                    rotate = RotateType.Rotate270;
                    break;

                case RotateFlipType.Rotate90FlipX:
                    rotate = RotateType.Rotate90;
                    flip = FlipType.Horizontal;
                    break;

                case RotateFlipType.Rotate90FlipXY:
                    rotate = RotateType.Rotate90;
                    flip = FlipType.Horizontal | FlipType.Vertical;
                    break;

                case RotateFlipType.Rotate90FlipY:
                    rotate = RotateType.Rotate90;
                    flip = FlipType.Vertical;
                    break;

                case RotateFlipType.RotateNoneFlipX:
                    flip = FlipType.Horizontal;
                    break;

                case RotateFlipType.RotateNoneFlipXY:
                    flip = FlipType.Horizontal | FlipType.Vertical;
                    break;

                case RotateFlipType.RotateNoneFlipY:
                    flip = FlipType.Vertical;
                    break;
            }
        }

        private static IImageFormat ParseFormat(ImageFormat format) {
            IImageFormat innerFormat;
            switch (format) {
                case ImageFormat.Gif:
                    innerFormat = new GifFormat();
                    break;

                case ImageFormat.Jpeg:
                    innerFormat = new JpegFormat();
                    break;

                case ImageFormat.Png:
                    innerFormat = new PngFormat();
                    break;

                default:
                    innerFormat = new BmpFormat();
                    break;
            }
            return innerFormat;
        }

        #endregion Private Static Methods

        #region Private Methods

        private void Dispose(bool disposing) {
            if (_disposed) { return; }
            if (disposing) {
                if (_image != null) {
                    _image.Dispose();
                }
            }

            _image = null;
            _disposed = true;
        }

        #endregion Private Methods

        #region IImage Members

        public int Height => _image.Height;

        public int Width => _image.Width;

        public IImage Crop(int x, int y, int width, int height) {
            var image = _image.Crop(new Rectangle(x, y, width, height));

            return new Image(image);
        }

        public IImage Resize(int width, int height) {
            var image = _image.Resize(width, height);

            return new Image(image);
        }

        public IImage RotateFlip(RotateFlipType type) {
            ParseRotateFlip(type, out RotateType rotate, out FlipType flip);

            var image = _image.RotateFlip(rotate, flip);

            return new Image(image);
        }

        public void Save(Stream outputStream, ImageFormat format = ImageFormat.Bitmap) {
            _image.Save(outputStream, ParseFormat(format));
        }

        public void Save(string outputPath, ImageFormat format = ImageFormat.Bitmap) {
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None)) {
                Save(fileStream, format);
            }
        }

        #endregion IImage Members

        #region IDisposable Members

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    } 
}