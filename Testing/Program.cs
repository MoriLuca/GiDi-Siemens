using System;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Siemens.Repo.SiemensRepo.PLC.Open();
            object b = Siemens.Repo.SiemensRepo.PLC.Read(S7.Net.DataType.DataBlock, 1, 4, S7.Net.VarType.DInt, 1);
            Int32 i = S7.Net.Conversion.ConvertToInt();
            //double a = ;
            Console.WriteLine(b);
            Siemens.Repo.SiemensRepo.PLC.Close();
            Console.Read();
        }
    }
}
