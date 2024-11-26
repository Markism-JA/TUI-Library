namespace TUI
{
    public class Window : ContainerWidget
    {
        public TerminalBuffer Buffer { get; set; }
        private int _x, _y, _width, _height;
        private ConsoleColor _backgroundColor;
        public bool GridOn { get; set; }

        public Window(int customWidth, int customHeight, ConsoleColor backgroundColor = ConsoleColor.DarkGray)
        {
            this._backgroundColor = backgroundColor;

            this._width = customWidth;
            this._height = customHeight;

            int terminalWidth = Console.WindowWidth;

            /*this._x = (terminalWidth - _width) / uncomment if
             want the window to adapt to the screen size. Nightmare to set
             layout because of relative window sizing.
             */

            /*this._y = 0;
             /sets the vertical position of the box with respect to [+x, +y}
             "cartesian plane"
             */ 

            
            Buffer = new TerminalBuffer(_width, _height);

            
            FillWindow();
        }

        //Calls the update cell method of the Buffer class 
        private void FillWindow()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Buffer.UpdateCell(i, j, ' ', ConsoleColor.Gray, _backgroundColor);
                }
            }
        }

        // Draw content inside the window (e.g., text) and center it horizontally inside the window
        public void DrawContent(string content, ConsoleColor contentColor = ConsoleColor.White)
        {
            // Split the content into lines (to handle multi-line content)
            string[] lines = content.Split(new[] { '\n' }, StringSplitOptions.None);

            // Calculate the starting Y position for vertical centering inside the window
            int startY = (_height - lines.Length) / 2;

            // Loop through each line of content and center it horizontally inside the window
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // Calculate the starting X position for horizontal centering inside the window
                int startX = (_width - line.Length) / 2;

                // Ensure content fits within the window boundaries (if content is too long) "Dynamic resizing"
                if (startX < 0)
                {
                    startX = 0; // Prevent content from going off the left side
                    line = line.Substring(0, _width); // Truncate if it's too long
                }

                // Draw each line of content at the calculated position inside the window
                for (int j = 0; j < line.Length; j++)
                {
                    Buffer.UpdateCell(_x + startX + j, _y + startY + i, line[j], contentColor, _backgroundColor);
                }
            }
        }
        
        public void DrawCentered(string[] content, int localStartY = 0, ConsoleColor contentColor = ConsoleColor.White)
        {
            for (int i = 0; i < content.Length; i++)
            {
                string line = content[i];
                
                //Centering logic
                int localStartX = (_width - line.Length) / 2;

                if (localStartY + i >= _height) break;
                if (localStartX < 0)
                {
                    line = line.Substring(-localStartX, Math.Min(_width, line.Length));
                    localStartX = 0;
                }

                for (int j = 0; j < line.Length; j++)
                {
                    int globalX = _x + localStartX + j;
                    int globalY = _y + localStartY + i;

                    // Skip out-of-bounds coordinates
                    if (globalX >= 0 && globalX < Buffer.Width && globalY >= 0 && globalY < Buffer.Height)
                    {
                        Buffer.UpdateCell(globalX, globalY, line[j], contentColor, _backgroundColor);
                    }
                }
            }
        }
        
        public void Draw(string[] content, int localStartX = 0, int localStartY = 0, ConsoleColor contentColor = ConsoleColor.White)
        {
            for (int i = 0; i < content.Length; i++)
            {
                string line = content[i];

                // Adjust line to fit within the window if it exceeds bounds
                if (localStartX < 0)
                {
                    line = line.Substring(-localStartX, Math.Min(_width, line.Length));
                    localStartX = 0;
                }

                // Ensure content stays within the vertical bounds of the window
                if (localStartY + i >= _height || _y + localStartY + i < 0) continue;

                // Render each character of the line
                for (int j = 0; j < line.Length; j++)
                {
                    int globalX = localStartX + j; //used to be x + localStartX + j;
                    int globalY = _y + localStartY + i;

                    // Skip out-of-bounds coordinates
                    if (globalX >= 0 && globalX < Buffer.Width && globalY >= 0 && globalY < Buffer.Height)
                    {
                        Buffer.UpdateCell(globalX, globalY, line[j], contentColor, _backgroundColor);
                    }
                }
            }
        }




        // Move the window to a new position (can be used for manual positioning if needed)
        public void Move(int newX, int newY)
        {
            // Update position
            this._x = newX;
            this._y = newY;

            // Redraw the window with the new position
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