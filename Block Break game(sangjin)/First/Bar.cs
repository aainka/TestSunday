using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Block_Break_Game.First
{
    public class Bar : GObject
    {
        public Bar(Ground ground, Point ploc): base(ground)
        {
            loc = ploc;
            Width = 100;
            Height = 15;
            block.Fill = Brushes.Yellow;
        }
    }
}
