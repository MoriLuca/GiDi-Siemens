using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luca.Siemens.Models
{
    /// <summary>
    /// La classe SiemenMemory mette a disposizione la Proprità Data, che è una Lista di oggetti istanziati della classe SiemensData.
    /// 
    /// </summary>
    public class SiemensPLC
    {
        #region Proprietà classe
        private object Blocker { get; set; } = new object();
        public S7.Net.Plc Plc;
        public List<SiemensTag> Data { get; set; } = new List<SiemensTag>();
        public int MillisecDealy { get; set; } 
        #endregion

        #region metodi

        /// <summary>
        /// Lettura di una singola variabile del set Data
        /// </summary>
        /// <param name="index">index della variabile da leggere</param>
        public void ReadSingleVariable(int index)
        {
            lock (Blocker)
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
                        if (Data[index].DotNetDataType == typeof(Int32))
                        {
                            Data[index].RawContent = S7.Net.Conversion.ConvertToInt((uint)Plc.Read(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].VariableType, 1));
                        }
                        if (Data[index].DotNetDataType == typeof(UInt32))
                        {
                            Data[index].RawContent = S7.Net.Conversion.ConvertToInt((uint)Plc.Read(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].VariableType, 1));
                        }
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
                    case S7.Net.VarType.DInt:
                        break;
                    case S7.Net.VarType.Real:
                        if (Data[index].DotNetDataType == typeof(Single))
                        {
                            Data[index].RawContent = (double)Plc.Read(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].VariableType, 1);
                        }
                        if (Data[index].DotNetDataType == typeof(double))
                        {
                            throw new Exception("La lettura del LREAL non è ancora stata implementata correttamente");
                            Data[index].RawContent = Convert.ToDecimal(Plc.Read(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].VariableType, 1));
                        }
                        break;
                    case S7.Net.VarType.String:
                        Data[index].RawContent = Plc.Read(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].VariableType, Data[index].MaxStringLenght + 2);
                        BuildWorkString_FromRaw(Data[index]);
                        break;
                    case S7.Net.VarType.Timer:
                        break;
                    case S7.Net.VarType.Counter:
                        break;
                    default:
                        break;
                } 
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
        /// Lettura asincrona delle variabili, con delay impostato dalla proprietà
        /// </summary>
        public void AsyncReadAllVariables()
        {
            Thread t = new Thread(()=>{
                while (true)
                {
                    ReadAllVariables();
                    Thread.Sleep(this.MillisecDealy);
                }
            });
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// Scrittura di una singola variabile sul plc
        /// </summary>
        /// <param name="plc">nome del plc sul quale andare a scrivere</param>
        /// <param name="index">indice del parametro contenuto nella lista da scrivere sul plc</param>
        public void WriteSingleVaraible(int index)
        {
            lock (Blocker)
            {
                Plc.Write(Data[index].DataType, Data[index].DBNumber, Data[index].DBOffset, Data[index].RawContent);
            }
            
        }

        public void BuildRawString_FromWork(Models.SiemensTag tag)
        {
            string str = tag.Content.ToString();
            int stringLenght = tag.Content.ToString().Length;
            byte[] b = new byte[stringLenght + 2];
            b[0] = (byte)(tag.MaxStringLenght + 2);
            b[1] = (byte)stringLenght;

            try
            {
                //controllo se la stringa che devo scrivere non sia null, oppure maggiore del limite massimo imposto dal plc
                if (str == null) throw new Exception("La stringa da convertire non può essere null.");

                if (str.Length > tag.MaxStringLenght) throw new Exception("La stringa Inserita è più lunga del massimo consentito da questa variabile");

                for (int i = 0; i < stringLenght; i++)
                {
                    b[i + 2] = (byte)str[i];
                }
                tag.RawContent = b;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void BuildWorkString_FromRaw(Models.SiemensTag tag)
        {
            string raw = tag.RawContent.ToString();
            int lunghezzaRaw = raw[1];
            int startingOffset = 2;
            tag.Content = raw.Substring(startingOffset, lunghezzaRaw);
        }

        #endregion

        #region costruttore
        public SiemensPLC() { }
        #endregion
    }
}


