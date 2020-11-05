using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageResizer.Helpers
{
    /// <summary>
    /// Image helper
    /// </summary>
    public static class ImageHelper
    {

        /// <summary>
        /// Resize image to a given ration
        /// </summary>
        /// <param name="base64Image"></param>
        /// <param name="ratio">ratio to resize image  </param>
        /// <returns>resised base64 image</returns>
        /// <example>
        /// This sample shows how to call the <see cref="ResizeImage"/> method.
        /// <code>
        /// class TestClass
        /// {
        ///     static int Main()
        ///     {
        ///         return ResizeImage("base 64 content",0.5);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static string ResizeImageByRatio(string base64Image, double ratio)
        {
            string retImage = "";
            string base64 = base64Image.Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64);

            using (var ms = new MemoryStream(bytes))
            {
                var image = Image.FromStream(ms);
                var width = (int)(image.Width * ratio);
                var height = (int)(image.Height * ratio);
                
                var newImage = new Bitmap(width, height);
                Graphics.FromImage(newImage).DrawImage(image, 0, 0, width, height);
                Bitmap bmp = new Bitmap(newImage);

                var data = ImageToByte2(bmp);
                retImage = "data:image/png;base64," + Convert.ToBase64String(data);
            }
            return retImage;
        }


        /// <summary>
        /// Resize image to a fixed pixel
        /// </summary>
        /// <param name="base64Image"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        public static string ResizeImageByPixel(string base64Image, int pixel) // this can be extended to have separate size for width and height
        {
            string base64 = base64Image.Split(',')[1];
            string retImage = base64Image;
            byte[] bytes = Convert.FromBase64String(base64);
       
            using (var ms = new MemoryStream(bytes))
            {
                var image = Image.FromStream(ms);

                if (image.Width > pixel)
                {
                    var width = pixel;
                    var height = pixel;

                    var thumbnailBitmap = new Bitmap(width, height);

                    var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
                    thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    var imageRectangle = new Rectangle(0, 0, width, height);
                    thumbnailGraph.DrawImage(image, imageRectangle);
                    var data = ImageToByte2(thumbnailBitmap);
                    retImage = "data:image/png;base64," + Convert.ToBase64String(data);
                }


            }
            return retImage;
        }

        public static byte[] ImageToByte2(Image img)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png); // you can change also have different formats
                stream.Close();

                byteArray = stream.ToArray();
            }
            return byteArray;
        }
    }
}