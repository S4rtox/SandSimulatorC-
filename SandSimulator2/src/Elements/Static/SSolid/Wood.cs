using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public class Wood : Element
{
    public Wood() : base(new Color())
    {
        var Wood0 = new Color(101, 67, 33);
        var Wood1 = new Color(120, 80, 40);
        var Wood2 = new Color(140, 102, 50);
        var Wood3 = new Color(160, 120, 70);

        Random randomWood = new Random();
        int numWood = randomWood.Next(0, 4);

        Color[] WoodColors = { Wood0, Wood1, Wood2, Wood3 };

        Color = WoodColors[numWood];

    }
    
    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {

    }
}