namespace ETlib;

public class EnergyPrice
{
    public int Id { get; set; }
    
    private double _dkkPerKWh;
    //ændres til offset så den kan se forskellen fra UTC og den tid der står på den. altså + 02:00 som der står i json fra api

    private DateTimeOffset _timeStart;

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
    
 
    public DateTimeOffset time_start
    {
        get => _timeStart;
        set
        {
            //er UTC datettime så den tager højde for tidszone og sommer/vinter
            if (value >= new DateTimeOffset(2125, 1, 1, 0, 0, 0, TimeSpan.FromHours(2)))
            {
                throw new ArgumentOutOfRangeException("Value must be less than 2125");
            }

            if (value <= new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.FromHours(2)))
            {
                throw new ArgumentOutOfRangeException("Value must be greater than 2025");
            }
            
            var DKTime = TimeZoneInfo.FindSystemTimeZoneById("central european standard time");
            _timeStart = TimeZoneInfo.ConvertTime(value, DKTime);
            
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