﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luca.Siemens.Models
{
    /// <summary>
    /// La classe SiemenMemory mette a disposizione la Proprità Data, che è una Lista di oggetti istanziati della classe SiemensData.
    /// 
    /// </summary>
    public class SiemensMemory
    {
        public List<SiemensTag> Data { get; set; } = new List<SiemensTag>();

        public SiemensMemory()
        {
            this.Data.Add(new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Int, typeof(Int16), 1, 0, "Intero 16 bits"));
            this.Data.Add(new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Int, typeof(UInt16), 1, 2, "Intero Senza Segno 16 bits"));
            this.Data.Add(new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.DInt, typeof(Int32), 1, 4, "Intero 32 bits"));
            this.Data.Add(new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Real, typeof(double), 1, 8, "Intero Senza Segno 32 bits"));
            this.Data.Add(new SiemensTag(Luca.Siemens.Data.TypeOfTag.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.String, typeof(string), 1, 12, "Stringa", 10));
        }

        /// <summary>
        /// Not implemented yet
        /// </summary>
        /// <param name="index"></param>
        public void ReadSingleVariable(int index)
        {
            //legge la variabile della lista con index index
        }

        /// <summary>
        /// Lettura di tutte le variabili contenute all- interno della lista Data dell- oggetto stesso.
        /// </summary>
        /// <param name="plc">Plc sul quale andare a leggere i dati</param>
        public void ReadAllVariables(S7.Net.Plc plc)
        {
            plc.Open();
            foreach (var i in this.Data)
            {
                try
                {
                    i.RawContent = plc.Read(i.DataType, i.DBNumber, i.DBOffset, i.VariableType, 1);
                    i.BuildWorkVariableFromRaw();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            plc.Close();
        }

        /// <summary>
        /// Scrittura di una singola variabile sul plc
        /// </summary>
        /// <param name="plc">nome del plc sul quale andare a scrivere</param>
        /// <param name="data">singola variabile (oggetto data) da scrivere sul plc</param>
        public void WriteSingleVaraible(S7.Net.Plc plc, Luca.Siemens.Models.SiemensTag data)
        {
            plc.Open();
            plc.Write(data.DataType, data.DBNumber, data.DBOffset, data.RawContent);
            plc.Close();
        }
    }

}
