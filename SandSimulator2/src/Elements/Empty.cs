using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public sealed class Empty : Element
{

    //Bonito y hermoso singleton



    private static Empty _instance;

    private static readonly object LockObject = new();

    public override byte Clock
    {
        get => 0;
        set
        {

        }
    }


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

    private Empty() : base(Color.Transparent) { }

    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        throw new InvalidOperationException("Empty element tried to make an update operation");
    }

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}