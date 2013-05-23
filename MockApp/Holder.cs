using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Base;

namespace MockApp
{
    public sealed class Holder<TContent> : IEditableHolderT<TContent>
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

        public event Action<object, TContent> ContentChanged;

        object IHolder.Content
        {
            get { return content; }
        }

        event EventHandler<GenericPropertyChangedEventArg<object>> IHolder.ContentChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        TContent IEditableHolderT<TContent>.Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                if (ContentChanged != null)
                {
                    ContentChanged(this, content);
                }
            }
        }

        TContent IHolderT<TContent>.Content
        {
            get { return content; }
        }


        
    }
}
