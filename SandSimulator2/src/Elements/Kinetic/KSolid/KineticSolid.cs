using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class KineticSolid : Element
{
    public KineticSolid(Color color) : base(color)
    {
    }


    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {

    }

    public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
    {

    }
}