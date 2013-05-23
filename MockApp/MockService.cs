using System;
using System.Windows.Controls;
using MockApp.Document;
using SafetyProgram.Base.Interfaces;

namespace MockApp
{
    public sealed class MockService : IIOService<IDocument>
    {
        public IDocument New()
        {
            return new MainDocument();
        }

        public bool CanNew()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Save(IDocument data)
        {
            throw new NotImplementedException();
        }

        public bool CanSave(IDocument data)
        {
            throw new NotImplementedException();
        }

        public void SaveAs(IDocument data)
        {
            throw new NotImplementedException();
        }

        public bool CanSaveAs(IDocument data)
        {
            throw new NotImplementedException();
        }

        public IDocument Load()
        {
            throw new NotImplementedException();
        }

        public bool CanLoad()
        {
            throw new NotImplementedException();
        }
    }
}
