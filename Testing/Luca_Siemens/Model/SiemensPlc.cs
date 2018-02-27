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
                case S7.Net.VarType.DInt:
                    break;
                case S7.Net.VarType.Int:
                    if (Data[index].DotNetDataType == typeof(Int16))
                    {
                        Data[index].RawContent = Convert.ToInt16(Plc.Read(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].VariableType, 1));
                    }
                    if (Data[index].DotNetDataType == typeof(UInt16))
                    {
                        Data[index].RawContent = S7.Net.Conversion.ConvertToUshort((short)Plc.Read(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].VariableType, 1));
                    }
                    break;
                case S7.Net.VarType.DWord:
                    if (Data[index].DotNetDataType == typeof(Int32))
                    {
                        Data[index].RawContent = S7.Net.Conversion.ConvertToInt((uint)Plc.Read(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].VariableType, 1));
                    }
                    if (Data[index].DotNetDataType == typeof(UInt32))
                    {
                        Data[index].RawContent = S7.Net.Conversion.ConvertToInt((uint)Plc.Read(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].VariableType, 1));
                    }
                    break;
                case S7.Net.VarType.Real:
                    if (Data[index].DotNetDataType == typeof(Single)) { }
                    if (Data[index].DotNetDataType == typeof(double)) { }
                    break;
                case S7.Net.VarType.String:
                    Data[index].RawContent = Plc.Read(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].VariableType, Data[index].MaxStringLenght+2);
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
        public void ReadAllVariables()
        {
            for (int i = 0; i < this.Data.Count; i++)
            {
                ReadSingleVariable(i);
            }
        }

        /// <summary>
        /// Scrittura di una singola variabile sul plc
        /// </summary>
        /// <param name="plc">nome del plc sul quale andare a scrivere</param>
        /// <param name="index">indice del parametro contenuto nella lista da scrivere sul plc</param>
        public void WriteSingleVaraible(S7.Net.Plc plc, int index)
        {
            plc.Open();
            plc.Write(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].RawContent);
            plc.Close();
        }
    }

}
