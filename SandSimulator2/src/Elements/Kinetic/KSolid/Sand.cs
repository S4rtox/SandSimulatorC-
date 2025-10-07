using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;
using System;

namespace SandSimulator2.Elements.Kinetic;

public sealed class Sand : KineticSolid
{

    public override float Density => 1.6f;
    private Random random = new Random();
    
    public Sand() : base(new Color(194, 178, 128))
    {
        var Color0 = new Color(234,190,117);
        var Color1 = new Color(245,209,151);
        var Color2 = new Color(251,227,188);
        var Color3 = new Color(255,240,217);

        Random random = new Random();
        int num = random.Next(0, 3);
        Color[] color = {Color0,Color1, Color2, Color3 };
        Color = color[num];
    }
    
    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        base.Update(position, gridManager, delta);
    }
}