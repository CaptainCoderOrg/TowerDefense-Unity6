namespace CaptainCoder.Extensions
{
    public static class ArrayExtensions
    {
        public static T SelectRandomOrDefault<T>(this T[] elements, T defaultValue)
        {
            if (elements.Length == 0) { return defaultValue; }
            int ix = UnityEngine.Random.Range(0, elements.Length);
            return elements[ix];
        }
    }
}