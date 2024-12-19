namespace TUI
{
    public class Window : ContainerWidget
    {
        public TerminalBuffer Buffer { get; set; } 
        public bool GridOn { get; set; }
        public bool BorderOn { get; set; }
        public char BorderHorizontal { get; set; } = '-';
        public char BorderVertical { get; set; } = '|';
        public ConsoleColor? BorderBackgroundColor { get; set; }
        public ConsoleColor? BorderForegroundColor { get; set; }
        public List<Window> ChildWindows { get; set; } = new List<Window>();


        public Window(int? x, int? y, int width, int height, ConsoleColor? backgroundColor = null)
        {
            BackgroundColor = backgroundColor;
            Width = width;
            Height = height;
            Console.WindowHeight = height;
            Console.WindowWidth = width;
            X = x ?? 0;
            Y = y ?? 0;
            Buffer = new TerminalBuffer(Width, Height);
            FillWindow();
        }

        protected void FillWindow()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Buffer.UpdateCell(i, j, ' ', ConsoleColor.Gray, BackgroundColor);
                }
            }
        }

        public virtual void RenderAll()
        {
            foreach (var child in Children.ToList())
            {
                if (child.IsRemoved)
                {
                    child.RemoveFromBuffer(Buffer);
                    child.Parent = null;
                    Children.Remove(child);
                }
                else if (child.IsVisible)
                {
                    child.AddToBuffer(Buffer);
                }
                else
                {
                    child.RemoveFromBuffer(Buffer);
                }
            }

            foreach (var childWindow in ChildWindows)
            {
                childWindow.RenderAll(); 
            }

            AddGrid();
            AddBorder();

            Buffer.Render(); //Calls the render method of the buffer
        }

        protected void AddGrid()
        {
            if (GridOn)
            {
                for (int x = 0; x < Buffer.Width; x++)
                {
                    char number = (char)('0' + (x % 10)); 
                    Buffer.UpdateCell(x, 0, number, ConsoleColor.Yellow, ConsoleColor.Black);
                }

                for (int y = 0; y < Buffer.Height; y++)
                {
                    char number = (char)('0' + (y % 10)); 
                    Buffer.UpdateCell(0, y, number, ConsoleColor.Yellow, ConsoleColor.Black);
                }
            }
        }

        protected void AddBorder()
        {
            if (BorderOn)
            {
                for (int x = 0; x < Buffer.Width; x++)
                {
                    Buffer.UpdateCell(x, 0, BorderHorizontal, BorderForegroundColor, BorderBackgroundColor);
                    Buffer.UpdateCell(x, Buffer.Height - 1, BorderHorizontal, BorderForegroundColor, BorderBackgroundColor);
                }
                for (int y = 1; y < Buffer.Height - 1; y++)
                {
                    Buffer.UpdateCell(0, y, BorderVertical, BorderForegroundColor, BorderBackgroundColor);
                    Buffer.UpdateCell(Buffer.Width - 1, y, BorderVertical, BorderForegroundColor, BorderBackgroundColor);
                }
            }
        }

        public void ReconcileChildBuffer(TerminalBuffer childBuffer, int offsetX, int offsetY)
        {
            for (int x = 0; x < childBuffer.Width; x++)
            {
                for (int y = 0; y < childBuffer.Height; y++)
                {
                    var cell = childBuffer.GetCell(x, y);
                    if (cell != null)
                    {
                        Buffer.UpdateCell(x + offsetX, y + offsetY, cell.Character, cell.ForegroundColor, cell.BackgroundColor);
                    }
                }
            }
        }
    }
}
