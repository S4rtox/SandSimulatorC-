using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public sealed class Steam : KineticGas
{
    public override float Density => -0.4f;

    public Steam() : base(new Color(235, 235, 235))
    {
        var Steam0 = new Color(235, 235, 235);
        var Steam1 = new Color(225, 228, 230);
        var Steam2 = new Color(245, 245, 245);

        Random randomSteam = new Random();
        int numSteam = randomSteam.Next(0, 3);

        Color[] SteamColors = { Steam0, Steam1, Steam2 };
        
        Color = SteamColors[numSteam];
    }

    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        base.Update(position, gridManager, delta);
    }
}