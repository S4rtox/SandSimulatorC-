using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using SandSimulator2.Controls;
using SandSimulator2.GridManagers;
using SandSimulator2.Multiplayer;
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

    private MultiplayerClient _client;
    private MultiplayerServer _server;

    private bool _isGameActive = false;
    private ElementMenu _elementMenu;

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
        _elementMenu = new ElementMenu();
        _elementMenu.AddToRoot();
        _elementMenu.HostClicked += StartHost;
        _elementMenu.JoinClicked += StartClient;

        var (x, y) = GraphicManager.GetGridSize(_graphics,PixelSize);
        _gridManager = new GridManager(x, y);
        _controllerManager = new ControllerManager(_gridManager, PixelSize);
        _graphicManager = new GraphicManager(_gridManager, PixelSize);

        _controllerManager.OnPlaceAction += OnPlaceAction;
        base.Initialize();
    }

    public void StartHost(int port)
    {
        if (_server != null || _client != null) return;
        _server = new MultiplayerServer(port, _gridManager);
        _server.StartListeningAsync();
        Console.WriteLine($"Started as server on port {port}.");
        _isGameActive = true;
        _elementMenu.Visual.Visible = false;
    }

    public void StartClient(string ip, int port)
    {
        if (_client != null || _server != null) return;
        _client = new MultiplayerClient(_gridManager);
        _client.Connect(ip, port);
        _client.StartListeningAsync();
        Console.WriteLine($"Started as client, connected to {ip}:{port}.");
        _isGameActive = true;
        _elementMenu.Visual.Visible = false;
    }

    private void OnPlaceAction(PlaceAction action)
    {
        _client?.SendActionAsync(action);
        _server?.BroadcastActionAsync(action, null);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _graphicManager.LoadContent(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime delta)
    {
        var keyboardState = Keyboard.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            keyboardState.IsKeyDown(Keys.Escape))
            Exit();



        GumUI.Update(delta);
        _gridManager.Update(delta);
        if (_isGameActive)
        {
            _controllerManager.HandleInput(delta);
        }
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

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _client?.Dispose();
            _server?.Dispose();
        }
        base.Dispose(disposing);
    }
}