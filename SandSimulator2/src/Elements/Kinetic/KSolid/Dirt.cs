using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Dirt : KineticSolid
{
    public override float Density => 1.8f;
    public Dirt() : base(new Color(89, 61, 46))
    {
        // Dirt
        var Dirt0 = new Color(120, 85, 60);
        var Dirt1 = new Color(140, 100, 75);
        var Dirt2 = new Color(100, 70, 50);

        Random randomDirt = new Random();
        int numDirt = randomDirt.Next(0, 3);

        Color[] DirtColors = { Dirt0, Dirt1, Dirt2 };

        Color = DirtColors[numDirt];
    }
    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        base.Update(position, gridManager, delta);
    }
}