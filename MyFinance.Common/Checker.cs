namespace MyFinance.Common;

public static class Checker
{
    public static void NotNull<T>(T item, Exception exception = null) where T : class
    {
        if (item == null)
        {
            throw exception == null ? new NullReferenceException() : exception;
        }
    }

    public static void IsTrue(bool predicate, string message = null)
    {
        if (!predicate)
        {
            throw new Exception(message);
        }
    }

    public static void NotNullOrEmpty(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("value cannot be null or empty");
        }
    }
}