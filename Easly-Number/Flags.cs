namespace EaslyNumber
{
    /// <summary>
    /// Flag containing information about the result of operations.
    /// </summary>
    public class Flags
    {
        #region Properties
        /// <summary>
        /// Signals a division of a nonzero number by zero.
        /// </summary>
        public bool DivideByZero { get; private set; }

        /// <summary>
        /// Signals that the result was rounded to a different mathematical value, but as close as possible to the original.
        /// </summary>
        public bool Inexact { get; private set; }
        #endregion

        #region Client Interface
        /// <summary>
        /// Clear flags.
        /// </summary>
        public void Clear()
        {
            DivideByZero = false;
            Inexact = false;
        }

        /// <summary>
        /// Forces the <see cref="DivideByZero"/> flag to true.
        /// </summary>
        internal void SetDivideByZero()
        {
            DivideByZero = true;
        }

        /// <summary>
        /// Forces the <see cref="Inexact"/> flag to true.
        /// </summary>
        internal void SetInexact()
        {
            Inexact = true;
        }
        #endregion
    }
}
