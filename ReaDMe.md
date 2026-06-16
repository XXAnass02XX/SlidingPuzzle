SlidingPuzzle/                     ← solution folder
├── SlidingPuzzle.sln              ← the solution file
├── PuzzleGame.Core/               ← class library (game logic, no UI)
│   ├── PuzzleGame.Core.csproj
│   └── (your logic classes)
└── PuzzleGame.App/                ← WPF project (the .exe)
    ├── PuzzleGame.App.csproj
    ├── App.xaml / App.xaml.cs
    ├── NameWindow.xaml / .cs
    └── MainWindow.xaml / .cs