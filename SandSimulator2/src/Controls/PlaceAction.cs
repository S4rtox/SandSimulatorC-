using System;
using Microsoft.Xna.Framework;

namespace SandSimulator2.Controls;

/// <summary>
/// Representa una acción de colocación de un elemento en la grilla.
/// </summary>
/// <param name="position">Posición central en coordenadas de grilla donde aplicar la acción.</param>
/// <param name="radius">Radio (en celdas) del pincel de colocación.</param>
/// <param name="elementType">Tipo concreto de <see cref="SandSimulator2.Elements.Element"/> a colocar.</param>
/// <param name="isReplacing">Si es verdadero, reemplaza cualquier elemento existente; si es falso, solo coloca sobre celdas vacías.</param>
[Serializable]
public class PlaceAction(Vector2I position, int radius, Type elementType, bool isReplacing)
{
    /// <summary>
    /// Posición central en coordenadas de grilla donde aplicar la acción.
    /// </summary>
    public Vector2I position { get; } = position;
    /// <summary>
    /// Radio (en celdas) del pincel de colocación.
    /// </summary>
    public int radius { get; } = radius;
    /// <summary>
    /// Tipo concreto del elemento a colocar.
    /// </summary>
    public Type elementType { get; } = elementType;
    /// <summary>
    /// Si es verdadero, reemplaza cualquier elemento existente; si es falso, solo coloca sobre celdas vacías.
    /// </summary>
    public bool isReplacing { get; } = isReplacing;
}
