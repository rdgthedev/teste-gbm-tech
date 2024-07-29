using FluentValidation.Results;

namespace GBMProject.Application.Results;

public sealed class Result
{
    public int StatusCode { get; private set; }
    public object Data { get; private set; } = null!;
    public string Message { get; private set; } = null!;
    public List<string> Errors { get; private set; }

    private Result()
    {
        Errors = new List<string>();
    }

    public Result(int statusCode) : this()
        => StatusCode = statusCode;

    public Result(int statusCode, object data, string message) : this()
    {
        Data = data;
        StatusCode = statusCode;
        Message = message;
    }

    public Result(int statusCode, object data) : this()
    {
        Data = data;
        StatusCode = statusCode;
    }

    public Result(int statusCode, string message) : this()
    {
        Message = message;
        StatusCode = statusCode;
    }


    public Result(int statusCode, string message, string error) : this()
    {
        StatusCode = statusCode;
        Message = message;
        Errors.Add(error);
    }

    public Result(int statusCode, string message, List<ValidationFailure> errors) : this()
    {
        Message = message;
        StatusCode = statusCode;
        errors.ForEach(x => Errors.Add(x.ErrorMessage));
    }
}