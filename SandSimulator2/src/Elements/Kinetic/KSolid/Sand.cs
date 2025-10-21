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
        Density = 1.5f;
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
        }
    }

    /// <summary>
    /// La arena no implementa interacciones especiales por ahora.
    /// </summary>
    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}