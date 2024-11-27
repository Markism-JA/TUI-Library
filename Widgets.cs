namespace TUI;

public abstract class Widgets
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public ConsoleColor? BackgroundColor{get;set;}
    public ConsoleColor? ForegroundColor{get;set;}

    public Widgets? Parent{ get; set; }
    public virtual ConsoleColor? ParentBackgroundColor{get;set;}
    public virtual ConsoleColor? ParentForegroundColor{get;set;}
    public virtual int ParentWidth => Parent?.Width ?? 0;
    public virtual int ParentHeight => Parent?.Height ?? 0;
    public virtual int ParentCoordinateX => Parent?.ParentCoordinateX ?? 0;
    public virtual int ParentCoordinateY => Parent?.ParentCoordinateY ?? 0;
    public abstract void AddToBuffer(TerminalBuffer? buffer);
}