using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public sealed class Smoke : KineticGas
{
    public override float Density => -0.5f;

    public Smoke() : base(new Color(70, 70, 70))
    {
        var Smoke0 = new Color(70, 70, 70);
        var Smoke1 = new Color(60, 60, 60);
        var Smoke2 = new Color(85, 85, 85);

        Random randomSmoke = new Random();
        int numSmoke = randomSmoke.Next(0, 3);

        Color[] SmokeColors = { Smoke0, Smoke1, Smoke2 };

        Color = SmokeColors[numSmoke];
    }

    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        base.Update(position, gridManager, delta);
    }
}