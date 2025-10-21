using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Dirt : Element
{
    public Dirt() : base(new Color(89, 61, 46))
    {
        Density = 1.4f;
        // Dirt
        var Dirt0 = new Color(120, 85, 60);
        var Dirt1 = new Color(140, 100, 75);
        var Dirt2 = new Color(100, 70, 50);

        Random randomDirt = RandomProvider.Random;
        int numDirt = randomDirt.Next(0, 3);

        Color[] DirtColors = { Dirt0, Dirt1, Dirt2 };

        Color = DirtColors[numDirt];
    }

    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        var belowElement = api.GetElement(0, -1);
        if (Density > belowElement.Density)
        {
            api.SwapWith(0, -1);
            return;
        }

        var belowLeftElement = api.GetElement(-1, -1);
        if (Density > belowLeftElement.Density)
        {
            api.SwapWith(-1, -1);
            return;
        }

        var belowRightElement = api.GetElement(1, -1);
        if (Density > belowRightElement.Density)
        {
            api.SwapWith(1, -1);
        }
    }

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}