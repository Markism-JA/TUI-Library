namespace TUI;

public class Label : DisplayWidget
{
    public Label(string text, int? x, int y, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, Align? align = null)
    {
        Content = new[] { " " + text + " " };
        X = x ?? 0;
        Y = y;
        Width = Content.Max(line => line.Length);
        Height = Content.Length;
        ForegroundColor = foregroundColor;
        BackgroundColor = backgroundColor;
        this.align = align ?? Align.Left;
    }
}