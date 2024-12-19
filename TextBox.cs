namespace TUI
{
    public class TextBox : DisplayWidget
    {
        private string _text;
        private int _cursorPosition;
        private int _maxLength;
        public bool IsFinalized { get; set; } // Flag to indicate finalization

        public string Text { get => _text; set => _text = value; } 

        public TextBox(int x, int y, int width, int height, int maxLength = 50)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            _text = string.Empty;
            _cursorPosition = 0;
            _maxLength = maxLength;
            IsFinalized = false;
        }

        public void HandleInput(ConsoleKeyInfo keyInfo)
        {
            char inputChar = GetCharacterFromKeyInfo(keyInfo);

            if (keyInfo.Key == ConsoleKey.Backspace && _cursorPosition > 0 && !IsFinalized)
            {
                _text = _text.Remove(_cursorPosition - 1, 1);
                _cursorPosition--;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                FinalizeText(); // Mark text as finalized
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow && _cursorPosition > 0 && !IsFinalized)
            {
                _cursorPosition--;
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow && _cursorPosition < _text.Length && !IsFinalized)
            {
                _cursorPosition++;
            }
            else if (keyInfo.Key == ConsoleKey.Tab && _text.Length + 4 <= _maxLength && !IsFinalized)
            {
                _text = _text.Insert(_cursorPosition, "    ");
                _cursorPosition += 4;
            }
            else if (keyInfo.Key == ConsoleKey.Spacebar && _text.Length < _maxLength && !IsFinalized)
            {
                _text = _text.Insert(_cursorPosition, " ");
                _cursorPosition++;
            }
            else if (inputChar != '\0' && _text.Length < _maxLength && !IsFinalized)
            {
                _text = _text.Insert(_cursorPosition, inputChar.ToString());
                _cursorPosition++;
            }
        }

        private char GetCharacterFromKeyInfo(ConsoleKeyInfo keyInfo)
        {
            bool shiftPressed = (keyInfo.Modifiers & ConsoleModifiers.Shift) != 0;

            if (keyInfo.Key >= ConsoleKey.A && keyInfo.Key <= ConsoleKey.Z)
            {
                return shiftPressed ? char.ToUpper((char)keyInfo.Key) : char.ToLower((char)keyInfo.Key);
            }

            return keyInfo.Key switch
            {
                ConsoleKey.D1 => shiftPressed ? '!' : '1',
                ConsoleKey.D2 => shiftPressed ? '@' : '2',
                ConsoleKey.D3 => shiftPressed ? '#' : '3',
                ConsoleKey.D4 => shiftPressed ? '$' : '4',
                ConsoleKey.D5 => shiftPressed ? '%' : '5',
                ConsoleKey.D6 => shiftPressed ? '^' : '6',
                ConsoleKey.D7 => shiftPressed ? '&' : '7',
                ConsoleKey.D8 => shiftPressed ? '*' : '8',
                ConsoleKey.D9 => shiftPressed ? '(' : '9',
                ConsoleKey.D0 => shiftPressed ? ')' : '0',
                ConsoleKey.OemMinus => shiftPressed ? '_' : '-',
                ConsoleKey.OemPlus => shiftPressed ? '+' : '=',
                ConsoleKey.OemComma => shiftPressed ? '<' : ',',
                ConsoleKey.OemPeriod => shiftPressed ? '>' : '.',
                _ => keyInfo.KeyChar
            };
        }

        private void FinalizeText()
        {
            IsFinalized = true; // Mark as finalized
        }

        public void SetText(string text)
        {
            if (!IsFinalized)
            {
                _text = text.Length <= _maxLength ? text : text.Substring(0, _maxLength);
                _cursorPosition = _text.Length;
            }
        }

        public override void AddToBuffer(TerminalBuffer? buffer)
        {
            if (buffer == null) return;

            // Center the text if finalized, else display with cursor
            string displayText;
            if (IsFinalized)
            {
                int padding = Math.Max(0, (Width - _text.Length) / 2);
                displayText = _text.PadLeft(_text.Length + padding).PadRight(Width);
            }
            else
            {
                displayText = _text.PadRight(Width - 2);
            }

            // Set default colors
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.White;

            // Render the text
            for (int i = 0; i < displayText.Length; i++)
            {
                buffer.UpdateCell(X + i + 1, Y, displayText[i], ForegroundColor, BackgroundColor);
            }

            // Render the cursor if not finalized
            if (!IsFinalized && _cursorPosition < Width - 2)
            {
                buffer.UpdateCell(X + _cursorPosition + 1, Y, '_', ConsoleColor.Black, ConsoleColor.Yellow);
            }
        }

        public override void RemoveFromBuffer(TerminalBuffer? buffer)
        {
            if (buffer == null) return;

            for (int i = 0; i < Width - 2; i++)
            {
                buffer.UpdateCell(X + i + 1, Y, ' ', ParentForegroundColor, ParentBackgroundColor);
            }
        }
    }
}
