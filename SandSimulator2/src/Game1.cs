using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SandSimulator2.GridManagers;

namespace SandSimulator2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // FRAMEWORK

    private GridManager _gridManager;
    private GraphicManager _graphicManager;

    private const int PixelSize = 4;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "..\\Content";
        IsMouseVisible = true;
    }
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        var (x, y) = GraphicManager.GetGridSize(PixelSize);
        _gridManager = new GridManager(x, y);
        _graphicManager = new GraphicManager(_gridManager, PixelSize);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _graphicManager.LoadContent(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime delta)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _gridManager.Update(delta);
        // TODO: Add your update logic here

        base.Update(delta);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _graphicManager.Draw(_spriteBatch);
        // TODO: Add your drawing code here
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}