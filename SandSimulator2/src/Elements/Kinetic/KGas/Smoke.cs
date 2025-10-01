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

        Random randomSmoke = new Random();
        int numSmoke = randomSmoke.Next(0, 4);

        Color[] SmokeColors = { Smoke0, Smoke1, Smoke2, Smoke3 };

        Color = SmokeColors[numSmoke];
    }


    //Metodo que se ejecuta por cada frame, en cada elemento
    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        //Si el elemento de abajo es vacio, se mueve hacia abajo
        if (gridManager[position.X, position.Y + 1] is Empty)
        {
            gridManager[position.X, position.Y + 1] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;

        }
        //Si el elemento de abajo a la izquierda es vacio, se mueve hacia abajo a la izquierda
        if (gridManager[position.X - 1, position.Y + 1] is Empty)
        {
            gridManager[position.X - 1, position.Y + 1] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }

        //Si el elemento de abajo a la derecha es vacio, se mueve hacia abajo a la derecha
        if (gridManager[position.X + 1, position.Y + 1] is Empty)
        {
            gridManager[position.X + 1, position.Y + 1] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }

        //si el elemento de la izquierda esta vacio se va hacia la izquierda
        if (gridManager[position.X - 1, position.Y] is Empty)
        {
            gridManager[position.X - 1, position.Y] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;

        }
        //si el elemento de la derecha esta vacio 
        if (gridManager[position.X + 1, position.Y] is Empty)
        {
            gridManager[position.X + 1, position.Y] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;

        }
    }
}