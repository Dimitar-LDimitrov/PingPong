namespace PingPong
{
    using System;
    using System.Threading;

    public class Program
    {
        static int p1PadSize = 4;
        static int p2PadSize = 4;
        static int ballPositionX = 0;
        static int ballPositionY = 0;
        static bool ballDirectionUp = true;
        static bool ballDirectionRight = true;
        static int p1Position = 0;
        static int p2Position = 0;
        static int p1Result = 0;
        static int p2Result = 0;
        static Random randomGenerator = new Random();

        static void RemoveScrollbars()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
            Console.CursorVisible = false;
        }

        static void DrawFirstPlayer()
        {
            for (int y = p1Position; y < p1Position + p1PadSize; y++)
            {
                PrintAtPosition(0, y, '|');
            }
        }

        static void DrawSecondPlayer()
        {
            for (int y = p2Position; y < p2Position + p2PadSize; y++)
            {
                PrintAtPosition(Console.WindowWidth - 3, y, '|');
            }
        }

        static void DrawBall()
        {
            PrintAtPosition(ballPositionX, ballPositionY, '*');
        }

        static void SetIntialPositions()
        {
            p1Position = Console.WindowHeight / 2 - p1PadSize / 2;
            p2Position = Console.WindowHeight / 2 - p2PadSize / 2;
            SetBallAtMiddleOfTable();
        }

        static void SetBallAtMiddleOfTable()
        {
            ballPositionX = Console.WindowWidth / 2;
            ballPositionY = Console.WindowHeight / 2;
        }

        static void PrintAtPosition(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        static void PrintResult()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 2, 0);
            Console.Write($"{p1Result}-{p2Result}");
        }

        static void MovePlOneDown()
        {
            if (p1Position < Console.BufferHeight - p1PadSize)
            {
                p1Position++;
            }
        }

        static void MovePlOneUp()
        {
            if (p1Position > 0)
            {
                p1Position--;
            }
        }

        static void MovePlTwoDown()
        {
            if (p2Position < Console.BufferHeight - p2PadSize)
            {
                p2Position++;
            }
        }

        static void MovePlTwoUp()
        {
            if (p2Position > 0)
            {
                p2Position--;
            }
        }

        static void SecondPlayerAIMove()
        {
            int randomNumber = randomGenerator.Next(1, 101);

            if (randomNumber < 70)
            {
                if (ballDirectionUp)
                {
                    MovePlTwoUp();
                }
                else
                {
                    MovePlTwoDown();
                }
            }
        }

        static void MoveBall()
        {
            if (ballPositionY == 0)
            {
                ballDirectionUp = false;
            }
            if (ballPositionY == Console.WindowHeight - 1)
            {
                ballDirectionUp = true;
            }

            if (ballDirectionUp)
            {
                ballPositionY--;
            }
            else
            {
                ballPositionY++;
            }

            if (ballDirectionRight)
            {
                ballPositionX++;
            }
            else
            {
                ballPositionX--;
            }

            if (ballPositionX == Console.WindowWidth - 2)
            {
                SetBallAtMiddleOfTable();
                ballDirectionUp = true;
                ballDirectionRight = false;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.Write("Player One Wins");
                p1Result++;
                PrintResult();
                Console.ReadKey();
            }
            if (ballPositionX == -1)
            {
                SetBallAtMiddleOfTable();
                ballDirectionUp = false;
                ballDirectionRight = true;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.Write("Player Two Wins");
                p2Result++;
                PrintResult();
                Console.ReadKey();
            }

            if (ballPositionX < 2)
            {
                if (ballPositionY >= p1Position && ballPositionY < p1Position + p1PadSize)
                {
                    ballDirectionRight = true;
                }
            }
            if (ballPositionX >= Console.WindowWidth - 3)
            {
                if (ballPositionY >= p2Position && ballPositionY < p2Position + p2PadSize)
                {
                    ballDirectionRight = false;
                }
            }
        }

        static void Main(string[] args)
        {
            RemoveScrollbars();
            SetIntialPositions();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        MovePlOneUp();
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        MovePlOneDown();
                    }
                }
                SecondPlayerAIMove();
                MoveBall();
                Console.Clear();
                DrawFirstPlayer();
                DrawSecondPlayer();
                DrawBall();
                PrintResult();
                Thread.Sleep(60);
            }
        }
    }
}

/*
 _________________________________________
|               1 - 0                     |
|                                         |
|                 *                       |
|                                         |
|                                       * |
|*                                      * |
|*                                      * |
|*                                      * |
|*                                        |
|                                         |
|                                         |
|_________________________________________|
 */
