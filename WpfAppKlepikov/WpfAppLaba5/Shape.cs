using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

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

        public void Draw(Canvas canvas, Point point)
        {
            Polygon polygon = new Polygon();
            polygon.Points.Add(point);
            polygon.Points.Add(new Point(point.X + Width, point.Y));
            polygon.Points.Add(new Point(point.X, point.Y + Height));
            polygon.Fill = new SolidColorBrush((Color)Background);
            polygon.Stroke = new SolidColorBrush((Color)Foreground);
            polygon.StrokeThickness = Thickness;
            canvas.Children.Add(polygon);
        }

        public void Save()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Файлы (xml)|*.xml|все файлы|*.*";
            if (fileDialog.ShowDialog() == false) { return; }
            XmlSerializer serializer = new XmlSerializer(typeof(Shape));
            using (FileStream file = new FileStream(fileDialog.FileName, FileMode.Create))
            {
                serializer.Serialize(file, this);
            }
        }
        public static Shape Load()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Файлы (xml)|*.xml|все файлы|*.*";
            if (fileDialog.ShowDialog() == false) { return null; }
            XmlSerializer serializer = new XmlSerializer(typeof(Shape));
            Shape shape = null;
            using (FileStream file = new FileStream(fileDialog.FileName, FileMode.Open))
            {
                shape = (Shape)serializer.Deserialize(file);
            }
            return shape;
        }
        public override string? ToString()
        {
            return $"Thickness = {Thickness} Background = {Background} Foreground = {Foreground}\n" +
                $"Width = {Width} Height = {Height}";
        }
    }
}
