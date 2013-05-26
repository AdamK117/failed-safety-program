using System;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base
{
    public sealed class ChildHolder<TParent, TChild> : IHolder<TChild>
    {
        private readonly IHolder<TParent> parentHolder;
        private readonly Func<TParent, TChild> accessor;
        private TChild content;

        public ChildHolder(IHolder<TParent> parentHolder, Func<TParent, TChild> accessor)
        {
            Helpers.NullCheck(parentHolder, accessor);

            this.parentHolder = parentHolder;
            this.accessor = accessor;

            this.content = accessor(parentHolder.Content);

            parentHolder.ContentChanged += parentHolderContentChanged;
        }

        public TChild Content
        {
            get { return content; }
            private set
            {
                content = value;
                ContentChanged.Raise(this, content);
            }
        }

        private void parentHolderContentChanged(object sender, object newContent)
        {
            if (parentHolder.Content == null)
            {
                this.Content = default(TChild);
            }
            else
            {
                this.Content = accessor(parentHolder.Content);
            }
        }

        public event EventHandler<GenericPropertyChangedEventArg<object>> ContentChanged;
    }
}
