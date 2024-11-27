namespace TUI;

public class Title : DisplayWidget
{
    public Title(string[] text, int y, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, Align? align = null, int x = 0)
    {
        Content = text;
        Y = y;
        X = x;
        ForegroundColor = foregroundColor ?? ConsoleColor.White;
        BackgroundColor = backgroundColor ?? ConsoleColor.Black;
        this.align = align ?? Align.Left;

        // Set Width to the length of the longest string in Content
        Width = text.Length > 0 ? text.Max(line => line.Length) : 0;

        // Set Height to the number of lines in Content
        Height = text.Length;
    }
}