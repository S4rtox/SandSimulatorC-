using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Water : Element
{
    public int Dispertion { get; set; }
    
    
    public Water() : base(Color.Blue)
    {
        this.Dispertion = MDispertion();
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


    public override void Update(GridManager gridManager, GameTime delta)
    {
      
        //Si el elemento de abajo es vacio, se mueve hacia abajo
        if (gridManager[Position.X, Position.Y - 1] is Empty)
        {
            gridManager[Position.X, Position.Y - 1] = this;
            gridManager[Position.X, Position.Y] = Empty.Instance;
            return;

        }

         //Si el elemento de abajo a la izquierda es vacio, se mueve hacia abajo a la izquierda
        if (gridManager[Position.X - 1, Position.Y - 1] is Empty)
        {
            gridManager[Position.X - MDispertion(), Position.Y - 1] = this;
            gridManager[Position.X, Position.Y] = Empty.Instance;
            return;
        }

        //Si el elemento de abajo a la derecha es vacio, se mueve hacia abajo a la derecha
        if (gridManager[Position.X + 1, Position.Y - 1] is Empty)
        {
            gridManager[Position.X +  MDispertion(), Position.Y - 1] = this;
            gridManager[Position.X, Position.Y] = Empty.Instance;
            return;
        }

        // Movimiento horizontal aleatorio si hay espacio vacÃ­o
        Random rand = new Random();
        bool tryLeft = rand.Next(0, 2) == 0; // 0 = izquierda, 1 = derecha

        //checa si ambos lados estan vacios para intentar moverse a cualquiera de los dos lados
        if (gridManager[Position.X - 1, Position.Y] is Empty && gridManager[Position.X + 1, Position.Y] is Empty)
        {
            if (tryLeft)
            {
                //checa si puede moverse a la izquierda y cada ciclo checa si puede seguir moviendose a la izquierda
                //4

                for (int i = 1; i <= MDispertion(); i++)
                {
                    if (gridManager[Position.X - i, Position.Y] is not Empty)
                    {
                        break;
                    }
                    else
                    {

                        gridManager[Position.X - i, Position.Y] = this;
                        gridManager[Position.X - (i - 1), Position.Y] = Empty.Instance;
                    }
                }
                return;

            }
            else
            {
                //checa si puede moverse a la derecha y cada ciclo checa si puede seguir moviendose a la derecha
                for (int i = 1; i <= MDispertion(); i++)
                {
                    if (gridManager[Position.X + i, Position.Y] is not Empty)
                    {
                        break;
                    }
                    else
                    {
                        gridManager[Position.X + i, Position.Y] = this;
                        gridManager[Position.X - (i - 1), Position.Y] = Empty.Instance;
                    }
                }
                return;
            }
        }
        //checa si solo el lado izquierdo esta vacio
        if (gridManager[Position.X - 1, Position.Y] is Empty)
        {
            //checa si puede moverse a la izquierda y cada ciclo checa si puede seguir moviendose a la izquierda
            for (int i = 1; i <= MDispertion(); i++)
            {
                if (gridManager[Position.X - i, Position.Y] is not Empty)
                {
                    break;
                }
                else
                {
                    gridManager[Position.X - i, Position.Y] = this;
                    gridManager[Position.X - (i - 1), Position.Y] = Empty.Instance;
                }
            }
            return;
        }
        //checa si solo el lado derecho esta vacio
        if (gridManager[Position.X + 1, Position.Y] is Empty)
        {
            //checa si puede moverse a la derecha y cada ciclo checa si puede seguir moviendose a la derecha
            for (int i = 1; i <= MDispertion(); i++)
            {
                if (gridManager[Position.X + i, Position.Y] is not Empty)
                {
                    break;
                }
                else
                {
                    gridManager[Position.X + i, Position.Y] = this;
                    gridManager[Position.X - (i - 1), Position.Y] = Empty.Instance;
                }
            }
                return;
        }




       

      
       
        //patron para cambio de color el agua
        WaterPattern();

    }

    public int MDispertion()
    {
        int dispertion = 3;
        Random disp = new Random();
         int j = disp.Next(0, dispertion);

        if (j == 0)
        {
            return 1;
        }
        if (j  == 1)
        {
            return 2;
        }
        if (j == 2)
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