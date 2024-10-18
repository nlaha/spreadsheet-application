namespace ExpressionTreeDemo
{
    using SpreadsheetEngine.ExpressionTree;

    internal class Program
    {
        static void Main(string[] args)
        {
            ExpressionTree expressionTree = new ExpressionTree();
            string expression = "";
            int choice;

            do
            {
                Console.WriteLine($"""
                Menu (Current expression="{expression}")
                    1. Enter a new expression
                    2. Set a variable value
                    3. Evaluate tree
                    4. Quit
                """);

                choice = int.Parse(Console.ReadLine() ?? "-1");

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter new expression: ");
                        expression = Console.ReadLine() ?? "";
                        expressionTree = new ExpressionTree(expression);
                        break;
                    case 2:
                        Console.Write("Set variable name: ");
                        var name = Console.ReadLine() ?? "";
                        Console.Write("Set variable value: ");
                        var value = Console.ReadLine() ?? "";
                        expressionTree.SetVariable(name, double.Parse(value));
                        break;
                    case 3:
                        Console.WriteLine(expressionTree.Evaluate());
                        break;
                    case 4:
                        return;
                }
            }
            while (choice != 4);
        }
    }
}
