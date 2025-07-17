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
                  "7. If you hit a bad guy, or are hit by a bad guy, you die.\n" +
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

// Initialize moving bad guys positions (row, column)
int badGuy1RowIndex = 5;
int badGuy1ColumnIndex = 10;
int badGuy1Direction = 1; // 1 means right, -1 means left

int badGuy2RowIndex = 15;
int badGuy2ColumnIndex = 28;
int badGuy2Direction = 1; // 1 means right, -1 means left

// Method to draw a character with color at specified position
void DrawColoredChar(int row, int column, char ch, ConsoleColor foreground, ConsoleColor background = ConsoleColor.Black)
{
    Console.SetCursorPosition(column, row);
    Console.BackgroundColor = background;
    Console.ForegroundColor = foreground;
    Console.Write(ch);
    Console.ResetColor();
}

// Draw initial maze characters with colors
for (int r = 0; r < mapRows.Length; r++)
{
    for (int c = 0; c < mapRows[r].Length; c++)
    {
        char ch = mapRows[r][c];
        ConsoleColor color = ConsoleColor.White;

        if (ch == '&') color = ConsoleColor.Red;
        else if (ch == '^' || ch == '$') color = ConsoleColor.Yellow;
        else if (ch == '#') color = ConsoleColor.Green;

        DrawColoredChar(r, c, ch, color);
    }
}

// Draw player at start
DrawColoredChar(playerRowIndex, playerColumnIndex, '@', ConsoleColor.Black, ConsoleColor.Blue);

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

    // Erase old player position by redrawing the maze character beneath
    DrawColoredChar(playerRowIndex, playerColumnIndex, mapRows[playerRowIndex][playerColumnIndex],
                    mapRows[playerRowIndex][playerColumnIndex] == '&' ? ConsoleColor.Red :
                    (mapRows[playerRowIndex][playerColumnIndex] == '^' || mapRows[playerRowIndex][playerColumnIndex] == '$') ? ConsoleColor.Yellow :
                    mapRows[playerRowIndex][playerColumnIndex] == '#' ? ConsoleColor.Green :
                    ConsoleColor.White);

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
    DrawColoredChar(playerRowIndex, playerColumnIndex, '@', ConsoleColor.Black, ConsoleColor.Blue);

    // Program correctly prints the win message when the player makes it to the goal
    if (mapRows[playerRowIndex][playerColumnIndex] == '#')
    {
        Console.Clear();
        Console.WriteLine("You found the exit! You win!");
        Console.WriteLine($"Final score: {playerScore}");
        break;
    }

    // Move moving bad guys (%) horizontally
    // First erase old bad guy 1 position (restore maze char)
    DrawColoredChar(badGuy1RowIndex, badGuy1ColumnIndex, mapRows[badGuy1RowIndex][badGuy1ColumnIndex],
                    mapRows[badGuy1RowIndex][badGuy1ColumnIndex] == '&' ? ConsoleColor.Red :
                    (mapRows[badGuy1RowIndex][badGuy1ColumnIndex] == '^' || mapRows[badGuy1RowIndex][badGuy1ColumnIndex] == '$') ? ConsoleColor.Yellow :
                    mapRows[badGuy1RowIndex][badGuy1ColumnIndex] == '#' ? ConsoleColor.Green :
                    ConsoleColor.White);

    int nextBadGuy1ColumnIndex = badGuy1ColumnIndex + badGuy1Direction;
    if (nextBadGuy1ColumnIndex < 0 || nextBadGuy1ColumnIndex >= mapRows[badGuy1RowIndex].Length ||
        mapRows[badGuy1RowIndex][nextBadGuy1ColumnIndex] == '*' || mapRows[badGuy1RowIndex][nextBadGuy1ColumnIndex] == '&')
    {
        badGuy1Direction = -badGuy1Direction; // reverse direction
        nextBadGuy1ColumnIndex = badGuy1ColumnIndex + badGuy1Direction;
    }
    badGuy1ColumnIndex = nextBadGuy1ColumnIndex;

    // Draw bad guy 1
    DrawColoredChar(badGuy1RowIndex, badGuy1ColumnIndex, '%', ConsoleColor.Red);

    // First erase old bad guy 2 position (restore maze char)
    DrawColoredChar(badGuy2RowIndex, badGuy2ColumnIndex, mapRows[badGuy2RowIndex][badGuy2ColumnIndex],
                    mapRows[badGuy2RowIndex][badGuy2ColumnIndex] == '&' ? ConsoleColor.Red :
                    (mapRows[badGuy2RowIndex][badGuy2ColumnIndex] == '^' || mapRows[badGuy2RowIndex][badGuy2ColumnIndex] == '$') ? ConsoleColor.Yellow :
                    mapRows[badGuy2RowIndex][badGuy2ColumnIndex] == '#' ? ConsoleColor.Green :
                    ConsoleColor.White);

    int nextBadGuy2ColumnIndex = badGuy2ColumnIndex + badGuy2Direction;
    if (nextBadGuy2ColumnIndex < 0 || nextBadGuy2ColumnIndex >= mapRows[badGuy2RowIndex].Length ||
        mapRows[badGuy2RowIndex][nextBadGuy2ColumnIndex] == '*' || mapRows[badGuy2RowIndex][nextBadGuy2ColumnIndex] == '&')
    {
        badGuy2Direction = -badGuy2Direction; // reverse direction
        nextBadGuy2ColumnIndex = badGuy2ColumnIndex + badGuy2Direction;
    }
    badGuy2ColumnIndex = nextBadGuy2ColumnIndex;

    // Draw bad guy 2
    DrawColoredChar(badGuy2RowIndex, badGuy2ColumnIndex, '%', ConsoleColor.Red);

    // End the game if player hits a bad guy
    if ((playerRowIndex == badGuy1RowIndex && playerColumnIndex == badGuy1ColumnIndex) ||
        (playerRowIndex == badGuy2RowIndex && playerColumnIndex == badGuy2ColumnIndex))
    {
        Console.Clear();
        Console.WriteLine("You were caught by a bad guy! Game over!");
        break;
    }

    // Display current score
    Console.SetCursorPosition(0, mapRows.Length + 1);
    Console.Write($"Score: {playerScore}     ");  // Extra spaces to clear previous score
}
