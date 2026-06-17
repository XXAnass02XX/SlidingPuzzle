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
                zeroTile = elt;
                break;
            }
        }

        var value = _grid[idxY][idxX];
        _grid[idxY][idxX] = 0;
        _grid[zeroTile[1]][zeroTile[0]] = value;
        return weMovedATile;
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