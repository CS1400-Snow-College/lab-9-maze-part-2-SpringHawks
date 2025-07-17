// Annette Hawks
// Due 7/16/2025
// Lab 8 Maze

// Program correctly displays rules and introduction before game starts
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

// Program correctly displays the maze on the screen
foreach (string row in mapRows)
    Console.WriteLine(row);

// Initialize player position
int playerRowIndex = 0;
int playerColumnIndex = 0;

// Set initial score to 0
int playerScore = 0;

// Move the cursor position to the top left corner and draw the player
Console.SetCursorPosition(playerColumnIndex, playerRowIndex);
Console.BackgroundColor = ConsoleColor.Blue;
Console.ForegroundColor = ConsoleColor.Black;
Console.Write('@');
Console.ResetColor();

while (true)
{
    // Program correctly responds to arrows and the escape key by moving the cursor or exiting the program
    ConsoleKey keyPressed = Console.ReadKey(true).Key;

    int newRowIndex = playerRowIndex;
    int newColumnIndex = playerColumnIndex;

    switch (keyPressed)
    {
        case ConsoleKey.Escape:
            return; // Exit game

        case ConsoleKey.UpArrow:
            newRowIndex--;
            break;

        case ConsoleKey.DownArrow:
            newRowIndex++;
            break;

        case ConsoleKey.LeftArrow:
            newColumnIndex--;
            break;

        case ConsoleKey.RightArrow:
            newColumnIndex++;
            break;
    }

    // Program correctly enforces the boundaries of the buffer and the map
    if (newRowIndex < 0 || newRowIndex >= mapRows.Length || newColumnIndex < 0 || newColumnIndex >= mapRows[newRowIndex].Length)
    {
        newRowIndex = playerRowIndex;
        newColumnIndex = playerColumnIndex;
    }
    // Program correctly enforces the walls (i.e. * characters) on the map
    else if (mapRows[newRowIndex][newColumnIndex] == '*')
    {
        newRowIndex = playerRowIndex;
        newColumnIndex = playerColumnIndex;
    }

    // Program correctly handles the static bad guy (&)
    else if (mapRows[newRowIndex][newColumnIndex] == '&')
    {
        Console.Clear();
        Console.WriteLine("You died in the cave!");
        break;
    }

    // Erase old player position by redrawing the original maze character
    Console.SetCursorPosition(playerColumnIndex, playerRowIndex);
    Console.Write(mapRows[playerRowIndex][playerColumnIndex]);

    // Handle scoring: Program correctly detects coins and gems and increases score
    char tile = mapRows[newRowIndex][newColumnIndex];
    if (tile == '^')
    {
        playerScore += 100;
        mapRows[newRowIndex] = mapRows[newRowIndex].Remove(newColumnIndex, 1).Insert(newColumnIndex, " ");
    }
    else if (tile == '$')
    {
        playerScore += 200;
        mapRows[newRowIndex] = mapRows[newRowIndex].Remove(newColumnIndex, 1).Insert(newColumnIndex, " ");
    }

    // Update player position
    playerRowIndex = newRowIndex;
    playerColumnIndex = newColumnIndex;

    // Redraw player character (@) with correct colors
    Console.SetCursorPosition(playerColumnIndex, playerRowIndex);
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.Write('@');
    Console.ResetColor();

    // Program correctly prints the win message when the player makes it to the goal
    if (mapRows[playerRowIndex][playerColumnIndex] == '#')
    {
        Console.Clear();
        Console.WriteLine("You found the exit! You win!");
        Console.WriteLine($"Final score: {playerScore}");
        break;
    }

    // Display current score
    Console.SetCursorPosition(0, mapRows.Length + 1);
    Console.Write($"Score: {playerScore}     ");  // Extra spaces to overwrite previous score
}
