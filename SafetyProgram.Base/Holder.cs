using System;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base
{
    public sealed class Holder<TContent> : IEditableHolder<TContent>
    {
        private TContent content;

        public Holder(TContent content)
        {
            if (content != null)
            {
                this.content = content;
            }
            else throw new ArgumentNullException();
        }

        public TContent Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                ContentChanged.Raise(this, content);
            }
        }

        TContent IHolder<TContent>.Content
        {
            get { return content; }
        }

        public event Action<object, TContent> ContentChanged;
    }
}
