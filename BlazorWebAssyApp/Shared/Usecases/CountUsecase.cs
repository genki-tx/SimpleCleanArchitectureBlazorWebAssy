using System.Collections.Generic;

namespace SCA
{
    // Usecase
    // Usecase can depend on Gateway through its interface.
    // Usecase can't be dependent on View, Presenter
    public class CountUsecase : ICountUsecase
    {
        // Reactive Properties
        public IPropertyObservable<Dictionary<CountType, Count>> Counts => _counts;
        private readonly PropertyObservable<Dictionary<CountType, Count>> _counts;

        // Dependency
        private readonly ICountDBGateway _gateway;

        public CountUsecase(ICountDBGateway gateway)
        {
            _gateway = gateway;

            // Create new PropertyObservable with initial value
            _counts = new(new()
            {
                { CountType.A, new() },
                { CountType.B, new() },
            });
        }

        public void IncrementCount(CountType type)
        {
            var current = _gateway.GetCount(type);
            var new_count = current + 1;
            _gateway.SetCount(type, new_count);

            // publish the value changed
            var dict = _counts.Value;
            dict[type].Num = new_count;
            _counts.Notify(dict);
        }

        public int GetCount(CountType type)
        {
            return _gateway.GetCount(type);
        }
    }
}