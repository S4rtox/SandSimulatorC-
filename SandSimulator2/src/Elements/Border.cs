using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public class Border : Element

{
    //Bonito y hermoso singleton

    private static Border _instance;

    private static readonly object LockObject = new();

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



    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        throw new InvalidOperationException("Border element tried to make an update operation");
    }

    public override void ReactToOther(GridManager gridManager, Element element, GameTime delta)
    {
        throw new InvalidOperationException("Border element tried to react to another element");
    }

}