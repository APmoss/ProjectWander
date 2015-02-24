using System;

namespace Project_IC {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (IC_Game game = new IC_Game())
            {
                game.Run();
            }
        }
    }
#endif
}

