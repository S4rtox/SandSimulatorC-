using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Sand : Element
{

    public Vector2I Position { get; set; }

    public Sand(Vector2I position, Color color) : base(position, color)
    {
        this.Position = position;
    }


    //Metodo que se ejecuta por cada frame, en cada elemento

    public override void step(GridManager gridManager, GameTime delta)
    {
        //Elemento x ?? Si es el elemento vacio
        if (gridManager.getElementAt(Position.X, Position.Y - 1) is Empty)
        {
            gridManager.moveElementTo(this, Position.X, Position.Y-1);
        }else if (gridManager.getElementAt(Position.X - 1, Position.Y - 1) is Empty)
        {
            gridManager.moveElementTo(this, Position.X -1, Position.Y-1);
        }else if (gridManager.getElementAt(Position.X + 1, Position.Y - 1) is Empty)
        {
            gridManager.moveElementTo(this, Position.X +1, Position.Y-1);
        }

    }
}