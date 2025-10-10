using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Dirt : Element
{
    public Dirt() : base(new Color(89, 61, 46))
    {
        // Dirt
        var Dirt0 = new Color(120, 85, 60);
        var Dirt1 = new Color(140, 100, 75);
        var Dirt2 = new Color(100, 70, 50);

        Random randomDirt = RandomProvider.Random;
        int numDirt = randomDirt.Next(0, 3);

        Color[] DirtColors = { Dirt0, Dirt1, Dirt2 };

        Color = DirtColors[numDirt];
    }

    //Metodo que se ejecuta por cada frame, en cada elemento
    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        var belowElement = api.GetElement(0, -1);
        //Si el elemento de abajo es vacio, se mueve hacia abajo
        if (belowElement is Empty)
        {
            api.MoveTo(0, -1);

        }else if (api.GetElement(-1, -1) is Empty)
        {
            api.MoveTo(-1, -1);

        }
        else if (api.GetElement(1, -1) is Empty)
        {
            api.MoveTo(1, -1);

        }
        //Si el elemento de abajo a la izquierda es vacio, se mueve hacia abajo a la izquierda


    }

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}