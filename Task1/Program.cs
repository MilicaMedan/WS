using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[6];
            for (int i = 0; i < 6; i++) {
                Console.WriteLine("Input "+i+". number!");
                string pom = "";
                while ((pom=Console.ReadLine()).Equals("")) ;
                arr[i] = int.Parse(pom);
            }

            if (check(arr))
            {
                Console.WriteLine("True");
            }
            else {
                Console.WriteLine("False");
            }

            while (true) { }
        }

        private static bool check(int[] arr) {
            for (int i = 0; i < 4; i++)
            {
                if ((arr[i] + arr[i + 1] + arr[i + 2]) == 6)
                {
                    return true;
                    
                }
            }
            return false;
        }
    }
}
