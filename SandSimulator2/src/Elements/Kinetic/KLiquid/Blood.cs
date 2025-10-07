using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Blood : KineticLiquid
{
    public override float Density => 1.1f;
    public Blood() : base(Color.Red)
    {
        var Blood0 = new Color(138, 7, 7);
        var Blood1 = new Color(165, 25, 25);
        var Blood2 = new Color(110, 0, 0);
        var Blood3 = new Color(190, 30, 30);

        Random randomBlood = new Random();
        int numBlood = randomBlood.Next(0, 4);

        Color[] BloodColors = { Blood0, Blood1, Blood2, Blood3 };

        Color = BloodColors[numBlood];


    }
    
    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        base.Update(position, gridManager, delta);
    }
}