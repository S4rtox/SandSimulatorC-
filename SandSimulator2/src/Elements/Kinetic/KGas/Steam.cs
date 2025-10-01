using System;
using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Kinetic;

public class Steam : Element
{
 public Steam() : base(new Color(20, 20, 20))
 {
  var Steam0 = new Color(220, 220, 220);
  var Steam1 = new Color(200, 200, 200);
  var Steam2 = new Color(245, 245, 245);

  Random randomSteam = new Random();
  int numSteam = randomSteam.Next(0, 3);

  Color[] SteamColors = { Steam0, Steam1, Steam2 };

  Color = SteamColors[numSteam];
 }
 public override void Update(Vector2I position, GridManager gridManager, GameTime delta){
  //Si el elemento de abajo es vacio, se mueve hacia abajo
  if (gridManager[position.X, position.Y + 1] is Empty)
  {
   gridManager[position.X, position.Y + 1] = this;
   gridManager[position.X, position.Y] = Empty.Instance;
   return;

  }
  //Si el elemento de abajo a la izquierda es vacio, se mueve hacia abajo a la izquierda
  if (gridManager[position.X - 1, position.Y + 1] is Empty)
  {
   gridManager[position.X - 1, position.Y + 1] = this;
   gridManager[position.X, position.Y] = Empty.Instance;
   return;
  }

  //Si el elemento de abajo a la derecha es vacio, se mueve hacia abajo a la derecha
  if (gridManager[position.X + 1, position.Y + 1] is Empty)
  {
   gridManager[position.X + 1, position.Y + 1] = this;
   gridManager[position.X, position.Y] = Empty.Instance;
   return;
  }

  //si el elemento de la izquierda esta vacio se va hacia la izquierda
  if (gridManager[position.X - 1, position.Y] is Empty)
  {
   gridManager[position.X - 1, position.Y] = this;
   gridManager[position.X, position.Y] = Empty.Instance;
   return;

  }
  //si el elemento de la derecha esta vacio 
  if (gridManager[position.X + 1, position.Y] is Empty)
  {
   gridManager[position.X + 1, position.Y] = this;
   gridManager[position.X, position.Y] = Empty.Instance;
   return;

  }
 }
}