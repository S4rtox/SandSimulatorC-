using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

/// <summary>
/// Elemento sólido madera; actualmente inerte.
/// </summary>
public class Wood : Element
{
    public Wood() : base(new Color())
    {
        Density = 0.8f;
        var Wood0 = new Color(101, 67, 33);
        var Wood1 = new Color(120, 80, 40);
        var Wood2 = new Color(140, 102, 50);
        var Wood3 = new Color(160, 120, 70);

        Random randomWood = RandomProvider.Random;
        int numWood = randomWood.Next(0, 4);

        Color[] WoodColors = { Wood0, Wood1, Wood2, Wood3 };

        Color = WoodColors[numWood];

    }

    /// <summary>
    /// La madera no cambia en el tiempo (sin actualización).
    /// </summary>
    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {

    }

    /// <summary>
    /// La madera no tiene interacciones activas.
    /// </summary>
    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}