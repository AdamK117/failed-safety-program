using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Base.Interfaces;

namespace MockApp.Document
{
    public sealed class MainDocument : IDocument
    {
        public System.Collections.ObjectModel.ObservableCollection<IRibbonTabItem> RibbonTabs
        {
            get { throw new NotImplementedException(); }
        }

        public string Title
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler<SafetyProgram.Base.GenericPropertyChangedEventArg<string>> TitleChanged;

        public IDocumentBody Body
        {
            get { throw new NotImplementedException(); }
        }

        public IFormat Format
        {
            get { throw new NotImplementedException(); }
        }

        public void ChangeFormat(IFormat newFormat)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<SafetyProgram.Base.GenericPropertyChangedEventArg<IFormat>> FormatChanged;

        public System.Windows.Controls.Control View
        {
            get { throw new NotImplementedException(); }
        }

        public IContextMenu ContextMenu
        {
            get { throw new NotImplementedException(); }
        }

        public void FlagForRemoval()
        {
            throw new NotImplementedException();
        }

        public bool RemoveFlag
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<SafetyProgram.Base.GenericPropertyChangedEventArg<bool>> RemoveFlagChanged;

        public void FlagAsEdited()
        {
            throw new NotImplementedException();
        }

        public bool EditedFlag
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<SafetyProgram.Base.GenericPropertyChangedEventArg<bool>> EditedFlagChanged;

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
