using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

/// <summary>
/// Clase base abstracta para todos los elementos del simulador.
/// </summary>
public abstract class Element
{
    /// <summary>
    /// Color con el que se representará el elemento en pantalla.
    /// </summary>
    public Color Color { get; protected set; }

    /// <summary>
    /// Reloj interno usado para evitar actualizaciones múltiples en el mismo ciclo de simulación.
    /// </summary>
    public virtual byte Clock { get; set; } = 0;

    /// <summary>
    /// Crea un elemento con el color especificado.
    /// </summary>
    /// <param name="color">Color inicial del elemento.</param>
    protected Element(Color color)
    {
        Color = color;
    }

    /// <summary>
    /// Lógica de actualización por paso de simulación.
    /// </summary>
    /// <param name="api">API de manipulación de celdas vecinas y movimiento.</param>
    /// <param name="delta">Tiempo transcurrido desde el último cuadro.</param>
    public abstract void Update(GridManager.ElementAPI api, GameTime delta);

    /// <summary>
    /// Lógica de interacción local con los elementos cercanos (lectura/consulta).
    /// </summary>
    /// <param name="interactionApi">API para consultar elementos circundantes con límites seguros.</param>
    /// <param name="elementApi">API de manipulación para el elemento actual.</param>
    public virtual void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }

    //Posible removal
}
