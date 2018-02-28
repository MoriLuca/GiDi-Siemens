using System;

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
    }
}
