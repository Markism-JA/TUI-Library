namespace TUI
{
    class Program
    {
        public static void Test2()
        {
            
        }

        public static void Main(string[] args)
        {
            Console.Clear();
            int windowHeight = 37;
            int windowWidth = 100;
            
            //int windowX = 126; - absolute size of terminal
            //int windowY = 47; - absolute size of terminal
            Window window = new Window(windowWidth, windowHeight,ConsoleColor.Green);
            
            string[] asciiArt =
            {
                "▗▄▄▄ ▗▄▄▖ ▗▄▄▄▖ ▗▄▄▖ ▗▄▄▖▗▄▄▄▖▗▖  ▗▖ ▗▄▄▖     ▗▄▄▖ ▗▄▖ ▗▖  ▗▖▗▄▄▄▖",
                "▐▌  █▐▌ ▐▌▐▌   ▐▌   ▐▌     █  ▐▛▚▖▐▌▐▌       ▐▌   ▐▌ ▐▌▐▛▚▞▜▌▐▌   ",
                "▐▌  █▐▛▀▚▖▐▛▀▀▘ ▝▀▚▖ ▝▀▚▖  █  ▐▌ ▝▜▌▐▌▝▜▌    ▐▌▝▜▌▐▛▀▜▌▐▌  ▐▌▐▛▀▀▘",
                "▐▙▄▄▀▐▌ ▐▌▐▙▄▄▖▗▄▄▞▘▗▄▄▞▘▗▄█▄▖▐▌  ▐▌▝▚▄▞▘    ▝▚▄▞▘▐▌ ▐▌▐▌  ▐▌▐▙▄▄▖",
            };

            string[] welcome =
            {
                "Welcome to TUI",
                "This is the first iteration test",
                "Please work"
            };

            
            window.GridOn = true;
            window.DrawCentered(asciiArt, 3, ConsoleColor.Black);

            Thread.Sleep(3000);
            window.DrawCentered(welcome, 9, ConsoleColor.Black);
            window.RenderAll();

            var label1 = new Label("Whatsup!", 20, 3) { X = 30, Y = 23 };
            window.AddChild(label1);
            
            window.RenderAll();
            
            Console.WriteLine($"This is the width {Console.WindowWidth} and height {Console.WindowHeight}" );
            
            Console.ReadKey();
        }
    }
}