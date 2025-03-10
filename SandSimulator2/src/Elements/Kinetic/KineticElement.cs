using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public abstract class KineticElement : Element
{
    public Vector2 Velocity { get; protected set; }

    protected KineticElement(Vector2I position, Color color) : base(position, color) { }


    protected void HandleMovement(GridManager gridManager, GameTime delta)
    {
        Position += (Vector2I)Velocity;
    }
    public override void step(GridManager gridManager, GameTime delta)
    {
        HandleMovement(gridManager, delta);
    }

    public override void reactToOther(GridManager gridManager, Element element, GameTime delta)
    {
        throw new System.NotImplementedException();
    }
}