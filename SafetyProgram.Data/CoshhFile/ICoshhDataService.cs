using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Data.CoshhFile
{
    interface ICoshhDataService
    {
        bool Save(CoshhFileData data);

        bool Load(CoshhFileData data);

        bool SaveAs(CoshhFileData data);

        bool Close();

        string Path();
    }
}
