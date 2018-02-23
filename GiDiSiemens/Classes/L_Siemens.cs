using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luca
{
    public class L_Siemens
    {
        public static void BuildCharArrayForSiemens(string stringToConvert, byte[] charArray)
        {
            //Check for Null
            int stringLenght = stringToConvert.Length;
            charArray[0] = charArray[0];
            charArray[1] = (byte)stringLenght;
            for (int i = 0; i < stringLenght; i++)
            {
                charArray[i + 2] = (byte)stringToConvert[i];
            }
        }

        public static string BuildStringFromSiemensCharArray(byte[] charArray)
        {
            int arrayLenght = Convert.ToInt32(charArray[0]);
            int wordLenght = Convert.ToInt32(charArray[1]);
            int startingOffset = 2;
            int endingOffset = (startingOffset + wordLenght);
            byte[] cp = new byte[wordLenght];
            Array.Copy(charArray, startingOffset, cp, 0, wordLenght);
            return System.Text.Encoding.Default.GetString(cp);
        }

        public static void ReadPlcAndComposeSiemensWorkFromSiemensDB()
        {
            GiDiSiemens.Repo.SiemensRepo.PLC.Open();
            GiDiSiemens.Repo.SiemensRepo.PLC.ReadClass(GiDiSiemens.Repo.SiemensRepo.SiemensDB, 1);
            GiDiSiemens.Repo.SiemensRepo.SiemensWork = new GiDiSiemens.Models.SiemensWork(GiDiSiemens.Repo.SiemensRepo.SiemensDB);
            GiDiSiemens.Repo.SiemensRepo.PLC.Close();
        }

        public static void WriteWorkClassOnDB(GiDiSiemens.Models.SiemensWork work)
        {
            GiDiSiemens.Repo.SiemensRepo.PLC.Open();
            GiDiSiemens.Repo.SiemensRepo.SiemensDB = new GiDiSiemens.Models.SiemensDB(work);
            GiDiSiemens.Repo.SiemensRepo.PLC.WriteClass(GiDiSiemens.Repo.SiemensRepo.SiemensDB, 1);
            GiDiSiemens.Repo.SiemensRepo.PLC.Close();
        }

        public static void WriteSingleVaraible(GiDiSiemens.Models.L_SiemensData data)
        {
            GiDiSiemens.Repo.SiemensRepo.PLC.Open();
            GiDiSiemens.Repo.SiemensRepo.PLC.Write(data.DataType, data.DBNumber, data.DBOffset, data.Content);
            GiDiSiemens.Repo.SiemensRepo.PLC.Close();
        }
    }
}
