namespace TUI;

public class Title : DisplayWidget
{
    public Title(string[] text, int y, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, Align? align = null, int x = 0)
    {
        Content = text;
        Y = y;
        X = x;
        ForegroundColor = foregroundColor;
        BackgroundColor = backgroundColor;
        this.align = align ?? Align.Left;

        Width = text.Length > 0 ? text.Max(line => line.Length) : 0;
        Height = text.Length;
    }
}