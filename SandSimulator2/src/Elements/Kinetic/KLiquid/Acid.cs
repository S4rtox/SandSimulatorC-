using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public sealed class Acid : KineticLiquid
{
    public override float Density => 1.2f;
    public Acid() : base(Color.Green)
    {
        var Acid0 = new Color(124, 252, 0);
        var Acid1 = new Color(173, 255, 47);
        var Acid2 = new Color(50, 205, 50);
        var Acid3 = new Color(107, 142, 35);

        Random randomAcid = new Random();
        int numAcid = randomAcid.Next(0, 4);

        Color[] AcidColors = { Acid0, Acid1, Acid2, Acid3 };

        Color = AcidColors[numAcid];
    }
    
    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        base.Update(position, gridManager, delta);
    }
}