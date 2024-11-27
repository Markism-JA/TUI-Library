namespace TUI;

public abstract class Widgets
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public ConsoleColor Background{get;set;}
    public ConsoleColor Foreground{get;set;}

    public Widgets? Parent{ get; set; }
    public virtual ConsoleColor ParentBackground{get;set;}
    public virtual ConsoleColor ParentForeground{get;set;}
    public virtual int ParentWidth => Parent?.Width ?? 0;
    public virtual int ParentHeight => Parent?.Height ?? 0;
    public virtual int ParentCoordinateX => Parent?.ParentCoordinateX ?? 0;
    public virtual int ParentCoordinateY => Parent?.ParentCoordinateY ?? 0;
    public abstract void AddToBuffer(TerminalBuffer? buffer);
}