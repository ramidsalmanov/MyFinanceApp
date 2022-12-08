namespace MyFinance.Common.Models;

public class ActionResult
{
    protected object Data { get; init; }
    public bool Success { get; protected set; }
    public bool Failure => !Success;
    public IEnumerable<string> Errors { get; init; }

    protected ActionResult()
    {
        Success = true;
    }

    public ActionResult(IEnumerable<string> errors)
    {
        Success = false;
        Errors = errors;
    }

    public T GetData<T>()
    {
        if (Failure)
        {
            throw new Exception($"You can't access .{nameof(Data)} when .{nameof(Success)} is false");
        }
        
        if (Data is T instance)
        {
            return instance;
        }

        throw new Exception("wrong data type");
    }

    public static ActionResult Fail(params string[] errors)
    {
        return new ActionResult(errors);
    }

    public static ActionResult Ok<T>(T data)
    {
        return new ActionResult()
        {
            Data = data
        };
    }
    
    public static ActionResult Ok()
    {
        return new ActionResult();
    }
}
