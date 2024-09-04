namespace LaraCroft;

internal class TerminateException(string? message = null, Exception? innerException = null) : Exception(message, innerException);