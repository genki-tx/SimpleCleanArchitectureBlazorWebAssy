namespace SCA
{
    // IPresenter
    // Interface for Presenter
    public interface ICountPresenter
    {
        IPropertyObservable<int> CountA { get; }
        IPropertyObservable<int> CountB { get; }
        void IncrementCount(CountType type);
    }
}