using System;
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
    public class SiemensPLC
    {
        public S7.Net.Plc Plc; 
        public List<SiemensTag> Data { get; set; } = new List<SiemensTag>();

        public SiemensPLC() { }

        /// <summary>
        /// Lettura di una singola variabile del set Data
        /// </summary>
        /// <param name="index">index della variabile da leggere</param>
        public void ReadSingleVariable(int index)
        {
            switch (Data[index].VariableType)
            {
                case S7.Net.VarType.Bit:
                    break;
                case S7.Net.VarType.Byte:
                    break;
                case S7.Net.VarType.Word:
                    break;
                case S7.Net.VarType.DWord:
                    break;
                case S7.Net.VarType.Int:
                    if (Data[index].DotNetDataType == typeof(Int16)) { }
                    if (Data[index].DotNetDataType == typeof(UInt16)) { }
                    break;
                case S7.Net.VarType.DInt:
                    if (Data[index].DotNetDataType == typeof(Int32))
                    {
                        S7.Net.Conversion.ConvertToInt((uint)Siemens.Repo.SiemensRepo.PLC.Read(S7.Net.DataType.DataBlock, 1, 4, S7.Net.VarType.DWord, 1));
                    }
                    if (Data[index].DotNetDataType == typeof(UInt32)) { }
                    break;
                case S7.Net.VarType.Real:
                    if (Data[index].DotNetDataType == typeof(Single)) { }
                    if (Data[index].DotNetDataType == typeof(double)) { }
                    break;
                case S7.Net.VarType.String:
                    break;
                case S7.Net.VarType.Timer:
                    break;
                case S7.Net.VarType.Counter:
                    break;
                default:
                    break;
            }
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
                    //Se è una stringa, devo definire la lettura come se fosse un array di caratteri, andando anche a definire come ultimo 
                    //paramentro la lunghezza totale della stringa
                    if (i.VariableType == S7.Net.VarType.String)
                    {
                        i.RawContent = plc.Read(i.DataType, i.DBNumber, i.DBOffset, i.VariableType, i.MaxStringLenght + 2);
                    }
                    // Se il parametro è un real, converto in double prima di inerirlo nel content. Da fare, non capisco il perchè
                    else if (i.VariableType == S7.Net.VarType.Real)
                    {
                        i.RawContent = Convert.ToDouble(plc.Read(i.DataType, i.DBNumber, i.DBOffset, i.VariableType, 1));
                    }
                    // Se il parametro è un double int 
                    else if (i.VariableType == S7.Net.VarType.DInt)
                    {
                        //i.RawContent = S7.Net.Conversion.ConvertToInt((uint)plc.Read(i.DataType, i.DBNumber, i.DBOffset, i.VariableType, 1));
                        object o = plc.Read(i.DataType, i.DBNumber, i.DBOffset, i.VariableType, 1);
                    }
                    //Se non è un tipo stringa, posso copiare l'oggetto raw, allinterno del content senza applicare nessuna modifica
                    else i.RawContent = plc.Read(i.DataType, i.DBNumber, i.DBOffset, i.VariableType, 1);
                    //i.BuildWorkVariableFromRaw();
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
