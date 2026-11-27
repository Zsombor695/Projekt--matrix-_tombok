
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
namespace projekt
{
    internal class Program
    {
        static string[,] matrix = new string[20, 30];
        static bool[,] enemies = new bool[20, 30];
        static List<int[]> bullets = new List<int[]>();
        static Random re = new Random();
        static int maxSor = 0;
        static int jatekosX = 14;
        static int jatekosY = 18;
        static int pont = 0;
        static int timer = 6000;
        static int  Irany = 1;
        static bool jatekVege = false;
        static bool gyozelem = false;
        static void Start()
        {
            Console.WriteLine("------------Welcome to Space Invaders!------------");
            Console.WriteLine("Press any key to start the game...");
            Console.ReadKey(true);
        }
        static void Feltoltes()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = "   ";
                }
            }
            for (int i = 2; i < matrix.GetLength(1) - 2; i += 4)
            {
                enemies[0, i] = true;
                enemies[1, i] = true;
                enemies[2, i] = true;
                enemies[3, i] = true;
            }
            modosit();
        }
        static void Jatek()
        {
            Console.Clear();
            Stopwatch ora = Stopwatch.StartNew();
            long utolsoGyorsEsemeny = 0;
            long utolsoLassuEsemeny = 0;
            long utolsoLassuEsemeny2 = 0;
            const int gyorsKozMs = 200;
            const int lassuKozMs = 3000;
            const int lassuKozMs2 = 1000;
            while (!jatekVege)
            {
                long most = ora.ElapsedMilliseconds;
               
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo billentyu = Console.ReadKey(true);
                    if (billentyu.Key == ConsoleKey.LeftArrow)
                    {
                        if (jatekosX > 0)
                            jatekosX--;
                    }
                    if (billentyu.Key == ConsoleKey.RightArrow)
                    {
                        if (jatekosX < matrix.GetLength(1) - 1)
                            jatekosX++;
                    }
                    if (billentyu.Key == ConsoleKey.Spacebar)
                    {
                        loves();
                    }
                }
               
                if (most - utolsoGyorsEsemeny >= gyorsKozMs)
                {
                    GolyoMozgas();
                    modosit();
                    Kirajzolas();
                    utolsoGyorsEsemeny = most;
                }
                if (most - utolsoLassuEsemeny >= lassuKozMs)
                {
                    EllensegMozgas();
                    utolsoLassuEsemeny = most;

                }
                if (most - utolsoLassuEsemeny2 >= lassuKozMs2)
                {
                    timer--;
                    utolsoLassuEsemeny2 = most;

                }
                Thread.Sleep(5);
            }
            Console.Clear();
            if (gyozelem)
            {
                Console.WriteLine("YOU WIN!");
            }
            else
            {
                Console.WriteLine("GAME OVER!");
            }
            Console.WriteLine("Your score: " + pont);
            Console.WriteLine("Press any key to exit...");

            Console.ReadKey(true);
        }
        static void modosit()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = "   ";
                }
            }
            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    if (enemies[i, j])
                    {
                        matrix[i, j] = "<X>";
                    }
                }
            }
            foreach (int[] golyo in bullets)
            {
                matrix[golyo[1], golyo[0]] = " | ";
            }
            matrix[jatekosY, jatekosX] = " A ";
        }
        static void loves()
        {
            if (jatekosY > 0)
            {
                bullets.Add(new int[] { jatekosX, jatekosY - 1 });
            }
        }
        static void GolyoMozgas()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                int x = bullets[i][0];
                int y = bullets[i][1];
                y--;
                if (y < 0)
                {
                    bullets.RemoveAt(i);
                    continue;
                }
                if (enemies[y, x])
                {
                    enemies[y, x] = false;
                    bullets.RemoveAt(i);
                    pont += 10;
                    continue;
                }
                bullets[i][1] = y;
            }
            int ellensegek = 0;
            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    if (enemies[i, j])
                        ellensegek++;
                }
            }
            if (ellensegek == 0)
            {
                gyozelem = true;
                jatekVege = true;
            }
            if (timer <= 0)
            {
                jatekVege = true;
            }
        }
        static void EllensegMozgas()
        {
            bool szel = false;
            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    if (enemies[i, j])
                    {
                        if (j + Irany < 0 ||
                            j + Irany >= enemies.GetLength(1))
                        {
                            szel = true;
                        }
                    }
                }
            }
            if (szel)
            {
                Irany *= -1;
            }
            bool[,] ujEnemies = new bool[20, 30];
            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    if (enemies[i, j])
                    {
                        int ujX = j + Irany;
                        int ujY = i;
                        if (szel)
                        {
                            ujY++;
                        }
                        // Ha az ellenseg eleri a jatekost
                        if (ujY >= jatekosY)
                        {
                            jatekVege = true;
                            continue;
                        }
                        ujEnemies[ujY, ujX] = true;
                    }
                }
            }
            enemies = ujEnemies;
        }
        static void Kirajzolas()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("SCORE: " + pont + "\t" + "Time: " + timer);
            Console.WriteLine("-----------------------------------------------");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Start();
            Feltoltes();
            Jatek();
            
            Console.CursorVisible = true;
        }
    }
}
