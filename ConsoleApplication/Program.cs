using ConsoleApplication;
using LaraCroft;

Lara lara = new TheFactory().MakeLara();

void ReturnOk() => Environment.Exit(0);

await new ChatWith(lara, onSuccess: ReturnOk).Start();

Environment.Exit(1);