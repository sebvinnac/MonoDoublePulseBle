﻿using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
//using Acr.UserDialogs.Forms;
using Shiny;

namespace ShinyMod
{
    public static class Extensions
    {
        public static IDisposable SubOnMainThread<T>(this IObservable<T> obs, Action<T> onNext)
    => obs
        .ObserveOn(RxApp.MainThreadScheduler)
        .Subscribe(onNext);


        public static IDisposable SubOnMainThread<T>(this IObservable<T> obs, Action<T> onNext, Action<Exception> onError)
            => obs
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(onNext, onError);


        public static IDisposable SubOnMainThread<T>(this IObservable<T> obs, Action<T> onNext, Action<Exception> onError, Action onComplete)
            => obs
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(onNext, onError, onComplete);
    }
}
