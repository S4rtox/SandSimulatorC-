using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public class Stone:Element
{
    public Stone() : base(Color.Gray)
    {

        var Stone0 = new Color(120, 120, 120);
        var Stone1 = new Color(140, 140, 140);
        var Stone2 = new Color(160, 160, 160);
        var Stone3 = new Color(100, 100, 100);
        var Stone4 = new Color(180, 180, 180);

        Random randomStone = RandomProvider.Random;
        int numStone = randomStone.Next(0, 5);

        Color[] StoneColors = { Stone0, Stone1, Stone2, Stone3, Stone4 };

        Color = StoneColors[numStone];

    }


    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {

    }

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}