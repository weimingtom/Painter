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

//see https://github.com/KaitoHH/Painter
namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Paint pic;
        private UserControl1 pictureBox1;


        public MainWindow()
        {
            InitializeComponent();
        }

        //画图事件处理
        //***********************************************************************
        private void pictureBox1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int x = Convert.ToInt32(e.GetPosition((IInputElement)sender).X);
            int y = Convert.ToInt32(e.GetPosition((IInputElement)sender).Y);

            pic.beginDraw(new System.Drawing.Point(x, y));
            
            pictureBox1.InvalidateVisual();
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = Convert.ToInt32(e.GetPosition((IInputElement)sender).X);
            int y = Convert.ToInt32(e.GetPosition((IInputElement)sender).Y);

            updateInfo(x, y);
            updateState();
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                pic.drawingProcess(new System.Drawing.Point(x, y));
            }
            else if (pic.mode == Paint.MODE.ERASER || pic.mode == Paint.MODE.DOTS)
            {
                pic.DotsMove(new System.Drawing.Point(x, y));
            }

            pictureBox1.InvalidateVisual();
        }
        private void pictureBox1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            pic.finshDraw();
            updateState();

            pictureBox1.InvalidateVisual();
        }
        //***********************************************************************


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pictureBox1 = new UserControl1();
            this.grid1.Children.Add(pictureBox1);
            pic = new Paint(pictureBox1.CreateGraphics(), pictureBox1.getWidth(), pictureBox1.getHeight());
            pic.backColor = pictureBox1.BackColor;
            updateState();

            pic.penWidth = (float)trackBar1.Value;

            //add
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;

        }


        private void updateState()
        {
            button10.IsEnabled = pic.canUndo();
            button11.IsEnabled = pic.canRedo();

            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(pic.penColor);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(100, 100, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image))
            {
                g.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, image.Width, image.Height));
            }
            pictureBox2.Source = BitmapHelper.ToBitmapSource(image);
        }


        private void updateInfo(int x = 0, int y = 0)
        {
            label2.Content = string.Format("当前坐标：({0,4},{1,4}) | 画布大小:({2,4},{3,4}) | 当前工具：{4}"
                                        , x, y, pictureBox1.getWidth(),
                                        pictureBox1.getHeight(), pic.mode);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult ret = MessageBox.Show("保存当前画板？", "保存", MessageBoxButton.YesNoCancel);
            if (ret == MessageBoxResult.Yes)
            {
                string dir = pic.save();
                MessageBox.Show("bmp保存已至" + dir);
            }
            else if (ret == MessageBoxResult.Cancel)
            {
                return;
            }
            pic.clearPaint();
            updateState();

            pictureBox1.InvalidateVisual();
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            pic.op_undo();
            updateState();

            pictureBox1.InvalidateVisual();
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            pic.op_redo();
            updateState();

            pictureBox1.InvalidateVisual();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            pic.mode = Paint.MODE.LINES;
            updateInfo();

            pictureBox1.InvalidateVisual();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            pic.mode = Paint.MODE.DOTS;
            updateInfo();

            pictureBox1.InvalidateVisual();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            pic.mode = Paint.MODE.CIRCLES;
            updateInfo();

            pictureBox1.InvalidateVisual();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            pic.mode = Paint.MODE.RECTANGLE;
            updateInfo();

            pictureBox1.InvalidateVisual();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            pic.mode = Paint.MODE.FILL_CIRCLE;
            updateInfo();

            pictureBox1.InvalidateVisual();
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            pic.mode = Paint.MODE.FILL_RECTANGLE;
            updateInfo();

            pictureBox1.InvalidateVisual();
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            pic.mode = Paint.MODE.ERASER;
            updateInfo();

            pictureBox1.InvalidateVisual();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            MyColorDialog colorDialog1 = new MyColorDialog();
            colorDialog1.Color = pic.penColor;
            colorDialog1.ShowDialog();
            pic.penColor = colorDialog1.Color;
            
            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(pic.penColor);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(100, 100, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image))
            {
                g.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, image.Width, image.Height));
            }
            pictureBox2.Source = BitmapHelper.ToBitmapSource(image);
        }

        private void trackBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (pic != null) 
            {
                pic.penWidth = (float)trackBar1.Value;
            }
            if (label1 != null)
            {
                label1.Content = string.Format("线宽:{0,2}", (float)trackBar1.Value);
            }
        }

        private void trackBar2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (pic != null)
            {
                pic.magicX = (int)trackBar2.Value;
            }
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            pic.magiColor = checkBox1.IsChecked.Value;
            trackBar2.IsEnabled = pic.magiColor;
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            string dir = pic.save();
            MessageBox.Show("bmp保存已至" + dir);
        }
    }
}
