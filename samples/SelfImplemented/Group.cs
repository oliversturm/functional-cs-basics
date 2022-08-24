using System;
using System.Collections.Generic;
using System.Collections;

namespace SelfImplemented {
	public class Group<TGroupKey, TData> : IEnumerable<TData> {
		public Group(TGroupKey key) {
			this.key = key;
			this.innerList = new List<TData>( );
		}

		private TGroupKey key;
		public TGroupKey Key {
			get { return key; }
		}

		private List<TData> innerList;
		public void Add(TData data) {
			innerList.Add(data);
		}

		#region IEnumerable<TData> Members

		IEnumerator<TData> IEnumerable<TData>.GetEnumerator( ) {
			return ((IEnumerable<TData>) innerList).GetEnumerator( );
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator( ) {
			return ((IEnumerable) innerList).GetEnumerator( );
		}

		#endregion
	}
}
