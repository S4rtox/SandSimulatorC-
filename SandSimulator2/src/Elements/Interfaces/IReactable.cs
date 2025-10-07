using Microsoft.Xna.Framework;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Elements.Interfaces;

public interface IReactable
{
    //TODO: Finish integration for reactions.
    public void CheckForReactions(Vector2I CurrentElementPosition,GridManager gridManager, GameTime delta)
    {
        var element = gridManager[CurrentElementPosition];
        if (element is Empty || !element.HasBeenUpdated) return;


    }

}