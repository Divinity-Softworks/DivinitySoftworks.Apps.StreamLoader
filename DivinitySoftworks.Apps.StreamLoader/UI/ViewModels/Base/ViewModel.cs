using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DivinitySoftworks.Apps.StreamLoader.UI.ViewModels.Base {
    public class ViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Notify([CallerMemberName] string? propertyName = null, params string[] additionalProperties) {
            OnPropertyChanged(propertyName);
            foreach (string p in additionalProperties)
                OnPropertyChanged(p);
        }

        public bool ChangeAndNotify<T>(ref T field, T value, [CallerMemberName] string? propertyName = null, params string[] additionalProperties) {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            if (comparer.Equals(field, value) == false) {
                field = value;
                OnPropertyChanged(propertyName);
                foreach (string p in additionalProperties)
                    OnPropertyChanged(p);
                return true;
            }
            return false;
        }

        public bool ChangeAndNotifyWithAction<T>(ref T field, T value, Action action, [CallerMemberName] string? propertyName = null, params string[] additionalProperties) {
            bool changed = ChangeAndNotify(ref field, value, propertyName, additionalProperties);
            if (changed)
                action();
            return changed;
        }
    }
}
