namespace TUI;

public class Typewritter : DisplayWidget
{
    private int _spacing = 0;
    private int _delay;
    
    
    public Typewritter(string[] content, int x, int y, int height, int width, int delay, ConsoleColor? foreground, ConsoleColor? background)
    {
        Content = content;
        X = x;
        Y = y;
        ForegroundColor = foreground;
        BackgroundColor = background;
        Height = height;
        Width = width;
        _delay = delay;
    }

    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;
            for (int i = 0; i < Content.Length; i++)
            {
                string line = Content[i];

                int lineLength = line.Length;
                int centeredX = X + (Width - lineLength) / 2;

                // Ensure that the starting X position doesn't go beyond the buffer width
                centeredX = Math.Max(centeredX, X);

                for (int j = 0; j < line.Length; j++)
                {
                    int globalX = centeredX + j;
                    int globalY = Y + i + _spacing;

                    if (globalY == Height)
                    {
                        // Handle any special cases if needed
                    }

                    buffer.UpdateCell(globalX, globalY, line[j], ForegroundColor, BackgroundColor, _delay);
                    // buffer.Render();
                } 
                // _add = false; 
                
            }
        
        // }
    }

    public override void RemoveFromBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;
        for (int i = 0; i < Content.Length; i++)
        {
            for (int j = 0; j < Width-2; j++)
            {
                int globalX = X + j;
                int globalY = Y + i + _spacing;

                buffer.UpdateCell(globalX, globalY, ' ', ForegroundColor, BackgroundColor);
            }
        }
    }
}