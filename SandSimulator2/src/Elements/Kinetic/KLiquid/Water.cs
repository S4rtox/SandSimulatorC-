using System;
using Microsoft.Xna.Framework;
using SandSimulator2.Elements.Kinetic.KSolid;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

/// <summary>
/// Elemento líquido que fluye hacia abajo y se dispersa horizontalmente.
/// </summary>
public class Water : LiquidElement
{
    private static readonly Color[] WaterColors =
    {
        new(37, 124, 196),
        new(57, 159, 225),
        new(33, 147, 212),
        new(104, 194, 243),
        new(95, 175, 218)
    };

    public Water() : base(WaterColors[RandomProvider.Random.Next(0, WaterColors.Length)])
    {
        Density = 1.0f;
    }

    /// <summary>
    /// El agua interactúa con arena y tierra para crear versiones húmedas.
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
}