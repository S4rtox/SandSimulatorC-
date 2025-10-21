using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

/// <summary>
/// Clase base para elementos con cinemática (velocidad). Puede no usarse por todos los elementos.
/// </summary>
public abstract class KineticElement : Element
{
    /// <summary>
    /// Velocidad actual del elemento en unidades de celdas por segundo.
    /// </summary>
    public Vector2 Velocity { get; protected set; }

    /// <summary>
    /// Inicializa un elemento cinético con el color especificado.
    /// </summary>
    protected KineticElement(Color color) : base(color) { }

    /// <summary>
    /// Maneja movimiento basado en <see cref="Velocity"/> y el tiempo transcurrido.
    /// </summary>
    protected void HandleMovement(GridManager gridManager, GameTime delta)
    {

    }

    /// <summary>
    /// Interacción por defecto para elementos cinéticos. Sobrescriba según sea necesario.
    /// </summary>
    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}