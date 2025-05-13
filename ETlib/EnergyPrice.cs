namespace ETlib;

public class EnergyPrice
{
    public int Id { get; set; }
    
    private double _dkkPerKWh;

    private DateTime _timeStart;

    private string? _category;
    
    


    
    
    public double DKK_per_kWh
    {
        get => _dkkPerKWh;
        set
        {
            if (value <= -20)
            {
                throw new ArgumentOutOfRangeException("Price cannot be less than -20" + value);
            }

            if (value >= 20)
            {
                throw new ArgumentOutOfRangeException("Value cannot be more than 19");
            }
            _dkkPerKWh = value;
        }
    }
    
 
    public DateTime time_start
    {
        get => _timeStart;
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
            _timeStart = value;
        }
    }

    public string Category
    {
        get => _category;
        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentOutOfRangeException("Category cannot be less than 3 characters");
            }
            _category = value;
        }
    }

    public EnergyPrice(double dkkPerKWh, DateTime timeStart, string category)
    {
        DKK_per_kWh = dkkPerKWh;
        time_start = timeStart;
        Category = category;
    }

    public EnergyPrice()
    {
        
    }




}