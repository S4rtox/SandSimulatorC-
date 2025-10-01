using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Water : Element
{
    public Water() : base(Color.Blue)
    {
        // Water
        var Water0 = new Color(64, 164, 223);
        var Water1 = new Color(80, 180, 230);
        var Water2 = new Color(100, 200, 240);
        var Water3 = new Color(140, 220, 255);
        var Water4 = new Color(180, 240, 255);

        Random randomWater = new Random();
        int numWater = randomWater.Next(0, 5);

        Color[] WaterColors = { Water0, Water1, Water2, Water3, Water4 };

        Color = WaterColors[numWater];

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