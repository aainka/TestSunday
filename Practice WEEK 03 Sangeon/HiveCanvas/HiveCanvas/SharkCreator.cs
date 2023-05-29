using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HiveCanvas
{
    public class SharkCreator
    {
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        Canvas _canvas = null;
        public SharkCreator(Canvas canvas)
        {
            timer.Tick += SharkCreate;
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Start();
            _canvas = canvas;
        }

        public void SharkCreate(object sender, EventArgs e)
        {
            new Shark("shark.png", _canvas);
        }
    }
}
