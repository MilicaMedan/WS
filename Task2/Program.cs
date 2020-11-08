using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<string, int> dict1 = new Dictionary<string, int>();
            Dictionary<string, int> dict2 = new Dictionary<string, int>();
            Dictionary<string, int> dict3 = new Dictionary<string, int>();

            var currentDate = DateTime.Now;

            using (var reader = new StreamReader(@"C:\Users\Milica\Desktop\test.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    
                    DateTime dt = DateTime.ParseExact(values[2].ToString(), "dd.MM.yyyy. HH:mm", CultureInfo.InvariantCulture);
                    if (dt > currentDate.AddHours(-24) && dt <= currentDate) {
                        if (values[0].Equals("1"))
                        {
                            dict1.Add(values[2],int.Parse(values[1]));
                        }
                        else if (values[0].Equals("2"))
                        {
                            dict2.Add(values[2], int.Parse(values[1]));
                        }
                        else if (values[0].Equals("3"))
                        {
                            dict3.Add(values[2], int.Parse(values[1]));
                        }
                    }

                    
                }
            }
            
            
            
            KeyValuePair<string, int> min1 = dict1.FirstOrDefault(x => x.Value.Equals(dict1.Values.Min()));
            DateTime mindt1 = DateTime.ParseExact(min1.Key, "dd.MM.yyyy. HH:mm", CultureInfo.InvariantCulture);
            Dictionary<string, int> pomdict1 = dict1.Where(x => (DateTime.ParseExact(x.Key, "dd.MM.yyyy. HH:mm", CultureInfo.InvariantCulture) > mindt1)).ToDictionary(x => x.Key, x => x.Value);
            KeyValuePair<string, int> max1 =  pomdict1.FirstOrDefault(x => x.Value.Equals(pomdict1.Values.Max()));
            int profit1 = max1.Value - min1.Value;
            

            KeyValuePair<string, int> min2 = dict2.FirstOrDefault(x => x.Value.Equals(dict2.Values.Min()));
            DateTime mindt2 = DateTime.ParseExact(min2.Key, "dd.MM.yyyy. HH:mm", CultureInfo.InvariantCulture);
            Dictionary<string, int> pomdict2 = dict2.Where(x => (DateTime.ParseExact(x.Key, "dd.MM.yyyy. HH:mm", CultureInfo.InvariantCulture) > mindt2)).ToDictionary(x => x.Key, x => x.Value);
            KeyValuePair<string, int> max2 = pomdict2.FirstOrDefault(x => x.Value.Equals(pomdict2.Values.Max()));
            Console.WriteLine(""+pomdict2.Count);
            int profit2 = max2.Value - min2.Value;
            
            KeyValuePair<string, int> min3 = dict3.FirstOrDefault(x => x.Value.Equals(dict3.Values.Min()));
            DateTime mindt3 = DateTime.ParseExact(min3.Key, "dd.MM.yyyy. HH:mm", CultureInfo.InvariantCulture);
            Dictionary<string, int> pomdict3 = dict3.Where(x => (DateTime.ParseExact(x.Key, "dd.MM.yyyy. HH:mm", CultureInfo.InvariantCulture) > mindt3)).ToDictionary(x => x.Key, x => x.Value);
            KeyValuePair<string, int> max3 = pomdict3.FirstOrDefault(x => x.Value.Equals(pomdict3.Values.Max()));
            int profit3 = max3.Value - min3.Value;


            Console.WriteLine("Greatest profit within next 24 houres for item with stock id 1 is "+profit1+". For such profit you should buy item at "+min1.Key+" by price "+min1.Value+" and sell it at "+max1.Key+" by price "+max1.Value+".");
            Console.WriteLine("Greatest profit within next 24 houres for item with stock id 2 is " + profit2 + ". For such profit you should buy item at " + min2.Key + " by price " + min2.Value + " and sell it at " + max2.Key + " by price " + max2.Value + ".");
            Console.WriteLine("Greatest profit within next 24 houres for item with stock id 3 is " + profit3 + ". For such profit you should buy item at " + min3.Key + " by price " + min3.Value + " and sell it at " + max3.Key + " by price " + max3.Value + ".");
            while (true) { }

        }
    }
}
