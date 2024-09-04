namespace LaraCroft;

public class TheConstants : Constants
{
    public string ConfigFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

    public string DefaultConfigContent =>
        """
        {
          "OutputDirectory": ".\\History",
          "CandleDurationInMinutes": 1
        }
        """;
}