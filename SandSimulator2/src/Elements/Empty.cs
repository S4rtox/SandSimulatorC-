using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public class Empty : Element
{
    private static Empty _instance;

    private static readonly object LockObject = new();

    public static Empty Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (LockObject)
                {
                    _instance ??= new Empty();
                }
            }

            return _instance;
        }
    }

    private Empty() : base(new Vector2I(-1, -1), Color.Yellow) { }

    public override void step(GridManager gridManager, GameTime delta) { }

    public override void reactToOther(GridManager gridManager, Element element, GameTime delta) { }


}