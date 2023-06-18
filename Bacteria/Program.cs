using System;
using System.Reflection;

namespace Bacteria 
{
    internal class Program
    {
        //class VolumeData
        //{
        //    public int Volume;
        //    public int Count;
        //    public int MaxGrouth;
        //};

        static void Main(string[] args)
        {
            Console.WriteLine("Bacteria test task");
            var hive = GetInput();
            // only volume matters
            //var byVolume = input.GroupBy(volume => volume)
            //                    .OrderBy(group => group.Key)
            //                    .Select(group => new VolumeData() { Volume = group.Key, Count = group.Count() })
            //                    .ToArray();
            //foreach (var item in byVolume) {
            //    item.MaxGrouth = MaxVolumeGrouth(byVolume, item.Volume);
            //    Console.WriteLine($"{item.Volume} can grow to {item.MaxGrouth}");
            //}


            // simple
            var maxSizes = new int[hive.Length];
            for (int i = 0; i < hive.Length; i++)
            {
                maxSizes[i] = MaxVolumeGrowth(hive, i);
            }
            for (int i = 0; i < hive.Length; i++)
            {
                Console.WriteLine($"{hive[i]} can grow to {maxSizes[i]}");
            }
            // binary
            var max = hive.Last();
            var left = 0;
            var right = hive.Length - 1;
            while (left <= right)
            {
                var mid = (left + right) / 2;
                var current = MaxVolumeGrowth(hive, mid);
                if (current < max)
                    right = mid - 1;
                else
                    left = mid + 1;
            }

            return;
        }

        static int[] GetInput()
        {
            //?? todo read from console
            return new int[] { 1, 1, 3, 4 };
        }

        static int MaxVolumeGrowth(IList<int> items, int index)
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

        //static int MaxVolumeGrouth(IList<VolumeData> byVolume, int volume)
        //{
        //    var result = volume;
        //    for (int i = 0; i<byVolume.Count; i++)
        //    {
        //        if (result > byVolume[i].Volume)
        //            result += byVolume[i].Volume * ((volume == byVolume[i].Volume) ? byVolume[i].Count - 1 : byVolume[i].Count);
        //        if (result < byVolume[i].Volume)
        //            break;
        //    }
        //    return result;
        //}
    }
}
