using System;
using Microsoft.SmallBasic.Library;
using LitDev;

namespace Checkers
{
    public class Game
    {
        public static void Main()
        {
            GraphicsWindow.Show();
            LDGraphicsWindow.State = 2;
            GraphicsWindow.Title = "Checkers";
            UI.DrawBoard(100);
        }
    }

    public class UI
    {
        public static void DrawBoard(int SizeofSquares,int Dimensions = 8)
        {
            int CellCount = 1;
            for (int x = SizeofSquares; x <= SizeofSquares*Dimensions; x += SizeofSquares)
            {
                for (int y = SizeofSquares; y <= SizeofSquares * Dimensions; y += SizeofSquares)
                {
                    CellCount++;
                    GraphicsWindow.DrawRectangle(x, y, SizeofSquares, SizeofSquares);
                    Console.WriteLine("({0},{1}) : {2}", x/SizeofSquares, y/SizeofSquares, CellCount % 2);
                }
            }
        }
    }
}
