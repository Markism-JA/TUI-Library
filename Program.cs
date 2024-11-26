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
            int windowHeight = 50;
            int windowWidth = 200;
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
            window.DrawCentered(asciiArt, 3, ConsoleColor.Black); // Draw ASCII art starting 3 lines from the top
            window.Render();
             
            Thread.Sleep(3000);
            window.DrawCentered(asciiArt, 8, ConsoleColor.Black);
            window.Render();
            
            Thread.Sleep(30000);
            window.DrawCentered(asciiArt, 13, ConsoleColor.Black);
            window.Render();
            
            Thread.Sleep(30000);
            window.DrawCentered(welcome, 18, ConsoleColor.Black);
            window.Render();
            
            //window.Draw(testContent1,4,3,ConsoleColor.Black);
            //  window.Render();            
            // Pause for 5 seconds
            Console.ReadKey();
        }
    }
}