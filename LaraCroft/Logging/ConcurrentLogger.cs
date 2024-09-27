namespace LaraCroft.Logging;

public class ConcurrentLogger : Logger
{
    private readonly Logger logger;

    private readonly object lockObject;

    public ConcurrentLogger(Logger theLogger)
    {
        logger = theLogger;
        lockObject = new();
    }

    private ConcurrentLogger(Logger theLogger, object theLockObject)
    {
        logger = theLogger;
        lockObject = theLockObject;
    }

    public void WriteLine(string value = "") => WithLock(logger.WriteLine)(value);

    public void WriteErrorLine(string value = "") => WithLock(logger.WriteErrorLine)(value);

    private Action<string> WithLock(Action<string> doIt) => (value) =>
    {
        lock (lockObject)
        {
            doIt(value);
        }
    };

    public Logger HoldThisPosition()
    {
        lock (lockObject)
        {
            return new ConcurrentLogger(logger.HoldThisPosition(), lockObject);
        }
    }

    public (int Left, int Top) GetCursorPosition()
    {
        lock (lockObject)
        {
            return logger.GetCursorPosition();
        }
    }

    public bool CursorVisible
    {
        get
        {
            lock (lockObject)
            {
                return logger.CursorVisible;
            }
        }
        set
        {
            lock (lockObject)
            {
                logger.CursorVisible = value;
            }
        }
    }

    public void SetCursorPosition(int left, int top)
    {
        lock (lockObject)
        {
            logger.SetCursorPosition(left, top);
        }
    }

    public void WriteError(Exception exception)
    {
        lock (lockObject)
        {
            logger.WriteError(exception);
        }
    }
}