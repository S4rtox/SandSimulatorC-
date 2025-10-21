using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

/// <summary>
/// Elemento líquido sangre, fluye hacia abajo y puede convertir agua adyacente en sangre.
/// </summary>
public class Blood : Element
{
    public Blood() : base(Color.Red)
    {
        Density = 1.1f;
        var Blood0 = new Color(138, 7, 7);
        var Blood1 = new Color(165, 25, 25);
        var Blood2 = new Color(110, 0, 0);
        var Blood3 = new Color(190, 30, 30);

        Random randomBlood = RandomProvider.Random;
        int numBlood = randomBlood.Next(0, 4);

        Color[] BloodColors = { Blood0, Blood1, Blood2, Blood3 };

        Color = BloodColors[numBlood];
    }

    /// <summary>
    /// Actualiza la sangre: cae si puede, si no se desplaza diagonalmente o lateralmente con dispersión.
    /// </summary>
    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        BloodPattern();

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
        bool tryLeft = rand.Next(0, 2) == 0;
        
        var leftElement = api.GetElement(-1, 0);
        var rightElement = api.GetElement(1, 0);

        bool canGoLeft = Density > leftElement.Density;
        bool canGoRight = Density > rightElement.Density;

        if (canGoLeft && canGoRight)
        {
            api.SwapWith(tryLeft ? -1 : 1, 0);
            return;
        }
        if (canGoLeft)
        {
            api.SwapWith(-1, 0);
            return;
        }
    }

    /// <summary>
    /// Aplica dispersión horizontal hasta el máximo permitido.
    /// </summary>
    private void ApplyDispertion(bool isLeft, GridManager.ElementAPI api)
    {
        var direction = isLeft ? -1 : 1;
        int maxDisp = MDispertion();
        for (int i = 1; i <= maxDisp; i++)
        {
            api.SwapWith(1, 0);
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
        if (j == 0) return 1;
        if (j == 1) return 2;
        if (j == 2) return 3;
        return 0;
    }

    /// <summary>
    /// Cambia ocasionalmente el color de la sangre para variación visual.
    /// </summary>
    public void BloodPattern()
    {
        var Blood0 = new Color(138, 7, 7);
        var Blood1 = new Color(165, 25, 25);
        var Blood2 = new Color(110, 0, 0);
        var Blood3 = new Color(190, 30, 30);
        Color[] BloodColors = { Blood0, Blood1, Blood2, Blood3 };
        Random rand = RandomProvider.Random;
        if (rand.NextDouble() < 0.005)
        {
            int numBlood = rand.Next(0, BloodColors.Length);
            Color = BloodColors[numBlood];
        }
    }

    /// <summary>
    /// Si hay agua adyacente, la convierte en sangre.
    /// </summary>
    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {
        var elementBelow = interactionApi.GetElement(0, -1);
        var  elementAbove = interactionApi.GetElement(0, 1);
        var elementLeft = interactionApi.GetElement(-1, 0);
        var elementRight = interactionApi.GetElement(1, 0);

        if (elementBelow is Water)
        {
            elementApi.SetElement(0,-1, new Blood());
        }
        if (elementAbove is Water)
        { 
            elementApi.SetElement(0,1, new Blood());
        }

        if (elementLeft is Water)
        {
            elementApi.SetElement(-1, 0, new Blood());
        }
        if (elementRight is Water)
        {
            elementApi.SetElement(1, 0, new Blood());
        }

    }
}