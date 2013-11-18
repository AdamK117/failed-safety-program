using System;
using SafetyProgram.Base.CSharp.Interfaces;

namespace SafetyProgram.Base.CSharp
{
    /// <summary>
    /// Defines a <c>ChildHolder</c>. The <c>ChildHolder</c> acts as a sub-holder for a parent <c>Holder</c>. Changes in the parent will echo into this <c>ChildHolder</c>.
    /// </summary>
    /// <typeparam name="TParent">The type of data held by the parent <c>Holder</c>.</typeparam>
    /// <typeparam name="TChild">The type of data held by this <c>ChildHolder</c>.</typeparam>
    /// <seealso cref="SafetyProgram.Base.Holder"/>
    public sealed class ChildHolder<TParent, TChild> : IHolder<TChild>
    {
        private readonly IHolder<TParent> parentHolder;
        private readonly Func<TParent, TChild> accessor;
        private TChild content;

        /// <summary>
        /// Construct an instance of the <c>ChildHolder</c>.
        /// </summary>
        /// <param name="parentHolder">The parent <c>Holder</c> of this <c>ChildHolder</c>. If the contents of the <c>Holder</c> changes, the <c>ChildHolder</c> will refresh its content using the <c>accessor</c>.</param>
        /// <param name="accessor">A method this <c>ChildHolder</c> will use to get content from the <c>parentHolder</c>.</param>
        public ChildHolder(IHolder<TParent> parentHolder, Func<TParent, TChild> accessor)
        {
            Helpers.NullCheck(parentHolder, accessor);

            this.parentHolder = parentHolder;
            this.accessor = accessor;

            //Immediately fill with content (if possible).
            this.content = accessor(parentHolder.Content);

            //Listen for changes in the parent.
            parentHolder.ContentChanged += parentHolderContentChanged;
        }

        /// <summary>
        /// Get the content of this <c>ChildHolder</c>.
        /// </summary>
        public TChild Content
        {
            get { return content; }
            private set
            {
                content = value;
                ContentChanged.Raise(this, content);
            }
        }

        /// <summary>
        /// A handler that fires if <c>parentHolder</c> changes. This <c>ChildHolder</c> will use <c>accessor</c> to retrieve its data from the new parent.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newContent"></param>
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

        /// <summary>
        /// An event that fires if the <c>Content</c> of the <c>ChildHolder</c> changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<object>> ContentChanged;
    }
}
