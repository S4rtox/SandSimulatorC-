using System.Runtime.InteropServices.Marshalling;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public abstract class Element
{
    public virtual Vector2I Position { get; set; }
    public Color Color { get; protected set; }

    public bool HasBeenUpdated { get; set; } = true;

    protected Element(Color color)
    {
        Color = color;
    }

    public abstract void Update(GridManager gridManager, GameTime delta);

    //Posible removal






}
