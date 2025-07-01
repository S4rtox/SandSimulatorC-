using System;
using Microsoft.Xna.Framework;
using SandSimulator2.Elements;

namespace SandSimulator2.GridManagers;

public class GridManager
{
    public Element[,] Grid { get; private set; }



    //TODO: Fuck double buffering, we doing normal buffering

    //TODO: Swapping will become a problem. Needs closer inspection
    //TODO: Deleting will also become a problem, needs even clsoer inspection
    //TODO: Kinetic bodies that do not move at 1 pixel per update will also become a problem. - Needs thinking. (TryMoveMethod?)
    // They should be handled by their own code.
    //TODO: Fix array out of bounds from the grid problems
    // Should be easy, I think?.

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
        //Reset isValid for all elements, so they are processed normally next loop.
        foreach (Element element in Grid)
        {
            if (element is Empty) continue;
            element.IsValid = true;
        }

        foreach (Element element in Grid)
        {
            if (element is Empty) continue;
            if (!element.IsValid) continue; // If the element was deleted by another element, skip it.
            element.step(this, delta);

            // !!! -> Do not attept to move using reacToOther. Really big complications might arise if done.
            element.reactToOther(this, GetElement(element.Position.X - 1, element.Position.Y), delta);
            element.reactToOther(this, GetElement(element.Position.X + 1, element.Position.Y), delta);
            element.reactToOther(this, GetElement(element.Position.X, element.Position.Y - 1), delta);
            element.reactToOther(this, GetElement(element.Position.X, element.Position.Y + 1), delta);


        }
    }

    public void RemoveElement(Element element)
    {
        Grid[element.Position.X, element.Position.Y] = Empty.Instance;
        element.IsValid = false;
    }

    public void MoveElement(Element element, int x, int y)
    {
        Grid[x, y] = element;
        element.Position = new Vector2I(x, y);
        element.IsValid = false;
    }

    public void MoveElement(Element element, Vector2I position)
    {
        Grid[position.X, position.Y] = element;
        element.Position = position;
        element.IsValid = false;
    }

    public Element GetElement(int x, int y)
    {
        if (!IsPositionValid(new Vector2I(x, y))) return Empty.Instance;
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

        Grid[pos2.X, pos2.Y] = element1;
        Grid[pos1.X, pos1.Y] = element2;

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

        Grid[pos2.X, pos2.Y] = element1;
        Grid[pos1.X, pos1.Y] = element2;

        element1.Position = pos2;
        element2.Position = pos1;

        element1.IsValid = false;
        element2.IsValid = false;

    }
}