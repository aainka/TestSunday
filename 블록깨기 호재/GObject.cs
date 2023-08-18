using gameproject.code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;

namespace gameproject
{
    public class GObject
    {
        public Rectangle block = new Rectangle();
        public Ground _ground;

        public GObject(Ground ground)
        {
            _ground = ground;
            ground._canvas.Children.Add(block);
        }

        public Point loc
        {
            get
            {
                return new Point(
                    Canvas.GetLeft(block), Canvas.GetTop(block));

            }
            set
            {
                Canvas.SetLeft(block, value.X);
                Canvas.SetTop(block, value.Y);
            }
        }
        public Double Width
        {
            get
            {
                return block.Width;
            }
            set
            {
                block.Width = value;
            }
        }
        public Double Height
        {
            get
            {
                return block.Height;
            }
            set
            {
                block.Height = value;
            }
        }
        public void Dispose()
        {
            _ground._canvas.Children.Remove(block);
        }
    }
}