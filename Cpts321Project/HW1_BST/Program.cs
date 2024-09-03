namespace HW1_BST
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a list of numbers separated by spaces:");

            // Read a list of integers
            var line = System.Console.ReadLine();
            if (line == null)
            {
                Console.WriteLine("Please enter a space separated list of numbers");
                return;
            }

            // split line into multiple strings
            var entries = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (entries == null || entries.Length == 0)
            {
                Console.WriteLine("No valid numbers detected");
                return;
            }

            // parse numbers
            var numbers = entries.Select(x => int.Parse(x)).ToList();

            // fill BST
            BST<int> bst = new BST<int>();
            foreach (var number in numbers)
            {
                bst.Add(number);
            }

            bst.Print();

            Console.WriteLine($"Height {bst.Height}");
            Console.WriteLine($"Count {bst.Count}");
            Console.WriteLine($"Min Height {bst.MinHeight}");
        }
    }
}
