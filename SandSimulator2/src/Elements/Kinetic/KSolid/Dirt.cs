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

        Random randomDirt = new Random();
        int numDirt = randomDirt.Next(0, 3);

        Color[] DirtColors = { Dirt0, Dirt1, Dirt2 };

        Color Dirt = DirtColors[numDirt];
    }


    //Metodo que se ejecuta por cada frame, en cada elemento


    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        //Si el elemento de abajo es vacio, se mueve hacia abajo
        if (gridManager[position.X, position.Y - 1] is Empty)
        {
            gridManager[position.X, position.Y - 1] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }

        //Si el elemento de abajo a la izquierda es vacio, se mueve hacia abajo a la izquierda
        if (gridManager[position.X - 1, position.Y - 1] is Empty)
        {
            gridManager[position.X - 1, position.Y - 1] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }

        //Si el elemento de abajo a la derecha es vacio, se mueve hacia abajo a la derecha
        if (gridManager[position.X + 1, position.Y - 1] is Empty)
        {
            gridManager[position.X + 1, position.Y - 1] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }
    }
}