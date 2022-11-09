namespace MyFinance.Common;

public static class ExceptionHelper
{
    public static void Throw<T>(string message = null) where T : Exception
    {
        var ex = (T)Activator.CreateInstance(typeof(T), message);
        throw ex;
    }
}