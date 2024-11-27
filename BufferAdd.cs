namespace TUI;

public class BufferAdd
{
    public int GlobalX { get; set; }
    public int GlobalY { get; set; }
    public int GlobalWidth { get; set; }
    public int GlobalHeight { get; set; }
    public TerminalBuffer Buffer { get; set; }
    public ConsoleColor ForegroundColor { get; set; }
    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Green;

    public BufferAdd(int x, int y, int width, int height, TerminalBuffer buffer)
    {
        GlobalX = x;
        GlobalY = y;
        GlobalWidth = width;    
        GlobalHeight = height;
        Buffer = buffer;
    }

    public BufferAdd(TerminalBuffer buffer)
    {
        Buffer = buffer;
    }
    
    public void LeftAlignedDraw(string[] content, ConsoleColor background, ConsoleColor contentColor = ConsoleColor.White)
    {
        BackgroundColor = background;
        for (int i = 0; i < content.Length; i++)
        {
            string line = content[i];

            // Ensure the content fits within the height of the defined region
            if (GlobalY + i >= GlobalY + GlobalHeight || GlobalY + i < GlobalY) continue;
    
            // Clip content if it exceeds the width of the defined region
            int x = GlobalX;  // Use GlobalX as the starting x-coordinate
            if (x + line.Length > GlobalX + GlobalWidth)
            {
                line = line.Substring(0, GlobalX + GlobalWidth - x); // Clip line if too wide
            }

            for (int j = 0; j < line.Length; j++)
            {
                int globalX = x + j;
                int globalY = GlobalY + i;

                // Update the buffer cell with the content
                Buffer.UpdateCell(globalX, globalY, line[j], contentColor, BackgroundColor);
            }
        }
    }
    
    public void CenterAlignedDraw(string[] content, ConsoleColor contentColor = ConsoleColor.White, ConsoleColor background = ConsoleColor.Green)
    {
        BackgroundColor = background;
        for (int i = 0; i < content.Length; i++)
        {
            string line = content[i];

            // Calculate centered X position within the GlobalWidth range, starting from GlobalX
            int x = GlobalX + (GlobalWidth - line.Length) / 2;
        
            // Ensure content fits within the height of the defined region
            if (GlobalY + i < GlobalY || GlobalY + i >= GlobalY + GlobalHeight) continue;

            // Clip content if it exceeds the width of the defined region
            if (x + line.Length > GlobalX + GlobalWidth)
            {
                line = line.Substring(0, GlobalX + GlobalWidth - x); // Clip line if too wide
            }

            for (int j = 0; j < line.Length; j++)
            {
                int globalX = x + j;
                int globalY = GlobalY + i;

                // Update the buffer cell with the content
                Buffer.UpdateCell(globalX, globalY, line[j], contentColor, BackgroundColor);
            }
        }
    }
}
    