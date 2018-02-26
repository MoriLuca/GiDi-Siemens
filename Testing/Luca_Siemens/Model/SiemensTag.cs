using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luca.Siemens.Models
{
    public class SiemensTag
    {
        public Luca.Siemens.Data.TypeOfTag ObjectType { get; }
        public S7.Net.DataType DataType { get; }
        public S7.Net.VarType VariableType { get; }
        public Type DotNetDataType { get; }
        public int DBNumber { get; }
        public int DBOffset { get; }
        public string Name { get; }
        public object Content { get; set; }
        public object RawContent { get; set; }
        public int MaxStringLenght { get; }

        public SiemensTag() { }

        public SiemensTag(Luca.Siemens.Data.TypeOfTag ObjectType,
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
                b[0] = (byte)(this.MaxStringLenght + 2);
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
                this.Content = raw.Substring(startingOffset, lunghezzaRaw);
            }
            else this.Content = this.RawContent;
        }
    }
}
