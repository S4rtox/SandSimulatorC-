using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public sealed class Water : KineticLiquid
{
    public override float Density => 1.0f;

    public Water() : base(Color.Blue)
    {
        // Water
       var Water0 = new Color(37, 124, 196);
        var Water1 = new Color(57, 159, 225);
        var Water2 = new Color(33, 147, 212);
        var Water3 = new Color(104, 194, 243);
        var Water4 = new Color(95, 175, 218);

        Random randomWater = new Random();
        int numWater = randomWater.Next(0, 5);
        Color[] WaterColors = { Water0, Water1, Water2, Water3, Water4 };
        Color = WaterColors[numWater];

    }
    
    public void WaterPattern()
    {
        
        var Water0 = new Color(137, 224, 254);
        var Water1 = new Color(57, 159, 225);
        var Water2 = new Color(33, 147, 212);
        var Water3 = new Color(104, 194, 243);
        var Water4 = new Color(95, 175, 218);
        Color[] WaterColors = { Water0, Water1, Water2, Water3, Water4 };

        // Probabilidad de cambiar de color al 0.005%
        Random rand = new Random();
        if (rand.NextDouble() < 0.005)
        {
            int numWater = rand.Next(0, WaterColors.Length);
            Color = WaterColors[numWater];
        }
    }
    
    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        base.Update(position, gridManager, delta);
        WaterPattern();
    }
}