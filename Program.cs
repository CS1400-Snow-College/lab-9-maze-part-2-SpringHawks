// Annette Hawks
// Due 7/16/2025
// Lab 8 Maze 

//Rules &Introduction ish
Console.WriteLine("MAZE RULES:\n" +
                        "1. Use arrow keys to move.\n" +
                        "2. Walls are '*'.\n" +
                        "3. Reach the exit, '#' to win.\n" +
                        "4. Bad Guys: '%'(move) and '&'(static).\n" +
                        "5. Coins: '^' worth 100 points.\n" +
                        "6. Gems: '$' worth 200 points.\n" +
                        "Press any key to start...");
Console.ReadKey(true);
Console.Clear();

// Program correctly loads the maze from the text file
string[] mapRows = File.ReadAllLines("map.txt");

// Program correctly loads the maze from the text file (display maze on screen)
foreach (string row in mapRows)
    Console.WriteLine(row);

// Move the cursor position to the top left corner of the screen
int playerRow = 0, playerCol = 0;
Console.SetCursorPosition(playerCol, playerRow);
Console.Write('@');

while (true)
{
    // Program correctly responds to arrows and the escape key by moving the cursor or exiting the program
    ConsoleKey key = Console.ReadKey(true).Key;
    int newRow = playerRow, newCol = playerCol;

    switch (key)
    {
        case ConsoleKey.Escape:
            return;  // Exit game

        case ConsoleKey.UpArrow:
            newRow--;
            break;

        case ConsoleKey.DownArrow:
            newRow++;
            break;

        case ConsoleKey.LeftArrow:
            newCol--;
            break;

        case ConsoleKey.RightArrow:
            newCol++;
            break;
    }

    // Program correctly enforces the boundaries of the buffer and the map
    if (newRow < 0 || newRow >= mapRows.Length || newCol < 0 || newCol >= mapRows[newRow].Length)
    {
        // Ignore move out of bounds
        newRow = playerRow;
        newCol = playerCol;
    }
    // Program correctly enforces the walls (i.e. * characters) on the map
    else if (mapRows[newRow][newCol] == '*')
    {

        newRow = playerRow;
        newCol = playerCol;
    }


    Console.SetCursorPosition(playerCol, playerRow);
    Console.Write(mapRows[playerRow][playerCol]);

    // Update player position
    playerRow = newRow;
    playerCol = newCol;

    // Player character (@) blue on a black backgroung
    Console.SetCursorPosition(playerCol, playerRow);
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.Write('@');
    Console.ResetColor();

    // Program correctly prints the win message when the player makes it to the goal
    if (mapRows[playerRow][playerCol] == '#')
    {
        Console.Clear();
        Console.WriteLine("You found the exit! You win!");
        break;
    }
}
