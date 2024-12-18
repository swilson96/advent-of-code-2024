namespace AdventOfCode2024.Solutions;

public class Day17 : IAdventSolution
{ 
    public object PartOne(string input)
    {
        var parts = input.Split(Environment.NewLine + Environment.NewLine);
        var registers = parts[0].Split(Environment.NewLine).Select(l => int.Parse(l.Split(": ")[1])).ToArray();
        var program = parts[1][9..].Split(",").Select(int.Parse).ToArray();

        return RunProgram(registers, program);
    }

    private string RunProgram(int[] registers, int[] program) {
        var instructionPointer = 0;
        var output = new List<int>();

        while (instructionPointer < program.Length)
        {
            var operand = program[instructionPointer + 1];
            var comboOperand = operand switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => registers[0],
                5 => registers[1],
                6 => registers[2],
                _ => throw new ArgumentOutOfRangeException($"combo operand {operand} was used, program invalid")
            };
            switch (program[instructionPointer])
            {
                    // The adv instruction (opcode 0) performs division. The numerator is the value in the A register.
                    // The denominator is found by raising 2 to the power of the instruction's combo operand.
                    // (So, an operand of 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.)
                    // The result of the division operation is truncated to an integer and then written to the A register.
                    case 0:
                        var numerator = registers[0];
                        var denominator = Math.Pow(2, comboOperand);
                        var result = (int) (numerator / denominator);
                        registers[0] = result;
                        instructionPointer += 2;
                        break;
                        
                    // The bxl instruction (opcode 1) calculates the bitwise XOR of register B and the instruction's literal operand,
                    // then stores the result in register B.
                    case 1:
                        registers[1] ^= operand;
                        instructionPointer += 2;
                        break;

                    // The bst instruction (opcode 2) calculates the value of its combo operand modulo 8
                    // (thereby keeping only its lowest 3 bits), then writes that value to the B register.
                    case 2:
                        registers[1] = comboOperand % 8;
                        instructionPointer += 2;
                        break;
                    
                    // The jnz instruction (opcode 3) does nothing if the A register is 0. However, if the A register is not zero,
                    // it jumps by setting the instruction pointer to the value of its literal operand; if this instruction jumps,
                    // the instruction pointer is not increased by 2 after this instruction.
                    case 3:
                        if (registers[0] == 0)
                        {
                            instructionPointer += 2;
                        }
                        else
                        {
                            instructionPointer = operand;
                        }
                        break;
                    
                    // The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C,
                    // then stores the result in register B. (For legacy reasons, this instruction reads an operand but ignores it.)
                    case 4:
                        registers[1] ^= registers[2];
                        instructionPointer += 2;
                        break;

                    // The out instruction (opcode 5) calculates the value of its combo operand modulo 8,
                    // then outputs that value. (If a program outputs multiple values, they are separated by commas.)
                    case 5:
                        output.Add(comboOperand % 8);
                        instructionPointer += 2;
                        break;
                    
                    // The bdv instruction (opcode 6) works exactly like the adv instruction except that
                    // the result is stored in the B register. (The numerator is still read from the A register.)
                    case 6:
                        var numerator1 = registers[0];
                        var denominator1 = Math.Pow(2, comboOperand);
                        registers[1] = (int) (numerator1 / denominator1);
                        instructionPointer += 2;
                        break;

                    // The cdv instruction (opcode 7) works exactly like the adv instruction except that the
                    // result is stored in the C register. (The numerator is still read from the A register.)
                    case 7:
                        var numerator2 = registers[0];
                        var denominator2 = Math.Pow(2, comboOperand);
                        registers[2] = (int) (numerator2 / denominator2);
                        instructionPointer += 2;
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException("invalid opcode " + program[instructionPointer]);
            }
        }

        return string.Join(",", output);
    }

    public object PartTwo(string input)
    {
        var parts = input.Split(Environment.NewLine + Environment.NewLine);
        var registers = parts[0].Split(Environment.NewLine).Select(l => int.Parse(l.Split(": ")[1])).ToArray();
        var programString = parts[1][9..];
        var program = programString.Split(",").Select(int.Parse).ToArray();
        
        var candidate = 2000000000;

        while (candidate < int.MaxValue)
        {
            var output = RunProgram(new[] { candidate, registers[1], registers[2] }, program);
            if (output == programString)
            {
                return candidate;
            }

            ++candidate;
        }

        Console.WriteLine("didn't find it");
        return -1;
    }
}