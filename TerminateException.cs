namespace LaraCroft;

public class TerminateException(string? message = null, Exception? innerException = null) : Exception(message, innerException)
{
}