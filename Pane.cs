namespace TUI;

public class Pane : DisplayWidget
{
    public bool BorderOn { get; set; } = true;
    public char BorderHorizontal { get; set; } = '-';
    public char BorderVertical { get; set; } = '|';
    public ConsoleColor? BorderBackgroundColor { get; set; }
    public ConsoleColor? BorderForegroundColor { get; set; }

    public Pane(int x, int y, int width, int height, ConsoleColor? background)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        BackgroundColor = background;
    }

    private void AddBorder(TerminalBuffer? buffer)
    {
        if (buffer == null || !BorderOn) return;

        for (int x = 0; x < Width; x++)
        {
            buffer.UpdateCell(X + x, Y, BorderHorizontal, BorderForegroundColor, BorderBackgroundColor); // Top border
            buffer.UpdateCell(X + x, Y + Height - 1, BorderHorizontal, BorderForegroundColor, BorderBackgroundColor); // Bottom border
        }

        for (int y = 1; y < Height - 1; y++)
        {
            buffer.UpdateCell(X, Y + y, BorderVertical, BorderForegroundColor, BorderBackgroundColor); // Left border
            buffer.UpdateCell(X + Width - 1, Y + y, BorderVertical, BorderForegroundColor, BorderBackgroundColor); // Right border
        }
    }

    private void FillWindow(TerminalBuffer? buffer)
    {
        if (buffer == null) return;

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                buffer.UpdateCell(X + i, Y + j, ' ', ForegroundColor, BackgroundColor); // Adjusted for pane position
            }
        }
    }

    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;

        FillWindow(buffer);
        AddBorder(buffer);
    }

    public override void RemoveFromBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;
        
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                buffer.UpdateCell(X + i, Y + j, ' ', ForegroundColor, ParentBackgroundColor); // Adjusted for pane position
            }
        }
    }
}
