using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.UI.ContentViews
{
    /// <summary>
    /// Defines a standard implementation of a content view 
    /// viewmodel.
    /// </summary>
    public sealed class ContentViewModel :
        IContentViewModel
    {
        public ContentViewModel(IHolder<Control> contentView)
        {
            Helpers.NullCheck(contentView);

            this.contentView = contentView;

            this.contentView.ContentChanged +=
                (s, e) => 
                    PropertyChanged.Raise(this, "Content");
        }

        private readonly IHolder<Control> contentView;

        /// <summary>
        /// Get the content view.
        /// </summary>
        public Control Content
        {
            get { return contentView.Content; }
        }

        /// <summary>
        /// Occurs if a property on the viewmodel changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
