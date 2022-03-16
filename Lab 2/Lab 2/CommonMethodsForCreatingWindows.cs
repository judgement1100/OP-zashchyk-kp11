using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab2
{
    class CommonMethodsForCreatingWindows
    {
        public void ExitButtnon_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        public void SetColor(Control o, string color)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom(color);
            o.Background = brush;
        }
        public void SetColor(Grid o, string color)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom(color);
            o.Background = brush;
        }
        public void SetMargin(Control o, double left = 0, double right = 0, double top = 0, double bottom = 0)
        {
            Thickness margin = o.Margin;
            margin.Left = left;
            margin.Right = right;
            margin.Top = top;
            margin.Bottom = bottom;
            o.Margin = margin;
        }
    }
}
