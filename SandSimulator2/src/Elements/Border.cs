using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

/// <summary>
/// Elemento de borde que actúa como límite impenetrable de la grilla.
/// Implementado como un <em>singleton</em>.
/// </summary>
public class Border : Element

{
    //Bonito y hermoso singleton

    private static Border _instance;

    private static readonly object LockObject = new();

    /// <summary>
    /// El reloj del borde siempre es 0 y no se actualiza.
    /// </summary>
    public override byte Clock
    {
        get => 0;
        set
        {

        }
    }

    /// <summary>
    /// Instancia única del elemento borde.
    /// </summary>
    public static Border Instance
    {
        get
        {
            if (_instance != null) return _instance;
            lock (LockObject)
            {
                _instance ??= new Border();
            }

            return _instance;
        }
    }

    private Border() : base(Color.Transparent) { }



    /// <summary>
    /// Border no debe actualizarse; lanzar excepción si ocurre.
    /// </summary>
    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        throw new InvalidOperationException("Border element tried to make an update operation");
    }

    /// <summary>
    /// Border no interactúa con otros elementos.
    /// </summary>
    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}