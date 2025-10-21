using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SandSimulator2.Elements;
using SandSimulator2.GridManagers;

namespace SandSimulator2;

/// <summary>
/// Responsable de cargar recursos gráficos y dibujar la grilla de elementos en pantalla.
/// </summary>
public class GraphicManager(GridManager gridManager,  int pixelSize)
{
    private Texture2D _pixelTexture;

    /// <summary>
    /// Carga el contenido gráfico necesario para el renderizado (textura de píxel).
    /// </summary>
    /// <param name="device">Dispositivo gráfico de MonoGame.</param>
    public void LoadContent(GraphicsDevice device)
    {
        _pixelTexture = new Texture2D(device, 1, 1);
        _pixelTexture.SetData([Color.White]);
    }
    /// <summary>
    /// Dibuja la grilla de elementos en el <see cref="SpriteBatch"/> proporcionado.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch activo para emitir draw calls.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        for (int x = 0; x < gridManager.Width; x++)
        {
            for (int y = 0; y < gridManager.Height; y++)
            {
                var element = gridManager.GetElement(x, y);
                if (element is Empty) continue;
                int invertedY = (gridManager.Height - 1 - y);
                var rect = new Rectangle(x * pixelSize, invertedY * pixelSize, pixelSize, pixelSize);
                var color = element.Color;
                spriteBatch.Draw(_pixelTexture, rect, color);
            }
        }
    }

    /// <summary>
    /// Calcula el tamaño de la grilla (columnas, filas) que cabe en la ventana actual.
    /// </summary>
    /// <param name="graphics">Administrador de dispositivo gráfico.</param>
    /// <param name="pixelSize">Tamaño de píxel lógico (lado de una celda en píxeles de pantalla).</param>
    /// <returns>Tupla con columnas y filas disponibles.</returns>
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