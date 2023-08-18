using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace gameproject.code
{
    internal class Bar : GObject
    {
        public Bar(Ground ground, Point ploc) : base(ground)
        {
            loc = ploc;
            Width = 30;
            Height = 15;
            block.Fill = Brushes.Yellow;
        }
    }
}