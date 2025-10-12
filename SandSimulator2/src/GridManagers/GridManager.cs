using System;
using Microsoft.Xna.Framework;
using SandSimulator2.Elements;

namespace SandSimulator2.GridManagers;

public class GridManager
{
    private readonly Element[,] _grid;

    public int Width { get; }
    public int Height { get; }

    public byte Generation { get; private set; } = 0;





    // Indexers - Adoro C# - No tocar si no sabes que pedo porfas :)
    private Element this[int x, int y]
    {
        get => IsInBounds(x, y) ? _grid[x, y] : Border.Instance;

        set
        {
            if (!IsInBounds(x, y)) return;
            _grid[x, y] = value;

        }
    }
    private Element this[Vector2I vector2I]
    {
        get => IsInBounds(vector2I) ? _grid[vector2I.X, vector2I.Y] : Empty.Instance;
        set
        {
            if (!IsInBounds(vector2I)) return;
            _grid[vector2I.X, vector2I.Y] = value;
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



    public void Clear()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _grid[x, y] = Empty.Instance;
            }
        }
        Generation = 0;
    }


    public void Update(GameTime delta)
    {
        Generation++;

        for (var y = Height - 1; y >= 0; y--)
        {
            for (var x = 0; x < Width; x++)
            {
                var element = _grid[x, y];
                if (element is Empty) continue;
                if (element.Clock == (byte)(Generation+1)) continue;

                element.Interact(new InteractionAPI(x, y, this), new ElementAPI(x, y, this));
            }
        }

        for (var y = Height - 1; y >= 0; y--)
        {
            for (var x = 0; x < Width; x++)
            {
                var element = _grid[x, y];
                UpdateCell(element, new ElementAPI(x, y, this), delta);
            }
        }
    }

    private void UpdateCell(Element element,ElementAPI api, GameTime delta)
    {
        if (element is Empty || element.Clock == (byte)(Generation+1)) return;
        element.Update( api, delta);
    }

    public bool IsInBounds(int x, int y)
    {
        return !(x < 0 || x >= Width || y < 0 || y >= Height);
    }

    public bool IsInBounds(Vector2I position)
    {
        return IsInBounds(position.X, position.Y);
    }



    public readonly ref struct InteractionAPI(int x, int y, GridManager gridManager)
    {
        public Element GetElement(int offsetX, int offsetY)
        {
            if(offsetX > 2 || offsetX < -2 || offsetY > 2 || offsetY < -2)
                throw new ArgumentOutOfRangeException("Offset values for interactions must be between -2 and 2");
            var targetX = x + offsetX;
            var targetY = y + offsetY;
            return gridManager[targetX, targetY] ;
        }

    }

    public readonly ref struct ElementAPI(int x, int y, GridManager gridManager)
    {
        public Element GetElement(int offsetX, int offsetY)
        {
            var targetX = x + offsetX;
            var targetY = y + offsetY;
            return gridManager[targetX, targetY] ;
        }

        public void SwapWith(int offsetX, int offsetY)
        {
            var targetX = x + offsetX;
            var targetY = y + offsetY;
            if (!gridManager.IsInBounds(targetX, targetY)) return;

            (gridManager[targetX, targetY], gridManager[x, y]) = (gridManager[x, y], gridManager[targetX, targetY]);

            gridManager[targetX, targetY].Clock = (byte)(gridManager.Generation + 1);
            gridManager[x, y].Clock = (byte)(gridManager.Generation + 1);
        }

        public void MoveTo(int offsetX, int offsetY)
        {
            var targetX = x + offsetX;
            var targetY = y + offsetY;
            if (!gridManager.IsInBounds(targetX, targetY)) return;

            gridManager[targetX, targetY] = gridManager[x, y];
            gridManager[x, y] = Empty.Instance;

            gridManager[targetX, targetY].Clock = (byte)(gridManager.Generation + 1);
        }

        public void SetElement(int offsetX, int offsetY, Element element)
        {
            var targetX = x + offsetX;
            var targetY = y + offsetY;
            if (!gridManager.IsInBounds(targetX, targetY)) return;

            gridManager[targetX, targetY] = element;
            element.Clock = (byte)(gridManager.Generation + 1);



        }




    }

    public Element GetElement(int x, int y)
    {
        return this[x, y];
    }

    public void SetElement(int x, int y, Element element)
    {
        this[x, y] = element;
    }
}