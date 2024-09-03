using LaraCroft;

Factory factory = new TheFactory();
Logger logger = factory.MakeLogger();

Lara lara = new TheLara(factory);

Environment.ExitCode = 1;

await lara.DownloadHistory(() =>
{
    logger.LogSuccess("Миссия выполнена");
    Environment.ExitCode = 0;
});
