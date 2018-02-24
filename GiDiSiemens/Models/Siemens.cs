using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiDiSiemens.Models
{

    public class SiemensWork
    {
        public List<L_SiemensData> Data { get; set; } = new List<L_SiemensData>();

        public SiemensWork()
        {
            this.Data.Add(new L_SiemensData(L_S7DataType.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Int, typeof(Int16), 1, 0, "Intero 16 bits"));
            this.Data.Add(new L_SiemensData(L_S7DataType.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Int, typeof(UInt16), 1, 2, "Intero Senza Segno 16 bits"));
            this.Data.Add(new L_SiemensData(L_S7DataType.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.DInt, typeof(Int32), 1, 4, "Intero 32 bits"));
            this.Data.Add(new L_SiemensData(L_S7DataType.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Real, typeof(double), 1, 8, "Intero Senza Segno 32 bits"));
            this.Data.Add(new L_SiemensData(L_S7DataType.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.String, typeof(string), 1, 12, "Stringa", 10));
        }

        public void ReadSingleVariable(int index)
        {
            //legge la variabile della lista con index index
        }

        public void ReadAllVariables()
        {
            Repo.SiemensRepo.PLC.Open();
            foreach (var i in this.Data)
            {
                //Se è una stringa, devo definire la lettura come se fosse un array di caratteri, andando anche a definire come ultimo 
                //paramentro la lunghezza totale della stringa
                if(i.VariableType == S7.Net.VarType.String)
                {
                    i.RawContent = Repo.SiemensRepo.PLC.Read(i.DataType, i.DBNumber, i.DBOffset, i.VariableType, i.MaxStringLenght+2);
                }
                //Se non è un tipo stringa, posso copiare l'oggetto raw, allinterno del content senza applicare nessuna modifica
                else i.RawContent = Repo.SiemensRepo.PLC.Read(i.DataType, i.DBNumber, i.DBOffset, i.VariableType, 1);
                i.BuildWorkVariableFromRaw();
            }
            Repo.SiemensRepo.PLC.Close();
        }



    }

    public enum L_S7DataType
    {
        SingleVariable,
        Array,
        Struct
    }

    public class L_SiemensData
    {
        public L_S7DataType ObjectType { get; }
        public S7.Net.DataType DataType { get; }
        public S7.Net.VarType VariableType { get; }
        public Type DotNetDataType { get; }
        public int DBNumber { get; }
        public int DBOffset { get; }
        public string Name { get; }
        public object Content { get; set; }
        public object RawContent { get; set; }
        public int MaxStringLenght { get; }

        public L_SiemensData() { }

        public L_SiemensData(L_S7DataType ObjectType,
                              S7.Net.DataType DataType,
                              S7.Net.VarType VariableType,
                              Type DotNetDataType,
                              int DBNumber,
                              int DBOffset,
                              string Name,
                              int MaxStringLenght = -1)
        {
            this.ObjectType = ObjectType;
            this.DataType = DataType;
            this.VariableType = VariableType;
            this.DotNetDataType = DotNetDataType;
            this.DBNumber = DBNumber;
            this.DBOffset = DBOffset;
            this.Name = Name;
            this.MaxStringLenght = MaxStringLenght;

        }

        public void BuildRawVariableFromWork()
        {
            if (this.VariableType == S7.Net.VarType.String)
            {
                string str = this.Content.ToString();
                int stringLenght = this.Content.ToString().Length;
                byte[] b = new byte[stringLenght + 2];
                //b[0] = (byte)(stringLenght + 2);
                b[1] = (byte)stringLenght;

                try
                {
                    //controllo se la stringa che devo scrivere non sia null, oppure maggiore del limite massimo imposto dal plc
                    if (str == null) throw new Exception("La stringa da convertire non può essere null.");
                    if (str.Length > this.MaxStringLenght) throw new Exception("La stringa Inserita è più lunga del massimo consentito da questa variabile");

                    for (int i = 0; i < stringLenght; i++)
                    {
                        b[i + 2] = (byte)str[i];
                    }
                    this.RawContent = b;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else this.RawContent = this.Content;
        }

        public void BuildWorkVariableFromRaw()
        {
            if (this.VariableType == S7.Net.VarType.String)
            {
                string raw = RawContent.ToString();
                int lunghezzaRaw = raw[1];
                int startingOffset = 2;
                this.Content = raw.Substring(startingOffset,lunghezzaRaw);
            }
            else this.Content = this.RawContent;
        }
    }

}
