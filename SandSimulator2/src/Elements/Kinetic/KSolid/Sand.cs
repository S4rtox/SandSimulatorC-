using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;
using Color = Microsoft.Xna.Framework.Color;

namespace SandSimulator2.Elements.Kinetic;

public class Sand : Element
{
    public Sand() : base(new Color(194, 178, 128))
    {
        //Primero tenemos los sprites de la arena:
        var Color0 = new Color(194, 178, 128);
        var Color1 = new Color(170, 165, 111);
        var Color2 = new Color(233, 222, 211);
        var Color3 = new Color(210, 200, 151);
        
        Random random = new Random();
        int num = random.Next(0, 3);
        
        Color[] color = {Color0,Color1, Color2, Color3 };

        Color = color[num];
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