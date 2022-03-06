namespace YAEP.CQRS.Abstractions
{
    public interface IResult
    {
        public string Message { get; }
        public bool IsSuccess { get; }
    }
}
