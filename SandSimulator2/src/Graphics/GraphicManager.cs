using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SandSimulator2.Elements;
using SandSimulator2.GridManagers;

namespace SandSimulator2;

public class GraphicManager
{
    private Texture2D _pixelTexture;
    private GridManager _gridManager;
    private int _pixelSize;

    public GraphicManager(GridManager gridManager, int pixelSize)
    {

        _gridManager = gridManager;
        _pixelSize = pixelSize;

    }

    public void LoadContent(GraphicsDevice device)
    {
        _pixelTexture = new Texture2D(device, 1, 1);
        _pixelTexture.SetData([Color.White]);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var element in _gridManager.Grid)
        {
            if (element is Empty) continue;
            var rect = new Rectangle(element.Position.X * _pixelSize, element.Position.Y * _pixelSize, _pixelSize, _pixelSize);
            var color = element.Color;
            spriteBatch.Draw(_pixelTexture, rect, color);
        }
    }


    public static (int columns, int rows) GetGridSize(int pixelSize)
    {
        var screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        var screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        var columns = screenWidth / pixelSize;
        var rows = screenHeight / pixelSize;

        return (columns, rows);

    }







}