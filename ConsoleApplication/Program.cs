using LaraCroft;

Lara lara = new TheFactory().MakeLara();

Environment.ExitCode = 1;

await lara.DownloadCandles(() => Environment.ExitCode = 0);

Console.ReadLine();