namespace Nameless.Framework.Drawing {

    public enum RotateFlipType {

        /// <summary>
        /// Specifies a 180-degree clockwise rotation without flipping.
        /// </summary>
        Rotate180FlipNone,

        /// <summary>
        ///Specifies a 180-degree clockwise rotation followed by a horizontal flip.
        /// </summary>
        Rotate180FlipX,

        /// <summary>
        ///Specifies a 180-degree clockwise rotation followed by a horizontal and vertical flip.
        /// </summary>
        Rotate180FlipXY,

        /// <summary>
        ///Specifies a 180-degree clockwise rotation followed by a vertical flip.
        /// </summary>
        Rotate180FlipY,

        /// <summary>
        /// Specifies a 270-degree clockwise rotation without flipping.
        /// </summary>
        Rotate270FlipNone,

        /// <summary>
        /// Specifies a 270-degree clockwise rotation followed by a horizontal flip.
        /// </summary>
        Rotate270FlipX,

        /// <summary>
        /// Specifies a 270-degree clockwise rotation followed by a horizontal and vertical flip.
        /// </summary>
        Rotate270FlipXY,

        /// <summary>
        /// Specifies a 270-degree clockwise rotation followed by a vertical flip.
        /// </summary>
        Rotate270FlipY,

        /// <summary>
        /// Specifies a 90-degree clockwise rotation without flipping.
        /// </summary>
        Rotate90FlipNone,

        /// <summary>
        /// Specifies a 90-degree clockwise rotation followed by a horizontal flip.
        /// </summary>
        Rotate90FlipX,

        /// <summary>
        /// Specifies a 90-degree clockwise rotation followed by a horizontal and vertical flip.
        /// </summary>
        Rotate90FlipXY,

        /// <summary>
        /// Specifies a 90-degree clockwise rotation followed by a vertical flip.
        /// </summary>
        Rotate90FlipY,

        /// <summary>
        /// Specifies no clockwise rotation and no flipping.
        /// </summary>
        RotateNoneFlipNone,

        /// <summary>
        /// Specifies no clockwise rotation followed by a horizontal flip.
        /// </summary>
        RotateNoneFlipX,

        /// <summary>
        /// Specifies no clockwise rotation followed by a horizontal and vertical flip.
        /// </summary>
        RotateNoneFlipXY,

        /// <summary>
        /// Specifies no clockwise rotation followed by a vertical flip.
        /// </summary>
        RotateNoneFlipY
    }
}