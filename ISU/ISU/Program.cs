using System;

namespace ISU
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MaleficentFire game = new MaleficentFire())
            {
                game.Run();
            }
        }
    }
#endif
}

