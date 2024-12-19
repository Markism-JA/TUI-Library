namespace TUI
{
    public class Menu : InteractiveWidget
    {
        private Dictionary<int, string> _menuItems;
        private int _selectedIndex;
        public bool SelectionMade { get;  set; }
        public string FinalKey {get; private set;}
        public int Spacing { get; set; } = 2;
        public int Columns { get; set; } = 1; // Number of columns to split the menu into

        public Menu(Dictionary<int, string> menuItems, int x, int y, int width, int height) : base(x, y, 0, 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            
            _menuItems = menuItems;
            _selectedIndex = _menuItems.Keys.First();  // Set the initial selection to the first key
        }

        public override void HandleInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Enter:
                    SelectionMade = true;
                    FinalKey = _menuItems[_selectedIndex];
                    break;
                default:
                    Navigate(key);
                    break;
            }
        }

        public override void Navigate(ConsoleKey key)
        {
            var keys = _menuItems.Keys.ToList(); // Get the list of keys

            int currentIndex = keys.IndexOf(_selectedIndex);
            int totalItems = _menuItems.Count;
            int itemsPerColumn = (int)Math.Ceiling((double)totalItems / Columns);

            if (key == ConsoleKey.UpArrow)
            {
                if (currentIndex > 0)
                    _selectedIndex = keys[currentIndex - 1]; // Move up
                else
                    _selectedIndex = keys.Last(); // Wrap to the last item
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (currentIndex < keys.Count - 1)
                    _selectedIndex = keys[currentIndex + 1]; // Move down
                else
                    _selectedIndex = keys.First(); // Wrap to the first item
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                // Navigate left between columns
                int columnIndex = currentIndex / itemsPerColumn;
                if (columnIndex > 0)
                    _selectedIndex = keys[currentIndex - itemsPerColumn]; // Move to the left column
                else
                    _selectedIndex = keys[keys.Count - 1]; // Wrap to the last column
            }
            else if (key == ConsoleKey.RightArrow)
            {
                // Navigate right between columns
                int columnIndex = currentIndex / itemsPerColumn;
                if (columnIndex < Columns - 1)
                    _selectedIndex = keys[currentIndex + itemsPerColumn]; // Move to the next column
                else
                    _selectedIndex = keys[0]; // Wrap to the first column
            }
        }

        public override void AddToBuffer(TerminalBuffer? buffer)
        {
            if (buffer == null) return;

            int totalItems = _menuItems.Count;
            int itemsPerColumn = (int)Math.Ceiling((double)totalItems / Columns);
            var keys = _menuItems.Keys.ToList();

            // Render each column
            for (int column = 0; column < Columns; column++)
            {
                int startIndex = column * itemsPerColumn;
                int columnHeight = Math.Min(itemsPerColumn, totalItems - startIndex);

                for (int i = 0; i < columnHeight; i++)
                {
                    string menuText = _menuItems[keys[startIndex + i]];

                    // Center-align the menu text and pad it to match the Width
                    int padding = (Width - menuText.Length) / 2;
                    string paddedMenuText = menuText.PadLeft(menuText.Length + padding).PadRight(Width);

                    // Set colors for the selected item
                    ForegroundColor = (keys[startIndex + i] == _selectedIndex) ? ConsoleColor.Black : ConsoleColor.Black;
                    BackgroundColor = (keys[startIndex + i] == _selectedIndex) ? ConsoleColor.Yellow : ConsoleColor.White;

                    // Render the menu text for each line with padding for separation
                    for (int j = 0; j < paddedMenuText.Length; j++)
                    {
                        int xPos = X + (column * (Width + 2)); // Shift columns
                        if (xPos + j < buffer.Width && Y + i * Spacing < buffer.Height)
                        {
                            buffer.UpdateCell(xPos + j, Y + i * Spacing, paddedMenuText[j], ForegroundColor, BackgroundColor);
                        }
                    }
                }
            }
        }

        public override void RemoveFromBuffer(TerminalBuffer? buffer)
        {
            if (buffer == null) return;

            int totalItems = _menuItems.Count;
            int itemsPerColumn = (int)Math.Ceiling((double)totalItems / Columns);

            // Loop through each column
            for (int column = 0; column < Columns; column++)
            {
                int startIndex = column * itemsPerColumn;
                int columnHeight = Math.Min(itemsPerColumn, totalItems - startIndex);

                for (int i = 0; i < columnHeight; i++)
                {
                    // Calculate positions
                    int xPos = X + (column * (Width + 2)); // Adjust for column position
                    int yPos = Y + i * Spacing;

                    // Clear the menu text from the buffer (fill with spaces)
                    for (int j = 0; j < Width; j++)
                    {
                        if (xPos + j < buffer.Width && yPos < buffer.Height)
                        {
                            buffer.UpdateCell(xPos + j, yPos, ' ', ParentForegroundColor, ParentBackgroundColor);
                        }
                    }
                }
            }
        }



        private void ExecuteSelection()
        {
            Console.SetCursorPosition(10, 15);
            Console.WriteLine($"Selected: {_menuItems[_selectedIndex]}");
        }

        // New method to get the selected item based on the key
        public string GetSelectedItem()
        {
            return FinalKey ?? string.Empty;
        }
        
    }
}
