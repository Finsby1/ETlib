namespace ETlib;

public class EnergyPrice
{
    public int Id { get; set; }
    
    public double _dkkPrice;

    public DateTime _time;

    public string _category;


    
    
    public double DkkPrice
    {
        get => _dkkPrice;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("Value cannot be negative");
            }

            if (value >= 20)
            {
                throw new ArgumentOutOfRangeException("Value cannot be more than 19");
            }
            _dkkPrice = value;
        }
    }

    public DateTime Time
    {
        get => _time;
        set
        {
            if (value >= new DateTime(2125, 1, 1, 0, 0, 0))
            {
                throw new ArgumentOutOfRangeException("Value must be less than 2125");
            }

            if (value <= new DateTime(2025, 1, 1, 0, 0, 0))
            {
                throw new ArgumentOutOfRangeException("Value must be greater than 2025");
            }
            _time = value;
        }
    }

    public string Category
    {
        get => _category;
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException("Category cannot be null");
            }

            if (value.Length < 3)
            {
                throw new ArgumentOutOfRangeException("Category cannot be less than 3 characters");
            }
            _category = value;
        }
    }

    public EnergyPrice(double dkkPrice, DateTime time, string category)
    {
        DkkPrice = dkkPrice;
        Time = time;
        Category = category;
    }

    public EnergyPrice()
    {
        
    }




}