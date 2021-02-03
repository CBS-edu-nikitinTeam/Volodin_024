using System;
using System.Threading;

namespace Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 80;


            for (int i = 0; i < 80; i++)
            {
                new Thread(() =>
                {
                    new MatrixWindow(i).Move();
                }).Start();
            }
        }

        public class MatrixWindow
        {
            protected static string RandomCharacter => GetRandomLetter();

            private static readonly object Locker = new object(); // = new object();
            private readonly int _wHeight; // window size Height

            /// <summary>
            /// Set size of console window
            /// </summary>
            /// <param name="wHeight">Height window</param>
            public MatrixWindow(int wHeight)
            {
                _wHeight = wHeight;
            }

            public void StartPrintMovingChains(int count)
            {
                Thread[] lettersThreads = new Thread[count];

                for (int i = 0; i < lettersThreads.Length; i++)
                {
                    lettersThreads[i] = new Thread(Move) { IsBackground = true };
                    lettersThreads[i].Start();
                }
            }

            public void Move() //Метод отображения одной цепочки
            {
                int left = _wHeight;
                System.Random rand = new System.Random();
                int lenght; //длина цепочки.
                int count; // счетчик цепочки.
                while (true)
                {
                    count = rand.Next(3, 6); //Метод возвращает случайное значение в указаном промежутке
                    lenght = 0;
                    Thread.Sleep(
                        GetRandomTimeSleep()); //Останавливаем поток на время полученое выполнением метода GetRandomTimeSleep
                    for (int i = 0; i < 40; i++) //Цикл со счетчиком
                    {
                        lock (Locker) //Блокирует блок кода, предназначен для контроля доступа к критической секции
                        {
                            Console.CursorTop = 0; //Устанавливае курсор в указанное положение
                            Console.ForegroundColor = ConsoleColor.Black; //Устанавливает цвет
                            for (int j = 0; j < i; j++)
                            {
                                Console.CursorLeft = left; //Устанавливаем позицию столбца курсора
                                Console.WriteLine("█");
                            }

                            if (lenght < count)
                                lenght++; //Увеличение длинны цепочки
                            else if (lenght == count) //Если длинна цепочки достигла требуемой обнуляем переменную count 
                                count = 0;

                            if (39 - i < lenght)
                                lenght--; //Уменьшение длинны цепочки по достижении нижней границы консоли
                            Console.CursorTop = i - lenght + 1; //Устанавливаем координату курсора от верхнего края консоли
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            for (int j = 0; j < lenght - 2; j++)
                            {
                                Console.CursorLeft = left; //Устанавливаем позицию курсора в рядке
                                Console.WriteLine(RandomCharacter); //Вызов метода GetChar
                            }

                            if (lenght >= 2) //Для каждого второго символа в цепочке
                            {
                                Console.ForegroundColor = ConsoleColor.Green; //устанавливаем цвет
                                Console.CursorLeft = left; //и позицию в столбце
                                Console.WriteLine(RandomCharacter);
                            }

                            if (lenght >= 1) //Для каждого первого символа в цепочке
                            {
                                Console.ForegroundColor = ConsoleColor.White; //Устанавливаем цвет
                                Console.CursorLeft = left; //и позицию в столбце
                                Console.WriteLine(RandomCharacter);
                            }

                            Thread.Sleep(20);
                        }
                    }
                }
            }

            private static int GetRandomTimeSleep()
            {
                System.Random rnd = new System.Random();
                return rnd.Next(10, 20000);
            }

            private static string GetRandomLetter()
            {
                string[] alphabet =
                {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U",
                "V", "W", "X", "Y", "Z"
            };

                System.Random rnd = new System.Random();

                int randomNumber = rnd.Next(0, alphabet.Length);

                return alphabet[randomNumber];
            }
        }
    }
}
