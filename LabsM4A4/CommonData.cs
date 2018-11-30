using System;


namespace LabsM4A4
{
    static class CommonData
    {
        public static int a { get => 0; }

        public static int b { get => 2; }

        public static int k { get => 1; }

        public static double T { get => 0.2; }

        public static int phi { get => 1; }

        public static double g1(double t) => Math.Pow(Math.E, 10 * t);

        public static double g2(double t) => Math.Pow(Math.E, t);

        public static int f { get => 0; }

        public const double t1 = 0.1;

        public const double t2 = 0.2;
    }
}
