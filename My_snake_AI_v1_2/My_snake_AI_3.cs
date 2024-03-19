using System;
using System.Collections.Generic;
using System.Drawing;
using PluginInterface;

namespace My_snake_AI_v1_3
{
    public class My_snake_AI_3 : ISmartSnake
    {
        public Move Direction { get; set; }
        public bool Reverse { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }

        private DateTime dt = DateTime.Now;
        private static Random rnd = new Random();

        // переменные для камней и еды
        List<Point> Mystones;
        List<Point> Food;
        Point targetFood;

        public void Startup(Size size, List<Point> stones)
        {
            Name = "Snake Eater (Blue)";
            Color = Color.Blue;
            Mystones = stones;
        }

        public void Update(Snake snake, List<Snake> enemies, List<Point> food, List<Point> dead)
        {
            // поиск еды
            Food = food;

            // если есть еда
            if (Food.Count != 0)
            {
                // если нет цели или цель съедена
                if (targetFood == null || !Food.Contains(targetFood))
                {
                    // поиск ближайшей еды
                    targetFood = FindNearestFood(snake.Position, Food);
                }

                // алгоритм поиска в ширину
                Queue<Point> queue = new Queue<Point>();
                queue.Enqueue(snake.Position);
                Dictionary<Point, Point> cameFrom = new Dictionary<Point, Point>();
                Dictionary<Point, int> distanceToFood = new Dictionary<Point, int>();
                distanceToFood[snake.Position] = 0;

                while (queue.Count > 0)
                {
                    Point current = queue.Dequeue();

                    // если нашли еду
                    if (current == targetFood)
                    {
                        // восстанавливаем путь
                        List<Point> path = new List<Point>();
                        while (current != snake.Position)
                        {
                            path.Add(current);
                            current = cameFrom[current];
                        }
                        path.Reverse();

                        // двигаемся к еде
                        if (path.Count >= 0)
                        {
                            Direction = GetDirection(snake.Position, path[0]);
                        }
                        break;
                    }

                    // добавляем соседние точки
                    foreach (Point neighbor in GetNeighbors(current))
                    {
                        if (!Mystones.Contains(neighbor) && !cameFrom.ContainsKey(neighbor))
                        {
                            queue.Enqueue(neighbor);
                            cameFrom[neighbor] = current;
                            distanceToFood[neighbor] = distanceToFood[current] + 1;
                        }
                    }
                }

                // Если не нашли путь к еде, выбираем ближайшую свободную точку

                if (Direction == Move.Nothing)
                {
                    int minDistance = int.MaxValue;
                    Point nearestFreePoint = new Point();

                    foreach (Point neighbor in GetNeighbors(snake.Position))
                    {
                        if (!Mystones.Contains(neighbor))
                        {
                            int distance = Math.Abs(neighbor.X - snake.Position.X) + Math.Abs(neighbor.Y - snake.Position.Y);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                nearestFreePoint = neighbor;
                            }
                        }
                    }

                    Direction = GetDirection(snake.Position, nearestFreePoint);
                }
            }
        }

        // поиск ближайшей еды
        private Point FindNearestFood(Point position, List<Point> food)
        {
            int minDistance = int.MaxValue;
            Point nearestFood = new Point();

            foreach (Point f in food)
            {
                int distance = Math.Abs(position.X - f.X) + Math.Abs(position.Y - f.Y);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestFood = f;
                }
            }

            return nearestFood;
        }

        // получение списка соседних точек
        private List<Point> GetNeighbors(Point point)
        {
            List<Point> neighbors = new List<Point>();

            neighbors.Add(new Point(point.X + 1, point.Y));
            neighbors.Add(new Point(point.X - 1, point.Y));
            neighbors.Add(new Point(point.X, point.Y + 1));
            neighbors.Add(new Point(point.X, point.Y - 1));

            return neighbors;
        }

        // определение направления движения
        private Move GetDirection(Point from, Point to)
        {
            if (to.X > from.X)
            {
                return Move.Right;
            }
            else if (to.X < from.X)
            {
                return Move.Left;
            }
            else if (to.Y > from.Y)
            {
                return Move.Down;
            }
            else
            {
                return Move.Up;
            }
        }
    }
}
