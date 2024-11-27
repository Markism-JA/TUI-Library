namespace TUI;

public abstract class Widgets
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Widgets? Parent{ get; set; }
    public virtual int GlobalWidth => Parent?.Width ?? 0;
    public virtual int GlobalHeight => Parent?.Height ?? 0;
    public virtual int GlobalX => (Parent?.GlobalX ?? 0) + X;
    public virtual int GlobalY => (Parent?.GlobalY ?? 0) + Y;
    public abstract void AddToBuffer(TerminalBuffer? buffer);
}