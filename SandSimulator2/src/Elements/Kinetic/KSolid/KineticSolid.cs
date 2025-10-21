using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

/// <summary>
/// Sólido con comportamiento cinético genérico. Base para futuros sólidos móviles.
/// </summary>
public class KineticSolid : Element
{
    public KineticSolid(Color color) : base(color)
    {
    }

    /// <summary>
    /// Actualización por cuadro no implementada para el sólido cinético genérico.
    /// </summary>
    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {

    }

    /// <summary>
    /// Interacciones no implementadas para el sólido cinético genérico.
    /// </summary>
    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}