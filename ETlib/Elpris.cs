namespace ETlib;

public class Elpris
{
    public double Pris { get; set; }
    public DateTime  Tid { get; set; }
    
    public Elpris( double pris, DateTime tid)
    {
        Pris = pris;
        Tid = tid;
    }
}