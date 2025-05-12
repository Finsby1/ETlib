using System.Text.Json.Nodes;

namespace ETlib.Repository;

public class EnergyPriceRepository
{

    private int _nextId = 0;
    private readonly Dictionary<int, EnergyPrice> _energyPrices = new Dictionary<int, EnergyPrice>();>
    
    public EnergyPrice Add(EnergyPrice energyPrice)
    {
        return _energyPrices[_nextId] = energyPrice;
    }
}