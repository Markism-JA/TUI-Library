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

        private void FillWindow()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Buffer.UpdateCell(i, j, ' ', ConsoleColor.Gray, BackgroundColor);
                }
            }
        }
        
        public void Move(int newX, int newY)
        {
            this.X = newX;
            this.Y = newY;
            FillWindow();
        }

        public void RenderAll()
        {
            foreach (var child in Children.ToList())
            {
                if (child.IsRemoved)
                {
                    // Console.WriteLine("child.isremoved is being run");
                    child.RemoveFromBuffer(Buffer);
                    child.Parent = null;
                    Children.Remove(child);
                }else if (child.IsVisible)
                {
                    // Console.WriteLine("child.is visible is being run");
                    child.AddToBuffer(Buffer);
                }
                else
                {
                    // Console.WriteLine("else is being run");
                    child.RemoveFromBuffer(Buffer);

                }
            }
            
            AddGrid();
            AddBorder();
            RenderWindows();
        }
        
        
        public void RenderWindows()
        {
            Buffer.Render();
        }
        
        private void AddGrid()
        {
            if (GridOn)
            {
                // Top Column
                for (int x = 0; x < Buffer.Width; x++)
                {
                    char number = (char)('0' + (x % 10)); // Single-digit cycle: 0-9
                    Buffer.UpdateCell(x, 0, number, ConsoleColor.Yellow, ConsoleColor.Black);
                }

                // Left Row
                for (int y = 0; y < Buffer.Height; y++)
                {
                    char number = (char)('0' + (y % 10)); // Single-digit cycle: 0-9
                    Buffer.UpdateCell(0, y, number, ConsoleColor.Yellow, ConsoleColor.Black);
                }
            }
        }
        private void AddBorder()
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
    }
}