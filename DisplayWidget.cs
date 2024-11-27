namespace TUI;

public abstract class DisplayWidget : Widgets
{
    public string[] Content { get; set; } // The actual rendered text
    public ConsoleColor ForegroundColor { get; set; }
    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Green;
    public Align align {get;set;}
    
    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;

        BufferUtil util = new BufferUtil(X, Y, ParentWidth, ParentHeight, buffer);

        switch (align)
        {
            case Align.Left:
                util.LeftAlignedDraw( Content, BackgroundColor, ForegroundColor);
                break;
            case Align.Center:
                util.CenterAlignedDraw(Content, ForegroundColor, BackgroundColor);
                break;
            case Align.Right:
                // Right-aligned draw logic can be added if needed.
                break;
        }
    }

}