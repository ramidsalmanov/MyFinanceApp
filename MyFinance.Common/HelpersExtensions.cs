namespace MyFinance.Common;

public static class HelpersExtensions
{
    public static bool IsNull<T>(this T obj) where T : class
    {
        return obj == null;
    }
    
    public static bool IsNullOrEmpty(this string source)
    {
        return string.IsNullOrEmpty(source);
    }
}