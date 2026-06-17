# SlidingPuzzle

A WPF-based sliding puzzle game built with C# and .NET.

## Description

SlidingPuzzle is a classic tile-sliding puzzle game with a graphical user interface. When launched, the player is greeted with a name entry screen where they type their name before starting. They then play the puzzle using mouse clicks — clicking a tile adjacent to the empty space slides it into position. The goal is to rearrange the shuffled tiles back into the correct order.

## Features

- Name entry screen before the game starts
- Mouse-driven gameplay (click tiles to slide them)
- Clean WPF UI

## Project Structure

```
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
```
