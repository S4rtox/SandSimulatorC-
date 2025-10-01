using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Sand : Element
{
    public Sand() : base(new Color(194, 178, 128))
    {

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