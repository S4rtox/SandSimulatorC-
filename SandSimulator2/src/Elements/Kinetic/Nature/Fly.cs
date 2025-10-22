using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;    
    
namespace SandSimulator2.Elements.Kinetic.Nature;

public class Fly : Element
{
    public Fly() : base(Color.Black)
    {
        Density = 0.01f;
    }

    public override void Update(GridManager.ElementAPI api, GameTime delta)
    {
        //Probabilidad de Descansar
        if (RandomProvider.Random.Next(0, 2) == 0)
        {
            return;
        }
        
        //Probabilidad de movimiento
        int dx = RandomProvider.Random.Next(-1, 2);
        int dy = RandomProvider.Random.Next(-1, 2);
        if (dx == 0 && dy == 0)
        {
            return;
        }
        var targetElement = api.GetElement(dx, dy);
        if (targetElement is Empty)
        {
            api.MoveTo(dx, dy);
        }
    }
}