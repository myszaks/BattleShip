using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Input;

namespace BattleShips
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly Placement ShipPlacement = new();
        private string winner;
        private bool playerHasHitOpponent, placementDone, battleInProgress;
        Timer timer;

        public List<Board> Boards = new()
        {
            new Board() { Name = "Player 1"},
            new Board() { Name = "Player 2"}
        };        

        private readonly List<List<int[]>> players = new()
        {
            new List<int[]>{ },
            new List<int[]>{ }
        };

        public void DrawClick(object sender, EventArgs e)
        {
            if (placementDone) Reset();

            ShipPlacement.Placements(Boards);

            foreach (Board b in Boards)
            {
                if (b.Name == "Player 1")
                {
                    foreach (Board.Ship s in Boards[0].Ships)
                    {
                        foreach (Board.Ship.Coord c in s.Coords)
                        {
                            Ellipse circle = new();
                            circle.Width = 20;
                            circle.Height = 20;
                            circle.Fill = s.Colour;
                            circle.ToolTip = s.Name;
                            Grid.SetColumn(circle, c.X);
                            Grid.SetRow(circle, c.Y);
                            board1grid.Children.Add(circle);
                        }
                    }
                }
                else if (b.Name == "Player 2")
                {
                    foreach (Board.Ship s in Boards[1].Ships)
                    {
                        foreach (Board.Ship.Coord c in s.Coords)
                        {
                            Ellipse circle = new();
                            circle.Width = 20;
                            circle.Height = 20;
                            circle.Fill = s.Colour;
                            circle.ToolTip = s.Name;
                            Grid.SetColumn(circle, c.X);
                            Grid.SetRow(circle, c.Y);
                            board2grid.Children.Add(circle);
                        }
                    }
                }
            }
            placementDone = true;
        }
        private void SeaBattleClick(object sender, EventArgs e)
        {
            if(!placementDone)
            {
                Message warning = new(this, "Warning!", "Place your ships before starting a battle!");
                warning.ShowDialog();
                return;
            }

            if(battleInProgress) return;
            
            battleInProgress = true;
            timer = new((state) =>
            {
                Random rand = new();
                int i = 0;
                int[] shotCoordinate;

                bool playerLost = false;
                do
                {
                    do
                    {
                        bool result = false;
                        do
                        {
                            shotCoordinate = new int[] { rand.Next(10), rand.Next(10) };
                            result = players[i].Any(s => s[0] == shotCoordinate[0] && s[1] == shotCoordinate[1]);
                        } while (result);
                        players[i].Add(shotCoordinate);

                        foreach (Board.Ship s in Boards[i].Ships)
                        {
                            foreach (Board.Ship.Coord c in s.Coords)
                            {
                                if (c.X == shotCoordinate[1] && c.Y == shotCoordinate[0])
                                {
                                    c.isHit = true;
                                    s.Hit++;
                                    playerHasHitOpponent = true;

                                    Dispatcher.Invoke(new Action(() =>
                                    {
                                        Ellipse circle = new();
                                        circle.Width = 10;
                                        circle.Height = 10;
                                        circle.Fill = Brushes.Red;
                                        circle.ToolTip = "Hit!";
                                        Grid.SetColumn(circle, shotCoordinate[1]);
                                        Grid.SetRow(circle, shotCoordinate[0]);
                                        Grid.SetZIndex(circle, 5);
                                        if (i == 1) board2grid.Children.Add(circle);
                                        else if (i == 0) board1grid.Children.Add(circle);
                                    }));

                                    if (s.Hit == s.Size)
                                    {
                                        s.Sunk = true;
                                        Boards[i].sunkCount++;
                                    }

                                    if (Boards[i].sunkCount == 5)
                                    {
                                        playerLost = true;

                                        if (i == 1) winner = Boards[0].Name.ToString();
                                        else if (i == 0) winner = Boards[1].Name.ToString();
                                    }
                                    break;
                                }
                                else
                                {
                                    Dispatcher.Invoke(new Action(() =>
                                    {
                                        Ellipse circle = new();
                                        circle.Width = 10;
                                        circle.Height = 10;
                                        circle.Fill = Brushes.DarkGray;
                                        circle.ToolTip = "Miss!";
                                        Grid.SetColumn(circle, shotCoordinate[1]);
                                        Grid.SetRow(circle, shotCoordinate[0]);
                                        Grid.SetZIndex(circle, 3);
                                        if (i == 1) board2grid.Children.Add(circle);
                                        else if (i == 0) board1grid.Children.Add(circle);
                                    }));

                                    playerHasHitOpponent = false;
                                }
                            }
                        }
                    } while (playerHasHitOpponent);
                    if (i == 0) i = 1;
                    else if (i == 1) i = 0;

                    Thread.Sleep(75);
                } while (!playerLost);

                Dispatcher.Invoke(new Action(() =>
                {
                    Message winnerMsg = new(this, "Congratulations!",  $"{winner} won!");
                    winnerMsg.ShowDialog();
                }));
            });

            timer.Change(10, Timeout.Infinite);
        }
        private void ResetClick(object sender, EventArgs e)
        {
            if (battleInProgress) return;

            Reset();
        }
        private void Reset()
        {
            foreach (Board b in Boards)
            {
                b.isShip = new bool[10, 10];
                b.sunkCount = 0;

                foreach (Board.Ship s in b.Ships)
                {
                    s.Placed = false;
                    s.Coords.Clear();
                    s.Sunk = false;
                    s.Direction = 0;
                    s.Hit = 0;
                }
            }
            board1grid.Children.Clear();
            board2grid.Children.Clear();
            for (int i = 0; i < players.Count; i++)
            {
                players[i].Clear();
            }

            placementDone = false;

            if(timer != null) timer.Dispose();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void QuitClick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
