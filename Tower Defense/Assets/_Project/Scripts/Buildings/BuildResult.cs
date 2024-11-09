public class BuildResult
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }

    public BuildResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static BuildResult Success(string message) => new (true, message);
    public static BuildResult Fail(string message) => new (false, message);
}