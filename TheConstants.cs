namespace LaraCroft;

public class TheConstants : Constants
{
    public string ConfigFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
}