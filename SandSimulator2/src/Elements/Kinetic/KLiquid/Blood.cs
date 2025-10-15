using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Blood : Element
{
    public Blood() : base(Color.Red)
    {
        var Blood0 = new Color(138, 7, 7);
        var Blood1 = new Color(165, 25, 25);
        var Blood2 = new Color(110, 0, 0);
        var Blood3 = new Color(190, 30, 30);

        Random randomBlood = RandomProvider.Random;
        int numBlood = randomBlood.Next(0, 4);

        Color[] BloodColors = { Blood0, Blood1, Blood2, Blood3 };

        Color = BloodColors[numBlood];


    }

    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        BloodPattern();
        // Si el elemento de abajo es vacío, se mueve hacia abajo
        if (api.GetElement(0, -1) is Empty)
        {
            api.MoveTo(0, -1);
            return;
        }
        // Si el elemento de abajo a la izquierda es vacío, se mueve hacia abajo a la izquierda
        if (api.GetElement(-1, -1) is Empty)
        {
            api.MoveTo(-1, -1);
            return;
        }
        // Si el elemento de abajo a la derecha es vacío, se mueve hacia abajo a la derecha
        if (api.GetElement(1, -1) is Empty)
        {
            api.MoveTo(1, -1);
            return;
        }
        // Movimiento horizontal aleatorio si hay espacio vacío
        Random rand = RandomProvider.Random;
        bool tryLeft = rand.Next(0, 2) == 0;
        var leftElement = api.GetElement(-1, 0);
        var rightElement = api.GetElement(1, 0);
        // Checa si ambos lados están vacíos para intentar moverse a cualquiera de los dos lados
        if (leftElement is Empty && rightElement is Empty)
        {
            ApplyDispertion(tryLeft, api);
            return;
        }
        // Checa si solo el lado izquierdo está vacío
        if (leftElement is Empty)
        {
            ApplyDispertion(true, api);
            return;
        }
        // Checa si solo el lado derecho está vacío
        if (rightElement is Empty)
        {
            ApplyDispertion(false, api);
            return;
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