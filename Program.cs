namespace TUI
{
    class Program
    {
        public static void Test2()
        {
            Console.Clear();
            int windowHeight = 37;
            int windowWidth = 100;
            //int windowX = 126; - absolute size of terminal
            //int windowY = 47; - absolute size of terminal
            Console.SetWindowSize(windowWidth, windowHeight);
            Window window = new Window(null, null, windowWidth, windowHeight, ConsoleColor.Magenta);
            
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
            
            // window.GridOn = true;
            window.BorderOn = true;
            window.BorderBackgroundColor = ConsoleColor.White;
            window.BorderForegroundColor = ConsoleColor.Black;
            Title game = new Title(asciiArt, 3, null,null, null,Align.Center);
            for (int i = 0; i < 5; i++)
            {
                window.AddChild(game);
                window.RenderAll();
            
                Thread.Sleep(3000);
                window.RemoveChild(game);
                window.RenderAll();
                Thread.Sleep(3000);
            }
            
            window.AddChild(game);
            Title subtext = new Title(welcome, 9, null,ConsoleColor.Black, ConsoleColor.Magenta, Align.Center);
            window.AddChild(subtext);
            window.RenderAll();

            // var label1 = new Label("Whatsup!", 20, 22, null, null, Align.Center); 
            // window.AddChild(label1);    
            
            //Console.WriteLine($"This is the width {Console.WindowWidth} and height {Console.WindowHeight}" );
            
            Console.ReadKey();
        }

        public static void Main(string[] args)
        {
            Test2();
        }
    }
}