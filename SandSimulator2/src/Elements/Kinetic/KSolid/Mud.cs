using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Mud : Element
{
    public Mud() : base(new Color(69, 41, 26))
    {
        Density = 1.6f;
        var Mud0 = new Color(80, 55, 40);
        var Mud1 = new Color(100, 70, 55);
        var Mud2 = new Color(60, 40, 30);

        Random randomMud = RandomProvider.Random;
        int numMud = randomMud.Next(0, 3);

        Color[] MudColors = { Mud0, Mud1, Mud2 };

        Color = MudColors[numMud];
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