using System;
using System.Threading;

namespace Testing
{
    class Program
    {
        public static object Locker { get; set; } = new object();
        static void Main(string[] args)
        {
            Siemens.Repo.SiemensRepo.PLC.Open();
            LaunchTest();
            Console.Read();
        }

        public static void LaunchTest()
        {
            Thread RunTestInt16 = new Thread(testInt16);
            Thread RunTestUInt16 = new Thread(testUInt16);
            Thread RunTestInt32 = new Thread(testInt32);
            Thread RunTestUInt32 = new Thread(testUInt32);
            Thread RunTestReal = new Thread(testReal);
            RunTestInt16.IsBackground = true;
            RunTestUInt16.IsBackground = true;
            RunTestInt32.IsBackground = true;
            RunTestUInt32.IsBackground = true;
            RunTestReal.IsBackground = true;
            RunTestInt16.Start();
            RunTestUInt16.Start();
            RunTestInt32.Start();
            RunTestUInt32.Start();
            RunTestReal.Start();
        }

        public static void testInt16()
        {
            Random ran = new Random();
            while (true)
            {
                Int16 randomed = (Int16)ran.Next(Int16.MinValue, Int16.MaxValue);
                object o = new object();
                lock (Locker)
                {
                    Siemens.Repo.SiemensRepo.PLC.Write(S7.Net.DataType.DataBlock, 1, 0, randomed);
                    o = Siemens.Repo.SiemensRepo.PLC.Read(S7.Net.DataType.DataBlock, 1, 0, S7.Net.VarType.Int, 1);
                }
                Int16 j = Convert.ToInt16(o);
                Console.WriteLine("int16 ] generato : " + randomed + " letto " + j);
                Thread.Sleep(500);
            }
        }

        public static void testUInt16()
        {
            Random ran = new Random();
            while (true)
            {
                UInt16 randomed = (UInt16)ran.Next(UInt16.MinValue, UInt16.MaxValue);
                object o = new object();
                lock (Locker)
                {
                    Siemens.Repo.SiemensRepo.PLC.Write(S7.Net.DataType.DataBlock, 1, 2, randomed);
                    o = Siemens.Repo.SiemensRepo.PLC.Read(S7.Net.DataType.DataBlock, 1, 2, S7.Net.VarType.Int, 1);
                }
                UInt16 j = S7.Net.Conversion.ConvertToUshort((short)o);
                Console.WriteLine("uint16 ]generato : " + randomed + " letto " + j);
                Thread.Sleep(500);
            }
        }

        public static void testInt32()
        {
            Random ran = new Random();
            while (true)
            {
                Int32 randomed = (Int32)ran.Next(Int32.MinValue, Int32.MaxValue);
                object o = new object();
                lock (Locker)
                {
                    Siemens.Repo.SiemensRepo.PLC.Write(S7.Net.DataType.DataBlock, 1, 4, randomed);
                    o = Siemens.Repo.SiemensRepo.PLC.Read(S7.Net.DataType.DataBlock, 1, 4, S7.Net.VarType.DInt, 1);
                }
                Int32 j = Convert.ToInt32(o);
                Console.WriteLine("int32 ]generato : " + randomed + " letto " + j);
                Thread.Sleep(500);
            }
        }

        public static void testUInt32()
        {
            Random ran = new Random();
            while (true)
            {
                UInt32 randomed = (UInt32)ran.Next(0, Int32.MaxValue);
                object o = new object();
                lock (Locker)
                {
                    Siemens.Repo.SiemensRepo.PLC.Write(S7.Net.DataType.DataBlock, 1, 8, randomed);
                    o = Siemens.Repo.SiemensRepo.PLC.Read(S7.Net.DataType.DataBlock, 1, 8, S7.Net.VarType.DInt, 1);
                }
                Int32 j = Convert.ToInt32(o);
                Console.WriteLine("uint32 ]generato : " + randomed + " letto " + j);
                Thread.Sleep(500);
            }
        }

        public static void testReal()
        {
            Random ran = new Random();
            while (true)
            {
                Single randomed = (Single)ran.NextDouble();
                object o = new object();
                lock (Locker)
                {
                    Siemens.Repo.SiemensRepo.PLC.Write(S7.Net.DataType.DataBlock, 1, 12, (double)randomed);
                    o = Siemens.Repo.SiemensRepo.PLC.Read(S7.Net.DataType.DataBlock, 1, 12, S7.Net.VarType.Real, 1);
                }
                Single j = Convert.ToSingle(o);
                Console.WriteLine("real ] generato : " + randomed + " letto " + j);
                Thread.Sleep(500);
            }

        }

    }
}
