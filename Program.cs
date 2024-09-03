using LaraCroft;

Factory factory = new TheFactory();
Logger logger = factory.MakeLogger();

Lara lara = new TheLara(factory);

bool isSuccess = false;

await lara.DownloadHistory(() =>
{
    logger.LogSuccess("Миссия выполнена");
    isSuccess = true;
});

Environment.ExitCode = isSuccess ? 0 : 1;