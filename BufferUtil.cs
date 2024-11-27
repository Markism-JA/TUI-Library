namespace TUI;

public class BufferUtil
{
    //LocalField
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public ConsoleColor? ForegroundColor { get; set; }
    public ConsoleColor? BackgroundColor { get; set; }
    
    //ParentField
    public int ParentX { get; set; }
    public int ParentY { get; set; }
    public int ParentWidth { get; set; }
    public int ParentHeight { get; set; }
    public ConsoleColor? ParentForegroundColor { get; set; }
    public ConsoleColor? ParentBackgroundColor { get; set; }
    
    public TerminalBuffer Buffer { get; set; }

    public BufferUtil(int x, int y, int width, int height, TerminalBuffer buffer)
    {
        X = x;
        Y = y;
        ParentWidth = width;    
        ParentHeight = height;
        Buffer = buffer;
    }
    
    public void LeftAlignedDraw(string[] content, ConsoleColor? background, ConsoleColor? contentColor = ConsoleColor.White)
    {
        BackgroundColor = background;
        for (int i = 0; i < content.Length; i++)
        {
            string line = content[i];

            // Ensure the content fits within the height of the defined region
            if (Y + i >= Y + ParentHeight || Y + i < Y) continue;
    
            // Clip content if it exceeds the width of the defined region
            int x = X;  // Use GlobalX as the starting x-coordinate
            if (x + line.Length > X + ParentWidth)
            {
                line = line.Substring(0, X + ParentWidth - x); // Clip line if too wide
            }

            for (int j = 0; j < line.Length; j++)
            {
                int globalX = x + j;
                int globalY = Y + i;

                // Update the buffer cell with the content
                Buffer.UpdateCell(globalX, globalY, line[j], contentColor, BackgroundColor);
            }
        }
    }
    
    public void CenterAlignedDraw(string[] content, ConsoleColor? contentColor = null, ConsoleColor? background = null)
    {
        BackgroundColor = background;
        for (int i = 0; i < content.Length; i++)
        {
            string line = content[i];

            // Calculate centered X position within the GlobalWidth range, starting from GlobalX
            int x = X + (ParentWidth - line.Length) / 2;
        
            // Ensure content fits within the height of the defined region
            if (Y + i < Y || Y + i >= Y + ParentHeight) continue;

            // Clip content if it exceeds the width of the defined region
            if (x + line.Length > X + ParentWidth)
            {
                line = line.Substring(0, X + ParentWidth - x); // Clip line if too wide
            }

            for (int j = 0; j < line.Length; j++)
            {
                int globalX = x + j;
                int globalY = Y + i;

                // Update the buffer cell with the content
                Buffer.UpdateCell(globalX, globalY, line[j], contentColor, BackgroundColor);
            }
        }
    }
}
    