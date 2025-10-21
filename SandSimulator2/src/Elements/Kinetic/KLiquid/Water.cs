using System;
using Microsoft.Xna.Framework;
using SandSimulator2.Elements.Kinetic.KSolid;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

/// <summary>
/// Elemento líquido que fluye hacia abajo y se dispersa horizontalmente.
/// </summary>
public class Water : Element
{
    /// <summary>
    /// Dispersión horizontal máxima al moverse lateralmente.
    /// </summary>
    public int Dispertion { get; set; }
    
    
    public Water() : base(Color.Blue)
    {
        Density = 1.0f;
        this.Dispertion = MDispertion();
        // Water
        var Water0 = new Color(37, 124, 196);
        var Water1 = new Color(57, 159, 225);
        var Water2 = new Color(33, 147, 212);
        var Water3 = new Color(104, 194, 243);
        var Water4 = new Color(95, 175, 218);

        Random randomWater = RandomProvider.Random;

        int numWater = randomWater.Next(0, 5);

        Color[] WaterColors = { Water0, Water1, Water2, Water3, Water4 };

        Color = WaterColors[numWater];

    }

    /// <summary>
    /// Actualiza el comportamiento del agua: cae si puede o se dispersa lateralmente.
    /// </summary>
    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        WaterPattern();

        var belowElement = api.GetElement(0, -1);
        if (Density > belowElement.Density)
        {
            api.SwapWith(0, -1);
            return;
        }

        var belowLeftElement = api.GetElement(-1, -1);
        if (Density > belowLeftElement.Density)
        {
            api.SwapWith(-1, -1);
            return;
        }

        var belowRightElement = api.GetElement(1, -1);
        if (Density > belowRightElement.Density)
        {
            api.SwapWith(1, -1);
            return;
        }

        // Movimiento horizontal aleatorio
        Random rand = RandomProvider.Random;
        bool tryLeft = rand.Next(0, 2) == 0; // 0 = izquierda, 1 = derecha
        var leftElement = api.GetElement(-1, 0);
        var rightElement = api.GetElement(1, 0);

        if (leftElement is Empty && rightElement is Empty)
        {
            ApplyDispertion(tryLeft, api);
            return;
        }
        if (leftElement is Empty)
        {
            ApplyDispertion(true, api);
            return;
        }
        if (rightElement is Empty)
        {
            ApplyDispertion(false, api);
        }
    }

    /// <summary>
    /// Agua no implementa interacciones activas por defecto.
    /// </summary>
    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {
        Random rand = RandomProvider.Random;
        // Revisa las 8 celdas circundantes
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue; // Omite la celda actual

                var neighbor = interactionApi.GetElement(i, j);

                // Si el vecino es arena, la convierte en arena mojada
                if (neighbor is Sand)
                {
                    elementApi.SetElement(i, j, new WetSand());
                    // El agua se consume
                    if (rand.Next(0, 5) == 0) // 1 de 5 probabilidades de desaparecer
                    {
                        elementApi.SetElement(0, 0, Empty.Instance);
                        return;
                    }
                }
                // Si el vecino es tierra, la convierte en lodo
                else if (neighbor is Dirt)
                {
                    elementApi.SetElement(i, j, new Mud());
                    // El agua se consume
                    if (rand.Next(0, 5) == 0) // 1 de 5 probabilidades de desaparecer
                    {
                        elementApi.SetElement(0, 0, Empty.Instance);
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Aplica la dispersión horizontal en la dirección indicada hasta el máximo permitido.
    /// </summary>
    private void ApplyDispertion(bool isLeft, GridManager.ElementAPI api)
    {
        var direction = isLeft ? -1 : 1;
        int maxDisp = MDispertion();
        for (int i = 1; i <= maxDisp; i++)
        {
            if (api.GetElement(i * direction, 0) is Empty) continue;
            api.MoveTo((i - 1) * direction, 0);
            return;
        }
        api.MoveTo(maxDisp * direction, 0);
    }

    /// <summary>
    /// Calcula un valor de dispersión máximo aleatorio (1..3).
    /// </summary>
    public int MDispertion()
    {
        int dispertion = 3;
        Random disp = RandomProvider.Random;
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

    /// <summary>
    /// Cambia ocasionalmente el color del agua para dar variación visual.
    /// </summary>
    public void WaterPattern()
    {

        var Water0 = new Color(37, 124, 196);
        var Water1 = new Color(57, 159, 225);
        var Water2 = new Color(33, 147, 212);
        var Water3 = new Color(104, 194, 243);
        var Water4 = new Color(95, 175, 218);
        Color[] WaterColors = { Water0, Water1, Water2, Water3, Water4 };

        // Probabilidad de cambiar de color
        Random rand = RandomProvider.Random;
        if (rand.NextDouble() < 0.005)
        {
            int numWater = rand.Next(0, WaterColors.Length);
            Color = WaterColors[numWater];
        }
    }

}