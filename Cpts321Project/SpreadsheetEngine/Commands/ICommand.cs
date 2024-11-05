// <copyright file="ICommand.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.Commands
{
    /// <summary>
    /// Interface for commands send from the UI
    /// </summary>
    internal interface ICommand
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        void Execute();

        /// <summary>
        /// Undo whatever action was done by the command
        /// in the Execute method
        /// </summary>
        void Undo();
    }
}
