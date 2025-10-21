using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

/// <summary>
/// Vapor que asciende y se desplaza lateralmente cuando encuentra espacio.
/// </summary>
public class Steam : Element
{
    public Steam() : base(new Color(20, 20, 20))
    {
        Density = 0.2f;
        var Steam0 = new Color(220, 220, 220);
        var Steam1 = new Color(200, 200, 200);
        var Steam2 = new Color(245, 245, 245);

        Random randomSteam = RandomProvider.Random;
        int numSteam = randomSteam.Next(0, 3);

        Color[] SteamColors = { Steam0, Steam1, Steam2 };

        Color = SteamColors[numSteam];
    }

 /// <summary>
 /// Actualiza el vapor: intenta subir; si no puede, se mueve en diagonal o lateralmente.
 /// </summary>
 public override void Update(GridManager.ElementAPI api, GameTime delta)
 {
  if (api.GetElement(0, 1) is Empty)
  {
   api.MoveTo(0, 1);
   return;

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

 /// <summary>
 /// El vapor no implementa interacciones activas por defecto.
 /// </summary>
 public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
 {

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
        // El vapor no interactúa con otros elementos
    }
}