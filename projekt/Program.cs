using System.Diagnostics;

namespace projekt
{
    internal class Program
    {
        static string[,] matrix = new string[20, 30];
        static Random re = new Random();
        static int maxSor = 0;
        static void Start()
        {
            Console.WriteLine("------------Welcome to Space Invaders!------------");
            Console.WriteLine("Press any key to start the game...");
            Console.ReadKey();
        }
        static void Feltoltes()
        {
            for (int i = 2; i < matrix.GetLength(1); i+=3)
            {
                matrix[0, i] = "<X>";

            }
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == null)
                    {
                        matrix[i, j] = "   ";
                    }
                }
            }
        }

        static void Jatek()
        {
            string Sajathajo = "👾";
            string ellenseghajo = "<X>";
            Console.Clear();
            Console.WriteLine("Game started!");
            // Game logic goes here
            Stopwatch ora = Stopwatch.StartNew();


            long utolsoGyorsEsemeny = 0;

            long utolsoLassuEsemeny = 0;


            const int gyorsKozMs = 1000;
            const int lassuKozMs = 1000;
            

            while (ora.ElapsedMilliseconds < 100000)

            {

                long most = ora.ElapsedMilliseconds;
                

                if (most - utolsoGyorsEsemeny >= gyorsKozMs)
                {
                    Console.Clear();
                    modosit();
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            Console.Write(matrix[i, j]);
                            
                        }
                        Console.WriteLine();
                    }
                    maxSor += 1;
                    
                    utolsoGyorsEsemeny = most;
                }
                if (most - utolsoLassuEsemeny >= lassuKozMs)
                {
                    Console.Write("");

                    utolsoLassuEsemeny = most;

                }
            }
            }

        private static void modosit()
        {
            if (maxSor <= 4)
            {
                for (int i = 2; i < matrix.GetLength(1); i += 3)
                {
                    matrix[0, i] = "<X>";

                }
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == null)
                        {
                            matrix[i, j] = "   ";
                        }
                    }
                }
            }
            
            for (int i = matrix.GetLength(0) -2; i >= 0; i--)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == "<X>")
                    {
                        matrix[i+1, j] = "<X>";
                        matrix[i, j] = " ";
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Start();
            Feltoltes();
            Jatek();
        }
    }
}
