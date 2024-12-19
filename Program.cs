namespace TUI
{
    class Test
    {
        public static int windowHeight = 37;
        public static int windowWidth = 100;
        public static void Test2()
        {
            Console.Clear();
            windowHeight = 37;
            windowWidth = 100;
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

        public static void Test3()
        {
            Window window = new Window(null, null, windowWidth, windowHeight, null)
            {
                BorderOn = true,
                BorderHorizontal = '*',
                BorderVertical = '*',
                BorderBackgroundColor = ConsoleColor.White,
                BorderForegroundColor = ConsoleColor.Black,
                // GridOn = true
            };
            
            string[] asciiArt =
            {
                "▗▄▄▄ ▗▄▄▖ ▗▄▄▄▖ ▗▄▄▖ ▗▄▄▖▗▄▄▄▖▗▖  ▗▖ ▗▄▄▖     ▗▄▄▖ ▗▄▖ ▗▖  ▗▖▗▄▄▄▖",
                "▐▌  █▐▌ ▐▌▐▌   ▐▌   ▐▌     █  ▐▛▚▖▐▌▐▌       ▐▌   ▐▌ ▐▌▐▛▚▞▜▌▐▌   ",
                "▐▌  █▐▛▀▚▖▐▛▀▀▘ ▝▀▚▖ ▝▀▚▖  █  ▐▌ ▝▜▌▐▌▝▜▌    ▐▌▝▜▌▐▛▀▜▌▐▌  ▐▌▐▛▀▀▘",
                "▐▙▄▄▀▐▌ ▐▌▐▙▄▄▖▗▄▄▞▘▗▄▄▞▘▗▄█▄▖▐▌  ▐▌▝▚▄▞▘    ▝▚▄▞▘▐▌ ▐▌▐▌  ▐▌▐▙▄▄▖",
                "                                                                  "
            };
            Title game = new Title(asciiArt, null, 8,null, null,Align.Center);
            var menuItems = new Dictionary<int, string>()
            {
                {1, "New Game"},
                {2, "Load Game"},
                {3, "Campaign"},
                {4, "Credits"},
                {5, "Exit"},
            };
            var menu = new Menu(menuItems, 40, 15, 20, 10);
            window.AddChild(game);
            ConsoleKey key;
            Console.CursorVisible = false;
            window.AddChild(menu);
            window.RenderAll();
            
            
            window.RemoveChild(menu);
            window.RenderAll();
                        Thread.Sleep(2000);

        }

        public static void Test4()
        {
            Window window = new Window(null, null, windowWidth, windowHeight, null);
            // window.BorderOn = true;
            window.GridOn = true;
            window.BorderHorizontal = '*';
            window.BorderVertical = '*';
            var options = new Dictionary<int, string>
            {
                { 0, "Pizza" },
                { 1, "Burger" },
                { 2, "Pasta" },
                { 3, "Salad" }
            };

            var checkBox = new CheckBox(options, 5, 5, 10, 3);
            Console.CursorVisible = false;
            window.AddChild(checkBox);
            ConsoleKey key;
            do
            {
                window.RenderAll();
                key = Console.ReadKey(true).Key;
                checkBox.HandleInput(key);

            } while (key != ConsoleKey.Escape);
        }
        
        public static void Test5()
        {
            // Create a new window with the specified dimensions
            Window window = new Window(null, null, windowWidth, windowHeight, null);

            // Enable grid display and set border characters
            window.GridOn = true;
            window.BorderHorizontal = '*';
            window.BorderVertical = '*';

            // Create a TextBox instance (let's say it's 30 characters wide and 3 lines tall)
            TextBox textBox = new TextBox(5, 5, 30, 3);  // X, Y, Width, Height

            // Add the TextBox to the window's child elements
            window.AddChild(textBox);

            // Hide the cursor to prevent it from showing during input (optional)
            Console.CursorVisible = false;

            // Declare keyInfo outside the loop to use it for checking the key input
            ConsoleKeyInfo keyInfo;

            do
            {
                // Render the window and all its children (including the TextBox)
                window.RenderAll();

                // Read the key pressed (do not show on screen, capture key info)
                keyInfo = Console.ReadKey(true);  // Capture the key info, including modifiers

                // Pass the input to the TextBox to handle it (such as typing and cursor movement)
                textBox.HandleInput(keyInfo);

                // Optionally, handle any other logic (like updating the window title, etc.)
                // window.UpdateTitle("Input received...");

            } while (keyInfo.Key != ConsoleKey.Escape); // Exit when the Escape key is pressed
        }
        
        static void Test6()
        {
            // Create the main window
            Window mainWindow = new Window(0, 0, 100, 37)
            {
                // BorderOn = true,
                GridOn = true,
                BorderHorizontal = '*',
                BorderVertical = '*',
                BorderBackgroundColor = ConsoleColor.White,
                BorderForegroundColor = ConsoleColor.Black,
            };

            // Create a child window
            ChildWindow childWindow = new ChildWindow(5, 5, 30, 10);
            childWindow.BorderOn = true;

            // Add child window to main window

            mainWindow.RenderAll();
            // mainWindow.RenderAll();
        }

        static void Test7()
        {
            Window mainWindow = new Window(0, 0, 100, 37)
            {
                BorderOn = true,
                BorderHorizontal = '*',
                BorderVertical = '*',
                BorderBackgroundColor = ConsoleColor.White,
                BorderForegroundColor = ConsoleColor.Black,
            };

            Pane pane = new Pane(5, 5, 90, 30, ConsoleColor.Cyan)
            {
                BorderOn = true,
                BorderHorizontal = ' ',
                BorderVertical = ' ',
                BorderBackgroundColor = ConsoleColor.White,
                BorderForegroundColor = ConsoleColor.Black,
            };

            string[] campaignTitle =
            [
                "┏┓         •    ",
                "┃ ┏┓┏┳┓┏┓┏┓┓┏┓┏┓",
                "┗┛┗┻┛┗┗┣┛┗┻┗┗┫┛┗",
                "       ┛     ┛  "
            ];
            
            string[] campaignStory =
            {
                "In the bustling city of Sirius, where style was everything and the streets buzzed",
                "with fashionistas, a hidden boutique held a secret that could completely change the",
                "game of fashion forever. Tucked away on a quiet street, Mystic Atelier appeared",
                "like an ordinary shop, but its magical aura set it apart from anything else.",
                "Yet, its enchanted fitting room whispered promises of magical transformation,",
                "transforming ordinary into extraordinary. Legend had it that anyone brave enough to",
                "step inside and complete the boutique's trial would emerge as a new person with",
                "an unbelievable sense of fashion and style. Not only would they possess an",
                "unparalleled sense of style, but they would also radiate confidence and creativity",
                "like never before, standing out in any crowd with grace and poise.",
                "",
                "One day, while looking for a job, a curious visitor stumbled upon the boutique.",
                "With a heart full of questions and curiosity, they pushed open the door and were",
                "greeted by a mysterious shopkeeper whose smile seemed to know everything without",
                "a word. \"Welcome,\" the shopkeeper said with a gentle and reassuring voice. \"Inside",
                "this wardrobe, your creativity will come alive. Are you ready to master the art",
                "of fashion and style?\" A sudden sense of excitement gushed like the wind, filling",
                "the room with possibilities.",
                "",
                "Intrigued, the visitor stepped into the fitting room, where walls of sparkling light",
                "revealed endless possibilities. Floating challenges appeared before them: mix and",
                "match pieces to create bold themes, layer textures to perfection, and accessorize to",
                "elevate every design to a new level. Each choice mattered, from bold patterns to",
                "subtle hues, and every completed look felt like unlocking a new level of artistic",
                "mastery. With each completed look, they unlocked a deeper understanding of the",
                "artistry behind styling, gaining confidence in their choices. As they immersed ",
                "themselves in the trials, they realized this wasn’t just a game—it was a life-changing ",
                "journey to master the art of styling and discover the power of true self-expression."
            };

            Typewritter campaignType = new Typewritter(campaignStory, 6, 6, 30, 90,5,ConsoleColor.Black, ConsoleColor.Cyan);


            Title title = new Title(campaignTitle, null,1, null, null, Align.Center);
            
            mainWindow.AddChild(title);
            mainWindow.AddChild(pane);
            
            mainWindow.RenderAll();
            
            Thread.Sleep(50);
            mainWindow.AddChild(campaignType);
            mainWindow.RenderAll();
            
            mainWindow.RemoveChild(campaignType);
            Console.SetCursorPosition(33, 35);
            Console.WriteLine("Press any key to go back to Main Menu...");
            mainWindow.RenderAll();
            Console.ReadKey();
        }

        static void Test8()
        {
            
            string[] art = {
                " ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄ ",
                "▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌",
                "▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀█░▌▀▀▀▀█░█▀▀▀▀  ▀▀▀▀█░█▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀ ",
                "▐░▌          ▐░▌       ▐░▌▐░▌          ▐░▌       ▐░▌    ▐░▌          ▐░▌     ▐░▌          ",
                "▐░▌          ▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄▄▄ ▐░▌       ▐░▌    ▐░▌          ▐░▌     ▐░█▄▄▄▄▄▄▄▄▄ ",
                "▐░▌          ▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░▌       ▐░▌    ▐░▌          ▐░▌     ▐░░░░░░░░░░░▌",
                "▐░▌          ▐░█▀▀▀▀█░█▀▀ ▐░█▀▀▀▀▀▀▀▀▀ ▐░▌       ▐░▌    ▐░▌          ▐░▌      ▀▀▀▀▀▀▀▀▀█░▌",
                "▐░▌          ▐░▌     ▐░▌  ▐░▌          ▐░▌       ▐░▌    ▐░▌          ▐░▌               ▐░▌",
                "▐░█▄▄▄▄▄▄▄▄▄ ▐░▌      ▐░▌ ▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄█░▌▄▄▄▄█░█▄▄▄▄      ▐░▌      ▄▄▄▄▄▄▄▄▄█░▌",
                "▐░░░░░░░░░░░▌▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░▌▐░░░░░░░░░░░▌     ▐░▌     ▐░░░░░░░░░░░▌",
                " ▀▀▀▀▀▀▀▀▀▀▀  ▀         ▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀       ▀       ▀▀▀▀▀▀▀▀▀▀▀ ",
            };
            
            Title credits = new Title(art, 0, 3, null, null, Align.Center);
            Window mainWindow = new Window(0, 0, 100, 37)
            {
                BorderOn = true,
                BorderHorizontal = '*',
                BorderVertical = '*',
                BorderBackgroundColor = ConsoleColor.White,
                BorderForegroundColor = ConsoleColor.Black,
            };

            int paneLoc = 31;
            Pane pane1 = new Pane(paneLoc, 15, 18, 5, ConsoleColor.Cyan)
            {
                BorderOn = true,
                BorderHorizontal = '~',
                BorderVertical = '|',
                BorderBackgroundColor = ConsoleColor.White,
                BorderForegroundColor = ConsoleColor.Black,
            };

            Pane pane2 = new Pane(paneLoc, 21, 18, 5, ConsoleColor.Cyan)
            {
                BorderOn = true,
                BorderHorizontal = '~',
                BorderVertical = '|',
                BorderBackgroundColor = ConsoleColor.White,
                BorderForegroundColor = ConsoleColor.Black,
            };
            Pane pane3 = new Pane(paneLoc, 27, 18, 5, ConsoleColor.Cyan)
            {
                BorderOn = true,
                BorderHorizontal = '~',
                BorderVertical = '|',
                BorderBackgroundColor = ConsoleColor.White,
                BorderForegroundColor = ConsoleColor.Black,
            };

            string[] markText = new[]
            {
                "Mark Joseph D. Amarille"
            };
            string[] siludText = new[]
            {
                "Andrei Silud"
            };

            string[] vinoyaText = new[]
            {
                "Mark Chester Vinoya"
            };

            Label mark = new Label("Mark Joseph D. Amarille", 51, 27, ConsoleColor.Black, ConsoleColor.Green, Align.Left);
            Label silud = new Label("Luis Andrei Silud", 51, 15, ConsoleColor.Black, ConsoleColor.Green, Align.Left);
            Label chester = new Label("Mark Chester Vinoya", 51, 21, ConsoleColor.Black, ConsoleColor.Green, Align.Left);

            Label chesterCat = new Label("(⸝⸝ᴗ﹏ᴗ⸝⸝)ᶻᶻᶻ", 33, 23, ConsoleColor.Black, ConsoleColor.Cyan, Align.Left);
            Label markCat = new Label("ฅ/ᐠ.﹏.ᐟ\\ฅ", 34, 29, ConsoleColor.Black, ConsoleColor.Cyan, Align.Left);
            Label siludCat = new Label("/ᐠ-\u02d5-ᐟ\\", 35, 17, ConsoleColor.Black, ConsoleColor.Cyan, Align.Left);

            Label messageMark = new Label("Chillax lang", 50, 29, null, null, Align.Left);
            Label messageChester = new Label("Kagigising ko lang...", 50, 23, null, null, Align.Left);
            Label messageSilud = new Label("Nanatiling mabuting tao", 50, 17, null, null, Align.Left);


            
            mainWindow.AddChild(credits);
            mainWindow.AddChild(pane1);
            mainWindow.AddChild(pane2);
            mainWindow.AddChild(pane3);
            mainWindow.AddChild(silud);
            mainWindow.AddChild(chester);
            mainWindow.AddChild(mark);
            
            mainWindow.AddChild(chesterCat);
            mainWindow.AddChild(markCat);
            mainWindow.AddChild(siludCat);
            
            mainWindow.AddChild(messageMark);
            mainWindow.AddChild(messageChester);
            mainWindow.AddChild(messageSilud);

            
            mainWindow.RenderAll();

            
            Console.SetCursorPosition(33, 33);
            Console.WriteLine("Press any key to go back to Main Menu...");
            Console.ReadKey();
        }

        static void Test9()
        {
            ConsoleColor background = ConsoleColor.DarkYellow;
            ConsoleColor bar = ConsoleColor.White;
            Window mainWindow = new Window(0, 0, 100, 37, background)
            {
                BorderOn = true,
                // GridOn = true,
                BorderHorizontal = '*',
                BorderVertical = '*',
                BorderBackgroundColor = ConsoleColor.White,
                BorderForegroundColor = ConsoleColor.Black,
            };
            
            Label newlabel = new Label("Calculating Score...", 0, 16, ConsoleColor.Black, background, Align.Center);
            LoadingBar newloading = new LoadingBar(30, 17, 40, 1, null, bar, 30000);
            // mainWindow.AddChild(newloading);
            
            mainWindow.AddChild(newlabel);
            
            mainWindow.RenderAll();
            
            mainWindow.AddChild(newloading);
            mainWindow.RenderAll();
        }


        static void Test10()
        {
            Window mainWindow = new Window(0, 0, 100, 37)
            {
                BorderOn = true,
                BorderHorizontal = '*',
                BorderVertical = '*',
                BorderBackgroundColor = ConsoleColor.White,
                BorderForegroundColor = ConsoleColor.Black,
            };
            int width = 75;
            int height = 23;
            int x = 14;
            int y = 8;

            Pane basePane = new Pane(x, y, width, height, null);

            Pane bodyPane = new Pane(x, y + 4, width, 19, null);
            Pane separation1 = new Pane(x, y + 4, 33, 14, null);
            Pane separation2 = new Pane(x, y + 4, 13, 14, null);
            
            Pane separation3 = new Pane(x + 34, y + 4, 15, 14, null);

            Pane bottomPane = new Pane(x, y + 17, width, 6, null);
            
            mainWindow.AddChild(basePane);
            mainWindow.AddChild(bodyPane);
            mainWindow.AddChild(bottomPane);
            mainWindow.AddChild(separation1);
            mainWindow.AddChild(separation2);
            mainWindow.AddChild(separation3);
            mainWindow.RenderAll();
        }
        
        public static void Main()
        {
             //Test3();
            // Test4();
            // Test5();
            // Test6();
            // Test7();
            // Test8();
            // Test9();
            
            Test10();
            Console.ReadKey();
        }
    }   
}