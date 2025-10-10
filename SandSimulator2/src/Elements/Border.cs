using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public class Border : Element

{
    //Bonito y hermoso singleton

    private static Border _instance;

    private static readonly object LockObject = new();

    public override byte Clock
    {
        get => 0;
        set
        {

        }
    }

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



    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        throw new InvalidOperationException("Border element tried to make an update operation");
    }

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}