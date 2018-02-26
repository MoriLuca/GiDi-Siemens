using System;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Siemens.Repo.SiemensRepo.PLC.Open();
            object result = Siemens.Repo.SiemensRepo.PLC.Read(S7.Net.DataType.DataBlock,1,8,S7.Net.VarType.Real,1);
            Console.WriteLine(result);

            result = Convert.ToDouble(87.4567895879465);
            Siemens.Repo.SiemensRepo.PLC.Write(S7.Net.DataType.DataBlock, 1, 8,result);
            result = Siemens.Repo.SiemensRepo.PLC.Read(S7.Net.DataType.DataBlock, 1, 8, S7.Net.VarType.Real, 1);
            Console.WriteLine(result);
            Siemens.Repo.SiemensRepo.PLC.Close();
            Console.Read();
        }
    }
}
