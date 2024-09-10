using LaraCroft;

namespace ConsoleFace;

internal class ChatWith(Lara lara)
{
    public void Start()
    {
        Mind mind = new TheMindOf(lara);

        while (mind.Is) mind.Do();
    }
}