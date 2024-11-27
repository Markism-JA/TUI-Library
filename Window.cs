namespace TUI
{
    public class Window : ContainerWidget
    {
        public TerminalBuffer Buffer { get; set; }
        public ConsoleColor BackgroundColor { get; }
        public bool GridOn { get; set; }

        public Window(int customWidth, int customHeight, ConsoleColor backgroundColor = ConsoleColor.DarkGray, int x = 0, int y = 0)
        {
            this.BackgroundColor = backgroundColor;

            Width = customWidth;
            Height = customHeight;

            int terminalWidth = Console.WindowWidth;
            int terminalHeight = Console.WindowHeight;
            X = x;
            Y = y;

            /*this._x = (terminalWidth - _width) / uncomment if
             want the window to adapt to the screen size. Nightmare to set
             layout because of relative window sizing.
             */

            /*this._y = 0;
             /sets the vertical position of the box with respect to [+x, +y}
             "cartesian plane"
             */
            
            Buffer = new TerminalBuffer(Width, Height);

            
            FillWindow();

        }

        //Calls the update cell method of the Buffer class 
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
        
  
        

        // Move the window to a new position (can be used for manual positioning if needed)
        public void Move(int newX, int newY)
        {
            // Update position
            this.X = newX;
            this.Y = newY;
            FillWindow();
        }

        public void RenderAll()
        {
            foreach (var child in Children)
            {
                child.AddToBuffer(Buffer);
            }
            AddGrid();  
            RenderWindows();

        }

        // Render the window (and buffer content) to the screen
        public void RenderWindows()
        {
            Buffer.Render();
        }
        
            private void AddGrid()
            {
                if (GridOn)
                {
                    // Render column numbers at the top
                    for (int x = 0; x < Buffer.Width; x++)
                    {
                        char number = (char)('0' + (x % 10)); // Single-digit cycle: 0-9
                        Buffer.UpdateCell(x, 0, number, ConsoleColor.Yellow, ConsoleColor.Black);
                    }

                    // Render row numbers on the left
                    for (int y = 0; y < Buffer.Height; y++)
                    {
                        char number = (char)('0' + (y % 10)); // Single-digit cycle: 0-9
                        Buffer.UpdateCell(0, y, number, ConsoleColor.Yellow, ConsoleColor.Black);
                    }
                }
            }
    }
}