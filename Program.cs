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
                "                                                                  "
            };

            string[] welcome =
            {
                "Welcome to TUI",
                "This is the first iteration test",
                "Please work"
            };

            
            window.GridOn = true;
            Title game = new Title(asciiArt, 3, ConsoleColor.White, ConsoleColor.Black, Align.Center);
            window.AddChild(game);

            Thread.Sleep(3000);
            Title subtext = new Title(welcome, 9, ConsoleColor.Black, ConsoleColor.Green, Align.Center);
            window.AddChild(subtext);
            
            var label1 = new Label("Whatsup!", 20, 3, ConsoleColor.White, ConsoleColor.Black, Align.Center); 
            window.AddChild(label1);    
            window.RenderAll();
            
            //Console.WriteLine($"This is the width {Console.WindowWidth} and height {Console.WindowHeight}" );
            
            Console.ReadKey();
        }
    }
}