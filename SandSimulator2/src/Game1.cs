using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using SandSimulator2.Controls;
using SandSimulator2.GridManagers;
using SandSimulator2.Screens;

namespace SandSimulator2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    GumService GumUI => GumService.Default;

    private SpriteBatch _spriteBatch;

    // FRAMEWORK

    private GridManager _gridManager;
    private GraphicManager _graphicManager;
    private ControllerManager _controllerManager;

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
        var gumProject = GumUI.Initialize(this,
            // This is relative to Content:
            "GumProject/Interface.gumx");
        var screen = new ElementMenu();
        screen.AddToRoot();

        var (x, y) = GraphicManager.GetGridSize(_graphics,PixelSize);
        _gridManager = new GridManager(x, y);
        _controllerManager = new ControllerManager(_gridManager, PixelSize);
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



        GumUI.Update(delta);
        _gridManager.Update(delta);
        _controllerManager.HandleInput(delta);
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
        GumUI.Draw();
        base.Draw(gameTime);
    }
}