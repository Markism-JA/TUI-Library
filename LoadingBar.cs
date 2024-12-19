namespace TUI;

public class LoadingBar : DisplayWidget
{
    private int _progress; // Current progress percentage (0-100)
    private int _duration;

    public LoadingBar(int x, int y, int width, int height, ConsoleColor? background, ConsoleColor? foreground, int duration)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        BackgroundColor = background;
        ForegroundColor = foreground;
        _progress = 0;
        _duration = duration;
    }

    public void UpdateProgress(int progress)
    {
        _progress = Math.Clamp(progress, 0, 100); // Ensure progress is within bounds
    }

    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;
        
        DrawBar(buffer);
        int steps = 100;
        int stepTime = _duration / steps;
        
        // Calculate the number of filled cells based on progress
        int filledWidth = (Width * _progress) / 100;

        for (int i = 0; i < Height; i++) // Handle multi-line progress bars if needed
        {
            for (int j = 0; j < Width; j++)
            {
                int localX = X + j;
                int localY = Y + i;
                buffer.UpdateCell(localX, localY, '\u2588', ForegroundColor, BackgroundColor, stepTime);
            }
        }
    }

    public void DrawBar(TerminalBuffer? buffer)
    {
        if (buffer == null) return;
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                int localX = X + j;
                int localY = Y + i;
                buffer.UpdateCell(localX, localY, ' ', ForegroundColor, BackgroundColor);
            }
        }
    }

    public override void RemoveFromBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                int globalX = X + j;
                int globalY = Y + i;

                // Clear the progress bar area
                buffer.UpdateCell(globalX, globalY, ' ', ParentForegroundColor, ParentBackgroundColor);
            }
        }
    }
}