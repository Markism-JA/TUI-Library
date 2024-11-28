namespace TUI
{
    public class TerminalBuffer
    {
        private Cell[,] _buffer;
        private Cell[,] _offScreenBuffer;
        private bool[,] _renderFlags;
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
                    _buffer[i, j] = new Cell
                    {
                        Character = ' ',  
                        ForegroundColor = null,  
                        BackgroundColor = null,  
                    };
                    

                    _offScreenBuffer[i, j] = new Cell
                    {
                        Character = ' ',
                        ForegroundColor = null,
                        BackgroundColor = null
                    };

                    _renderFlags[i, j] = false; 
                }
            }
        }

        public void UpdateCell(int x, int y, char character, ConsoleColor? foreground, ConsoleColor? background)
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
        
        public void Render()
        {
            // Iterate over the buffer and render only cells that need to be rendered
            for (int i = 0; i < _buffer.GetLength(0); i++) //rows
            {
                for (int j = 0; j < _buffer.GetLength(1); j++)  //columns
                {
                    if (_renderFlags[i, j]) // Render marked cell
                    {
                        var cell = _offScreenBuffer[i, j];  // Get the cell from the off-screen buffer
                        Console.SetCursorPosition(j, i);  // Move the cursor to the appropriate position
                        
                        if (cell.ForegroundColor.HasValue)
                            Console.ForegroundColor = cell.ForegroundColor.Value; 
                        if (cell.BackgroundColor.HasValue)
                            Console.BackgroundColor = cell.BackgroundColor.Value;
                        
                        Console.Write(cell.Character);  // Write the character to the console
                        Console.ResetColor();
                    }
                }
            }

            CopyOffScreenToMainBuffer();
            
            ResetRenderFlags();
        }

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
