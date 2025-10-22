using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Fire : Element
{
    public Fire() : base(new Color(20, 20, 20))
    {
        var Fire0 = new Color(255, 0, 0); // Rojo
        var Fire1 = new Color(255, 165, 0); // Naranja
        var Fire2 = new Color(255, 255, 0); // Amarillo

        Random randomFire = RandomProvider.Random;
        int numFire = randomFire.Next(0, 3);

        Color[] FireColors = { Fire0, Fire1, Fire2 };

        Color = FireColors[numFire];
    }

    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        Random rand = RandomProvider.Random;

        // Probabilidad de desaparecer
        if (rand.Next(0, 27) == 0)
        {
            api.SetElement(0, 0, Empty.Instance);
            return;
        }

        // Probabilidad de moverse hacia abajo (chispa)
        if (rand.Next(0, 20) == 0)
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
        bool tryLeft = rand.Next(0, 5) < 2; // 40% izquierda, 60% derecha
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
        Random rand = RandomProvider.Random;
        // Revisa las 8 celdas circundantes
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue; // Omite la celda actual

                // Revisa si el vecino es madera
                if (interactionApi.GetElement(i, j) is Wood)
                {
                    // Probabilidad de que la madera se convierta en fuego
                    if (rand.Next(0, 32) == 0) // 1 de 32 probabilidades
                    {
                        elementApi.SetElement(i, j, new Fire());

                        // Pequeña probabilidad de que el fuego original se extinga después de propagarse
                        if (rand.Next(0, 10) == 0) // 1 de 10 probabilidades
                        {
                            elementApi.SetElement(0, 0, Empty.Instance);
                            return; // Detiene la interacción después de extinguirse
                        }
                    }
                }
            }
        }
    }
}