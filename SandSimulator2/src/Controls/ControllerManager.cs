using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SandSimulator2.Elements;
using SandSimulator2.Elements.Kinetic;
using SandSimulator2.GridManagers;

namespace SandSimulator2.Controls;

/// <summary>
/// Gestiona la entrada del usuario (teclado/ratón) y la traduce en acciones sobre la grilla.
/// </summary>
public class ControllerManager
{

    private GridManager _gridManager;
    private readonly int _pixelSize;
    private bool _isReplacing = false;
    private bool _clickedBefore = false;
    private bool middleClickedBefore = false;


    /// <summary>
    /// Evento que se dispara cuando el usuario realiza una acción de colocación.
    /// </summary>
    public Action<PlaceAction> OnPlaceAction;

    private bool isJClickedBefore = false;
    private bool isGClickedBefore = false;
    private int _scrollWheelValue = 0;


    /// <summary>
    /// Tipo de elemento seleccionado actualmente para colocar.
    /// </summary>
    /// <exception cref="ArgumentException">Si el tipo no hereda de <see cref="Element"/>.</exception>
    public Type SelectedElementType
    {
        get => _selectedElementType;
        set
        {
            if (!typeof(Element).IsAssignableFrom(value))
                throw new ArgumentException("SelectedElementType must be a subclass of Element.");
            _selectedElementType = value;
        }
    }
    private Type _selectedElementType = typeof(Sand);

    /// <summary>
    /// Radio del pincel de colocación en celdas.
    /// </summary>
    public int Radius { get; set; } = 5;

    /// <summary>
    /// Crea un nuevo administrador de control para la grilla indicada.
    /// </summary>
    /// <param name="gridManager">Administrador de grilla objetivo.</param>
    /// <param name="pixelSize">Tamaño del pixel lógico (escala de celda a pantalla).</param>
    public ControllerManager(GridManager gridManager, int pixelSize)
    {
        _gridManager = gridManager;
        _pixelSize = pixelSize;
    }

    /// <summary>
    /// Procesa la entrada de usuario para este cuadro.
    /// </summary>
    public void HandleInput(GameTime time)
    {
        HandleKeyboard();
        HandleMouse();
        HandleScrollWheel();
    }

    private void HandleKeyboard()
    {
        var keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
        {
            _isReplacing = true;
        }
        else
        {
            _isReplacing = false;
        }

        if (keyboardState.IsKeyDown(Keys.J) && !isJClickedBefore)
        {
            isJClickedBefore = true;
            // Logic to start as client will be in Game1.cs
        }
        else if (keyboardState.IsKeyUp(Keys.J) && isJClickedBefore)
        {
            isJClickedBefore = false;
        }

        if (keyboardState.IsKeyDown(Keys.G) && !isGClickedBefore)
        {
            isGClickedBefore = true;
            // Logic to start as server will be in Game1.cs
        }
        else if (keyboardState.IsKeyUp(Keys.G) && isGClickedBefore)
        {
            isGClickedBefore = false;
        }


        // Cambiar el tipo de elemento con los numeros
        if (keyboardState.IsKeyDown(Keys.D1))
        {
            SelectedElementType = typeof(Sand);
            Console.WriteLine("Changed to sand");
        }
        else if (keyboardState.IsKeyDown(Keys.D2))
        {
            SelectedElementType = typeof(Stone);
            Console.WriteLine("Changed to stone");
        }else if (keyboardState.IsKeyDown(Keys.D3))
        {
            SelectedElementType = typeof(Water);
            Console.WriteLine("Changed to water");
        }else if (keyboardState.IsKeyDown(Keys.D4))
        {
            SelectedElementType = typeof(Dirt);
            Console.WriteLine("Changed to dirt");
        }else if (keyboardState.IsKeyDown(Keys.D5))
        {
            SelectedElementType = typeof(Smoke);
            Console.WriteLine("Changed to steam");
        }else if (keyboardState.IsKeyDown(Keys.D6))
        {
            SelectedElementType = typeof(Steam);
            Console.WriteLine("Changed to Steam");
        }else if (keyboardState.IsKeyDown(Keys.D7))
        {
            SelectedElementType = typeof(Water);
            Console.WriteLine("Changed to water");
        }else if (keyboardState.IsKeyDown(Keys.D8))
        {
            SelectedElementType = typeof(Flesh);
            Console.WriteLine("Changed to Flesh");
        }else if (keyboardState.IsKeyDown(Keys.D9))
        {
            SelectedElementType = typeof(Wood);
            Console.WriteLine("Changed to Wood");
        }else if (keyboardState.IsKeyDown(Keys.D0))
        {
            SelectedElementType = typeof(Blood);
            Console.WriteLine("Changed to Blood");
        }
        

    }
    private void HandleMouse()
    {
        var mouseState = Mouse.GetState();
        if(mouseState.LeftButton == ButtonState.Pressed)
        {
            var mousePosition = getGridRelativePosition(mouseState.X, mouseState.Y, _gridManager);
            var action = new PlaceAction(mousePosition, Radius, SelectedElementType, _isReplacing);
            _gridManager.EnqueueAction(action);
            OnPlaceAction?.Invoke(action);

        }else if(mouseState.LeftButton == ButtonState.Released)
        {
        }


        if(mouseState.RightButton == ButtonState.Pressed)
        {
            var mousePosition = getGridRelativePosition(mouseState.X, mouseState.Y, _gridManager);
            var action = new PlaceAction(mousePosition, Radius, typeof(Empty), true);
            _gridManager.EnqueueAction(action);
            OnPlaceAction?.Invoke(action);
        }else if (mouseState.RightButton == ButtonState.Released)
        {

        }

        //Clear on middle
        if(mouseState.MiddleButton == ButtonState.Pressed && !middleClickedBefore)
        {
            _gridManager.Clear();
            middleClickedBefore = true;
        }else if(mouseState.MiddleButton == ButtonState.Released && middleClickedBefore)
        {
            middleClickedBefore = false;
        }


    }

    /// <summary>
    /// Convierte coordenadas de pantalla a coordenadas de grilla.
    /// </summary>
    private Vector2I getGridRelativePosition(int mouseX, int mouseY, GridManager _gridManager)
    {
        int gridX = mouseX / _pixelSize;
        int gridY = (_gridManager.Height - 1) - (mouseY / _pixelSize);
        return new Vector2I(gridX, gridY);
    }

    private void HandleScrollWheel()
    {
        var mouseState = Mouse.GetState();
        int delta = mouseState.ScrollWheelValue - _scrollWheelValue;
        if (delta > 0)
        {
            Radius++;
        }
        else if (delta < 0)
        {
            Radius = Math.Max(0, Radius - 1);
        }
        _scrollWheelValue = mouseState.ScrollWheelValue;
    }



}