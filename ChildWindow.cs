namespace TUI;

public class ChildWindow : Window
{
    public TerminalBuffer ChildBuffer { get; set; }

    public ChildWindow(int? x, int? y, int width, int height, ConsoleColor? backgroundColor = null)
        : base(x, y, width, height, backgroundColor)
    {
        ChildBuffer = new TerminalBuffer(width, height);
    }

    public override void RenderAll()
    {
        RenderChildContent();

        if (Parent is Window parentWindow)
        {
            parentWindow.ReconcileChildBuffer(ChildBuffer, X, Y);
        }

        foreach (var childWindow in ChildWindows)
        {
            childWindow.RenderAll();
        }
            
        AddBorder();
        
        FillWindow();            
    }

    private void RenderChildContent()
    {
        for (int i = 0; i < ChildBuffer.Width; i++)
        {
            for (int j = 0; j < ChildBuffer.Height; j++)
            {
                ChildBuffer.UpdateCell(i, j, ' ', ConsoleColor.Gray, BackgroundColor);
            }
        }
    }
    
}