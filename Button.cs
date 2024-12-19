namespace TUI
{
    public class Button : InteractiveWidget
    {
        private string _label;
        private bool _isSelected;
        private int _delay;
        public bool IsPressed { get; private set; } // Indicates whether the button has been pressed
        public ConsoleColor DefaultColor { get; set; } = ConsoleColor.White;
        public ConsoleColor HighlightColor { get; set; } = ConsoleColor.Yellow;

        public Button(string label, int x, int y, int width, int height, int delay) 
            : base(x, y, width, height)
        {
            _label = label;
            _delay = delay;
            _isSelected = false;
        }

        public override void HandleInput(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                IsPressed = true;
                _isSelected = false;
            }
        }

        public override void Navigate(ConsoleKey key)
        {
            throw new NotImplementedException();
        }

        public override void AddToBuffer(TerminalBuffer? buffer)
        {
            if (buffer == null) return;

            // Set the button color based on whether it's selected or not
            ConsoleColor foreground = _isSelected ? HighlightColor : DefaultColor;

            // Center align the label text
            int padding = (Width - _label.Length) / 2;
            string paddedLabel = _label.PadLeft(_label.Length + padding).PadRight(Width);

            // Render the label in the buffer
            for (int i = 0; i < paddedLabel.Length; i++)
            {
                int xPos = X + i;
                int yPos = Y;

                // Ensure we're within the buffer's dimensions
                if (xPos < buffer.Width && yPos < buffer.Height)
                {
                    buffer.UpdateCell(xPos, yPos, paddedLabel[i], ConsoleColor.Black, foreground);
                    
                }
            }
        }

        public override void RemoveFromBuffer(TerminalBuffer? buffer)
        {
            if (buffer == null) return;

            // Clear the button from the buffer (fill with spaces)
            for (int i = 0; i < Width; i++)
            {
                int xPos = X + i;
                int yPos = Y;

                // Ensure we're within the buffer's dimensions
                if (xPos < buffer.Width && yPos < buffer.Height)
                {
                    buffer.UpdateCell(xPos, yPos, ' ', ParentForegroundColor, ParentBackgroundColor);
                }
            }
        }
    }
}
