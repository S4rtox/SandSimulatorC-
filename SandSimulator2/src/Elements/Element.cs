using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public abstract class Element
{
    public Vector2I Position { get; set; }
    public Color Color { get; protected set; }

    public bool isUpdated { get; set; }

    public bool IsValid { get; set; } = true;

    protected Element(Vector2I position, Color color)
    {
        Position = position;
        Color = color;
    }

    public abstract void step(GridManager gridManager, GameTime delta);
    public virtual void reactToOther(GridManager gridManager, Element element, GameTime delta)
    {
        if (element is Empty || !element.IsValid) return;
    }










}
