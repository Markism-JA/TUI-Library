namespace TUI;

abstract class Widgets
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Widgets? Parent{ get; set; }
    
    public virtual int GlobalX => (Parent?.GlobalY ?? 0) + X;
    public virtual int GlobalY => (Parent?.GlobalY ?? 0) + Y;
    public abstract void Render(TerminalBuffer? buffer);
}