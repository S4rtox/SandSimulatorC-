using System.Runtime.InteropServices.Marshalling;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements;

public abstract class Element
{
    public Color Color { get; protected set; }

    public virtual byte Clock { get; set; } = 0;

    protected Element(Color color)
    {
        Color = color;
    }

    public abstract void Update(GridManager.ElementAPI api, GameTime delta);

    public virtual void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }

    //Posible removal








}
