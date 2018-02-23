using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiDiSiemens.Repo
{
    public static class SiemensRepo
    {
        public static GiDiSiemens.Models.SiemensDB SiemensDB = new Models.SiemensDB();
        public static GiDiSiemens.Models.SiemensWork SiemensWork = new Models.SiemensWork();
        public static S7.Net.Plc PLC = new S7.Net.Plc(S7.Net.CpuType.S71200, "192.168.2.1", 0, 1);
    }
}
