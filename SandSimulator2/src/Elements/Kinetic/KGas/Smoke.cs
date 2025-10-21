using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Smoke : Element
{
    public Smoke() : base(new Color(20, 20, 20))
    {
        Density = 0.1f;
        var Smoke0 = new Color(105, 105, 105);
        var Smoke1 = new Color(128, 128, 128);
        var Smoke2 = new Color(169, 169, 169);
        var Smoke3 = new Color(192, 192, 192);

        Random randomSmoke = RandomProvider.Random;
        int numSmoke = randomSmoke.Next(0, 4);

        Color[] SmokeColors = { Smoke0, Smoke1, Smoke2, Smoke3 };

        Color = SmokeColors[numSmoke];
    }

    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        Random rand = RandomProvider.Random;

        // Probabilidad de desaparecer
        if (rand.Next(0, 150) == 0) // 1 de 150 probabilidades de desaparecer en cada fotograma
        {
            api.SetElement(0, 0, Empty.Instance);
            return;
        }

        // Probabilidad de moverse hacia abajo
        if (rand.Next(0, 20) == 0) // 1 de 20 probabilidades
        {
            if (api.GetElement(0, -1) is Empty)
            {
                api.MoveTo(0, -1);
                return;
            }
        }

        // Movimiento ascendente
        if (api.GetElement(0, 1) is Empty)
        {
            api.MoveTo(0, 1);
            return;
        }
        if (api.GetElement(-1, 1) is Empty)
        {
            api.MoveTo(-1, 1);
            return;
        }
        if (api.GetElement(1, 1) is Empty)
        {
            api.MoveTo(1, 1);
            return;
        }

        // Movimiento horizontal aleatorio con tendencia a la derecha
        bool tryLeft = rand.Next(0, 5) < 2; // 40% de probabilidad de intentar ir a la izquierda, 60% a la derecha
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

    public int MDispertion()
    {
        int dispertion = 3;
        Random disp = RandomProvider.Random;
        int j = disp.Next(0, dispertion);

        if (j == 0) return 1;
        if (j == 1) return 2;
        if (j == 2) return 3;
        return 0;
    }

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {
        // El humo no interactúa con otros elementos
    }
}