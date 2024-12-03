namespace TUI;

public class Title : DisplayWidget
{
    public Title(string[] text, int? x, int y, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, Align? align = null)
    {
        Content = text;
        Y = y;
        X = x ?? 0;
        ForegroundColor = foregroundColor;
        BackgroundColor = backgroundColor;
        this.align = align ?? Align.Left;

        Width = text.Length > 0 ? text.Max(line => line.Length) : 0;
        Height = text.Length;
    }
}