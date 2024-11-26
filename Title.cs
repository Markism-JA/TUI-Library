namespace TUI;

public class Title : Widgets
{
    public string[] Content { get; private set; }
    private ConsoleColor _foregroundColor;
    private int _y = 0;
    private int _x = 0;
    private ConsoleColor _backgroundColor;
    private Align align = Align.Left;
    
    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;
        BufferAdd add = new BufferAdd(GlobalX, GlobalY, buffer);
        switch (align)
        {
            case Align.Left:
                add.LeftAlignedDraw(Content, _x,_y, _backgroundColor, _foregroundColor);
                break;
            case Align.Center:
                add.CenterAlignedDraw(Content,  _y, _foregroundColor, _backgroundColor);
                break;
            case Align.Right:
                break;
        }
    }

    public Title(string[] text, int y, ConsoleColor foreground, ConsoleColor background, Align align, int x = 0)
    {
        Content = text;
        _foregroundColor = foreground;
        _backgroundColor = background;
        _y = y;
        _x = x;
        this.align = align;
    }
}