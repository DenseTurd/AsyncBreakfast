public class Coffee 
{

}

public class Egg
{
    public bool Cracked { get; set; }
}

public class Bacon
{

}

public class Bread
{
    public Bread()
    {
        Toast = false;
        Buttered = false;
        Jammy = false;
    }
    public bool Toast { get; set; }
    public bool Buttered { get; set; }
    public bool Jammy { get; set; }
}

public class Juice
{

}
