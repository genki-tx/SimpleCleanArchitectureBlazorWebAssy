using System.Collections.Generic;

namespace SCA
{
    // IUsecase
    // Interface for Usecase
    public interface ICountUsecase
    {
        // Increase counter of specifed type
        void IncrementCount(CountType type);
        // Reactive Property to get update of Counter
        IPropertyObservable<Dictionary<CountType, Count>> Counts { get; } // pair of <count type, count object>
    }
}