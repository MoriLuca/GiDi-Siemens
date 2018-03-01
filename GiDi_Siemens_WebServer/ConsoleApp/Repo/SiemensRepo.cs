using System;
using System.Collections.Generic;
using Luca.Siemens.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class Repo
    {
        public static Luca.Siemens.Models.SiemensPLC SiemensPlc = new Luca.Siemens.Models.SiemensPLC()
        {
            Plc = new S7.Net.Plc(S7.Net.CpuType.S71200, "192.168.2.1", 0, 1),
            MillisecDealy = 100,
            Data = new List<Luca.Siemens.Models.SiemensTag>() {
                (new SiemensTag (Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Int, typeof(Int16), 1, 0, "Intero 16 bit")),
                (new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Int, typeof(UInt16), 1, 2, "Intero 16 bit senza segno")),
                (new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.DWord, typeof(Int32), 1, 4, "Intero 32 bit")),
                (new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.DWord, typeof(UInt32), 1, 8, "Intero 32 bit senza segno")),
                (new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Real, typeof(Single), 1, 12, "Virgola mobile 32 bit")),
                //(new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Real, typeof(double), 1, 16, "Long double",1)),
                (new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.String, typeof(string), 1, 24, "Stringa di caratteri", 20))
            }
        };
    }
}
