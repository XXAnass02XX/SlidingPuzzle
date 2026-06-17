using PuzzleGame.Core;

var board = new grid();

Console.WriteLine("=== Sliding Puzzle ===");
Console.WriteLine("Enter the tile to move as: x y  (0-based, column then row)");
Console.WriteLine("Type 'q' to quit.\n");

while (true)
{
    Console.WriteLine(board);
    Console.Write("> ");

    var line = Console.ReadLine();
    if (line == null || line.Trim().ToLower() == "q")
        break;

    var parts = line.Trim().Split(' ');
    if (parts.Length != 2 || !int.TryParse(parts[0], out int x) || !int.TryParse(parts[1], out int y))
    {
        Console.WriteLine("Invalid input. Enter two numbers separated by a space (e.g. 1 2).\n");
        continue;
    }

    bool moved = board.moveTile(x, y);
    Console.WriteLine(moved ? $"Moved tile at ({x}, {y}).\n" : $"Cannot move tile at ({x}, {y}) — it must be adjacent to the empty cell.\n");
}
