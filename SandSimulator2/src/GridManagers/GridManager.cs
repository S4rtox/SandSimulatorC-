using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SandSimulator2.Controls;
using SandSimulator2.Elements;
using Vector2 = System.Numerics.Vector2;

namespace SandSimulator2.GridManagers;

/// <summary>
/// Administra la grilla de elementos, su simulación por pasos y la cola de acciones de usuario.
/// </summary>
public class GridManager
{
    private readonly Element[,] _grid;

    /// <summary>
    /// Ancho de la grilla en celdas.
    /// </summary>
    public int Width { get; }
    /// <summary>
    /// Alto de la grilla en celdas.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Contador de generación de la simulación. Aumenta en cada <see cref="Update"/>.
    /// </summary>
    public byte Generation { get; private set; } = 0;

    private readonly ConcurrentQueue<PlaceAction> _actionQueue = new();

    // Indexers - Adoro C# - No tocar si no sabes que pedo porfas :)
    private Element this[int x, int y]
    {
        get => IsInBounds(x, y) ? _grid[x, y] : Border.Instance;

        set
        {
            if (!IsInBounds(x, y)) return;
            _grid[x, y] = value;

        }
    }
    private Element this[Vector2I vector2I]
    {
        get => IsInBounds(vector2I) ? _grid[vector2I.X, vector2I.Y] : Empty.Instance;
        set
        {
            if (!IsInBounds(vector2I)) return;
            _grid[vector2I.X, vector2I.Y] = value;
        }
    }


    /// <summary>
    /// Crea una grilla vacía del tamaño especificado.
    /// </summary>
    /// <param name="width">Ancho en celdas.</param>
    /// <param name="height">Alto en celdas.</param>
    public GridManager(int width, int height)
    {
        Width = width;
        Height = height;
        _grid = new Element[width, height];

        //fill both grids with empty elements
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _grid[x, y] = Empty.Instance;
            }
        }
    }



    /// <summary>
    /// Limpia la grilla, estableciendo todas las celdas a <see cref="Empty"/> y reinicia la generación.
    /// </summary>
    public void Clear()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _grid[x, y] = Empty.Instance;
            }
        }
        Generation = 0;
    }

    /// <summary>
    /// Avanza la simulación un paso, procesando la cola de acciones y actualizando elementos.
    /// </summary>
    /// <param name="delta">Tiempo transcurrido desde el último cuadro.</param>
    public void Update(GameTime delta)
    {
        while (_actionQueue.TryDequeue(out var action))
        {
            HandlePlaceAction(action);
        }
        Generation++;

        for (var y = Height - 1; y >= 0; y--)
        {
            for (var x = 0; x < Width; x++)
            {
                var element = _grid[x, y];
                if (element is Empty) continue;
                if (element.Clock == (byte)(Generation+1)) continue;

                element.Interact(new InteractionAPI(x, y, this), new ElementAPI(x, y, this));
            }
        }

        for (var y = Height - 1; y >= 0; y--)
        {
            for (var x = 0; x < Width; x++)
            {
                var element = _grid[x, y];
                UpdateCell(element, new ElementAPI(x, y, this), delta);
            }
        }
    }

    private void UpdateCell(Element element,ElementAPI api, GameTime delta)
    {
        if (element is Empty || element.Clock == (byte)(Generation+1)) return;
        element.Update( api, delta);
    }

    /// <summary>
    /// Indica si una posición está dentro de los límites de la grilla.
    /// </summary>
    /// <param name="x">Coordenada X.</param>
    /// <param name="y">Coordenada Y.</param>
    /// <returns><c>true</c> si la posición es válida; en caso contrario, <c>false</c>.</returns>
    public bool IsInBounds(int x, int y)
    {
        return !(x < 0 || x >= Width || y < 0 || y >= Height);
    }

    /// <summary>
    /// Indica si una posición está dentro de los límites de la grilla.
    /// </summary>
    /// <param name="position">Posición como <see cref="Vector2I"/>.</param>
    /// <returns><c>true</c> si la posición es válida; en caso contrario, <c>false</c>.</returns>
    public bool IsInBounds(Vector2I position)
    {
        return IsInBounds(position.X, position.Y);
    }



    /// <summary>
    /// API de solo lectura para consultar elementos cercanos con desplazamientos limitados.
    /// </summary>
    /// <param name="x">Coordenada X del elemento actual.</param>
    /// <param name="y">Coordenada Y del elemento actual.</param>
    /// <param name="gridManager">Referencia a la grilla.</param>
    public readonly ref struct InteractionAPI(int x, int y, GridManager gridManager)
    {
        /// <summary>
        /// Obtiene el elemento en un desplazamiento relativo. Los valores deben estar entre -2 y 2.
        /// </summary>
        /// <param name="offsetX">Desplazamiento en X (-2..2).</param>
        /// <param name="offsetY">Desplazamiento en Y (-2..2).</param>
        /// <returns>Elemento en la posición relativa o borde si está fuera.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Si el desplazamiento está fuera del rango permitido.</exception>
        public Element GetElement(int offsetX, int offsetY)
        {
            if(offsetX > 2 || offsetX < -2 || offsetY > 2 || offsetY < -2)
                throw new ArgumentOutOfRangeException("Offset values for interactions must be between -2 and 2");
            var targetX = x + offsetX;
            var targetY = y + offsetY;
            return gridManager[targetX, targetY] ;
        }

    }

    /// <summary>
    /// API de manipulación para mover/intercambiar/colocar elementos relativos a la celda actual.
    /// </summary>
    /// <param name="x">Coordenada X del elemento actual.</param>
    /// <param name="y">Coordenada Y del elemento actual.</param>
    /// <param name="gridManager">Referencia a la grilla.</param>
    public readonly ref struct ElementAPI(int x, int y, GridManager gridManager)
    {
        /// <summary>
        /// Obtiene el elemento en la posición relativa indicada.
        /// </summary>
        public Element GetElement(int offsetX, int offsetY)
        {
            var targetX = x + offsetX;
            var targetY = y + offsetY;
            return gridManager[targetX, targetY] ;
        }

        /// <summary>
        /// Intercambia el elemento actual con el de la posición relativa indicada.
        /// </summary>
        public void SwapWith(int offsetX, int offsetY)
        {
            var targetX = x + offsetX;
            var targetY = y + offsetY;
            if (!gridManager.IsInBounds(targetX, targetY)) return;

            (gridManager[targetX, targetY], gridManager[x, y]) = (gridManager[x, y], gridManager[targetX, targetY]);

            gridManager[targetX, targetY].Clock = (byte)(gridManager.Generation + 1);
            gridManager[x, y].Clock = (byte)(gridManager.Generation + 1);
        }

        /// <summary>
        /// Mueve el elemento actual a la posición relativa indicada, dejando vacío el origen.
        /// </summary>
        public void MoveTo(int offsetX, int offsetY)
        {
            var targetX = x + offsetX;
            var targetY = y + offsetY;
            if (!gridManager.IsInBounds(targetX, targetY)) return;

            gridManager[targetX, targetY] = gridManager[x, y];
            gridManager[x, y] = Empty.Instance;

            gridManager[targetX, targetY].Clock = (byte)(gridManager.Generation + 1);
        }

        /// <summary>
        /// Establece un nuevo elemento en la posición relativa indicada.
        /// </summary>
        public void SetElement(int offsetX, int offsetY, Element element)
        {
            var targetX = x + offsetX;
            var targetY = y + offsetY;
            if (!gridManager.IsInBounds(targetX, targetY)) return;

            gridManager[targetX, targetY] = element;
            element.Clock = (byte)(gridManager.Generation + 1);



        }




    }

    /// <summary>
    /// Obtiene el elemento en las coordenadas indicadas.
    /// </summary>
    public Element GetElement(int x, int y)
    {
        return this[x, y];
    }

    /// <summary>
    /// Establece el elemento en las coordenadas indicadas.
    /// </summary>
    public void SetElement(int x, int y, Element element)
    {
        this[x, y] = element;
    }

    /// <summary>
    /// Encola una acción de colocación para aplicarla en el siguiente ciclo de simulación.
    /// </summary>
    public void EnqueueAction(PlaceAction action)
    {
        _actionQueue.Enqueue(action);
    }

    /// <summary>
    /// Construye un estado serializable con los elementos no vacíos presentes en la grilla.
    /// </summary>
    public GridState GetGridState()
    {
        var elementInfos = new List<ElementInfo>();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var element = _grid[x, y];
                if (element is not Empty)
                {
                    elementInfos.Add(new ElementInfo { X = x, Y = y, ElementType = element.GetType() });
                }
            }
        }
        return new GridState { Elements = elementInfos };
    }

    /// <summary>
    /// Reemplaza el contenido de la grilla con el estado proporcionado.
    /// </summary>
    public void SetGridState(GridState gridState)
    {
        Clear();
        foreach (var elementInfo in gridState.Elements)
        {
            var newElement = elementInfo.ElementType == typeof(Empty)
                ? Empty.Instance
                : (Element)Activator.CreateInstance(elementInfo.ElementType);
            SetElement(elementInfo.X, elementInfo.Y, newElement);
        }
    }

    private void HandlePlaceAction(PlaceAction action)
    {
        for (var x = -action.radius; x <= action.radius; x++)
        {
            for (var y = -action.radius; y <= action.radius; y++)
            {
                var offset = new Vector2I(x, y);
                var targetPosition = action.position + offset;

                //Para que sea un circulito :)
                if (Vector2.Distance(action.position, action.position + offset) > action.radius) continue;
                if (!IsInBounds(targetPosition)) continue;

                // Si estamos remplazando
                if(!action.isReplacing && GetElement(targetPosition.X, targetPosition.Y) is not Empty) continue;

                var newElement = action.elementType == typeof(Empty)
                    ? Empty.Instance
                    : (Element)Activator.CreateInstance(action.elementType);

                SetElement(targetPosition.X, targetPosition.Y, newElement);
                newElement!.Clock = (byte)(Generation + 1); // Evita doble actualización en el mismo ciclo
            }
        }
    }
}