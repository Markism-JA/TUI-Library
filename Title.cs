namespace TUI;

public class Title : Widgets
{
    public string[] Content { get; private set; }
    private ConsoleColor _foregroundColor;
    private ConsoleColor _backgroundColor;
    private Align align = Align.Left;

    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;

        BufferAdd add = new BufferAdd(GlobalX, GlobalY, GlobalWidth, GlobalHeight, buffer);

        switch (align)
        {
            case Align.Left:
                add.LeftAlignedDraw( Content, _backgroundColor, _foregroundColor);
                break;
            case Align.Center:
                add.CenterAlignedDraw(Content, _foregroundColor, _backgroundColor);
                break;
            case Align.Right:
                // Right-aligned draw logic can be added if needed.
                break;
        }
    }

    public Title(string[] text, int y, ConsoleColor foreground, ConsoleColor background, Align align, int x = 0)
    {
        Content = text;
        _foregroundColor = foreground;
        _backgroundColor = background;
        Y = y;
        X = x;
        this.align = align;

        // Set Width to the length of the longest string in Content
        Width = text.Length > 0 ? text.Max(line => line.Length) : 0;

        // Set Height to the number of lines in Content
        Height = text.Length;
    }
}