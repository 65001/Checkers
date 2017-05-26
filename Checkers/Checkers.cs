using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.SmallBasic.Library;
using LitDev;

namespace Checkers
{
    public class Game
    {
        
        public readonly static int SizeofSquares = 100;
        public readonly static int Dimensions = 8;
        public static int[,] ColorBoard = new int[Dimensions,Dimensions];
        public static string[,] ShapeBoard = new string[Dimensions,Dimensions];

        public enum Color { Brown, Red, Empty };

        public static void Main()
        {
            GraphicsWindow.Show();
            LDGraphicsWindow.State = 2;
            GraphicsWindow.Title = "Checkers";
            UI.DrawBoard(SizeofSquares,Dimensions);

            GraphicsWindow.MouseDown += Events.MD;
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
                    int xCord = x / SizeofSquares;
                    int yCord = y / SizeofSquares;
                    int Modulo =(int) (xCord + yCord) % 2;
                    GraphicsWindow.DrawRectangle(x, y, SizeofSquares, SizeofSquares);

                    if (Modulo == 1)
                    {
                        GraphicsWindow.BrushColor = LDColours.Black;
                        GraphicsWindow.FillRectangle(x, y, SizeofSquares, SizeofSquares);
                        GraphicsWindow.DrawText(x + .25 * SizeofSquares, y + .5 * SizeofSquares, "(" + xCord+ "," + yCord+ ") : " + Modulo);
                        GraphicsWindow.BrushColor = "White";
                        if (y/SizeofSquares <= 3)
                        {
                            Game.ColorBoard[xCord - 1, yCord - 1] = (int) Game.Color.Red;

                            Game.ShapeBoard[xCord -1,yCord -1] =
                            GenerateCircle(x+SizeofSquares/6, y+SizeofSquares/6,(int)((decimal).65* SizeofSquares), "Red");
                        }
                        else if (y/SizeofSquares >= (Dimensions-2) )
                        {
                            Game.ColorBoard[xCord - 1, yCord -1] = (int)Game.Color.Brown;

                            Game.ShapeBoard[xCord - 1, yCord - 1] =
                            GenerateCircle(x + SizeofSquares / 6, y + SizeofSquares / 6, (int)((decimal).65 * SizeofSquares), "Brown");
                        }
                    }
                }
            }
            GraphicsWindow.DrawRectangle(SizeofSquares, SizeofSquares, SizeofSquares * Dimensions, SizeofSquares * Dimensions);
        }

        public static string GenerateCircle(int x,int y,int radius,string Color)
        {
            GraphicsWindow.BrushColor = Color;
            string _return = Shapes.AddEllipse(radius, radius);
            Shapes.Move(_return, x, y);
            return _return;
        }

    }

    public class Events
    {
        public async static void MD()
        {
            Hanlders.Selected(GraphicsWindow.MouseX / Game.SizeofSquares, GraphicsWindow.MouseY / Game.SizeofSquares);
            
        }
    }

    public class Hanlders
    {
        public static void Selected(int xCord,int yCord)
        {
            if (xCord <= Game.Dimensions && yCord <= Game.Dimensions)
            {
                GraphicsWindow.ShowMessage("(" + xCord + "," + yCord + ") Control :" + Game.ShapeBoard[xCord - 1, yCord - 1] +" Color : " + Game.ColorBoard[xCord - 1, yCord - 1], null);
            }
        }
    }
}
