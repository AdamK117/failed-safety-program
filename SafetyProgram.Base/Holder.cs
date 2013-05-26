using System;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base
{
    public sealed class Holder<TContent> : IEditableHolder<TContent>
    {
        private TContent content;

        public Holder(TContent content)
        {
            Helpers.NullCheck(content);

            this.content = content;
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

        public event EventHandler<GenericPropertyChangedEventArg<object>> ContentChanged;
    }
}
