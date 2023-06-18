namespace Bacteria
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bacteria test task");
            if (args.Contains("-test")) 
            {
                Test();
                return;
            }

            var hive = GetInput();
            var winConditions = CalcWinConditions(hive);
            foreach(var i in winConditions)
            {
                Console.WriteLine(i ? "1" : "0");
            } 
            return;
        }

        static bool[] CalcWinConditions(IReadOnlyList<int> hive)
        {
            var result = new bool[hive.Count];
            // binary
            var max = hive.Last();
            var left = 0;
            var right = hive.Count - 1;
            var mid = -1;
            var midValue = -1;
            while (left <= right)
            {
                mid = (left + right) / 2;
                midValue = MaxVolumeGrowth(hive, mid);
                // Console.WriteLine($"{left} -> {mid} <- {right} = {midValue}");
                if (midValue > max)
                    right = mid - 1;
                else
                    left = mid + 1;
            }
            if (mid > 1)
                Array.Fill(result, false, 0, mid - 1);
            result[mid] = (midValue > max);
            if (mid < (result.Length - 1))
                Array.Fill(result, true, mid + 1, result.Length - mid - 1);
            return result;
        }

        static int[] GetInput()
        {
            var len = Convert.ToInt32(Console.ReadLine());
            var result = new int[len];
            for (int i = 0; i < len; i++)
                result[i] = Convert.ToInt32(Console.ReadLine());
            return result;
        }

        static int MaxVolumeGrowth(IReadOnlyList<int> items, int index)
        {
            var result = items[index];
            for (int i = 0; i < index; i++)
            {
                if (result > items[i])
                    result += items[i];
                else if (result < items[i])
                    break;
            }
            for (int i = index + 1; i < items.Count; i++)
            {
                if (result > items[i])
                    result += items[i];
                else if (result < items[i])
                    break;
            }
            return result;
        }

        static void Test()
        {
            var hive = GenTestData();
            var maxVolumes = GenMaxGrowth(hive);
            var winConditions = CalcWinConditions(hive);
            var max = hive.Last();
            for (int i = 0; i < hive.Length; i++)
            {
                Console.WriteLine($"{hive[i]} can grow to {maxVolumes[i]}. {(winConditions[i] ? "1" : "0")}");
                if (maxVolumes[i] > max != winConditions[i])
                    throw new Exception("Test failed");
            }
            Console.WriteLine("Test complete");
        }

        static int[] GenTestData(int maxLen = 16, int maxVolume = 5)
        {
            var rnd = new Random();
            var len = rnd.Next(1, maxLen);
            var result = new int[len];
            for (int i = 0; i < len; i++)
                result[i] = rnd.Next(1, maxVolume);
            Array.Sort(result);
            return result;
        }

        static int[] GenMaxGrowth(IReadOnlyList<int> hive)
        {
            var result = new int[hive.Count];
            for (int i = 0; i < hive.Count; i++)
                result[i] = MaxVolumeGrowth(hive, i);
            return result;
        }
    }
}
