namespace LaraCroft;

public class ThePublicFactory
{
    public Lara MakeLara() => new TheFactory().MakeLara();
}