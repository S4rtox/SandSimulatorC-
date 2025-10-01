using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;


//clase para futuro
//Cuando pongamos velocidad
//Pueden no usarla
public abstract class KineticElement : Element
{
    public Vector2 Velocity { get; protected set; }

    protected KineticElement(Color color) : base(color) { }


    protected void HandleMovement(GridManager gridManager, GameTime delta)
    {

    }


    public override void ReactToOther(GridManager gridManager, Element element, GameTime delta)
    {
        throw new System.NotImplementedException();
    }
}