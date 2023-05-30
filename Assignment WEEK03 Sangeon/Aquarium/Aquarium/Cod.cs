using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Aquarium
{

    class Cod : Fish
    {
        public static FishSchool CodSchool = new FishSchool();
        public Cod(string imgName, Canvas canvas) : base(imgName, canvas)
        {
            CodSchool.Add(this);
            //fishkind = FishKind.Cod;
        }



        public override void NormalMove()
        {
            switch (dice.Next(4))
            {
                case 0:
                    if (position.X + speed <= _canvas.ActualWidth - width) { position = new point(position.x + speed, position.y); }
                    else { NormalMove(); }
                    break;
                case 1:
                    if (position.X - speed >= 0) { position = new point(position.x - speed, position.y); }
                    else { NormalMove(); }
                    break;
                case 2:
                    if (position.Y + speed <= _canvas.ActualHeight - height) { position = new point(position.x, position.y + speed); }
                    else { NormalMove(); }
                    break;
                case 3:
                    if (position.Y - speed >= 0) { position = new point(position.x, position.y - speed); }
                    else { NormalMove(); }
                    break;
            }
        }

        public override void TimerMove(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
