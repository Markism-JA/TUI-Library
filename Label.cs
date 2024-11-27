namespace TUI;

public class Label : Widgets
{
    public string[] Content { get; set; } // The actual rendered text
    public ConsoleColor ForegroundColor { get; set; }
    public ConsoleColor BackgroundColor { get; set; }
    public Align Align { get; set; } = Align.Left; // Default alignment: Left

    public Label(string text, int x, int y, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, Align align = Align.Left)
    {
        // Add spaces for padding around the text
        Content = new[] { " " + text + " " };

        Y = y;

        // Calculate the Width as the length of the longest string in Content
        Width = Content.Max(line => line.Length);

        // Set Height as the number of lines in Content
        Height = Content.Length;

        // Set default colors if not provided
        ForegroundColor = foregroundColor ?? ConsoleColor.White;
        BackgroundColor = backgroundColor ?? ConsoleColor.Black;

        Align = align;
    }

    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;

        // Create a BufferAdd instance
        BufferAdd bufferAdd = new BufferAdd(GlobalX, GlobalY, GlobalWidth, GlobalHeight, buffer);
        //Console.WriteLine($"this is x {bufferAdd.GlobalX}, and y {bufferAdd.GlobalY}, and width {bufferAdd.GlobalWidth}, and height {bufferAdd.GlobalHeight} ");
        // Render content based on alignment
        switch (Align)
        {
            case Align.Left:
                bufferAdd.LeftAlignedDraw(Content, BackgroundColor, ForegroundColor);
                break;

            case Align.Center:
                bufferAdd.CenterAlignedDraw(Content, ForegroundColor, BackgroundColor);
                break;

            // Add Right alignment logic if needed
        }
    }
}