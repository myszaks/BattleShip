using System.Collections.Generic;
using System.Windows.Media;

namespace BattleShips
{
    public class Board
    {
        public string Name;
        public bool[,] isShip = new bool[10, 10];
        public int sunkCount;

        public List<Ship> Ships = new List<Ship>()
        {
            new Ship(){Name = "Carier", Size = 5, Colour = Brushes.DarkGreen},
            new Ship(){Name = "Battleship", Size = 4, Colour = Brushes.Purple},
            new Ship(){Name = "Destroyer", Size = 3, Colour = Brushes.Yellow},
            new Ship(){Name = "Submarine", Size = 3, Colour = Brushes.Orange},
            new Ship(){Name = "Patrol Boat", Size = 2, Colour = Brushes.Chartreuse}
        };

        public class Ship
        {
            public string Name;
            public int Size;
            public bool Placed;
            public Direction Direction;
            public bool Sunk;
            public int Hit;
            public List<Coord> Coords = new();
            public Brush Colour;

            public class Coord
            {
                public int X;
                public int Y;
                public bool isHit;
            }
        }

        public enum Direction
        {
            Horizontal,
            Vertical
        }
    }
}
