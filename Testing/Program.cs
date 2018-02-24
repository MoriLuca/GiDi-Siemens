using System;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            GiDiSiemens.Repo.SiemensRepo.PLC.Open();
            object result = GiDiSiemens.Repo.SiemensRepo.PLC.Read(S7.Net.DataType.DataBlock,1,8,S7.Net.VarType.Real,1);
            Console.WriteLine(result);

            result = Convert.ToDouble(87.4567895879465);
            GiDiSiemens.Repo.SiemensRepo.PLC.Write(S7.Net.DataType.DataBlock, 1, 8,result);
            result = GiDiSiemens.Repo.SiemensRepo.PLC.Read(S7.Net.DataType.DataBlock, 1, 8, S7.Net.VarType.Real, 1);
            Console.WriteLine(result);
            GiDiSiemens.Repo.SiemensRepo.PLC.Close();
            Console.Read();
        }
    }
}
