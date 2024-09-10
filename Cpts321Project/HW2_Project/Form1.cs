using System.Collections.Generic;
using System.Text;

namespace HW2_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RunDistinctIntegers();
        }

        private void RunDistinctIntegers()
        {
            List<int> random = FillListRandomIntegers();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"HashSet method: {UniqueIntegersOne(random)} unique numbers, the time complexity of this method is O(n) because we could have a list entirely made of unique numbers.");
            sb.AppendLine($"O(1) storage method: {UniqueIntegersTwo(random)} unique numbers");
            sb.AppendLine($"Sorted method: {UniqueIntegersThree(random)} unique numbers");

            textBox1.Text = sb.ToString();
        }

        /// <summary>
        /// Returns the number of unique integers in a list
        /// using a hash set
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int UniqueIntegersOne(List<int> list)
        {
            HashSet<int> set = new HashSet<int>();
            list.ForEach((x) => set.Add(x));

            return set.Count;
        }

        /// <summary>
        /// Returns the number of unique integers in a list
        /// using an O(1) storage complexity
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static int UniqueIntegersTwo(List<int> list)
        {
            int unique = 0;
            for (int i = 0; i < 20000; i++)
            {
                if (list.Contains(i))
                {
                    unique++;
                }
            }

            return unique;
        }

        /// <summary>
        /// Returns the number of unique integers in a list using sorting
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static int UniqueIntegersThree(List<int> list)
        {
            return 0;
        }

        /// <summary>
        /// Returns a list containing 10,000 random integers from 0 to 20000
        /// </summary>
        /// <returns></returns>
        public static List<int> FillListRandomIntegers()
        {
            List<int> list = new List<int>(10000);
            Random rand = new Random();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(rand.Next() % 20000);
            }

            return list;
        }
    }
}
