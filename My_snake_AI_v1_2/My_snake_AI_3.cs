using System;
using System.Collections.Generic;
using System.Drawing;
using PluginInterface;

namespace My_snake_AI_v1_2
{
    public class My_snake_AI_3 : ISmartSnake
    {
        public Move Direction { get; set; }
        public bool Reverse { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }

        private DateTime dt = DateTime.Now;
        private static Random rnd = new Random();

        // вводим переменную для камней
        List<Point> Mystones;

        public void Startup(Size size, List<Point> stones)
        {
            Name = "My_snake_AI_3";
            Color = Color.Blue;
            // заполняем нашу переменную камнями
            Mystones = stones;
        }

        public void Update(Snake snake, List<Snake> enemies, List<Point> food, List<Point> dead)
        {            
            // число врагов            
            int Count_wariors = enemies.Count;

            // Если есть враги
            if (enemies.Count != 0)
            {
                // переменная врага
                Snake Warior;
                // добавляем первого по списку врага и начинаем охоту
                Warior = enemies[rnd.Next(0, enemies.Count)];

                // если кого-то убили, то выберем нового противника рандомно
                if (Count_wariors != enemies.Count )
                {
                    Count_wariors = enemies.Count;
                    Warior = enemies[rnd.Next(0, enemies.Count)];

                    
                }
                // добавляем к камням мертвых, чтобы их тоже можно было огибать
                foreach (Point item in dead)
                {
                    Mystones.Add(item);
                }

                // отладка вывод координат
                Console.WriteLine("Position: {0} ", snake.Position );

                // 3 четверть (-,-) 1.1
                // сначала определяем , какое растояние больше по оси Х  или по оси Y
                if (snake.Position.X < Warior.Position.X | snake.Position.Y < Warior.Position.Y)
                {
                    // если дальше по Х то
                    if (snake.Position.X - Warior.Position.X < snake.Position.Y - Warior.Position.Y)
                    {
                        // если на пути направо нет камня И НЕТ ХВОСТА, двигаемся вправо, иначе по часовой (вниз)
                        if (!Mystones.Contains(new Point(snake.Position.X + 1, snake.Position.Y)) & !snake.Tail.Contains(new Point(snake.Position.X + 1, snake.Position.Y)))
                        {
                            Console.WriteLine("Move.Right\t1.1");
                            Direction = Move.Right;
                        }
                        // иначе по часовой (вниз)
                        else if (!Mystones.Contains(new Point(snake.Position.X, snake.Position.Y + 1)) & !snake.Tail.Contains(new Point(snake.Position.X , snake.Position.Y + 1)))
                        {
                            Console.WriteLine("Move.Down\t1.1");
                            Direction = Move.Down;
                        }
                        // проверяем возможность хода вверх
                        else if (!Mystones.Contains(new Point(snake.Position.X, snake.Position.Y - 1)) & !snake.Tail.Contains(new Point(snake.Position.X , snake.Position.Y - 1)))
                        {
                            Console.WriteLine("Move.Up\t1.1");
                            Direction = Move.Up;
                        }
                        // проверяем возможность хода вверх
                        else
                        {
                            Console.WriteLine("Reverse = true\t1.1");
                            Reverse = true;
                            Console.WriteLine("Reverse = true => Move.Up\t1.1");
                            Direction = Move.Up;
                        }
                    }
                    // если дальше по Y то 1.2
                    else if (snake.Position.X - Warior.Position.X > snake.Position.Y - Warior.Position.Y)
                    {
                        // если на пути вниз нет камня, двигаемся вниз, иначе по часовой (влево)
                        if (!Mystones.Contains(new Point(snake.Position.X, snake.Position.Y + 1)) & !snake.Tail.Contains(new Point(snake.Position.X, snake.Position.Y + 1)))
                        {
                            Console.WriteLine("Move.Down\t1.2");
                            Direction = Move.Down;
                        }
                        // иначе по часовой (влево)
                        else if (!Mystones.Contains(new Point(snake.Position.X - 1, snake.Position.Y )) & !snake.Tail.Contains(new Point(snake.Position.X - 1, snake.Position.Y)))
                        {
                            Console.WriteLine("Move.Left\t1.2");
                            Direction = Move.Left;
                        }                        
                        // проверяем возможность хода направо
                        else if (!Mystones.Contains(new Point(snake.Position.X + 1, snake.Position.Y)) & !snake.Tail.Contains(new Point(snake.Position.X + 1, snake.Position.Y)))
                        {
                            Console.WriteLine("Move.Right\t1.2");
                            Direction = Move.Right;
                        }
                        // проверяем возможность хода направо
                        else
                        {
                            Console.WriteLine("Reverse = true\t1.2");
                            Reverse = true;
                            Console.WriteLine("Reverse = true => Move.Right\t1.2");
                            Direction = Move.Right;
                        }

                    }
                }

                // 1 четверть (+,+) 1.3
                // сначала определяем , какое растояние больше по оси Х  или по оси Y
                if (snake.Position.X > Warior.Position.X | snake.Position.Y > Warior.Position.Y)
                {
                    // если дальше по Х то
                    if (snake.Position.X - Warior.Position.X > snake.Position.Y - Warior.Position.Y)
                    {
                        // если на пути налево нет камня, двигаемся влево, иначе по часовой (вверх)
                        if (!Mystones.Contains(new Point(snake.Position.X - 1, snake.Position.Y)) & !snake.Tail.Contains(new Point(snake.Position.X - 1, snake.Position.Y)))
                        {
                            Console.WriteLine("Move.Left\t1.3");
                            Direction = Move.Left;
                        }
                        // иначе по часовой (вверх)
                        else if (!Mystones.Contains(new Point(snake.Position.X , snake.Position.Y - 1)) & !snake.Tail.Contains(new Point(snake.Position.X, snake.Position.Y - 1)))
                        {
                            Console.WriteLine("Move.Up\t1.3");
                            Direction = Move.Up;
                        }
                         // проверяем возможность хода вниз
                        else if (!Mystones.Contains(new Point(snake.Position.X , snake.Position.Y + 1)) & !snake.Tail.Contains(new Point(snake.Position.X , snake.Position.Y + 1)))
                        {
                            Console.WriteLine("Move.Down\t1.3");
                            Direction = Move.Down;
                        }
                        // проверяем возможность хода вниз
                        else
                        {
                            Console.WriteLine("Reverse = true\t1.3");
                            Reverse = true;
                            Console.WriteLine("Reverse = true => Move.Down\t1.3");
                            Direction = Move.Down;
                        }
                    }
                    // если дальше по Y то 1.4
                    else if (snake.Position.X - Warior.Position.X < snake.Position.Y - Warior.Position.Y)
                    {
                        // если на пути налево нет камня, двигаемся вверх, иначе по часовой (вправо)
                        if (!Mystones.Contains(new Point(snake.Position.X, snake.Position.Y - 1)) & !snake.Tail.Contains(new Point(snake.Position.X , snake.Position.Y - 1)))
                        {
                            Console.WriteLine("Move.Up\t1.4");
                            Direction = Move.Up;
                        }
                        // иначе по часовой (вверх)
                        else if (!Mystones.Contains(new Point(snake.Position.X + 1, snake.Position.Y)) & !snake.Tail.Contains(new Point(snake.Position.X + 1, snake.Position.Y)))
                        {
                            Console.WriteLine("Move.Right\t1.4");
                            Direction = Move.Right;
                        }
                         // проверяем возможность хода налево
                        else if (!Mystones.Contains(new Point(snake.Position.X - 1, snake.Position.Y)) & !snake.Tail.Contains(new Point(snake.Position.X -1, snake.Position.Y)))
                        {
                            Console.WriteLine("Move.Left\t1.4");
                            Direction = Move.Left;
                        }
                        // проверяем возможность хода влево
                        else
                        {
                            Console.WriteLine("Reverse = true\t1.4");
                            Reverse = true;
                            Console.WriteLine("Reverse = true => Move.Left\t1.4");
                            Direction = Move.Left;
                        }

                    }
                }
            }
        }
    }
}
