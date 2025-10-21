using System;

namespace Microsoft.Xna.Framework;

/// <summary>
/// Proveedor compartido de números aleatorios para todo el proyecto.
/// </summary>
public static class RandomProvider
{
    /// <summary>
    /// Instancia global de <see cref="System.Random"/> segura para reutilizar.
    /// </summary>
    public static readonly Random Random = new Random();


}