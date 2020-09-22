/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7/25
// desc： 参考：https://www.cnblogs.com/yuefei/p/4062998.html
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections;

namespace WLib.WinCtrls.ListCtrl
{
    [System.ComponentModel.ListBindable(false)]
    public class ListBoxItemCollection : IList, ICollection, IEnumerable
    {
        public ListBoxItemCollection(ImageListBoxEx owner) => this.Owner = owner;

        internal ImageListBoxEx Owner { get; }

        #region  override
        public ListBoxItem this[int index]
        {
            get => Owner.Items[index] as ListBoxItem;
            set { Owner.Items[index] = value; }
        }

        public int Count => Owner.Items.Count;

        public bool IsReadOnly => Owner.Items.IsReadOnly;

        public int Add(ListBoxItem item)
        {
            if (item == null)
                throw new ArgumentException("item is null");
            return Owner.Items.Add(item);
        }

        public void AddRange(ListBoxItem[] items) => Owner.Items.AddRange(items);

        public void Clear() => Owner.Items.Clear();

        public bool Contains(ListBoxItem item)
        {
            bool rst = false;
            foreach (ListBoxItem oldItem in Owner.Items)
            {
                if (oldItem == item)
                {
                    rst = true;
                    break;
                }
            }
            return rst;
        }

        public void CopyTo(ListBoxItem[] destination, int arrayIndex) => Owner.Items.CopyTo(destination, arrayIndex);

        public int IndexOf(ListBoxItem item) => Owner.Items.IndexOf(item);

        public void Insert(int index, ListBoxItem item)
        {
            if (item == null)
                throw new ArgumentException("item is null");
            Owner.Items.Insert(index, item);
        }

        public void Remove(ListBoxItem item) => Owner.Items.Remove(item);

        public void RemoveAt(int index) => Owner.Items.RemoveAt(index);

        public IEnumerator GetEnumerator() => Owner.Items.GetEnumerator();

        int IList.Add(object value)
        {
            if (!(value is ListBoxItem))
                throw new ArgumentException();
            return Add(value as ListBoxItem);
        }

        void IList.Clear() => Clear();

        bool IList.Contains(object value) => Contains(value as ListBoxItem);

        int IList.IndexOf(object value) => IndexOf(value as ListBoxItem);

        void IList.Insert(int index, object value)
        {
            if (!(value is ListBoxItem))
                throw new ArgumentException();
            Insert(index, value as ListBoxItem);
        }

        bool IList.IsFixedSize => false;

        bool IList.IsReadOnly => IsReadOnly;

        void IList.Remove(object value) => Remove(value as ListBoxItem);

        void IList.RemoveAt(int index) => RemoveAt(index);

        object IList.this[int index]
        {
            get { return this[index]; }
            set
            {
                if (!(value is ListBoxItem))
                    throw new ArgumentException();
                this[index] = value as ListBoxItem;
            }
        }

        void ICollection.CopyTo(Array array, int index) => CopyTo((ListBoxItem[])array, index);

        int ICollection.Count => Count;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => false;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
}
