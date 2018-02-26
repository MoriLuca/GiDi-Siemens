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
    public class SiemensMemory
    {
        public List<SiemensTag> Data { get; set; } = new List<SiemensTag>();

        public SiemensMemory(){}

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
