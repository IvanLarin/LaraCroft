namespace LaraCroft
{
    public interface Lara
    {
        Task DownloadHistory(Action? onSuccess = null);
    }
}
