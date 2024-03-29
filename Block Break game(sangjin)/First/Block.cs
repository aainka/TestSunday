﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Block_Break_Game.First
{
    public class Block : GObject
    {
        static public List<Block> BlockList = new List<Block>();
        public bool[] sight = new bool[1]; // 죽은 block과 죽지 않은 block을 구분
        int live = 0;
      

        public Block(Ground ground, Point ploc, int index) : base(ground){ 
            loc = ploc;
            live = index;
            Width = 30;
            Height = 15;
            block.Fill = Brushes.Red;
        }

        static public void InitAll(Ground ground) 
        {
            for (int locx = 0; locx <= 11; locx++)
            {
                for (int locy = 0; locy <= 5; locy++)
                {
                    BlockList.Add(
                        new Block(ground,
                            new Point(locx * 35, locy * 20),
                            20)
                        );
                }
            }
        }
        public void BlockMove()
        {
            loc = new Point(loc.Y , loc.X + 20);
        }
 
        public bool contains(Ball k)
        {
            if (loc.X < k.loc.X && loc.X + Width > k.loc.X)
            {
                if (loc.Y < k.loc.Y && loc.Y + Height > k.loc.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
