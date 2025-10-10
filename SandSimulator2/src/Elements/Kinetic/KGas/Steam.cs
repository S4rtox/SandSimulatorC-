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

  Random randomSteam = RandomProvider.Random;
  int numSteam = randomSteam.Next(0, 3);

  Color[] SteamColors = { Steam0, Steam1, Steam2 };

  Color = SteamColors[numSteam];
 }

 public override void Update(GridManager.ElementAPI api, GameTime delta)
 {
  if (api.GetElement(0, 1) is Empty)
  {
   api.MoveTo(0, 1);
   return;

  }
  //Si el elemento de arriba a la izquierda es vacio, se mueve hacia arriba a la izquierda
  if (api.GetElement(-1, 1) is Empty)
  {
   api.MoveTo(0, 1);
   return;
  }

  //Si el elemento de arriba a la derecha es vacio, se mueve hacia arriba a la derecha
  if (api.GetElement(1, 1) is Empty)
  {
   api.MoveTo(0, 1);
   return;
  }

  //si el elemento de la izquierda esta vacio se va hacia la izquierda
  if (api.GetElement(-1, 0) is Empty)
  {
   api.MoveTo(0, 1);
   return;

  }
  //si el elemento de la derecha esta vacio
  if (api.GetElement(1, 0) is Empty)
  {
   api.MoveTo(0, 1);
   return;

  }
 }

 public override void Interact(GridManager.InteractionAPI interactionApi, GridManager.ElementAPI elementApi)
 {

 }
}