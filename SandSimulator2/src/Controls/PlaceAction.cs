using System;
using Microsoft.Xna.Framework;

namespace SandSimulator2.Controls;

[Serializable]
public class PlaceAction(Vector2I position, int radius, Type elementType, bool isReplacing)
{
    public Vector2I position { get; } = position;
    public int radius { get; } = radius;
    public Type elementType { get; } = elementType;
    public bool isReplacing { get; } = isReplacing;
}

