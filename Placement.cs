using System;
using System.Collections.Generic;

namespace BattleShips
{
    public class Placement
    {
        private readonly Random random = new();

        public void Placements(List<Board> boards)
        {
            foreach (Board b in boards)
            {
                foreach (Board.Ship s in b.Ships)
                {
                    do
                    {
                        int direction = random.Next(10);
                        if (direction < 5)
                        {
                            int shipStartX = random.Next(10);
                            int shipStartY = random.Next(10 - s.Size);
                            int isEmpty = 0;

                            if (isEmpty == 0 || isEmpty != s.Size)
                            {
                                for (int i = 0; i < s.Size; i++)
                                {
                                    if (b.isShip[shipStartY + i, shipStartX] == false)
                                    {
                                        isEmpty++;
                                    }
                                }
                            }

                            if (isEmpty == s.Size)
                            {
                                for (int i = 0; i < s.Size; i++)
                                {
                                    b.isShip[shipStartY + i, shipStartX] = true;
                                    s.Coords.Add(new Board.Ship.Coord { X = shipStartX, Y = shipStartY + i, isHit = false });
                                }
                                s.Placed = true;
                                s.Direction = Board.Direction.Horizontal;
                            }
                        }
                        else if (direction >= 5)
                        {
                            int shipStartX = random.Next(10 - s.Size);
                            int shipStartY = random.Next(10);
                            int isEmpty = 0;

                            if (isEmpty == 0 || isEmpty != s.Size)
                            {
                                for (int i = 0; i < s.Size; i++)
                                {
                                    if (b.isShip[shipStartY, shipStartX + i] == false)
                                    {
                                        isEmpty++;
                                    }
                                }
                            }

                            if (isEmpty == s.Size)
                            {
                                for (int i = 0; i < s.Size; i++)
                                {
                                    b.isShip[shipStartY, shipStartX + i] = true;
                                    s.Coords.Add(new Board.Ship.Coord { X = shipStartX + i, Y = shipStartY, isHit = false });
                                }
                                s.Placed = true;
                                s.Direction = Board.Direction.Vertical;
                            }
                        }
                    } while (!s.Placed);
                }
            }
        }
    }
}
