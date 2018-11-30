using System;


namespace LabM4A2
{
    class Program
    {
        static void Main(string[] args)
        {
            Progonka.Test();

            ITask[] tasks =
            {
                new Task1(),
                new Task2DoubleAccuracy(),
                new Task3(),
                new Task4()
            };

            double[] accuracy = { 0.001, 0.001, 0.03, 0.01 };

            int count = 0;
            
            while(count < 3)
            {
                Console.Clear();
                Console.WriteLine("Количество точек, которое мы хотим взять для начала: ");
                count = int.Parse(Console.ReadLine());
            }

            for (int i = 0; i < 4; ++i)
            {
                try
                {
                    var result = StepChanging.ChooseStep(tasks[i], count, accuracy[i]);
                    Console.WriteLine($"\nКол-во точек: {result.X.Length}");
                    Console.WriteLine("------------------------\n");
                    result.Out();
                }
                catch(InvalidOperationException)
                {
                    Console.WriteLine($"Task #{tasks[i].GetType().Name} failed!");
                }
            }

            Console.ReadKey(true);
        }
    }
}