using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Blood : Element
{
    public Blood() : base(Color.Red)
    {
        var Blood0 = new Color(138, 7, 7);
        var Blood1 = new Color(165, 25, 25);
        var Blood2 = new Color(110, 0, 0);
        var Blood3 = new Color(190, 30, 30);

        Random randomBlood = RandomProvider.Random;
        int numBlood = randomBlood.Next(0, 4);

        Color[] BloodColors = { Blood0, Blood1, Blood2, Blood3 };

        Color = BloodColors[numBlood];


    }

    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        var belowElement = api.GetElement(0, -1);
        //Si el elemento de abajo es vacio, se mueve hacia abajo
        if (belowElement is Empty)
        {
            api.MoveTo(0, -1);

        }
        else if (api.GetElement(-1, -1) is Empty)
        {
            api.MoveTo(-1, -1);

        }
        else if (api.GetElement(1, -1) is Empty)
        {
            api.MoveTo(1, -1);

        }
    }

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}