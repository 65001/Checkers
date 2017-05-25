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
            for (int x = SizeofSquares; x <= SizeofSquares*Dimensions; x += SizeofSquares)
            {
                for (int y = SizeofSquares; y <= SizeofSquares * Dimensions; y += SizeofSquares)
                {
                    int Modulo = (x/SizeofSquares+y/SizeofSquares) % 2;
                    GraphicsWindow.DrawRectangle(x, y, SizeofSquares, SizeofSquares);

                    if (Modulo == 1)
                    {
                        GraphicsWindow.BrushColor = LDColours.Black;
                        GraphicsWindow.FillRectangle(x, y, SizeofSquares, SizeofSquares);
                        GraphicsWindow.DrawText(x + .25 * SizeofSquares, y + .5 * SizeofSquares, "(" + x / SizeofSquares + "," + y / SizeofSquares + ") : " + Modulo);
                        GraphicsWindow.BrushColor = "White";
                    }
                    else
                    {
                        GraphicsWindow.BrushColor = "Black";
                    }
                    GraphicsWindow.DrawText(x + .25 * SizeofSquares, y + .5 * SizeofSquares, "(" + x / SizeofSquares + "," + y / SizeofSquares + ")");
                }
            }
            GraphicsWindow.DrawRectangle(SizeofSquares, SizeofSquares, SizeofSquares * Dimensions, SizeofSquares * Dimensions);
        }
    }
}
