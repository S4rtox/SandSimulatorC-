using System;
using Microsoft.Xna.Framework;
using SandSimulator2.Elements;

namespace SandSimulator2.GridManagers;

public class GridManager
{
    public Element[,] Grid { get; private set; }
    private Element[,] _bufferGrid;



    //TODO: Swapping will become a problem. Needs closer inspection
    //TODO: Deleting will also become a problem, needs even clsoer inspection
    //TODO: Kinetic bodies that do not move at 1 pixel per update will also become a problem. - Needs thinking. (TryMoveMethod?)
    //TODO: Fix array out of bounds from the grid problems

    //maybe look for videos of how people fixed these problems.

    //THis shit became more complicated than I thought it was gonna be.
    public GridManager(int width, int height)
    {
        Grid = new Element[width, height];
        _bufferGrid = new Element[width, height];

        //fill both grids with empty elements
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Grid[x, y] = Empty.Instance;
                _bufferGrid[x, y] = Empty.Instance;
            }
        }
    }


    public void Update(GameTime delta)
    {
        //Reset isValid for all elements, so they are processed normally next loop.
        foreach(Element element in Grid)
        {
            element.IsValid = true;
        }

        foreach(Element element in Grid)
        {
            if(!element.IsValid) continue; // If the element was deleted by another element, skip it.
            element.step(this, delta);
            // !!! -> Do not attept to move using reacToOther. Really big complications might arise if done.
            element.reactToOther(this, Grid[element.Position.X - 1, element.Position.Y], delta);
            element.reactToOther(this, Grid[element.Position.X + 1, element.Position.Y], delta);
            element.reactToOther(this, Grid[element.Position.X, element.Position.Y - 1], delta);
            element.reactToOther(this, Grid[element.Position.X, element.Position.Y + 1], delta);

        }

        // Swap the grids
        (Grid, _bufferGrid) = (_bufferGrid, Grid); //WHAT IS THIS SYNTAX -Deconstruction swapping.
        //This is a thing apparently

        // Clear the buffer grid
        Array.Clear(_bufferGrid, 0, _bufferGrid.Length);
    }

    public void RemoveElement(Element element)
    {
        _bufferGrid[element.Position.X, element.Position.Y] = Empty.Instance;
        element.IsValid = false;
    }

    public void MoveElement(Element element, int x, int y)
    {
        _bufferGrid[x, y] = element;
        element.Position = new Vector2I(x, y);
    }

    public void MoveElement(Element element, Vector2I position)
    {
        _bufferGrid[position.X, position.Y] = element;
        element.Position = position;
    }

    public Element GetElement(int x, int y)
    {
        return Grid[x, y];
    }

    public bool IsPositionValid(Vector2I position)
    {
        return position.X >= 0 && position.X < Grid.GetLength(0) && position.Y >= 0 && position.Y < Grid.GetLength(1);
    }

 public void SwapElements(Vector2I pos1, Vector2I pos2)
 {

     Element element1 = Grid[pos1.X, pos1.Y];
     Element element2 = Grid[pos2.X, pos2.Y];

     _bufferGrid[pos2.X, pos2.Y] = element1;
     _bufferGrid[pos1.X, pos1.Y] = element2;

     element1.Position = pos2;
     element2.Position = pos1;

     //Elements become invalid for this loop in the update method, to avoid possible problems related to update order.
     element1.IsValid = false;
     element2.IsValid = false;

 }

 public void SwapElements(Element element1, Element element2)
 {
     Vector2I pos1 = element1.Position;
     Vector2I pos2 = element2.Position;

     _bufferGrid[pos2.X, pos2.Y] = element1;
     _bufferGrid[pos1.X, pos1.Y] = element2;

     element1.Position = pos2;
     element2.Position = pos1;

     element1.IsValid = false;
     element2.IsValid = false;

 }
}