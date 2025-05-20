using System.Text.Json.Nodes;
using ETlib.Models;
using ETlib.Repository;

namespace ETlib.Repository;

public class EnergyPriceRepository
{

    PriceIntervalRepository _priceIntervalRepository;
    
    public EnergyPriceRepository(PriceIntervalRepository priceIntervalRepository)
    {
        _priceIntervalRepository = priceIntervalRepository;
    }
    
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
    public void Restart()
    {
        _nextId = 0;
        _energyPricesWest.Clear();
        _energyPricesEast.Clear();
    }
    public IEnumerable<EnergyPrice> GetAllForTest(int zone)
    {
        if (zone == 1)
        {
            var values = _energyPricesWest.Values;
            return values;
        }
        else if (zone == 2)
        {
            var values = _energyPricesEast.Values;
            return values;
        }
        else
        {
            throw new ArgumentException("Invalid zone");
        }

    }

    
    public IEnumerable<EnergyPrice> GetSavedPrices(int zone)
    {
        if (zone == 1)
        {
            var values = _energyPricesWest.Values;
            foreach (EnergyPrice EP in values)
            {
                SetCategory(EP);
            }
            return values;
            
        }
        else if (zone == 2)
        {
            
            var values = _energyPricesEast.Values;
            foreach (EnergyPrice EP in values)
            {
                SetCategory(EP);
            }
            return values;
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
        EnergyPrice EPToSend = SetCategory(EPNow);
        return EPToSend;
    }
    public EnergyPrice GetByHourForTest( int hour, int zone)
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
    

    public EnergyPrice SetCategory(EnergyPrice energyPrice)
    {
        try
        {
            PriceInterval? PI = _priceIntervalRepository.GetById(2);

            if (PI == null)
            {
                PI = _priceIntervalRepository.GetById(1);
            }

            SetCategory2(energyPrice, PI);
        }
        catch (Exception e) 
        {
            throw new ArgumentException("could not retrieve price interval");
        }
        return energyPrice;
        
    }

    public static void SetCategory2(EnergyPrice energyPrice, PriceInterval PI)
    {
        if (energyPrice.DKK_per_kWh >= PI.High)
        {
            energyPrice.Category = "high";
        }

        else if (energyPrice.DKK_per_kWh <= PI.Low)
        {
            energyPrice.Category = "low";
        }

        else if (energyPrice.DKK_per_kWh < PI.High && energyPrice.DKK_per_kWh > PI.Low)
        {
            energyPrice.Category = "medium";
        }
    }
}