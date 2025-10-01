using System;
using Microsoft.Xna.Framework;
using SandSimulator2.Elements;

namespace SandSimulator2.GridManagers;

public class GridManager
{
    private Element[,] _grid;

    public int Width { get; }
    public int Height { get; }



    // Indexers - Adoro C# - No tocar si no sabes que pedo porfas :)
    public Element this[int x, int y]
    {
        get => IsInBounds(x, y) ? _grid[x, y] : Border.Instance;

        set
        {
            if (IsInBounds(x, y)) _grid[x, y] = value;
        }
    }
    public Element this[Vector2I vector2I]
    {
        get => IsInBounds(vector2I) ? _grid[vector2I.X, vector2I.Y] : Empty.Instance;
        set
        {
            if (IsInBounds(vector2I)) _grid[vector2I.X, vector2I.Y] = value;
        }
    }


    public GridManager(int width, int height)
    {
        Width = width;
        Height = height;
        _grid = new Element[width, height];

        //fill both grids with empty elements
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _grid[x, y] = Empty.Instance;
            }
        }
    }


    public void Update(GameTime delta)
    {
        for (int y = Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                var element = _grid[x, y];
                if (element is Empty || element.HasBeenUpdated) continue;
                element.Update(new Vector2I(x, y), this, delta);
                element.HasBeenUpdated = true;
            }
        }

        //Reset all elements to not updated
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                _grid[x, y].HasBeenUpdated = false;
            }
        }
    }

    public Element getElementAt(int x, int y)
    {
        return _grid[x, y];
    }


    public void moveElementTo(Element element, int x, int y)
    {
        _grid[x, y] = element;
    }

    public bool IsInBounds(int x, int y)
    {
        return !(x < 0 || x >= Width || y < 0 || y >= Height);
    }

    public bool IsInBounds(Vector2I position)
    {
        return IsInBounds(position.X, position.Y);
    }

}