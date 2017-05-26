using System;
using System.Threading.Tasks;
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

        static int[,] _ColorBoard = new int[Dimensions,Dimensions];
        static string[,] _ShapeBoard = new string[Dimensions,Dimensions];

        public enum Color { Brown, Red, Empty };

        public static int[,] ColorBoard
        {
            get { return _ColorBoard; }
        }

        public static string[,] ShapeBoard
        {
            get { return ShapeBoard; }
        }

        public static void Main()
        {
            GraphicsWindow.Show();
            LDGraphicsWindow.State = 2;
            GraphicsWindow.Title = "Checkers";
            DrawBoard(SizeofSquares,Dimensions);

            GraphicsWindow.MouseDown += Events.MD;
        }

        public static string GetSelected(int xCord, int yCord)
        {
            if (xCord > Dimensions || yCord > Dimensions) throw new ArgumentOutOfRangeException();
            return _ShapeBoard[xCord, yCord];
        }

        public static string GetColor(int xCord, int yCord)
        {
            if (xCord > Dimensions || yCord > Dimensions) throw new ArgumentOutOfRangeException();
            switch ((Color)_ColorBoard[xCord, yCord])
            {
                case Color.Brown:
                    return "Brown";
                case Color.Empty:
                    return "Empty";
                case Color.Red:
                    return "Red";
            }
            throw new ArgumentOutOfRangeException();
        }

        public static void DrawBoard(int SizeofSquares, int Dimensions = 8)
        {
            for (int x = SizeofSquares; x <= SizeofSquares * Dimensions; x += SizeofSquares)
            {
                for (int y = SizeofSquares; y <= SizeofSquares * Dimensions; y += SizeofSquares)
                {
                    int xCord = x / SizeofSquares;
                    int yCord = y / SizeofSquares;
                    int Modulo = (int)(xCord + yCord) % 2;
                    GraphicsWindow.DrawRectangle(x, y, SizeofSquares, SizeofSquares);

                    if (Modulo == 1)
                    {
                        GraphicsWindow.BrushColor = LDColours.Black;
                        GraphicsWindow.FillRectangle(x, y, SizeofSquares, SizeofSquares);
                        GraphicsWindow.DrawText(x + .25 * SizeofSquares, y + .5 * SizeofSquares, "(" + xCord + "," + yCord + ") : " + Modulo);
                        GraphicsWindow.BrushColor = "White";
                        if (y / SizeofSquares <= 3)
                        {
                            Game._ColorBoard[xCord - 1, yCord - 1] = (int)Game.Color.Red;

                            Game._ShapeBoard[xCord - 1, yCord - 1] =
                            GenerateCircle(x + SizeofSquares / 6, y + SizeofSquares / 6, (int)((decimal).65 * SizeofSquares), "Red");
                        }
                        else if (y / SizeofSquares >= (Dimensions - 2))
                        {
                            Game._ColorBoard[xCord - 1, yCord - 1] = (int)Game.Color.Brown;

                            Game._ShapeBoard[xCord - 1, yCord - 1] =
                            GenerateCircle(x + SizeofSquares / 6, y + SizeofSquares / 6, (int)((decimal).65 * SizeofSquares), "Brown");
                        }
                        else
                        {
                            _ColorBoard[xCord - 1, yCord - 1] = (int)Game.Color.Empty;
                        }
                    }
                    else
                    {
                        _ColorBoard[xCord - 1, yCord - 1] = (int)Game.Color.Empty;
                    }
                }
            }
            GraphicsWindow.DrawRectangle(SizeofSquares, SizeofSquares, SizeofSquares * Dimensions, SizeofSquares * Dimensions);
        }

        public static string GenerateCircle(int x, int y, int radius, string Color)
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
            await Task.Run(() => { Handlers.Selected(GraphicsWindow.MouseX.xCord(), GraphicsWindow.MouseY.yCord()); });
        }
    }

    public class Handlers
    {
        public static void Selected(int xCord,int yCord)
        {
            if (xCord <= Game.Dimensions && yCord <= Game.Dimensions)
            {
                GraphicsWindow.ShowMessage("Location : (" + xCord + "," + yCord + ") \nControl :" +
                    Game.GetSelected(xCord - 1, yCord - 1) +"\nColor : " + 
                    Game.GetColor(xCord - 1, yCord - 1), null);  
            }
        }
    }

    public static class Transform
    {
        public static int xCord(this int Data)
        {
            return Data / Game.SizeofSquares;
        }

        public static int xCord(this Primitive Data)
        {
            return Data / Game.SizeofSquares;
        }

        public static int yCord(this int Data)
        {
            return Data / Game.SizeofSquares;
        }

        public static int yCord(this Primitive Data)
        {
            return Data / Game.SizeofSquares;
        }
    }
}
