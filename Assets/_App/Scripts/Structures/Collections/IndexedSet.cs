using System.Collections;
using System.Collections.Generic;

namespace TTR24.Structures.Collections
{
	public sealed class IndexedSet<T> : IIndexedSet<T>
	{
		private readonly Dictionary<T, int> _indexDict;
		private readonly List<T> _items;

		public event IReadOnlyIndexedSet<T>.OnChanged OnChange;
		public int Count => _items.Count;
		
		public IndexedSet(int capacity)
		{
			_indexDict = new Dictionary<T, int>(capacity);
			_items = new List<T>(capacity);
		}
		
		public IndexedSet() : this(0)
		{
			
		}

		public bool TryAdd(T item)
		{
			if (!_indexDict.ContainsKey(item))
			{
				_items.Add(item);
				_indexDict[item] = _items.Count - 1;
				OnChange?.Invoke(IndexedSetOperation.Add, item);
				return true;
			}

			return false;
		}

		public void Add(T item)
		{
			TryAdd(item);
		}

		public bool TryGetIndex(T item, out int index)
		{
			return _indexDict.TryGetValue(item, out index);
		}
		
		public bool Contains(T item)
		{
			return _indexDict.ContainsKey(item);
		}

		public T GetByIndex(int index)
		{
			return _items[index];
		}
		
		public void RemoveByIndex(int index)
		{
			RemoveByIndex(index, out _);
		}
		
		public void RemoveByIndex(int index, out T value)
		{
			value = _items[index];
			var item = _items[^1];
			_items[index] = item;
			_indexDict[item] = index;
			_items.RemoveAt(_items.Count - 1);
			_indexDict.Remove(value);
			OnChange?.Invoke(IndexedSetOperation.Remove, item);
		}

		public bool Remove(T item)
		{
			if (_indexDict.TryGetValue(item, out var index))
			{
				var lastIndex = _items.Count - 1;
				var lastItem = _items[lastIndex];
					
				_items[index] = lastItem;
				_indexDict[lastItem] = index;
					
				_items.RemoveAt(lastIndex);
				_indexDict.Remove(item);
				OnChange?.Invoke(IndexedSetOperation.Remove, item);
				return true;
			}

			return false;
		}

		public void Clear()
		{
			_items.Clear();
			_indexDict.Clear();
			OnChange?.Invoke(IndexedSetOperation.Clear, default);
		}

		public IReadOnlyList<T> GetItemsAsList() => _items;
		public IEnumerator<T> GetEnumerator()
		{
			for (var i = 0; i < Count; i++)
			{
				yield return _items[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}