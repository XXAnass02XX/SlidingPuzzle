using System.Windows;

namespace PuzzleGame.App;

public partial class NameWindow : Window
{
    public NameWindow()
    {
        InitializeComponent();
    }

    private void PlaySlidingPuzzleButton_Click(object sender, RoutedEventArgs e)
    {
        string name = NameBox.Text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            MessageBox.Show("Please enter your name.", "Name required",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        new MainWindow(name).Show();
        Close();
    }

    private void PlayReflexButton_Click(object sender, RoutedEventArgs e)
    {
        string name = NameBox.Text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            MessageBox.Show("Please enter your name.", "Name required",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        new ReflexWindow(name).Show();
        Close();
    }
}
