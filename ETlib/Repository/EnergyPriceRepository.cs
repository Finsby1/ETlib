using System.Text.Json.Nodes;

namespace ETlib.Repository;

public class EnergyPriceRepository
{

    private int _nextId = 0;
    private readonly Dictionary<int, EnergyPrice> _energyPricesWest = new Dictionary<int, EnergyPrice>();
    private readonly Dictionary<int, EnergyPrice> _energyPricesEast = new Dictionary<int, EnergyPrice>();
    
    public EnergyPrice Add(EnergyPrice energyPrice, int zone)
    {

        
        energyPrice.Id = _nextId++;
        
        if (zone == 1)
        {
            _energyPricesWest.Add(energyPrice.time_start.Hour, energyPrice);
        }

        else if (zone == 2)
        {
            _energyPricesEast.Add(energyPrice.time_start.Hour, energyPrice);
        }
  
        else
        {
            throw new ArgumentException("Invalid zone");
        }
        return energyPrice;
        
    }

    public IEnumerable<EnergyPrice> GetSavedPrices(int zone)
    {
        if (zone == 1)
        {
            return _energyPricesWest.Values;
            
        }
        else if (zone == 2)
        {
            return _energyPricesEast.Values;
        }
        else
        {
            throw new ArgumentException("Invalid zone");
        }
    }
    
    public EnergyPrice GetByHour(int hour, int zone)
    {
        EnergyPrice EPNow;
        if (zone == 1)
        {
            try
            {
                EPNow = _energyPricesWest[hour];
            }
            catch (Exception e)
            {
                throw new ArgumentException("No energy price found for the given hour");
            }
        }
        else if (zone == 2)
        {
            try
            {
                EPNow = _energyPricesEast[hour];
            }
            catch (Exception e)
            {
                throw new ArgumentException("No energy price found for the given hour");
            }
        }
        else
        {
            throw new ArgumentException("Invalid zone");
        }

        return EPNow;
    }
}