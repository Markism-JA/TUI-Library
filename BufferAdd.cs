namespace TUI;

public class BufferAdd
{
    public int GlobalX { get; set; }
    public int GlobalY { get; set; }
    public TerminalBuffer Buffer { get; set; }
    public ConsoleColor ForegroundColor { get; set; }
    public ConsoleColor BackgroundColor { get; set; }

    public BufferAdd(int x, int y, TerminalBuffer buffer)
    {
        GlobalX = x;
        GlobalY = y;
        Buffer = buffer;
    }
    
    public void Draw(string[] content, int x = 1, int y = 1, ConsoleColor contentColor = ConsoleColor.White)
    {
        for (int i = 0; i < content.Length; i++)
        {
            string line = content[i];

            // Adjust line to fit within the window if it exceeds bounds
            if (x < 0)
            {
                line = line.Substring(-x, Math.Min(GlobalX, line.Length));
                x = 0;
            }

            // Ensure content stays within the vertical bounds of the window
            if (y + i >= GlobalX) continue;

            // Render each character of the line
            for (int j = 0; j < line.Length; j++)
            {
                int globalX = x + j; //used to be x + localStartX + j;
                int globalY = y + i;

                // Skip out-of-bounds coordinates
                if (globalX >= 0 && globalX < Buffer.Width && globalY >= 0 && globalY < Buffer.Height)
                {
                    Buffer.UpdateCell(globalX, globalY, line[j], contentColor, BackgroundColor);
                }
            }
        }
    }
}