using MoreEnumerable = MoreLinq.MoreEnumerable;

namespace AdventOfCode2024.Solutions;

public class Day09 : IAdventSolution
{
    public object PartOne(string input)
    {
        var data = input.Select(c => c.ToString()).Select(int.Parse).ToArray();
        var leftP = 0;
        var rightP = data.Length + 1;
        var location = 0;
        var lastBlockRemainingSize = 0;
        var leftId = 0; // value of the file data
        var rightId = (rightP + 1) / 2;
        var checksum = 0L;

        while (leftP < rightP)
        {
            var size = data[leftP];
            while (size > 0)
            {
                checksum += leftId * location;
                --size;
                ++location;
            }

            ++leftP;
            if (leftP == rightP) break;
            size = data[leftP]; // some free space to use
            while (size > 0)
            {
                while (size > 0 && lastBlockRemainingSize > 0)
                {
                    // move a block
                    checksum += rightId * location;

                    --size;
                    --lastBlockRemainingSize;
                    ++location;
                }

                if (lastBlockRemainingSize == 0 && size > 0)
                {
                    --rightP;
                    --rightP; // skip the free memory
                    if (leftP >= rightP) break;
                    lastBlockRemainingSize = data[rightP];
                    --rightId;
                }
            }

            ++leftP;
            ++leftId;
        }

        while (lastBlockRemainingSize > 0)
        {
            checksum += rightId * location;

            --lastBlockRemainingSize;
            ++location;
        }

        return checksum;
    }

    private static long CheckSum(int[] sizes, int[] values)
    {
        var location = 0;
        var checksum = 0L;
        foreach (var (size, value) in sizes.Zip(values))
        {
            if (value < 0)
            {
                location += size;
                continue;
            }
            var sizeLocal = size;
            while (sizeLocal > 0)
            {
                checksum += location * value;   
                ++location;
                --sizeLocal;
            } 
        }

        return checksum;
    }

    public object PartTwo(string input)
    {
        var data = input.Select(c => c.ToString()).Select(int.Parse).ToArray();
        var values = new int[data.Length];
        for (var i = 0; i <= data.Length/2; ++i)
        {
            values[2 * i] = i;
            if (2 * i + 1 < data.Length)
            {
                values[2 * i + 1] = -1; // Use negative to mark free memory
            }
        }
        
        var rightP = data.Length - 1;
        while (rightP > 0)
        {
            if (values[rightP] < 0)
            {
                --rightP;
                continue; // nothing to move
            }

            var sizeOfBlockBeingMoved = data[rightP];
            
            var leftP = 1;
            while (leftP < rightP)
            {
                var sizeToFill = data[leftP]; // how much free space to use
                if (values[leftP] < 0 && sizeOfBlockBeingMoved <= sizeToFill)
                {
                    // move a block
                    if (sizeOfBlockBeingMoved < sizeToFill)
                    {
                        // record the remaining space
                        data[leftP] = sizeOfBlockBeingMoved;
                        data = MoreEnumerable.Insert(data, new [] { sizeToFill - sizeOfBlockBeingMoved }, leftP + 1).ToArray();
                        values = MoreEnumerable.Insert(values, new []{ -1 }, leftP + 1).ToArray();
                        ++rightP;
                    }

                    values[leftP] = values[rightP];
                    values[rightP] = -1;

                    break;
                }

                ++leftP;
            }
            
            --rightP;
        }

        return CheckSum(data, values);   
    }
}