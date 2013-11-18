using System;
using SafetyProgram.Base.CSharp.Interfaces;

namespace SafetyProgram.Base.CSharp
{
    /// <summary>
    /// Defines a class that may hold another class as content. Contains events that notify if the content changes.
    /// </summary>
    /// <typeparam name="TContent">The type of the content held by this holder.</typeparam>
    /// <remarks>
    ///     This is used when content may be swapped out at one location. Traditionally, observers may subsribe to a field of a larger construct: a document, a window, a table, etc.
    ///     
    ///     ADVANTAGES
    ///     1) This allows observers to take a sparse interface of a mutable field instead of having to take the whole construct.
    ///     2) A side effect of point 1), this allows observer implementations to be extremely flexible.
    ///     
    ///     DISADVANTAGES
    ///     1) Implementations that observe the <c>Holder</c>, being generic, may be difficult to characterize where they are useful. This requires more knowledge of each generic implementation available.
    /// </remarks>
    public sealed class Holder<TContent> : IEditableHolder<TContent>
    {
        private TContent content;

        /// <summary>
        /// Construct an instance of the <c>Holder</c> class.
        /// </summary>
        /// <param name="content">The content the <c>Holder</c> will hold. (Nullable)</param>
        public Holder(TContent content)
        {
            //Helpers.NullCheck(content);

            this.content = content;
        }

        /// <summary>
        /// Get/Set the content of the <c>Holder</c>. (Nullable)
        /// </summary>
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

        /// <summary>
        /// Get the content of the <c>IHolder</c>. (Nullable)
        /// </summary>
        TContent IHolder<TContent>.Content
        {
            get { return content; }
        }

        /// <summary>
        /// An event that fires if the <c>Content</c> of the <c>Holder</c> changes.
        /// </summary>
        public event EventHandler<
            GenericPropertyChangedEventArg<
                object>> ContentChanged;
    }
}
