using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public class Flesh : Element
{
    public Flesh() : base(new Color())
    {
        // Flesh
        var Flesh0 = new Color(224, 172, 146);
        var Flesh1 = new Color(205, 150, 130);
        var Flesh2 = new Color(235, 192, 170);
        var Flesh3 = new Color(255, 210, 190);

        Random randomFlesh = new Random();
        int numFlesh = randomFlesh.Next(0, 4);

        Color[] FleshColors = { Flesh0, Flesh1, Flesh2, Flesh3 };

        Color = FleshColors[numFlesh];

    }
    
    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {

    }
}