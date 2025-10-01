using System.Runtime.InteropServices.Marshalling;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public abstract class Element
{
    public Color Color { get; protected set; }

    public bool HasBeenUpdated { get; set; } = true;

    protected Element(Color color)
    {
        Color = color;
    }

    public abstract void Update(Vector2I position,GridManager gridManager, GameTime delta);

    //Posible removal
    public virtual void ReactToOther(GridManager gridManager, Element element, GameTime delta)
    {
        if (element is Empty || !element.HasBeenUpdated) return;
    }










}
