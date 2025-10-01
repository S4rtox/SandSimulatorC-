using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SandSimulator2.Elements;
using SandSimulator2.GridManagers;

namespace SandSimulator2;

public class GraphicManager(GridManager gridManager,  int pixelSize)
{
    private Texture2D _pixelTexture;


    public void LoadContent(GraphicsDevice device)
    {
        _pixelTexture = new Texture2D(device, 1, 1);
        _pixelTexture.SetData([Color.White]);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        for (int x = 0; x < gridManager.Width; x++)
        {
            for (int y = 0; y < gridManager.Height; y++)
            {
                var element = gridManager[x, y];
                if (element is Empty) continue;
                int invertedY = (gridManager.Height - 1 - y);
                var rect = new Rectangle(x * pixelSize, invertedY * pixelSize, pixelSize, pixelSize);
                var color = element.Color;
                spriteBatch.Draw(_pixelTexture, rect, color);
            }
        }
    }


    public static (int rows, int columns) GetGridSize(GraphicsDeviceManager graphics,int pixelSize)
    {

        //var screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        var windowWidth = graphics.GraphicsDevice.Viewport.Width;
        var windowHeight = graphics.GraphicsDevice.Viewport.Height;



        var columns = windowWidth / pixelSize;
        var rows = windowHeight / pixelSize;

        return (columns, rows);

    }









}