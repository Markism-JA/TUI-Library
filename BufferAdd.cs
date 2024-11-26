namespace TUI;

public class BufferAdd
{
    public int GlobalX { get; set; }
    public int GlobalY { get; set; }
    public TerminalBuffer Buffer { get; set; }
    public ConsoleColor ForegroundColor { get; set; }
    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Green; // Default background color

    public BufferAdd(int x, int y, TerminalBuffer buffer)
    {
        GlobalX = x;
        GlobalY = y;
        Buffer = buffer;
    }

    public BufferAdd(TerminalBuffer buffer)
    {
        Buffer = buffer;
    }

    public void LeftAlignedDraw(string[] content, int x, int y, ConsoleColor background, ConsoleColor contentColor = ConsoleColor.White)
    {
        BackgroundColor = background;
        for (int i = 0; i < content.Length; i++)
        {
            string line = content[i];

            if (x < 0)
            {
                line = line.Substring(-x, Math.Min(GlobalX, line.Length));
                x = 0;
            }

            if (y + i >= Buffer.Height) continue;

            for (int j = 0; j < line.Length; j++)
            {
                int globalX = x + j;
                int globalY = y + i;

                if (globalX >= 0 && globalX < Buffer.Width && globalY >= 0 && globalY < Buffer.Height)
                {
                    Buffer.UpdateCell(globalX, globalY, line[j], contentColor, BackgroundColor);
                }
            }
        }
    }

    public void CenterAlignedDraw(string[] content, int y = 0, ConsoleColor contentColor = ConsoleColor.White, ConsoleColor background = ConsoleColor.Green)
    {
        BackgroundColor = background;
        for (int i = 0; i < content.Length; i++)
        {
            string line = content[i];

            int x = (GlobalX - line.Length) / 2;
    
            if (y + i >= Buffer.Height) break;

            if (x < 0) x = 0;

            if (x + line.Length > Buffer.Width)
                line = line.Substring(0, Buffer.Width - x);

            for (int j = 0; j < line.Length; j++)
            {
                int globalX = x + j;
                int globalY = y + i;

                if (globalX >= 0 && globalX < Buffer.Width && globalY >= 0 && globalY < Buffer.Height)
                {
                    Buffer.UpdateCell(globalX, globalY, line[j], contentColor, BackgroundColor);
                }
            }
        }
    }

    public void DrawStringCentered(string content, ConsoleColor contentColor = ConsoleColor.White)
    {
        // Split the content into lines (to handle multi-line content)
        string[] lines = content.Split(new[] { '\n' }, StringSplitOptions.None);

        // Calculate the starting Y position for vertical centering inside the window
        int startY = (GlobalY - lines.Length) / 2;

        // Loop through each line of content and center it horizontally inside the window
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            // Calculate the starting X position for horizontal centering inside the window
            int startX = (GlobalX - line.Length) / 2;

            // Ensure content fits within the window boundaries (if content is too long) "Dynamic resizing"
            if (startX < 0)
            {
                startX = 0; // Prevent content from going off the left side
                line = line.Substring(0, GlobalX); // Truncate if it's too long
            }

            // Draw each line of content at the calculated position inside the window
            for (int j = 0; j < line.Length; j++)
            {
                Buffer.UpdateCell(startX + j, startY + i, line[j], contentColor, BackgroundColor);
            }
        }
    }
    
}