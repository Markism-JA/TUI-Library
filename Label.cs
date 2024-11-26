namespace TUI;

public class Label : Widgets
{
    public string[] Content { get; set; } // Changed from Text to Content for consistency
    public ConsoleColor ForegroundColor { get; set; }
    public ConsoleColor BackgroundColor { get; set; }
    public Align Align { get; set; } = Align.Left; // Naming consistency: align -> Align

    public Label(string text, int width, int height, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, Align align = Align.Left)
    {
        Content = new[] { " " + text + " " }; // Correct array initialization
        Width = width;
        Height = height;
        ForegroundColor = foregroundColor ?? ConsoleColor.White;
        BackgroundColor = backgroundColor ?? ConsoleColor.Black;
        Align = align; // Fixed inconsistent use of 'align'
    }

    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;

        BufferAdd bufferAdd = new BufferAdd(GlobalX, GlobalY, buffer);

        switch (Align) // Changed align -> Align for consistency
        {
            case Align.Left:
                bufferAdd.LeftAlignedDraw(Content, X, Y, BackgroundColor, ForegroundColor);
                break;
            case Align.Center:
                bufferAdd.CenterAlignedDraw(Content, Width, ForegroundColor, BackgroundColor);
                break;
        }
    }
}