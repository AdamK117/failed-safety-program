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
            if (parentHolder == null ||
                accessor == null)
            throw new ArgumentNullException();
            else
            {
                this.parentHolder = parentHolder;
                this.accessor = accessor;

                this.content = accessor(parentHolder.Content);

                parentHolder.ContentChanged += (sender, newContent) =>
                    {
                        if (newContent == null) this.Content = default(TChild);
                        else this.Content = accessor(newContent);
                    };
            }
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

        public event Action<object, TChild> ContentChanged;
    }
}
