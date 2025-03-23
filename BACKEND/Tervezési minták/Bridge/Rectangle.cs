using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Bridge
{
    public class Rectangle : Shape
    {
        int x, y, height, width;
        IShapeColor color;
        public Rectangle(int x, int y, int height, int width, IShapeColor color)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
            this.color = color;
        }

        public override ConsoleColor Color
        {
            get
            {
                return color.Color;
            }
        }

        public override void Draw()
        {
            Console.ForegroundColor = Color;
            for (int i = 0; i < x + width; i++)
            {
                for (int j = 0; j < y + height; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write('█');
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
