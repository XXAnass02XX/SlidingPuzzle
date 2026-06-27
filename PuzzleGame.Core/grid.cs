namespace PuzzleGame.Core;

public class grid
{
    private List<List<int>> _grid;

    public grid()
    {
        var numbers = Enumerable.Range(0, 9).ToList();
        var rnd = new Random();
        // Fisher–Yates shuffle
        for (int i = numbers.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
        }
        // slice the flat list into 3 rows of 3
        _grid = new List<List<int>>();
        for (int r = 0; r < 3; r++)
            _grid.Add(numbers.GetRange(r * 3, 3));
    }

    public int GetValue(int x, int y) => _grid[y][x];

    public List<List<int>> getNeighbours(int idxX, int idxY)
    {
        return new List<List<int>>
        {
            new List<int> {idxX-1, idxY},
            new List<int> {idxX+1, idxY},
            new List<int> {idxX, idxY+1},
            new List<int> {idxX, idxY-1}
        };
        ;
    }

    public bool isWithinBounds(int idxX, int idxY)
    {
        if (idxX < 0 || idxX >= _grid[0].Count || idxY < 0 || idxY >= _grid.Count) return false;
        return true;
    }
    public bool moveTile(int idxX, int idxY)
    {
        bool weMovedATile = false;
        List<int> zeroTile = new List<int> { 0, 0 };
        List<List<int>> neighbours = getNeighbours(idxX, idxY);
        foreach (var elt in neighbours)
        {
            if (isWithinBounds(elt[0], elt[1]) && _grid[elt[1]][elt[0]] == 0)
            {
                weMovedATile = true;
                var value = _grid[idxY][idxX];
                _grid[idxY][idxX] = 0;
                _grid[elt[1]][elt[0]] = value;
                break;
            }
        }
        return weMovedATile;
    }
    
    public bool isWon()
    {
        var flat = _grid.AsEnumerable().Reverse().SelectMany(row => row).ToList();
        for (int i = 0; i < flat.Count - 1; i++)
            if (flat[i] != i + 1) return false;
        return flat[^1] == 0;
    }

    // GoalIndex[v] = flat index (row*3+col) where value v belongs in the winning state.
    // isWon() reverses rows before comparing, so the win layout is: row0=[7,8,0], row1=[4,5,6], row2=[1,2,3].
    private static readonly int[] GoalIndex = { 2, 6, 7, 8, 3, 4, 5, 0, 1 };
    private static readonly int[] Goal = { 7, 8, 0, 4, 5, 6, 1, 2, 3 };

    public (int col, int row)? GetHint()
    {
        int[] start = new int[9];
        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
                start[r * 3 + c] = _grid[r][c];

        if (start.SequenceEqual(Goal)) return null;

        int startBlank = Array.IndexOf(start, 0);
        var pq = new PriorityQueue<(int[] board, int blank, int firstClick, int g), int>();
        var visited = new HashSet<string>();
        visited.Add(string.Join(",", start));

        foreach (int adj in AdjIndices(startBlank))
        {
            int[] next = (int[])start.Clone();
            (next[startBlank], next[adj]) = (next[adj], next[startBlank]);
            pq.Enqueue((next, adj, adj, 1), 1 + ManhattanDist(next));
        }

        while (pq.Count > 0)
        {
            var (board, blank, firstClick, g) = pq.Dequeue();
            string key = string.Join(",", board);
            if (visited.Contains(key)) continue;
            visited.Add(key);

            if (board.SequenceEqual(Goal))
                return (firstClick % 3, firstClick / 3);

            foreach (int adj in AdjIndices(blank))
            {
                int[] next = (int[])board.Clone();
                (next[blank], next[adj]) = (next[adj], next[blank]);
                if (!visited.Contains(string.Join(",", next)))
                    pq.Enqueue((next, adj, firstClick, g + 1), g + 1 + ManhattanDist(next));
            }
        }

        return null;
    }

    private static int ManhattanDist(int[] board)
    {
        int dist = 0;
        for (int i = 0; i < 9; i++)
        {
            int v = board[i];
            if (v == 0) continue;
            int g = GoalIndex[v];
            dist += Math.Abs(i % 3 - g % 3) + Math.Abs(i / 3 - g / 3);
        }
        return dist;
    }

    private static IEnumerable<int> AdjIndices(int idx)
    {
        int r = idx / 3, c = idx % 3;
        if (r > 0) yield return idx - 3;
        if (r < 2) yield return idx + 3;
        if (c > 0) yield return idx - 1;
        if (c < 2) yield return idx + 1;
    }

    public override string ToString()
    {
        string str = "";
        foreach (var row in _grid)
        {
             str += string.Join(" ", row);
             str += "\n";
        }
        return str;
    }
}