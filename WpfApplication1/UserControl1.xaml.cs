using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private System.Drawing.Bitmap image;
        private System.Drawing.Graphics graphics;

        public System.Drawing.Graphics CreateGraphics()
        {
            return graphics;
        }

        public int getWidth()
        {
            return image.Width;
        }

        public int getHeight()
        {
            return image.Height;
        }

        public System.Drawing.Color BackColor 
        {
            get { return System.Drawing.Color.White; }
        }

        public UserControl1()
        {
            InitializeComponent();
            //Brush = Brushes.Red;
            image = new System.Drawing.Bitmap(1024, 768, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            graphics = System.Drawing.Graphics.FromImage(image);

            //test
            //using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image))
            //{
            //    System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black, 3);
            //    System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            //    g.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, 100, 100));
            //    g.DrawRectangle(pen, new System.Drawing.Rectangle(0, 0, 100, 100));
            //}
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (image != null)
            {
                BitmapSource bitmapSource = BitmapHelper.ToBitmapSource(image);
                drawingContext.DrawImage(bitmapSource, new Rect(0, 0, image.Width, image.Height));
            }
        }  
    }
}
