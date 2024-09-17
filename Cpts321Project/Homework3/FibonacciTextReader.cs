namespace Homework3
{
    using System.Numerics;

    /// <summary>
    /// Custom <see cref="TextReader"/> for generating fibonacci numbers
    /// </summary>
    public class FibonacciTextReader : TextReader
    {
        private int _index = 0;
        private int _maxLines = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// </summary>
        /// <param name="maxLines">number of fibonacci lines to generate</param>
        /// <exception cref="ArgumentOutOfRangeException">throws when maxLines is less than zero</exception>
        public FibonacciTextReader(int maxLines)
        {
            if (maxLines < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLines));
            }

            this._maxLines = maxLines;
        }

        /// <inheritdoc/>
        public override string? ReadLine()
        {
            // check if we've gone past our max
            if (this._index >= this._maxLines)
            {
                return null;
            }


            // generate
            BigInteger output = BigInteger.Zero;
            BigInteger previous = BigInteger.Zero;
            BigInteger previousPrevious = BigInteger.Zero;

            if (this._index == 0)
            {
                output = BigInteger.Zero;
                previous = BigInteger.Zero;
                previousPrevious = BigInteger.Zero;
            }
            else if (this._index == 1)
            {
                output = BigInteger.One;
                previous = BigInteger.Zero;
                previousPrevious = BigInteger.Zero;
            } else
            {
                // compute fibonacci number from previous
                // and previous previous numbers sum
                output = previous + previousPrevious;
                previousPrevious = previous;
                previous = output;
            }

            // increment index
            this._index++;

            return $"{this._index + 1}: {output}";
        }

        /// <inheritdoc/>
        public override string ReadToEnd()
        {
            string outString = string.Empty;
            string? line = null;
            do
            {
                line = this.ReadLine();
                outString = outString + line;
            } while (line != null);

            return outString;
        }
    }
}
