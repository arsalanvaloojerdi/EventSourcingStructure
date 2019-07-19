namespace Framework.Core.Extentions
{
    /// <summary>
    /// The char extensions.
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// The to int - Int32 -.
        /// </summary>
        /// <param name="src">
        /// The src.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int ToInt32(this char src)
        {
            return (int)char.GetNumericValue(src);
        }
    }
}