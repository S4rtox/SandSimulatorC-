using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Smoke : Element
{
    public Smoke() : base(new Color(20, 20, 20))
    {
        var Smoke0 = new Color(105, 105, 105);
        var Smoke1 = new Color(128, 128, 128);
        var Smoke2 = new Color(169, 169, 169);
        var Smoke3 = new Color(192, 192, 192);

        Random randomSmoke = RandomProvider.Random;
        int numSmoke = randomSmoke.Next(0, 4);

        Color[] SmokeColors = { Smoke0, Smoke1, Smoke2, Smoke3 };

        Color = SmokeColors[numSmoke];
    }

    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        if (api.GetElement(0, 1) is Empty)
        {
            api.MoveTo(0, 1);
            return;

        }
        //Si el elemento de arriba a la izquierda es vacio, se mueve hacia arriba a la izquierda
        if (api.GetElement(-1, 1) is Empty)
        {
            api.MoveTo(-1, 1);
            return;
        }

        //Si el elemento de arriba a la derecha es vacio, se mueve hacia arriba a la derecha
        if (api.GetElement(1, 1) is Empty)
        {
            api.MoveTo(1, 1);
            return;
        }

        //si el elemento de la izquierda esta vacio se va hacia la izquierda
        if (api.GetElement(-1, 0) is Empty)
        {
            api.MoveTo(-1, 0);
            return;

        }
        //si el elemento de la derecha esta vacio
        if (api.GetElement(1, 0) is Empty)
        {
            api.MoveTo(1, 0);
            return;

        }


    }

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}