namespace TUI;

public class Label : DisplayWidget
{

    public Label(string text, int x, int y, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, Align? align = null)
    {
        // Add spaces for padding around the text
        Content = new[] { " " + text + " " };
        X = x;
        Y = y;

        // Calculate the Width as the length of the longest string in Content
        Width = Content.Max(line => line.Length);

        // Set Height as the number of lines in Content
        Height = Content.Length;

        // Set default colors if not provided
        ForegroundColor = foregroundColor;
        BackgroundColor = backgroundColor ?? ConsoleColor.Black;
        this.align = align ?? Align.Left;
    }
}