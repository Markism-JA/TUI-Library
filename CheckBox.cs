namespace TUI
{
    public class CheckBox : InteractiveWidget
    {
        public Dictionary<string, bool> CheckedStates = new Dictionary<string, bool>();
        private Dictionary<int, string> _options;
        private int _selectedIndex;
        private HashSet<int> _selectedIndexes;
        private int _labelWidth;
        public bool SelectionMade { get; set; } = false;

        
        public CheckBox(Dictionary<int, string> options, int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            _options = options;
            _selectedIndexes = new HashSet<int>();
            _selectedIndex = _options.Keys.First();
            _labelWidth = _options.Values.Max(option => option.Length);

            // Initialize all options in CheckedStates to false
            foreach (var option in _options.Values)
            {
                CheckedStates[option] = false;
            }
        }


        public override void HandleInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Enter:
                    ExecuteSelection();  // Finalize the choice
                    break;

                case ConsoleKey.Spacebar:
                    ToggleSelection();  // Toggle checkbox selection
                    break;

                default:
                    Navigate(key);  // Handle navigation
                    break;
            }
        }

        public override void Navigate(ConsoleKey key)
        {
            var keys = _options.Keys.ToList();
            int currentIndex = keys.IndexOf(_selectedIndex);

            if (key == ConsoleKey.UpArrow)
            {
                if (currentIndex > 0)
                    _selectedIndex = keys[currentIndex - 1];  // Move up
                else
                    _selectedIndex = keys.Last();  // Wrap around to the last option
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (currentIndex < keys.Count - 1)
                    _selectedIndex = keys[currentIndex + 1];  // Move down
                else
                    _selectedIndex = keys.First();  // Wrap around to the first option
            }
        }

        private void ToggleSelection()
        {
            if (_selectedIndexes.Contains(_selectedIndex))
                _selectedIndexes.Remove(_selectedIndex);  // Deselect
            else
                _selectedIndexes.Add(_selectedIndex);  // Select

            // Update checked states
            var selectedOption = _options[_selectedIndex];
            CheckedStates[selectedOption] = !CheckedStates.GetValueOrDefault(selectedOption, false);
        }

        private void ExecuteSelection()
        {
            foreach (var option in _options.Values)
            {
                if (_selectedIndexes.Contains(_options.First(kvp => kvp.Value == option).Key))
                {
                    CheckedStates[option] = true;  // Mark as checked
                }
                else
                {
                    CheckedStates[option] = false; // Mark as unchecked
                }
            }
            SelectionMade = true;
        }

        
        public override void AddToBuffer(TerminalBuffer? buffer)
        {
            if (buffer == null) return;

            var keys = _options.Keys.ToList();

            // Render each option vertically with the checkbox
            for (int i = 0; i < keys.Count; i++)
            {
                string optionText = _options[keys[i]];
                string displayText = _selectedIndexes.Contains(keys[i]) ? $"✔ {optionText}" : $"  {optionText}"; // Checkbox with space after it

                // Ensure label has equal width by padding the text to the maximum label width
                displayText = displayText.PadRight(_labelWidth + 3); // 3 for the checkbox and spacing

                // Render checkbox with its own distinct background
                ForegroundColor = (keys[i] == _selectedIndex) ? ConsoleColor.Black : ConsoleColor.Black;
                BackgroundColor = (keys[i] == _selectedIndex) ? ConsoleColor.Yellow : ConsoleColor.White;

                // Render the checkbox
                buffer.UpdateCell(X, Y + i * 2, displayText[0], ForegroundColor, BackgroundColor);

                // Render the option text after the checkbox with separate background
                string optionTextOnly = displayText.Substring(2); // Get the text without the checkbox
                for (int j = 0; j < optionTextOnly.Length; j++)
                {
                    // Set a different background for the text area
                    BackgroundColor = ConsoleColor.White; // Ensure text background is different from checkbox
                    buffer.UpdateCell(X + j + 2, Y + i * 2, optionTextOnly[j], ForegroundColor, BackgroundColor);
                }
            }
        }
        
        public override void RemoveFromBuffer(TerminalBuffer? buffer)
        {
            if (buffer == null) return;

            var keys = _options.Keys.ToList();

            // Erase each option (clear previously rendered checkbox and text)
            for (int i = 0; i < keys.Count; i++)
            {
                string optionText = _options[keys[i]];
                string displayText = _selectedIndexes.Contains(keys[i]) ? $"✔ {optionText}" : $"  {optionText}";

                // Erase the checkbox and text separately (fill with spaces)
                // Clear the checkbox part
                buffer.UpdateCell(X, Y + i * 2, ' ', ParentForegroundColor, ParentBackgroundColor); // Clear the checkbox

                // Clear the text part (after the checkbox) and make sure to clear the right number of characters
                for (int j = 0; j < displayText.Length; j++)
                {
                    buffer.UpdateCell(X + j, Y + i * 2, ' ', ParentForegroundColor, ParentBackgroundColor);
                }

                // This ensures that the space after the checkbox is cleared completely
                // For this, we pad the entire line to the expected length of the full option, clearing any leftover spaces.
                int fullLineLength = _labelWidth + 3; // 3 for the checkbox and space after it
                int remainingLength = fullLineLength - displayText.Length;
                for (int j = 0; j < remainingLength; j++)
                {
                    buffer.UpdateCell(X + displayText.Length + j, Y + i * 2, ' ', ParentForegroundColor, ParentBackgroundColor);
                }
            }
        }

    }
}
