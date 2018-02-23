using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiDiSiemens.Models
{
    public class SiemensDB
    {
        public Int16 _int16 { get; set; }
        public UInt16 _uint16 { get; set; }
        public Int32 _int32 { get; set; }
        public double _real { get; set; }
        public byte[] stringa { get; set; } = new byte[12];

        public SiemensDB() { }
        public SiemensDB(SiemensWork s)
        {
            _int16 = (Int16)s.Data[0].Content;
            _uint16 = (UInt16)s.Data[1].Content;
            _int32 = (Int32)s.Data[2].Content;
            _real = (double)s.Data[3].Content;
            Luca.L_Siemens.BuildCharArrayForSiemens(s.Data[4].Content.ToString(), this.stringa);
        }
    }

    public class SiemensWork
    {
        public List<L_SiemensData> Data { get; set; } = new List<L_SiemensData>();

        public SiemensWork() { }

        public SiemensWork(SiemensDB s)
        {
            //L_SiemensData temp = new L_SiemensData(L_S7DataType.SingleVariable,S7.Net.DataType.DataBlock,S7.Net.VarType.Int,1,0,nameof(s._int16),s._int16);

            this.Data.Add(new L_SiemensData(L_S7DataType.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Int, 1, 0, nameof(s._int16), s._int16));
            this.Data.Add(new L_SiemensData(L_S7DataType.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Int, 1, 2, nameof(s._uint16), s._uint16));
            this.Data.Add(new L_SiemensData(L_S7DataType.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.DInt, 1, 4, nameof(s._int32), s._int32));
            this.Data.Add(new L_SiemensData(L_S7DataType.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.Real, 1, 8, nameof(s._real), s._real));
            this.Data.Add(new L_SiemensData(L_S7DataType.SingleVariable, S7.Net.DataType.DataBlock, S7.Net.VarType.String, 1, 12, nameof(s.stringa), s.stringa));

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
        public int DBNumber { get; }
        public int DBOffset { get; }
        public string Name { get; }
        public object Content { get; set; }

        public L_SiemensData(L_S7DataType ObjectType,
                              S7.Net.DataType DataType,
                              S7.Net.VarType VariableType,
                              int DBNumber,
                              int DBOffset,
                              string Name,
                              object Content)
        {
            this.ObjectType = ObjectType;
            this.DataType = DataType;
            this.VariableType = VariableType;
            this.DBNumber = DBNumber;
            this.DBOffset = DBOffset;
            this.Name = Name;
            this.Content = Content;

            if (this.VariableType == S7.Net.VarType.String)
            {
                this.Content = Luca.L_Siemens.BuildStringFromSiemensCharArray((byte[])Content);
            }
        }
    }

}
