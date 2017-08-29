using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace WpfApplication1
{
    class BitmapHelper
    {
        public static BitmapSource ToBitmapSource(System.Drawing.Image image)
        {
            return ToBitmapSource(image as System.Drawing.Bitmap);
        }

        /// <summary>
        /// Convert an IImage to a WPF BitmapSource. The result can be used in the Set Property of Image.Source
        /// </summary>
        /// <param name="bitmap">The Source Bitmap</param>
        /// <returns>The equivalent BitmapSource</returns>
        private static BitmapSource ToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            if (bitmap == null) return null;

            using (System.Drawing.Bitmap source = (System.Drawing.Bitmap)bitmap.Clone())
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                NativeMethods.DeleteObject(ptr); //release the HBitmap
                bs.Freeze();
                return bs;
            }
        }

        static int kkk = 0;

        //see https://stackoverflow.com/questions/28411460/bitmap-graphics-vs-winform-control-graphics
        public static BitmapSource ToBitmapSource2(System.Drawing.Image image)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                image.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                //image.Save("kkk-" + (kkk) + ".png");
                memory.Position = 0;
                var source = new BitmapImage();
                source.BeginInit();
                source.StreamSource = memory;
                source.CacheOption = BitmapCacheOption.OnLoad;
                source.EndInit();

                return source;
            }
        }

        private static BitmapSource ToBitmapSource(byte[] bytes, int width, int height, int dpiX, int dpiY)
        {
            var result = BitmapSource.Create(
                            width,
                            height,
                            dpiX,
                            dpiY,
                            PixelFormats.Bgra32,
                            null /* palette */,
                            bytes,
                            width * 4 /* stride */);
            result.Freeze();

            return result;
        }
    }
}
