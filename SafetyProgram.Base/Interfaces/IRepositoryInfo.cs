namespace SafetyProgram.Base.Interfaces
{
    public interface IRepositoryInfo
    {
        string Source { get; }
        string ContentType { get; }
        string Path { get; }
        string Login { get; }
        string Password { get; }
    }
}
