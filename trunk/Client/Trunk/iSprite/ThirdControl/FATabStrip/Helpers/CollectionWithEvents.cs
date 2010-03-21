namespace iSprite.ThirdControl.FarsiLibrary.Helpers
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class CollectionWithEvents : CollectionBase
    {
        private int _suspendCount = 0;

        [Browsable(false)]
        public event CollectionClear Cleared;

        [Browsable(false)]
        public event CollectionClear Clearing;

        [Browsable(false)]
        public event CollectionChange Inserted;

        [Browsable(false)]
        public event CollectionChange Inserting;

        [Browsable(false)]
        public event CollectionChange Removed;

        [Browsable(false)]
        public event CollectionChange Removing;

        protected int IndexOf(object value)
        {
            return base.List.IndexOf(value);
        }

        protected override void OnClear()
        {
            if (!this.IsSuspended && (this.Clearing != null))
            {
                this.Clearing();
            }
        }

        protected override void OnClearComplete()
        {
            if (!this.IsSuspended && (this.Cleared != null))
            {
                this.Cleared();
            }
        }

        protected override void OnInsert(int index, object value)
        {
            if (!this.IsSuspended && (this.Inserting != null))
            {
                this.Inserting(index, value);
            }
        }

        protected override void OnInsertComplete(int index, object value)
        {
            if (!this.IsSuspended && (this.Inserted != null))
            {
                this.Inserted(index, value);
            }
        }

        protected override void OnRemove(int index, object value)
        {
            if (!this.IsSuspended && (this.Removing != null))
            {
                this.Removing(index, value);
            }
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            if (!this.IsSuspended && (this.Removed != null))
            {
                this.Removed(index, value);
            }
        }

        public void ResumeEvents()
        {
            this._suspendCount--;
        }

        public void SuspendEvents()
        {
            this._suspendCount++;
        }

        [Browsable(false)]
        public bool IsSuspended
        {
            get
            {
                return (this._suspendCount > 0);
            }
        }
    }
}
