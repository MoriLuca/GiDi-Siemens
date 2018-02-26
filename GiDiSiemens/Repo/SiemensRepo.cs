using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siemens.Repo
{
    public static class SiemensRepo
    {
        public static Luca.Siemens.Models.SiemensMemory SiemensMem = new Luca.Siemens.Models.SiemensMemory();
        public static S7.Net.Plc PLC = new S7.Net.Plc(S7.Net.CpuType.S71200, "192.168.2.1", 0, 1);
    }
}
