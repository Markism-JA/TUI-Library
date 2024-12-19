namespace TUI;

public abstract class InteractiveWidget : DisplayWidget
{
    public InteractiveWidget(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
    public abstract void HandleInput(ConsoleKey key);

    public abstract void Navigate(ConsoleKey key);
    
}
