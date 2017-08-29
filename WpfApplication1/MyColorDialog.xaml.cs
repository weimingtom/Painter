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
using System.Windows.Shapes;

//see https://github.com/sskodje/WpfColorFont
namespace WpfApplication1
{
    /// <summary>
    /// MyColorDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MyColorDialog : Window
    {
        public System.Drawing.Color Color;

        public MyColorDialog()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Color c = colorPicker.SelectedColor.Brush.Color;
            this.Color = System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
