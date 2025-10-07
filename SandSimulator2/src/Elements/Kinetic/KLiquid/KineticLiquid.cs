using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public abstract class KineticLiquid : Element, IKinetic
{
    private static readonly Random rand = new Random();
    
    public abstract float Density { get; }

    protected KineticLiquid(Color color) : base(color)
    {
    }

    public override void Update(Vector2I position, GridManager gridManager, GameTime delta)
    {
        // CAER RECTO 

        if (gridManager[position.X, position.Y - 1] is Empty)
        {
            gridManager[position.X, position.Y - 1] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }

        //  ESPARCIRSE HORIZONTALMENTE ANTES QUE DIAGONAL
        bool isLeftEmpty = gridManager[position.X - 1, position.Y] is Empty;
        bool isRightEmpty = gridManager[position.X + 1, position.Y] is Empty;

        if (isLeftEmpty && isRightEmpty)
        {
            int randomDirection = (rand.Next(0, 2) == 0) ? -1 : 1;
            gridManager[position.X + randomDirection, position.Y] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }
        else if (isLeftEmpty)
        {
            gridManager[position.X - 1, position.Y] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }
        else if (isRightEmpty)
        {
            gridManager[position.X + 1, position.Y] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }
        
        for (int i = 2; i <= 4; i++)
        {
            // (Lógica de dispersión amplia)
        }
        
        // CAER EN DIAGONAL
        if (gridManager[position.X - 1, position.Y - 1] is Empty)
        {
            gridManager[position.X - 1, position.Y - 1] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }
        if (gridManager[position.X + 1, position.Y - 1] is Empty)
        {
            gridManager[position.X + 1, position.Y - 1] = this;
            gridManager[position.X, position.Y] = Empty.Instance;
            return;
        }
    }
}