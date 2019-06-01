using FourWays.Core.Services;

namespace FourWays.Core
{
    public class CalculationService : ICalculationService
    {
        public decimal TipAmount(decimal subTotal, double generosity)
        {
            return subTotal * (decimal)(generosity / 100);
        }
    }
}

