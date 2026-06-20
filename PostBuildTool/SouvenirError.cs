namespace SouvenirPostBuildTool;

[Serializable]
public class SouvenirErrorException : Exception
{
    public SouvenirErrorException() { }
    public SouvenirErrorException(string message) : base(message) { }
    public SouvenirErrorException(string message, Exception inner) : base(message, inner) { }
}
