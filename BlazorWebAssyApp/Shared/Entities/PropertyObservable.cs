using System;
using System.Reactive.Subjects;
namespace SCA
{
    // This is a wrapper class of Subject from Reactive Extentions.
    // We use Subject as Observable, but Subject exposes some method that can change the internal value from subscriber
    // (e.g. subscriber can call Subject.OnNext)
    // But if we just hide it by IObservable interface, we have another problem,
    // We can't get the current value simlar as ReactiveProperty
    // We use BehaviorSubject but if we filter the interfaces by IObservable, it will hide Value method
    // So we introduced IPropertyObservable inheriting IObservable but also with "Value" getter
    // Internally we use BehaviorSubject to cache the latest value

    public interface IPropertyObservable<T> : IObservable<T>
    {
        public T Value { get; }
    }
    public class PropertyObservable<T> : IPropertyObservable<T>
    {
        public BehaviorSubject<T> Subject { get; set; }
        public PropertyObservable(T initial)
        {
            Subject = new(initial);
        }
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return Subject.Subscribe(observer);
        }
        public T Value => Subject.Value;

        // Notify the value updated to subscribers
        public void Notify(T new_value)
        {
            Subject.OnNext(new_value);
        }
    }
}
