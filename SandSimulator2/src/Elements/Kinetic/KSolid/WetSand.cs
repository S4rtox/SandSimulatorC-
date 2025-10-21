using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic.KSolid;

public class WetSand : Element
{
    public WetSand() : base(new Color(164, 148, 98))
    {
        Density = 1.7f;
        var Color0 = new Color(184, 160, 107);
        var Color1 = new Color(195, 179, 141);
        var Color2 = new Color(201, 197, 178);
        var Color3 = new Color(205, 210, 207);

        Random random = RandomProvider.Random;
        int num = random.Next(0, 4);

        Color[] color = { Color0, Color1, Color2, Color3 };

        Color = color[num];
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