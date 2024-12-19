using FashionDressingGame.Database;

namespace TUI
{
    public class TableMenu : InteractiveWidget
    {
        private Dictionary<int, ECharacter> _menuItems;
        private int _selectedIndex;
        public bool SelectionMade { get; set; }
        public int SelectedKey { get; private set; }
        public int Spacing { get; set; } = 2;
        public int Columns { get; set; } = 1; // Number of columns to split the menu into
        private List<int> columnWidths;

        public TableMenu(Dictionary<int, ECharacter> menuItems, int x, int y, int width, int height) : base(x, y, 0, 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            _menuItems = menuItems;
            _selectedIndex = _menuItems.Keys.FirstOrDefault();  // Set the initial selection to the first key, or 0 if empty
            columnWidths = new List<int> { 10, 20, 15 };  // Example column widths: adjust as needed
        }

        public override void HandleInput(ConsoleKey key)
        {
            if (_menuItems.Count == 0) return;
            switch (key)
            {
                case ConsoleKey.Enter:
                    SelectionMade = true;
                    SelectedKey = _selectedIndex;
                    break;
                default:
                    Navigate(key);
                    break;
            }
        }

        public override void Navigate(ConsoleKey key)
        {
            if (_menuItems.Count == 0) return; // Prevent navigation if the dictionary is empty

            var keys = _menuItems.Keys.ToList(); // Get the list of keys
            int currentIndex = keys.IndexOf(_selectedIndex);
            int totalItems = _menuItems.Count;
            int itemsPerColumn = (int)Math.Ceiling((double)totalItems / Columns);

            // Skip header in navigation by adjusting the logic to only affect menu items
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

            if (_menuItems.Count == 0)
            {
                string emptyMessage = "No Characters, Press Escape to go back to load menu to reload or add characters.";

                int messageLength = emptyMessage.Length;
                int startPosX = (ParentWidth - messageLength) / 2; // Center the message based on ParentWidth
                int startPosY = Y + (Height / 2); // Center the message vertically in the available space


                for (int i = 0; i < messageLength; i++)
                {
                    if (startPosX + i < buffer.Width && startPosY < buffer.Height)
                    {
                        buffer.UpdateCell(startPosX + i, startPosY, emptyMessage[i], null, null);
                    }
                }
                return;
            } else if (_menuItems.Count > 0)
            {
                int totalItems = _menuItems.Count;
                int itemsPerColumn = (int)Math.Ceiling((double)totalItems / Columns);
                var keys = _menuItems.Keys.ToList();

                // Render Table Header (not included in navigation)
                string headerText = "    ID        Name                Grade      ";
                ForegroundColor = null;
                BackgroundColor = null;  // Set a fixed background color for the header
                RenderRow(headerText, 0, buffer);  // Row 0 is for header

                // Render each column, starting from index 1 (skip header)
                for (int column = 0; column < Columns; column++)
                {
                    int startIndex = column * itemsPerColumn;
                    int columnHeight = Math.Min(itemsPerColumn, totalItems - startIndex);

                    for (int i = 0; i < columnHeight; i++)
                    {
                        var character = _menuItems[keys[startIndex + i]];
                        string menuText = $"{character.Id} - {GetValue(character.Name)} - {(character.CharacterGrade).ToString()}";

                        // Pad the menu text to match the column widths
                        string paddedMenuText = FormatColumn(menuText);

                        // Set colors for the selected item (excluding the header)
                        ForegroundColor = (keys[startIndex + i] == _selectedIndex) ? ConsoleColor.Black : ConsoleColor.Black;
                        BackgroundColor = (keys[startIndex + i] == _selectedIndex) ? ConsoleColor.Yellow : ConsoleColor.White;

                        // Render the menu text for each line with padding for separation
                        RenderRow(paddedMenuText, i + 1, buffer); // Start rendering from row 1 (skip header)
                    }
                }
            }

            
        }
        
        public override void RemoveFromBuffer(TerminalBuffer? buffer)
        {
            if (buffer == null) return;

            // Calculate the maximum width of the table
            int maxRowWidth = Math.Max(columnWidths.Sum() + (Columns - 1) * Spacing + 4, ParentWidth); // Include table width and parent width

            // Calculate the total number of rows, including the header
            int totalRows = _menuItems.Count > 0 ? _menuItems.Count + 1 : 1; // +1 for the header row or the empty message row
            int rowHeight = Spacing; // Row height is determined by the spacing

            for (int rowIndex = 0; rowIndex < totalRows; rowIndex++)
            {
                int yPos = Y + (rowIndex * rowHeight);

                // Clear the entire row by filling it with spaces up to maxRowWidth
                string emptyRow = new string(' ', maxRowWidth);

                for (int x = X; x < X + maxRowWidth; x++)
                {
                    if (x < buffer.Width && yPos < buffer.Height)
                    {
                        buffer.UpdateCell(x, yPos, ' ', ParentForegroundColor, ParentBackgroundColor);
                    }
                }
            }

            // Special case: If no menu items, ensure the empty message row is cleared
            if (_menuItems.Count == 0)
            {
                string emptyMessage = "No Characters, Press Escape to go back to main menu to reload or add characters.";
                int messageLength = emptyMessage.Length;
                int startPosX = (ParentWidth - messageLength) / 2; // Center the message based on ParentWidth
                int startPosY = Y + (Height / 2); // Center the message vertically in the available space

                for (int i = 0; i < messageLength; i++)
                {
                    if (startPosX + i < buffer.Width && startPosY < buffer.Height)
                    {
                        buffer.UpdateCell(startPosX + i, startPosY, ' ', ParentForegroundColor, ParentBackgroundColor);
                    }
                }
            }
        }

        private void RenderRow(string rowText, int rowIndex, TerminalBuffer? buffer)
        {
            int xPos = X;
            int yPos = Y + (rowIndex * Spacing);

            for (int i = 0; i < rowText.Length; i++)
            {
                if (xPos + i < buffer.Width && yPos < buffer.Height)
                {
                    buffer.UpdateCell(xPos + i, yPos, rowText[i], ForegroundColor, BackgroundColor);
                }
            }
        }

        private string FormatColumn(string text)
        {
            // Split the text into columns and pad to column widths
            var columns = text.Split('-').Select(t => t.Trim()).ToList();
            string paddedText = "";

            // Add 4 spaces to the left of the first column
            if (columns.Count > 0)
            {
                paddedText += "    " + columns[0].PadRight(columnWidths[0]); // Pad the first column with 4 leading spaces
            }

            // Pad the rest of the columns
            for (int i = 1; i < columns.Count; i++)
            {
                paddedText += columns[i].PadRight(columnWidths[i]); // Pad each subsequent column
            }

            return paddedText;
        }

        // New method to get the selected item based on the key
        public ECharacter GetSelectedItem()
        {
            return _menuItems.ContainsKey(SelectedKey) ? _menuItems[SelectedKey] : null;
        }
        
        private static string GetValue(string s)
        {
            if (s.Length > 15) return s.Substring(0, 15) + "...";
            else return s;
        }
    }
}
