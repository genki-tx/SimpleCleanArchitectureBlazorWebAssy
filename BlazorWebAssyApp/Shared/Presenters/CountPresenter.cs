using System;
namespace SCA
{
    // Presenter
    // Presenter can depend on Usecase through its interface
    // Presenter can't dependent on View, Gateway
    public class CountPresenter : ICountPresenter, IDisposable
    {
        // Reactive Properties
        public IPropertyObservable<int> CountA => _countA;
        private readonly PropertyObservable<int> _countA;
        public IPropertyObservable<int> CountB => _countB;
        private readonly PropertyObservable<int> _countB;

        // Dependency
        private readonly ICountUsecase _usecase;

        private readonly DisposableList _disposables = new();

        public CountPresenter(ICountUsecase usecase)
        {
            _usecase = usecase;

            // Create reactive properties with initial values
            _countA = new(0);
            _countB = new(0);

            // Start subscribing reactive property
            var disposable = _usecase.Counts.Subscribe((dict) =>
            {
                // This function here will be called when _usecase.Counts updated
                // This update signal will be triggered by _counts.Notify() in the Usecase
                foreach (var element in dict)
                {
                    switch (element.Key)
                    {
                        case CountType.A:
                            _countA.Notify(element.Value.Num);
                            break;
                        case CountType.B:
                            _countB.Notify(element.Value.Num);
                            break;
                        default:
                            break;
                    }
                }
            });

            // You need to hold disposable objects
            // And release those disposables when this instance is disposed
            _disposables.Add(disposable);
        }

        public void IncrementCount(CountType type)
        {
            _usecase.IncrementCount(type);
        }

        public void Dispose()
        {
            // Release all of registered subscriptions
            _disposables.DisposeAll();
        }
    }
}