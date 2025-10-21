// csharp
using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

/// <summary>
/// Representa un elemento fuego con comportamiento cinético.
/// El color se elige aleatoriamente entre tonos rojo/naranja/amarillo.
/// </summary>
public class Fire : Element
{
    /// <summary>
    /// Crea una nueva instancia de <see cref="Fire"/> y asigna un color aleatorio.
    /// </summary>
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

    /// <summary>
    /// Actualiza el estado del fuego en cada frame.
    /// - Posibilidad de extinguirse.
    /// - Movimiento ascendente prioritario.
    /// - Movimiento horizontal aleatorio con dispersión.
    /// </summary>
    /// <param name="api">API para consultar y modificar elementos relativos a la posición actual.</param>
    /// <param name="delta">Información de tiempo del frame (no usada actualmente para lógica temporal).</param>
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

    /// <summary>
    /// Aplica la dispersión horizontal del fuego desplazándolo hasta <see cref="MDispertion"/> celdas
    /// o hasta la primera celda ocupada.
    /// </summary>
    /// <param name="isLeft">Si es verdadero se dispersa a la izquierda; si no, a la derecha.</param>
    /// <param name="api">API para consultar y mover el elemento relativo a la posición actual.</param>
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
    /// Determina la cantidad máxima de dispersión horizontal aleatoria.
    /// Devuelve 1, 2 o 3 con probabilidad uniforme.
    /// </summary>
    /// <returns>Valor entero en {1,2,3} representando la distancia de dispersión.</returns>
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

    /// <summary>
    /// Maneja la interacción con elementos vecinos (por ejemplo, propagar fuego a la madera).
    /// Revisa las 8 celdas circundantes y puede convertir <see cref="Wood"/> en <see cref="Fire"/>.
    /// </summary>
    /// <param name="interactionApi">API de solo lectura para consultar elementos vecinos.</param>
    /// <param name="elementApi">API para modificar elementos relativos a la posición actual.</param>
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
