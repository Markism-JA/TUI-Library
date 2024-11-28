    namespace TUI;

    public class ContainerWidget : Widgets
    {
        protected List<Widgets> Children { get; set; } = new List<Widgets>();

        public void AddChild(Widgets child)
        {
            child.Parent = this;
            Children.Add(child);
            child.IsRemoved = false;
        }
            
        public void RemoveChild(Widgets child)
        {
            if (Children.Contains(child))
            {
                child.IsRemoved = true;
            }
        }

        public override void AddToBuffer(TerminalBuffer? buffer)
        {
            foreach (var child in Children)
            {
                child.AddToBuffer(buffer);
            }
        }

        public override void RemoveFromBuffer(TerminalBuffer? buffer)
        {
            foreach (var child in Children)
            {
                child.RemoveFromBuffer(buffer);
            }
        }

            
    }