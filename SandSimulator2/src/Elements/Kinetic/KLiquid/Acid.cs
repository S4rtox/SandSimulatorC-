using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

/// <summary>
/// Elemento líquido corrosivo que fluye, se dispersa y disuelve otros elementos.
/// </summary>
public class Acid : LiquidElement
{
    private static readonly Color[] AcidColors =
    {
        new(173, 255, 47), // Verde-Amarillo
        new(127, 255, 0),  // Chartreuse
        new(0, 255, 0),     // Lima
        new(50, 205, 50)   // Verde Lima
    };

    public Acid() : base(AcidColors[RandomProvider.Random.Next(0, AcidColors.Length)])
    {
        Density = 1.1f; // Ligeramente más denso que el agua
    }

    /// <summary>
    /// El ácido disuelve los elementos con los que entra en contacto.
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

                // No disuelve otros ácidos ni el vacío
                if (neighbor is Acid || neighbor is Empty) continue;

                // Probabilidad de disolver al vecino
                if (rand.Next(0, 15) == 0)
                {
                    elementApi.SetElement(i, j, Empty.Instance);

                    // Probabilidad de que el ácido se consuma al disolver algo
                    if (rand.Next(0, 8) == 0)
                    {
                        elementApi.SetElement(0, 0, Empty.Instance);
                        return; // El ácido desapareció, no puede interactuar más
                    }
                }
            }
        }
    }
}