using System;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Base.DocumentFormats;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Body;

namespace SafetyProgram.Document
{
    public static class CoshhDocumentDefaults
    {
        public static CoshhDocument DefaultCoshhDocument(IConfiguration appConfiguration)
        {
            return new CoshhDocument(
                appConfiguration,
                DefaultTitle,
                DefaultFormat(),
                DefaultBody(),
                DefaultViewCtor()
                );
        }

        public const string DefaultTitle = "someDefaultTitle";

        public static IDocFormat DefaultFormat()
        {
            return new A4DocFormat();
        }

        public static IDocumentBody DefaultBody()
        {
            return new CoshhDocumentBody();
        }

        public static Func<CoshhDocument, Control> DefaultViewCtor()
        {
            return (viewModel) => new CoshhDocumentView(viewModel);
        }
    }
}
