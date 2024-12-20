using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfAppLaba5
{
    public class Shape
    {
        public Shape()
        {
        }

        public Shape(int width, int height, int thickness, Color? background, Color? foreground)
        {
            Width = width;
            Height = height;
            Thickness = thickness;
            Background = background;
            Foreground = foreground;
        }

        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public int Thickness { get; set; } = 0;
        public Color? Background { get; set; }
        public Color? Foreground { get; set; }

        public override string? ToString()
        {
            return $"Thickness = {Thickness} Background = {Background} Foreground = {Foreground}\n" +
                $"Width = {Width} Height = {Height}";
        }
    }
}
