namespace com.lvrenyang
{
    public class DataUtils
    {

        public static T[][][] GenerateTripleArray<T>(int length, int width, int height)
        {
            T[][][] data = new T[length][][];
            for (int i = 0; i < length; ++i)
            {
                data[i] = new T[width][];
                for (int j = 0; j < width; ++j)
                {
                    data[i][j] = new T[height];
                }
            }
            return data;
        }

        // 公式a+bx+cx2+dx3 微调
        public static double GetThreeOrderEqutionSolution(double x, double A0, double A1, double A2, double A3)
        {
            return A0 + A1 * x + A2 * x * x + A3 * x * x * x;
        }
    }
}
