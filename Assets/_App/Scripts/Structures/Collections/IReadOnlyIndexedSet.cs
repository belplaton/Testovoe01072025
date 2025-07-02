using System.Collections.Generic;

namespace TTR24.Structures.Collections
{
	public interface IReadOnlyIndexedSet<T> : IEnumerable<T>
	{
		public delegate void OnChanged(IndexedSetOperation op, T value);
		public event OnChanged OnChange;
		
		public int Count { get; }
		public bool TryGetIndex(T item, out int index);
		public bool Contains(T item);
		public T GetByIndex(int index);
	}
}