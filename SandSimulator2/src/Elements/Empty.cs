using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

/// <summary>
/// Elemento vacío (inmutable) que representa la ausencia de materia en la grilla.
/// Implementado como un <em>singleton</em>.
/// </summary>
public sealed class Empty : Element
{

    //Bonito y hermoso singleton



    private static Empty _instance;

    private static readonly object LockObject = new();

    /// <summary>
    /// El reloj del vacío siempre es 0 y no se actualiza.
    /// </summary>
    public override byte Clock
    {
        get => 0;
        set
        {

        }
    }

    /// <summary>
    /// Instancia única del elemento vacío.
    /// </summary>
    public static Empty Instance
    {
        get
        {
            if (_instance != null) return _instance;
            lock (LockObject)
            {
                _instance ??= new Empty();
            }

            return _instance;
        }
    }

    private Empty() : base(Color.Transparent)
    {
        Density = 0.0f;
    }

    /// <summary>
    /// Empty no debe actualizarse nunca; lanzar excepción si ocurre.
    /// </summary>
    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        throw new InvalidOperationException("Empty element tried to make an update operation");
    }

    /// <summary>
    /// Empty no interactúa con otros elementos.
    /// </summary>
    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}