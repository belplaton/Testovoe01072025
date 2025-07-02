namespace TTR24.Structures.Collections
{
	public interface IIndexedSet<T> : IReadOnlyIndexedSet<T>
	{
		public bool TryAdd(T item);
		public void Add(T item);
		public bool Remove(T item);
		public void Clear();
	}
}