    namespace TUI;

    public class ContainerWidget : Widgets
    {
        protected List<Widgets> Children { get; set; } = new List<Widgets>();

        public void AddChild(Widgets child)
        {
            child.Parent = this;
            Children.Add(child);
        }
            
        public void RemoveChild(Widgets child)

        {
            child.Parent = this;
            Children.Remove(child);
        }

        public override void AddToBuffer(TerminalBuffer? buffer)
        {
            foreach (var child in Children)
            {
                child.AddToBuffer(buffer);
            }
        }

            
    }