using System;
using Microsoft.Xna.Framework;
using SandSimulator2.Elements;

namespace SandSimulator2.GridManagers;

public class GridManager
{
    public Element[,] Grid { get; private set; }




    //maybe look for videos of how people fixed these problems.

    //THis shit became more complicated than I thought it was gonna be.
    public GridManager(int width, int height)
    {
        Grid = new Element[width, height];

        //fill both grids with empty elements
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Grid[x, y] = Empty.Instance;
            }
        }
    }


    public void Update(GameTime delta)
    {
        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            for (int y = Grid.GetLength(1); y > 0; y--)
            {
                if (Grid[x, y] is Empty) continue;

                Grid[x,y].step(this, delta);
            }
        }
    }

    public Element getElementAt(int x, int y)
    {
        return Grid[x, y];
    }


    public void moveElementTo(Element element, int x, int y)
    {
        Grid[x, y] = element;
    }

}