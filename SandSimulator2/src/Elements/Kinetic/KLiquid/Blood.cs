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

        Random randomBlood = new Random();
        int numBlood = randomBlood.Next(0, 4);

        Color[] BloodColors = { Blood0, Blood1, Blood2, Blood3 };

        Color = BloodColors[numBlood];


    }


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