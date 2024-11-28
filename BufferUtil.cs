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
    public int ParentParentWidth { get; set; }
    public int ParentParentHeight { get; set; }
    public ConsoleColor? ParentForegroundColor { get; set; }
    public ConsoleColor? ParentBackgroundColor { get; set; }
    
    public TerminalBuffer Buffer { get; set; }
    
    public int MaxCenterX { get; set;  }

    public BufferUtil(int x, int y, int parentX, int parentY, int width, int height, int parentWidth, int parentHeight, ConsoleColor? foreground, ConsoleColor? background, ConsoleColor? parentForeground, ConsoleColor? parentBackground, TerminalBuffer buffer)
    {
        // localValuesSet
        X = x;
        Y = y;
        Width = width;
        Height = height;
        ForegroundColor = foreground;
        BackgroundColor = background;
        
        // parentValues Set
        ParentX = parentX;
        ParentY = parentY;
        ParentForegroundColor = parentForeground;
        ParentBackgroundColor = parentBackground;
        ParentParentWidth = parentWidth;    
        ParentParentHeight = parentHeight;
        
        Buffer = buffer;
    }
    
    public void LeftAlignedDraw(string[] content)
    {
        for (int i = 0; i < content.Length; i++)
        {
            string line = content[i];

            // Ensure the content fits within the height of the defined region
            if (Y + i >= Y + ParentParentHeight || Y + i < Y) continue;
    
            // Clip content if it exceeds the width of the defined region
            int x = X;  // Use GlobalX as the starting x-coordinate
            if (x + line.Length > X + ParentParentWidth)
            {
                line = line.Substring(0, X + ParentParentWidth - x); // Clip line if too wide
            }

            for (int j = 0; j < line.Length; j++)
            {
                int globalX = x + j;
                int globalY = Y + i;

                // Update the buffer cell with the content
                Buffer.UpdateCell(globalX, globalY, line[j], ForegroundColor, BackgroundColor);
            }
        }
    }
    
        public void CenterAlignedDraw(string[] content)
        {
            MaxCenterX = 0;
            for (int i = 0; i < content.Length; i++)
            {
                string line = content[i];
                int x = X + (ParentParentWidth - line.Length) / 2;
                MaxCenterX = Math.Max(MaxCenterX, x);
                if (Y + i < Y || Y + i >= Y + ParentParentHeight) continue;
                if (x + line.Length > X + ParentParentWidth)
                {
                    line = line.Substring(0, X + ParentParentWidth - x); // Clip line if too wide
                }
                for (int j = 0; j < line.Length; j++)
                {
                    int localX = x + j;
                    int localY = Y + i;
                    Buffer.UpdateCell(localX, localY, line[j], ForegroundColor, BackgroundColor);
                }
            }
        }
        
        public void EraseFromBuffer(string[] content, Align align)
        {
            string[] blankContent = new string[content.Length];
            for (int i = 0; i < content.Length; i++)
            {
                blankContent[i] = new string(' ', content[i].Length);
            }

            switch (align)
            {
                case Align.Center:
                    EraseCenter(blankContent);
                    break;
                    
            }
            
        }
        
        public void EraseCenter(string[] content)
        {
            MaxCenterX = 0;
            for (int i = 0; i < content.Length; i++)
            {
                string line = content[i];
                int x = X + (ParentParentWidth - line.Length) / 2;
                MaxCenterX = Math.Max(MaxCenterX, x);
                if (Y + i < Y || Y + i >= Y + ParentParentHeight) continue;
                if (x + line.Length > X + ParentParentWidth)
                {
                    line = line.Substring(0, X + ParentParentWidth - x); // Clip line if too wide
                }
                for (int j = 0; j < line.Length; j++)
                {
                    int localX = x + j;
                    int localY = Y + i;
                    Buffer.UpdateCell(localX, localY, line[j], ForegroundColor, ParentBackgroundColor);
                }
            }
        }

}
    