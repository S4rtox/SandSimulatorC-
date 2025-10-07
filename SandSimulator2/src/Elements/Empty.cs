using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public sealed class Empty : Element
{

    //Bonito y hermoso singleton



    private static Empty _instance;

    private static readonly object LockObject = new();

    private readonly Vector2I _nullPosition = new(-1, -1);
    public override Vector2I Position
    {
        get => _nullPosition;
        set => throw new InvalidOperationException("Cannot set position of Empty element");
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



    public override void Update( GridManager gridManager, GameTime delta)
    {
        throw new InvalidOperationException("Empty element tried to make an update operation");
    }

}