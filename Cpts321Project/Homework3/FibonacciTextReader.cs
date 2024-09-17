// <copyright file="FibonacciTextReader.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Homework3
{
    using System.Numerics;
    using System.Text;

    /// <summary>
    /// Custom <see cref="TextReader"/> for generating fibonacci numbers
    /// </summary>
    public class FibonacciTextReader : TextReader
    {
        private int _index = 0;
        private int _maxLines = 0;
        private BigInteger _previous = BigInteger.Zero;
        private BigInteger _previousPrevious = BigInteger.Zero;

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

            if (this._index == 0)
            {
                output = BigInteger.Zero;
            }
            else if (this._index == 1)
            {
                output = BigInteger.One;
                this._previous = BigInteger.One;
            }
            else
            {
                // compute fibonacci number from previous
                // and previous previous numbers sum
                output = this._previous + this._previousPrevious;
                this._previousPrevious = this._previous;
                this._previous = output;
            }

            // increment index
            this._index++;

            return $"{this._index}: {output.ToString()}";
        }

        /// <inheritdoc/>
        public override string ReadToEnd()
        {
            var outSb = new StringBuilder();
            string? line = null;
            while (true)
            {
                line = this.ReadLine();
                if (line == null)
                {
                    break;
                }

                outSb.AppendLine(line);
            }

            return outSb.ToString();
        }
    }
}
