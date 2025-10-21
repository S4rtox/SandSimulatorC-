using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;
using Color = Microsoft.Xna.Framework.Color;

namespace SandSimulator2.Elements.Kinetic;

/// <summary>
/// Elemento sólido granular que cae por gravedad y se apila.
/// </summary>
public class Sand : Element
{
    /// <summary>
    /// Crea una partícula de arena con un color elegido aleatoriamente dentro de una paleta.
    /// </summary>
    public Sand() : base(new Color(194, 178, 128))
    {
        //Primero tenemos los sprites de la arena:
        var Color0 = new Color(234,190,117);
        var Color1 = new Color(245,209,151);
        var Color2 = new Color(251,227,188);
        var Color3 = new Color(255,240,217);
        
        Random random = RandomProvider.Random;
        int num = random.Next(0, 3);
        
        Color[] color = {Color0,Color1, Color2, Color3 };

        Color = color[num];
    }

    /// <summary>
    /// Actualiza el comportamiento de la arena: cae y, si no puede, se desplaza diagonalmente.
    /// </summary>
    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        var belowElement = api.GetElement(0, -1);
        //Si el elemento de abajo es vacio, se mueve hacia abajo
        if (belowElement is Empty)
        {
            api.MoveTo(0, -1);
        }else if (api.GetElement(-1, -1) is Empty)
        {
            api.MoveTo(-1, -1);

        }
        else if (api.GetElement(1, -1) is Empty)
        {
            api.MoveTo(1, -1);

        }
        //Si el elemento de abajo a la izquierda es vacio, se mueve hacia abajo a la izquierda

    }

    /// <summary>
    /// La arena no implementa interacciones especiales por ahora.
    /// </summary>
    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}