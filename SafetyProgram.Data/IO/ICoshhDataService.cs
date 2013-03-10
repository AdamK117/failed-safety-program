using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Data.DOM;

namespace SafetyProgram.Data.IO
{
    public interface ICoshhDataService
    {
        bool Save(CoshhDocument data);

        bool Load(CoshhDocument data);

        bool SaveAs(CoshhDocument data);

        bool Close();

        string Path();
    }
}
