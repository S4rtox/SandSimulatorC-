using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public sealed class Empty : Element
{

    //Bonito y hermoso singleton

    private static Empty _instance;

    private static readonly object LockObject = new();

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



    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        throw new InvalidOperationException("Empty element tried to make an update operation");
    }

    public override void ReactToOther(GridManager gridManager, Element element, GameTime delta)
    {
        throw new InvalidOperationException("Empty element tried to react to another element");
    }


}