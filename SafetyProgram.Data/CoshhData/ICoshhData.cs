using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Data
{
    interface ICoshhData
    {
        bool Save();

        bool Load();

        bool SaveAs();

        bool Close();
    }
}
