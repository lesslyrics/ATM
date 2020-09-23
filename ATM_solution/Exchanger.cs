using System;
using System.Collections.Generic;
using System.Linq;
namespace ATM_solution
{
    class Exchanger
    {
        /** recursive algorithm for banknotes exchange **/
        private List<List<int>> ExchangeBanknotes(int value, int[] banknotes)
        {
            var result = new List<List<int>>();
            Array.Sort(banknotes);
            for (var i = banknotes.Length - 1; i > -1; --i)
            {
                if (value - banknotes[i] < 0)
                {
                    continue;
                }

                if (value - banknotes[i] == 0)
                {
                    var tmp = new List<int> {banknotes[i]};
                    result.Add(tmp);
                }

                if (value - banknotes[i] <= 0) continue;
                {
                    foreach (var exBanknote in ExchangeBanknotes(value - banknotes[i],
                        CopyPartly(banknotes, 0, i + 1)))
                    {
                        exBanknote.Add(banknotes[i]);
                        result.Add(exBanknote);
                    }
                }
            }
            return result;
        }

        /** driver function for banknotes exchange **/
        public void PerformExchange()
        {
            String value = "";
            try
            {
                Console.WriteLine("Input banknote to change: ");
                value = Console.ReadLine();

                // check for null and negative value
                if (Int32.Parse(value ?? throw new InvalidOperationException()) <= 0)
                {
                    throw new Exception("Wrong input: negative banknote: " + Int32.Parse(value));
                }
            }
            catch (FormatException e)
            {
                Console.Write("No banknote for input or wrong format detected");
                System.Environment.Exit(1);
            }


            var banknotes = new List<int>();
            Console.WriteLine("Input banknotes: ");
            try
            {
                var input = Convert.ToInt32(Console.ReadLine());
                while (input != 0)
                {
                    banknotes.Add(input);
                    input = Convert.ToInt32(Console.ReadLine());
                }
            }
            catch (Exception e)
            {
                // also counts for negative banknotes
                Console.Write("Invalid input banknote");
                System.Environment.Exit(1);
            }

            banknotes = removeDuplicates(banknotes);

            var banknotesArr = banknotes.ToArray();
            var result = ExchangeBanknotes(Int32.Parse(value), banknotesArr);
            Console.WriteLine("Number of options: " + result.Count);

            result.ForEach(element =>
            {
                element.ForEach(num =>
                {
                    if (num > 0)
                    {
                        Console.Write(num + " ");
                    }
                    else
                    {
                        throw new Exception("Calculation error" + num);
                    }
                });
                Console.WriteLine();
            });
        }

        /** return subarray from var start to var end **/
        private int[] CopyPartly(int[] src, int start, int end)
        {
            var length = end - start;
            var destinationArray = new int[length];
            Array.Copy(src, start, destinationArray, 0, length);
            return destinationArray;
        }

        /** remove duplicate banknotes from input array **/
        private List<int> removeDuplicates(List<int> banknotes)
        {
            var set = new HashSet<int>();
            return set.Union(banknotes).ToList();
        }
    }
}