using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;


// Clase base para elementos líquidos que fluyen y se dispersan.

public abstract class LiquidElement : Element
{
    protected LiquidElement(Color color) : base(color)
    {
    }

    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        // 1. Intenta caer por densidad
        var belowElement = api.GetElement(0, -1);
        if (Density > belowElement.Density)
        {
            api.SwapWith(0, -1);
            return;
        }

        // 2. Intenta fluir en diagonal hacia abajo
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

        // 3. Intenta dispersarse horizontalmente
        Random rand = RandomProvider.Random;
        bool tryLeft = rand.Next(0, 2) == 0;
        var leftElement = api.GetElement(-1, 0);
        var rightElement = api.GetElement(1, 0);

        if (leftElement.Density < Density && rightElement.Density < Density)
        {
            ApplyDispersion(tryLeft, api);
        }
        else if (leftElement.Density < Density)
        {
            ApplyDispersion(true, api);
        }
        else if (rightElement.Density < Density)
        {
            ApplyDispersion(false, api);
        }
    }

    private void ApplyDispersion(bool isLeft, GridManager.ElementAPI api)
    {
        var direction = isLeft ? -1 : 1;
        int maxDisp = RandomProvider.Random.Next(1, 5); // Dispersión aleatoria de 1 a 4
        for (int i = 1; i <= maxDisp; i++)
        {
            // Si encontramos una celda que no es menos densa antes de llegar al máximo, nos movemos junto a ella.
            if (api.GetElement(i * direction, 0).Density >= Density)
            {
                api.SwapWith((i - 1) * direction, 0);
                return;
            }
        }
        // Si todas las celdas hasta la dispersión máxima son menos densas, nos movemos a la distancia máxima.
        api.SwapWith(direction * maxDisp, 0);
    }
}