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

        BufferAdd add = new BufferAdd(GlobalX, GlobalY, GlobalWidth, GlobalHeight, buffer);

        switch (align)
        {
            case Align.Left:
                add.LeftAlignedDraw( Content, BackgroundColor, ForegroundColor);
                break;
            case Align.Center:
                add.CenterAlignedDraw(Content, ForegroundColor, BackgroundColor);
                break;
            case Align.Right:
                // Right-aligned draw logic can be added if needed.
                break;
        }
    }

}