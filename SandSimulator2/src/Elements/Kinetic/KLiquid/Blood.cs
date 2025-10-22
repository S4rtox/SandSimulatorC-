using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

/// <summary>
/// Elemento líquido sangre, fluye hacia abajo y puede convertir agua adyacente en sangre.
/// </summary>
public class Blood : LiquidElement
{
    private static readonly Color[] BloodColors =
    {
        new(138, 7, 7),
        new(165, 25, 25),
        new(110, 0, 0),
        new(190, 30, 30)
    };

    public Blood() : base(BloodColors[RandomProvider.Random.Next(0, BloodColors.Length)])
    {
        Density = 1.1f;
    }
    
    // Si hay agua adyacente, la convierte en sangre.

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {
        Random rand = RandomProvider.Random;
        
        // Revisa las 8 celdas circundantes
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue; // Omite la celda actual

                // Revisa si el vecino es madera
                if (interactionApi.GetElement(i, j) is Water)
                {
                    // Probabilidad de que el agua se convierta en sangre
                    if (rand.Next(0, 50) == 0) // 1 de 50 probabilidades
                    {
                        elementApi.SetElement(i, j, new Blood());
                    }
                }
            }
        }

    }
}