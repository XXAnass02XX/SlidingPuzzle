using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PuzzleGame.Core;

namespace PuzzleGame.App;

public partial class MainWindow : Window
{
    private readonly grid _grid = new();
    private (int col, int row)? _hintCell;

    public MainWindow(string playerName)
    {
        InitializeComponent();
        WelcomeText.Text = $"Welcome, {playerName}!";
        RenderGrid();
    }

    private void RenderGrid()
    {
        PuzzleGrid.Children.Clear();
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                int value = _grid.GetValue(col, row);
                bool isEmpty = value == 0;
                bool isHint = !isEmpty && _hintCell == (col, row);

                var label = new TextBlock
                {
                    Text = isEmpty ? "" : value.ToString(),
                    FontSize = 40,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Brush bg = isEmpty ? new SolidColorBrush(Color.FromRgb(220, 220, 220))
                         : isHint  ? new SolidColorBrush(Color.FromRgb(255, 220, 60))
                                   : Brushes.White;

                var cell = new Border
                {
                    Background = bg,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(180, 180, 180)),
                    BorderThickness = new Thickness(2),
                    CornerRadius = new CornerRadius(6),
                    Margin = new Thickness(5),
                    Child = label,
                    Tag = (col, row),
                    Cursor = isEmpty ? Cursors.Arrow : Cursors.Hand
                };

                if (!isEmpty)
                    cell.MouseLeftButtonUp += TileClicked;

                PuzzleGrid.Children.Add(cell);
            }
        }
    }

    private void HintClicked(object sender, RoutedEventArgs e)
    {
        _hintCell = _grid.GetHint();
        if (_hintCell == null)
            MessageBox.Show("No hint available.", "Hint", MessageBoxButton.OK, MessageBoxImage.Information);
        RenderGrid();
    }

    private void TileClicked(object sender, MouseButtonEventArgs e)
    {
        _hintCell = null;
        var (col, row) = ((int, int))((Border)sender).Tag;

        bool moved = _grid.moveTile(col, row);
        if (!moved) return;

        RenderGrid();

        if (_grid.isWon())
            MessageBox.Show("Congratulations! You solved the puzzle!", "You won!",
                MessageBoxButton.OK, MessageBoxImage.Information);
    }
}