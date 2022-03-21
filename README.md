# BattleShip
BattleShip game using C# and WPF. This version is a simulation between two computer players. Code documentation in the wiki.

The game is played on two grids, one for each player. The grids are square 10×10. 

Before battle begins, each board is randomly populated with ships. Each ship occupies a number of consecutive squares on the grid, arranged either horizontally or vertically. The number of squares for each ship is determined by the type of the ship. The ships cannot overlap (i.e., only one ship can occupy any given square in the grid). The types and numbers of ships allowed are the same for each player. These may vary depending on the rules.

The following are the typical set of ships:
Carrier 5
Battleship 4
Destroyer 3
Submarine 3
Patrol boat 2

After the ships have been positioned, the game proceeds in a series of rounds. In each round, each player takes a turn to announce a target square in the opponent's grid which is to be shot at. The opponent announces whether or not the square is occupied by a ship, and if it is a "miss", the player marks their primary grid with a gray peg; if a "hit" they mark this on their own primary grid with a red peg. The attacking player notes the hit or miss on their own "tracking" grid with the appropriate color peg (red for "hit", white for "miss"), in order to build up a picture of the opponent's fleet.

When all of the squares of a ship have been hit, the ship is sunk, and the ship's owner announces this (e.g. "You sank my battleship!"). If all of a player's ships have been sunk, the game is over and their opponent wins.
