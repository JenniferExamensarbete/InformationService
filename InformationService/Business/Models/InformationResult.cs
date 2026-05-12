namespace InformationService.Business.Models;

public class InformationResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
}

public class InformationResult<T> : InformationResult
{
    public T? Result { get; set; }
}