using LaraCroft;

namespace ConsoleApp;

internal class ChatWith(Lara lara)
{
    public void Start()
    {
        Mind mind = new TheMindOf(lara);

        while (mind.Is) mind.Do();
    }
}