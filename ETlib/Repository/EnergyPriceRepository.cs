using System.Text.Json.Nodes;

namespace ETlib.Repository;

public class EnergyPriceRepository
{

    private int _nextId = 0;
    private readonly Dictionary<int, EnergyPrice> _energyPricesWest = new Dictionary<int, EnergyPrice>();
    private readonly Dictionary<int, EnergyPrice> _energyPricesEast = new Dictionary<int, EnergyPrice>();
    
    public EnergyPrice Add(EnergyPrice energyPrice, int zone)
    {
        if (zone == 1)
        {
            _energyPricesWest.Clear();
        }

        if (zone == 2)
        {
            _energyPricesEast.Clear();
        }
        
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

    public IEnumerable<EnergyPrice> GetEnergyPricesWest()
    {
        return _energyPricesWest.Values;
    }
    
    
}