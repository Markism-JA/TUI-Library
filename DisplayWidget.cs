namespace TUI;

public abstract class DisplayWidget : Widgets
{
    public string[] Content { get; set; } // The actual rendered text
    public Align align {get;set;}
    
    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;
        BufferUtil util = new BufferUtil(X, Y, ParentCoordinateX, ParentCoordinateY, Width, Height, ParentWidth,
            ParentHeight, ForegroundColor, BackgroundColor, ParentForegroundColor, ParentBackgroundColor, buffer);

        switch (align)
        {
            case Align.Left:
                util.LeftAlignedDraw(Content);
                break;
            case Align.Center:
                util.CenterAlignedDraw(Content);
                break;
            case Align.Right:
                //ToBeAdded
                break;
        }
    }

    public override void RemoveFromBuffer(TerminalBuffer? buffer)
    {
        // Console.WriteLine("Removing from the buffer is being called");
        if (buffer == null) return;
        BufferUtil util = new BufferUtil(X, Y, ParentCoordinateX, ParentCoordinateY, Width, Height, ParentWidth,
            ParentHeight, ForegroundColor, BackgroundColor, ParentForegroundColor, ParentBackgroundColor, buffer);
        
        util.EraseFromBuffer(Content, align);
    }


}