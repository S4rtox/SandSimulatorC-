using System;
using System.Collections.Generic;

namespace SandSimulator2.GridManagers
{
    /// <summary>
    /// Representa un estado serializable de la grilla, utilizado para sincronización o persistencia.
    /// </summary>
    [Serializable]
    public class GridState
    {
        /// <summary>
        /// Lista de elementos presentes en la grilla con sus posiciones y tipos.
        /// </summary>
        public List<ElementInfo> Elements { get; set; }
    }

    /// <summary>
    /// Información mínima de un elemento para reconstruirlo: posición y tipo.
    /// </summary>
    [Serializable]
    public class ElementInfo
    {
        /// <summary>
        /// Coordenada X (columna) del elemento dentro de la grilla.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Coordenada Y (fila) del elemento dentro de la grilla.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Tipo concreto del elemento (<see cref="Type"/>) para instanciar al reconstruir.
        /// </summary>
        public Type ElementType { get; set; }
    }
}
