using System;
using System.Collections.ObjectModel;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DOM.Models;

namespace SafetyProgram.DOM
{
    /// <summary>
    /// Defines an implementation of IDoc. A class that holds document information (content, print format, etc.)
    /// </summary>
    public sealed class Doc : IDoc
    {
        /// <summary>
        /// Construct a new instance of Doc. A class that holds the information associated with a document.
        /// </summary>
        /// <param name="items">The items (content) of the Doc.</param>
        /// <param name="format">The format associated with the Doc.</param>
        public Doc(ObservableCollection<IDocObj> items, IFormat format)
        {
            Helpers.NullCheck(items, format);

            this.items = items;
            this.format = format;
        }

        private readonly ObservableCollection<IDocObj> items;

        /// <summary>
        /// Get the IDocObjects contained within the Doc.
        /// </summary>
        public ObservableCollection<IDocObj> Items
        {
            get { return items; }
        }

        private IFormat format;

        /// <summary>
        /// Get the IFormat associated with the Doc.
        /// </summary>
        public IFormat Format
        {
            get { return format; }
        }

        /// <summary>
        /// Change the Format of the Doc to a new (i.e. non-null) format.
        /// </summary>
        /// <param name="newFormat">The new format of the Doc.</param>
        public void ChangeFormat(IFormat newFormat)
        {
            if (newFormat != null)
            {
                format = newFormat;
                FormatChanged.Raise(this, format);
            }
            else throw new ArgumentNullException("Cannot set a Doc's format to null!");
        }

        /// <summary>
        /// Occurs when the Format of the Doc changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<IFormat>> FormatChanged;
    }
}
