using System.Diagnostics;
using System.Numerics;

namespace projekt
{
    internal class Program
    {
        static char[] HajoPozicio = new char[30];
        static char[,] matrix = new char[20, 30];

        static void Feltoltes()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int ellenseg = Random.Shared.Next(0, 2);
                    if (ellenseg == 1 & i < 3)
                    {
                        matrix[i, j] = 'x';
                    }
                    else
                    {
                        matrix[i, j] = ' ';
                    }
                }
            }
        }
        static void Kiir()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("\t\t");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($" {matrix[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        static void modosit()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[matrix.GetLength(0) - 1, j] == ' ' | matrix[matrix.GetLength(0) - 1, j] == '|')
                    {
                        matrix[i + 1, j] = matrix[i, j];
                        matrix[i, j] = ' ';
                    }
                    else
                    {
                        //vereség helye
                        return;
                    }


                }
            }
        }

        static void Jatek()
        {
            int sorok = matrix.GetLength(0);
            int oszlopok = matrix.GetLength(1);
            int jatekosOszlop = oszlopok / 2;

            bool jatekFut = true;

            Stopwatch ora = Stopwatch.StartNew();
            Console.CursorVisible = false;

            long utolsoGyorsEsemeny = 0;

            long utolsoLassuEsemeny = 0;


            const int gyorsKozMs = 1000;
            const int lassuKozMs = 1000;

            while (jatekFut)
            {
                Console.SetCursorPosition(0, 0);
                long most = ora.ElapsedMilliseconds;


                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo gomb = Console.ReadKey(true);
                    if (gomb.Key == ConsoleKey.LeftArrow && jatekosOszlop > 0)

                    {
                        jatekosOszlop--;
                    }
                    else if (gomb.Key == ConsoleKey.RightArrow && jatekosOszlop < oszlopok - 1)
                    {
                        jatekosOszlop++;
                    }
                    else if (gomb.Key == ConsoleKey.UpArrow)
                    {
                        // Itt indíthattok el egy új lövedéket az aktuális oszlopból
                        matrix[matrix.GetLength(0) - 1, jatekosOszlop] = '|';
                    }
                    else if (gomb.Key == ConsoleKey.Escape)
                    {
                        jatekFut = false;
                    }
                }

                Kiir();
                modosit();
                if (most - utolsoGyorsEsemeny >= gyorsKozMs)
                {
                    modosit();
                    utolsoGyorsEsemeny = most;
                }
                else
                {
                    {
                        Console.Write("");

                        utolsoLassuEsemeny = most;

                    }

                    // Játékos sorának kirajzolása alulra
                    Console.Write("\t\t");
                    for (int o = 0; o < oszlopok; o++)
                    {
                        if (o == jatekosOszlop)
                            Console.Write(" A ");
                        else
                            Console.Write("   ");
                    }
                    Console.WriteLine();

                }
            }
        }
            static void Pelda()
            {
                const int sorok = 20;
                const int oszlopok = 40;

                char[,] palya = new char[sorok, oszlopok];
                char[] jatekosSor = new char[oszlopok];

                // Játékos kezdeti pozíciója középen
                int jatekosOszlop = oszlopok / 2;

                Stopwatch ora = Stopwatch.StartNew();
                long utolsoEllensegMozgás = 0;
                long utolsoLovedekMozgás = 0;

                const int ellensegKozMs = 500; // 0.5 mp-ként lép az ellenség
                const int lovedekKozMs = 150;  // Gyorsabb lövedék mozgás

                Console.CursorVisible = false;

                bool jatekFut = true;
                while (jatekFut)
                {
                    long most = ora.ElapsedMilliseconds;

                    // 1. Nem blokkoló billentyűzetkezelés
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo gomb = Console.ReadKey(true);
                        if (gomb.Key == ConsoleKey.LeftArrow && jatekosOszlop > 0)
                        {
                            jatekosOszlop--;
                        }
                        else if (gomb.Key == ConsoleKey.RightArrow && jatekosOszlop < oszlopok - 1)
                        {
                            jatekosOszlop++;
                        }
                        else if (gomb.Key == ConsoleKey.UpArrow)
                        {
                            // Itt indíthattok el egy új lövedéket az aktuális oszlopból

                        }
                        else if (gomb.Key == ConsoleKey.Escape)
                        {
                            jatekFut = false;
                        }
                    }

                    // 2. Ellenségek léptetése időzítővel
                    if (most - utolsoEllensegMozgás >= ellensegKozMs)
                    {
                        // TODO: Ellenségek lefelé mozgatása a mátrixban
                        utolsoEllensegMozgás = most;
                    }

                    // 3. Lövedékek léptetése időzítővel
                    if (most - utolsoLovedekMozgás >= lovedekKozMs)
                    {
                        // TODO: Lövedékek felfelé mozgatása és ütközésvizsgálat
                        utolsoLovedekMozgás = most;
                    }

                    // 4. Kirajzolás (Kurzor vissza a sarokba)
                    Console.SetCursorPosition(0, 0);

                    for (int s = 0; s < sorok; s++)
                    {
                        for (int o = 0; o < oszlopok; o++)
                        {
                            // Ha üres a mező, írjunk ki szóközt vagy alap karaktert
                            Console.Write(palya[s, o] == '\0' ? ' ' : palya[s, o]);
                        }
                        Console.WriteLine();
                    }

                    // Játékos sorának kirajzolása alulra
                    for (int o = 0; o < oszlopok; o++)
                    {
                        if (o == jatekosOszlop)
                            Console.Write('A');
                        else
                            Console.Write(' ');
                    }
                    Console.WriteLine();

                    // Kis szünet, hogy ne terhelje túl a CPU-t
                    System.Threading.Thread.Sleep(10);
                }
            }


            static void Main(string[] args)
            {
                Feltoltes();
                //Kiir();
                Jatek();
                //string hajo = "    /\\\n  |<==>|";
                //SajatUrhajo();
                //Pelda();
                //Jatek();
            }
        }
    }

/*
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
 */


