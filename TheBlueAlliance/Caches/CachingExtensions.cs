using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace TBA.Caches
{
    public static class CachingExtensions
    {
        public static IObservable<T> WithCache<T>(
            this IObservable<T> source,
            Func<T> get,
            Action<T> put) where T : class
        {
            return Observable.Create<T>(observer =>
            {
                var cached = get();
                if (cached != null)
                {
                    observer.OnNext(cached);
                }
                source.Subscribe(item =>
                {
                    put(item);
                    observer.OnNext(item);
                }, observer.OnError, observer.OnCompleted);
                return Disposable.Empty;
            });
        }
    }
}
