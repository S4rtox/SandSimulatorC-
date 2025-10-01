using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class KineticSolid : Element
{
    public KineticSolid(Color color) : base(color)
    {
    }

    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        throw new NotImplementedException();
    }
}