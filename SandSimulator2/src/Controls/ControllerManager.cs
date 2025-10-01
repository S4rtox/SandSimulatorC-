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
         handleScrollWheel();

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
        }

    }
    private void HandleMouse()
    {
        var mouseState = Mouse.GetState();
        if(mouseState.LeftButton == ButtonState.Pressed)
        {
            var mousePosition = getGridRelativePosition(mouseState.X, mouseState.Y, _gridManager);
            Draw(mousePosition, _isReplacing);
            _clickedBefore = true;
        }else if(mouseState.LeftButton == ButtonState.Released)
        {
            _clickedBefore = false;
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

    private void handleScrollWheel()
    {
        var mouseState = Mouse.GetState();
        int delta = mouseState.ScrollWheelValue - _scrollWheelValue;
        if (delta > 0)
        {
            Radius++;
        }
        else if (delta < 0)
        {
            Radius = Math.Max(1, Radius - 1);
        }
        _scrollWheelValue = mouseState.ScrollWheelValue;
    }


    private void Draw(Vector2I CenterPosition,bool isReplacing = false, Element element = null )
    {
        for (var x = -Radius; x < Radius; x++)
        {
            for (var y = -Radius; y < Radius; y++)
            {
                var offset = new Vector2I(x, y);
                var targetPosition = CenterPosition + offset;
                //Para que sea un circulito :)
                if (Vector2.Distance(CenterPosition, CenterPosition + offset) > Radius) continue;
                if (!_gridManager.IsInBounds(targetPosition)) continue;
                // Si estamos remplazando
                if(!isReplacing && _gridManager[x,y] is not Empty) continue;

                _gridManager[targetPosition] = element?? (Element)Activator.CreateInstance(SelectedElementType);
            }

        }
    }

}