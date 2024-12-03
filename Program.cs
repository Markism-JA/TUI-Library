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
            Window window = new Window(null, null, windowWidth, windowHeight, null);
            
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
                "Welcome!",
                "Design your character or Load existing"
            };
            
            //window.GridOn = true;
            window.BorderOn = true;
            window.BorderBackgroundColor = ConsoleColor.White;
            window.BorderForegroundColor = ConsoleColor.Black;
            window.BorderHorizontal = '*';
            window.BorderVertical = '*';
            Title game = new Title(asciiArt, null, 8,null, null,Align.Center);
            var label1 = new Label(" Load Character ", null, 22, ConsoleColor.Black, ConsoleColor.White, Align.Center); 
            var label2 = new Label("Create Character", null, 24, ConsoleColor.Black, ConsoleColor.White, Align.Center);
            Title subtext = new Title(welcome,null,16,null, null, Align.Center);
            window.AddChild(subtext);
            window.AddChild(label1);
            window.AddChild(label2);
            for (int i = 0; i < 5; i++)
            {
                window.AddChild(game);
                window.RenderAll();
                Thread.Sleep(500);
                window.RemoveChild(game);
                window.RenderAll();
                Thread.Sleep(500);
            }
            
            window.AddChild(game);
            window.RenderAll();
            
            Thread.Sleep(500);

            // Console.SetCursorPosition(43,3);
            //left is x and top is y 
            
            //Console.WriteLine($"This is the width {Console.WindowWidth} and height {Console.WindowHeight}" );
            
            Console.ReadKey();
        }

        public static void Main(string[] args)
        {
            Test2();
        }
    }   
}