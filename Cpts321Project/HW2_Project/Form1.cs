using System.Collections.Generic;

namespace HW2_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RunDistinctIntegers();
        }

        private static void RunDistinctIntegers()
        {

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
