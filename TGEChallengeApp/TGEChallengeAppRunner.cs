using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGEChallengeApp.DataAccess;
using TGEChallengeApp.Interfaces;

namespace TGEChallengeApp
{
    public class TGEChallengeAppRunner : ITGEChallengeApp
    {
        private static IPostcodeManager _postcodeManager;
        public TGEChallengeAppRunner(IPostcodeManager postcodeManager)
        {
            _postcodeManager = postcodeManager;
        }
        public void Run()
        {
            PrintBanner();

            while (true)
            {

                PrintMenu();

                    ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.L:
                         ListPostcodes();
                        break;
                    case ConsoleKey.X:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.N:
                        Console.WriteLine("Enter new postcodes - an empty input will submit:");
                        var input = ReadLinesFromUser();
                         _postcodeManager.AddNewPostcodes(input);
                        break;
                    case ConsoleKey.C:
                        try
                        {
                            _postcodeManager.GetPostcodeDistrictsAsync();
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        break;
                    case ConsoleKey.D:
                        Console.WriteLine("Enter a postcode to delete - an empty input will cancel: ");
                        var deleteInput = Console.ReadLine();
                        if (!string.IsNullOrEmpty(deleteInput))
                        {
                            _postcodeManager.DeletePostcode(deleteInput);
                        }
                        break;
                    case ConsoleKey.V:
                         _postcodeManager.ValidatePostcodes();
                        break;
                }

                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }
        }

        static async Task ListPostcodes()
        {
            var postcodes = await _postcodeManager.GetAllPostcodesAsync();
            Console.WriteLine("Listing postcodes: ");
            for (var i = 0; i < postcodes.Count(); i++)
            {
                Console.WriteLine($"{postcodes.ElementAt(i)}");
            }
          
        }

        public void PrintMenu()
        {
            Console.WriteLine(@"Welcome to the Postcode Manager Console App
===========================================
Menu Options:
L - List Postcodes
N - Enter new postcodes
C - Get Postcode Districts
D - Delete Postcode
V - Validate Postcodes
X - Exit");
        }
        static IEnumerable<string> ReadLinesFromUser()
        {
            var strings = new List<string>();
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    break;

                strings.Add(input);
            }
            return strings;
        }

        static void ReturnToMenu()
        {
            Console.WriteLine($"Press any key to return to the menu");
            Console.ReadLine();
        }
        static void PrintBanner()
        {

            Console.WriteLine(@"
 _______ _____            _   _  _____  _____ _      ____  ____          _      
 |__   __|  __ \     /\   | \ | |/ ____|/ ____| |    / __ \|  _ \   /\   | |     
    | |  | |__) |   /  \  |  \| | (___ | |  __| |   | |  | | |_) | /  \  | |     
    | |  |  _  /   / /\ \ | . ` |\___ \| | |_ | |   | |  | |  _ < / /\ \ | |     
    | |  | | \ \  / ____ \| |\  |____) | |__| | |___| |__| | |_) / ____ \| |____ 
    |_|  |_|  \_\/_/____\_\_|_\_|_____/_\_____|______\____/|____/_/    \_\______|
               |  ____\ \ / /  __ \|  __ \|  ____|/ ____/ ____|                  
  ______ ______| |__   \ V /| |__) | |__) | |__  | (___| (___ ______ ______      
 |______|______|  __|   > < |  ___/|  _  /|  __|  \___ \\___ \______|______|     
               | |____ / . \| |    | | \ \| |____ ____) |___) |                  
   _____ _    _|______/_/_\_\_| _  |_| _\_\______|_____/_____/___ ___  ____      
  / ____| |  | |   /\   | |    | |    |  ____| \ | |/ ____|  ____|__ \|___ \     
 | |    | |__| |  /  \  | |    | |    | |__  |  \| | |  __| |__     ) | __) |    
 | |    |  __  | / /\ \ | |    | |    |  __| | . ` | | |_ |  __|   / / |__ <     
 | |____| |  | |/ ____ \| |____| |____| |____| |\  | |__| | |____ / /_ ___) |    
  \_____|_|  |_/_/    \_\______|______|______|_| \_|\_____|______|____|____/ 
                                                                                 
                                                                                 
");
        }

    }
}
