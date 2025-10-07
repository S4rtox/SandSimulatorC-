using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SandSimulator2.Elements;
using SandSimulator2.Elements.Kinetic;
using SandSimulator2.GridManagers;
using Vector2 = System.Numerics.Vector2;

namespace SandSimulator2.Controls;

public class ControllerManager
{

    private GridManager _gridManager;
    private readonly int _pixelSize;
    private bool _isReplacing = false;
    private bool _clickedBefore = false;

    private int _scrollWheelValue = 0;


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

    public int Radius { get; set; } = 5;

    public ControllerManager(GridManager gridManager, int pixelSize)
    {
        _gridManager = gridManager;
        _pixelSize = pixelSize;
    }

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
            Draw(mousePosition, _isReplacing);

        }else if(mouseState.LeftButton == ButtonState.Released)
        {
        }


        if(mouseState.RightButton == ButtonState.Pressed)
        {
            var mousePosition = getGridRelativePosition(mouseState.X, mouseState.Y, _gridManager);
            Draw(mousePosition, true, Empty.Instance);
        }else if (mouseState.RightButton == ButtonState.Released)
        {

        }


    }

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


    private void Draw(Vector2I CenterPosition,bool isReplacing = false, Element element = null )
    {
        for (var x = -Radius; x <= Radius; x++)
        {
            for (var y = -Radius; y <= Radius; y++)
            {
                var offset = new Vector2I(x, y);
                var targetPosition = CenterPosition + offset;

                //Para que sea un circulito :)

                if (Vector2.Distance(CenterPosition, CenterPosition + offset) > Radius) continue;
                if (!_gridManager.IsInBounds(targetPosition)) continue;

                // Si estamos remplazando
                if(!isReplacing && _gridManager[x+CenterPosition.X,CenterPosition.Y+y] is not Empty) continue;

                _gridManager[targetPosition] = element?? (Element)Activator.CreateInstance(SelectedElementType);
            }

        }
    }

}