namespace TUI;

public class Label : Widgets
{
    public string Text { get; set; }
    public ConsoleColor TextColor { get; set; } = ConsoleColor.White;

    public Label(string text, int width, int height)
    {
        Text = " " + text + " ";
        Width = width;
        Height = height;
    }

    public override void AddToBuffer(TerminalBuffer? buffer)
    {
        if (buffer == null) return;

        // Split the text into lines
        string[] lines = Text.Split('\n');

        for (int i = 0; i < lines.Length && i < Height; i++)
        {
            string line = lines[i];
            for (int j = 0; j < line.Length && j < Width; j++)
            {
                buffer.UpdateCell(GlobalX + j, GlobalY + i, line[j], TextColor, ConsoleColor.Black);
            }
        }
    }
}