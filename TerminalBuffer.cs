namespace TUI
{
    public class TerminalBuffer
    {
        private Cell[,] _buffer;  // 2D grid of cells
        private Cell[,] _offScreenBuffer; // Records cell changes to bulk render
        private bool[,] _renderFlags;  // Flags to indicate if a cell needs rendering

        public int Width { get; private set; }
        public int Height { get; private set; }

        public TerminalBuffer(int width, int height)
        {
            _buffer = new Cell[height, width];
            _offScreenBuffer = new Cell[height, width];
            _renderFlags = new bool[height, width];
            Width = width;
            Height = height;
            
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // Initialize both buffers with default cell values
                    _buffer[i, j] = new Cell
                    {
                        Character = ' ',  
                        ForegroundColor = ConsoleColor.Gray,  
                        BackgroundColor = ConsoleColor.Black  
                    };
                    

                    _offScreenBuffer[i, j] = new Cell
                    {
                        Character = ' ',
                        ForegroundColor = ConsoleColor.Gray,
                        BackgroundColor = ConsoleColor.Black
                    };

                    _renderFlags[i, j] = false; 
                }
            }
        }

        // Method to update a specific cell in the off-screen buffer
        public void UpdateCell(int x, int y, char character, ConsoleColor foreground, ConsoleColor background)
        {
            if (x >= 0 && y >= 0 && x < _buffer.GetLength(1) && y < _buffer.GetLength(0))
            {
                _offScreenBuffer[y, x] = new Cell
                {
                    Character = character,
                    ForegroundColor = foreground,
                    BackgroundColor = background
                };

                _renderFlags[y, x] = true; // Mark the cell for rendering
            }
            else
            {
                Console.WriteLine("Error: Attempted to update a cell outside the buffer's bounds.");
            }
        }

        // Method to update multiple cells in the off-screen buffer
        /*public void UpdateMultipleCells(IEnumerable<(int x, int y, char character, ConsoleColor? foreground, ConsoleColor? background)> cells)
        {
            foreach (var (x, y, character, foreground, background) in cells)
            {
                UpdateCell(x, y, character, foreground ?? ConsoleColor.Gray, background ?? ConsoleColor.Black); 
                // Use default colors if not specified
            }
        }*/

        // Method to render a specific cell in the buffer
        /*public void RenderCell(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < _buffer.GetLength(1) && y < _buffer.GetLength(0))
            {
                var cell = _offScreenBuffer[y, x]; 
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = cell.ForegroundColor;
                Console.BackgroundColor = cell.BackgroundColor;
                Console.Write(cell.Character);
                Console.ResetColor();   
            }
        }*/
        
        /*
         * Specific rendering of widgets is handled by individual widgets class that calls to the buffer's UpdateCell Method.
         */

        /*Method to render the entire buffer (render all cells). Note because of flag only the cell
         *that needs rendering is rendered.
         */
        public void Render()
        {
            // Iterate over the buffer and render only cells that need to be rendered
            for (int i = 0; i < _buffer.GetLength(0); i++)  // Iterate over rows
            {
                for (int j = 0; j < _buffer.GetLength(1); j++)  // Iterate over columns
                {
                    if (_renderFlags[i, j]) // Render only if the cell is marked for rendering
                    {
                        var cell = _offScreenBuffer[i, j];  // Get the cell from the off-screen buffer
                        Console.SetCursorPosition(j, i);  // Move the cursor to the appropriate position
                        Console.ForegroundColor = cell.ForegroundColor;
                        Console.BackgroundColor = cell.BackgroundColor;
                        Console.Write(cell.Character);  // Write the character to the console
                        Console.ResetColor();
                    }
                }
            }

            // After rendering, copy changes from off-screen buffer to main buffer
            CopyOffScreenToMainBuffer();

            // Reset render flags after rendering
            ResetRenderFlags();
        }

        // Method to copy the changes from the off-screen buffer to the main buffer
        private void CopyOffScreenToMainBuffer()
        {
            for (int i = 0; i < _buffer.GetLength(0); i++)
            {
                for (int j = 0; j < _buffer.GetLength(1); j++)
                {
                    if (_renderFlags[i, j]) 
                    {
                        _buffer[i, j] = _offScreenBuffer[i, j];
                    }
                }
            }
        }

        // Reset the render flags after each render cycle
        private void ResetRenderFlags()
        {
            for (int i = 0; i < _buffer.GetLength(0); i++)
            {
                for (int j = 0; j < _buffer.GetLength(1); j++)
                {
                    _renderFlags[i, j] = false;
                }
            }
        }
        
    }
}
