using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Fire : Element
{
    public Fire() : base(new Color(20, 20, 20))
    {
        var Fire0 = new Color(243, 123, 46);
        var Fire1 = new Color(255, 240, 93);
        var Fire2 = new Color(214, 66, 16);
        var Fire3 = new Color(255, 187, 69);
        var Fire4 = new Color(206, 58, 12);
        var Fire5 = new Color(255, 237, 100);

        Random randomFire = RandomProvider.Random;

        Color[] FireColors = { Fire0, Fire1, Fire2, Fire3, Fire4, Fire5 };
        int numFire = randomFire.Next(0, FireColors.Length);
        Color = FireColors[numFire];
    }

    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        Random rand = RandomProvider.Random;

        // Probabilidad de moverse hacia abajo (como una chispa/ascua que cae)
        if (rand.Next(0, 20) == 0) // 1 de 20 probabilidades
        {
            // Priorizar el movimiento diagonal hacia abajo al azar
            int side = rand.Next(0, 2) == 0 ? -1 : 1; 
            if (api.GetElement(side, -1) is Empty)
            {
                api.MoveTo(side, -1);
                return;
            }
            // Probar la otra diagonal
            if (api.GetElement(-side, -1) is Empty)
            {
                api.MoveTo(-side, -1);
                return;
            }
            // Probar hacia abajo
            if (api.GetElement(0, -1) is Empty)
            {
                api.MoveTo(0, -1);
                return;
            }
        }

        // Si el elemento de arriba es vacío, se mueve hacia arriba
        if (api.GetElement(0, 1) is Empty)
        {
            api.MoveTo(0, 1);
            return;
        }
        // Si el elemento de arriba a la izquierda es vacío, se mueve hacia arriba a la izquierda
        if (api.GetElement(-1, 1) is Empty)
        {
            api.MoveTo(-1, 1);
            return;
        }
        // Si el elemento de arriba a la derecha es vacío, se mueve hacia arriba a la derecha
        if (api.GetElement(1, 1) is Empty)
        {
            api.MoveTo(1, 1);
            return;
        }
        // Movimiento horizontal aleatorio si hay espacio vacío
        bool tryLeft = rand.Next(0, 2) == 0; // 0 = izquierda, 1 = derecha
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

        if (j == 0)
        {
            return 1;
        }
        if (j  == 1)
        {
            return 2;
        }
        if (j == 2)
        {
            return 3;
        }
        return 0;
    }

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}