using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class KLiquid : Element
{

    protected int Dispertion { get; set; }
    protected int Density { get; set; }
    
    

    public KLiquid(int dispertion, int density) : base(Color.Blue)
    {
        this.Dispertion = MDispertion(dispertion);
    }


    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {

        // Si el elemento de abajo es vacío, se mueve hacia abajo
        if (api.GetElement(0, -1) is Empty)
        {
            api.SwapWith
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

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }

    // Nuevo método para dispersión usando ElementAPI
    private void ApplyDispertion(bool isLeft, GridManager.ElementAPI api)
    {
        var direction = isLeft ? -1 : 1;
        int maxDisp = Dispertion;
        for (int i = 1; i <= maxDisp; i++)
        {
            if (api.GetElement(i * direction, 0) is Empty) continue;
            api.MoveTo((i - 1) * direction, 0);
            return;
        }
        api.MoveTo(maxDisp * direction, 0);
    }
    public int MDispertion(int dispertion)
    {
        
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


}