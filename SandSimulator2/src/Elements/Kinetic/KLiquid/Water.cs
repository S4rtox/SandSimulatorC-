using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

/// <summary>
/// Elemento líquido que fluye hacia abajo y se dispersa horizontalmente.
/// </summary>
public class Water : Element
{
    /// <summary>
    /// Dispersión horizontal máxima al moverse lateralmente.
    /// </summary>
    public int Dispertion { get; set; }
    
    
    public Water() : base(Color.Blue)
    {
        this.Dispertion = MDispertion();
        // Water
        var Water0 = new Color(37, 124, 196);
        var Water1 = new Color(57, 159, 225);
        var Water2 = new Color(33, 147, 212);
        var Water3 = new Color(104, 194, 243);
        var Water4 = new Color(95, 175, 218);

        Random randomWater = RandomProvider.Random;

        int numWater = randomWater.Next(0, 5);

        Color[] WaterColors = { Water0, Water1, Water2, Water3, Water4 };

        Color = WaterColors[numWater];

    }

    /// <summary>
    /// Actualiza el comportamiento del agua: cae si puede o se dispersa lateralmente.
    /// </summary>
    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        WaterPattern();
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

    /// <summary>
    /// Agua no implementa interacciones activas por defecto.
    /// </summary>
    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }

    /// <summary>
    /// Aplica la dispersión horizontal en la dirección indicada hasta el máximo permitido.
    /// </summary>
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
    /// Calcula un valor de dispersión máximo aleatorio (1..3).
    /// </summary>
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

    /// <summary>
    /// Cambia ocasionalmente el color del agua para dar variación visual.
    /// </summary>
    public void WaterPattern()
    {

        var Water0 = new Color(37, 124, 196);
        var Water1 = new Color(57, 159, 225);
        var Water2 = new Color(33, 147, 212);
        var Water3 = new Color(104, 194, 243);
        var Water4 = new Color(95, 175, 218);
        Color[] WaterColors = { Water0, Water1, Water2, Water3, Water4 };

        // Probabilidad de cambiar de color
        Random rand = RandomProvider.Random;
        if (rand.NextDouble() < 0.005)
        {
            int numWater = rand.Next(0, WaterColors.Length);
            Color = WaterColors[numWater];
        }
    }

}