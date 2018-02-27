using System;
using System.Collections.Generic;
using Luca.Siemens.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Siemens.Repo
{
    public static class SiemensRepo
    {
        public static Luca.Siemens.Models.SiemensPLC SiemensPlc = new Luca.Siemens.Models.SiemensPLC()
        {
            Plc = new S7.Net.Plc(S7.Net.CpuType.S71200, "192.168.2.1", 0, 1),
            Data = new List<Luca.Siemens.Models.SiemensTag>() {
                (new SiemensTag (Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Int, typeof(Int16), 1, 0, "Int16")),
                (new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Int, typeof(UInt16), 1, 2, "UInt16")),
                (new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.DInt, typeof(Int32), 1, 4, "Int32")),
                (new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.DInt, typeof(UInt32), 1, 8, "UInt32")),
                (new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Real, typeof(double), 1, 12, "Double")),
                (new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Real, typeof(double), 1, 16, "Long double")),
                (new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.String, typeof(string), 1, 24, "Stringa", 22))
            }
        };
    }
}
