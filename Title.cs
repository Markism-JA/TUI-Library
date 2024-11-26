namespace TUI;

public class Title : Widgets
{
    public string[] Content { get; private set; }
    private ConsoleColor _foregroundColor = ConsoleColor.White;
    private int _y = 0;
    private int _x = 0;
    private ConsoleColor _backgroundColor = ConsoleColor.Black;
    private Align align = Align.Left;
    
    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        BufferAdd add = new BufferAdd(GlobalX, GlobalY, buffer);
        switch (align)
        {
            case Align.Left:
                add.Draw(Content, _x, _y);
                break;
            case Align.Center:
                break;
            case Align.Right:
                break;
        }
    }

    public Title(string[] text, int y, ConsoleColor foreground, ConsoleColor background, Align align)
    {
        Content = text;
        _foregroundColor = foreground;
        _backgroundColor = background;
        _y = y;
        this.align = align;
    }
}

public enum Align
{
    Center = 1, Right = 2, Left = 3
}