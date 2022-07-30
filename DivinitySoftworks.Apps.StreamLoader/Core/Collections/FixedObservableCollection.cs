namespace System.Collections.ObjectModel {
    public class FixedObservableCollection<T> : ObservableCollection<T> {

        public int Capacity { get; }

        public FixedObservableCollection(int capacity) {
            Capacity = capacity;
        }

        public new void Add(T item) {
            if (Count >= Capacity) 
                RemoveAt(0);

            base.Add(item);
        }
    }
}
