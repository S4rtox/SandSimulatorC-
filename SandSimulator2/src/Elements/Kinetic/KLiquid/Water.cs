using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Water : Element
{
    public Water() : base(Color.Blue)
    {
        // Water
        var Water0 = new Color(37, 124, 196);
        var Water1 = new Color(57, 159, 225);
        var Water2 = new Color(33, 147, 212);
        var Water3 = new Color(104, 194, 243);
        var Water4 = new Color(95, 175, 218);

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

             // Movimiento horizontal aleatorio si hay espacio vacío
        Random rand = new Random();
        bool tryLeft = rand.Next(0, 2) == 0; // 0 = izquierda, 1 = derecha

        if (tryLeft)
        {
            if (gridManager[position.X - 1, position.Y] is Empty)
            {
                gridManager[position.X - Dispertion(), position.Y] = this;
                gridManager[position.X, position.Y] = Empty.Instance;
                return;
            }
        }
        else
        {
            if (gridManager[position.X + 1, position.Y] is Empty)
            {
                gridManager[position.X + Dispertion(), position.Y] = this;
                gridManager[position.X, position.Y] = Empty.Instance;
                return;
            }
        }
        
        
        //Si el elemento de abajo a la izquierda es vacio, se mueve hacia abajo a la izquierda
        if (gridManager[position.X - 1, position.Y - 1] is Empty)
        {
            gridManager[position.X - Dispertion(), position.Y - 1] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }

        //Si el elemento de abajo a la derecha es vacio, se mueve hacia abajo a la derecha
        if (gridManager[position.X + 1, position.Y - 1] is Empty)
        {
            gridManager[position.X +  Dispertion(), position.Y - 1] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }

      
       
        //patron para cambio de color el agua
        WaterPattern();

    }

    public int Dispertion()
    {
        int dispertion = 3;
        Random disp = new Random(); disp.Next(0, dispertion);

        if (disp.NextDouble() == 1)
        {
            return 1;
        }
        if (disp.NextDouble() == 2)
        {
            return 2;
        }
        if (disp.NextDouble() == 3)
        {
            return 3;
        }
        return 0;
    }

    public void WaterPattern()
    {

        var Water0 = new Color(37, 124, 196);
        var Water1 = new Color(57, 159, 225);
        var Water2 = new Color(33, 147, 212);
        var Water3 = new Color(104, 194, 243);
        var Water4 = new Color(95, 175, 218);
        Color[] WaterColors = { Water0, Water1, Water2, Water3, Water4 };

        // Probabilidad de cambiar de color
        Random rand = new Random();
        if (rand.NextDouble() < 0.005)
        {
            int numWater = rand.Next(0, WaterColors.Length);
            Color = WaterColors[numWater];
        }
    }
    

}