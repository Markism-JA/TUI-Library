namespace TUI;

public abstract class InteractiveWidget : DisplayWidget
{
    public bool IsFocused { get; set; }
    public InteractiveWidget(int x, int y, int width, int height)
    {
        
    }
    public abstract void HandleInput(ConsoleKey key);

    public abstract void Navigate(ConsoleKey key);

    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        if (IsFocused)
        {
            BackgroundColor = ConsoleColor.Yellow;
        }
        base.AddToBuffer(buffer);
    }

}