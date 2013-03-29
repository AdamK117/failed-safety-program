using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Data.DOM;

namespace SafetyProgram.Data.IO
{
    public interface ICoshhDataService
    {
        bool Save(CoshhDocument document);

        bool Load(CoshhDocument document);

        bool SaveAs(CoshhDocument document);

        bool Close();

        string Path();
    }
}
