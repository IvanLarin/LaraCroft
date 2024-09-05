namespace LaraCroft;

public class GoodException(string message, Exception? innerException = null) : Exception(message, innerException);