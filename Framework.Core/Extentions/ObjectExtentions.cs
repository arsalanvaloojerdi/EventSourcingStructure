namespace Framework.Core.Extentions
{
    /// <summary>
    /// The Object Extentions
    /// </summary>
    public static class ObjectExtentions
    {
        /// <summary>
        /// Check if Two Objects Type Is Not Same
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNot<T>(this object obj)
        {
            return !(obj is T);
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}