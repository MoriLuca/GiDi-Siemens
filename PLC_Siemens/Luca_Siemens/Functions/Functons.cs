using System;
using System.Collections.Generic;
using System.Text;

namespace Luca.Siemens
{
    public static class Functons
    {
        public static object RebuildTheBlackBox(string stringFromAjaxPost, Type type)
        {
            try
            {
                if (type == typeof(Int16))
                {
                    return Convert.ToInt16(stringFromAjaxPost);
                }
                if (type == typeof(UInt16))
                {
                    return Convert.ToUInt16(stringFromAjaxPost);
                }
                if (type == typeof(Int32))
                {
                    return Convert.ToInt32(stringFromAjaxPost);
                }
                if (type == typeof(UInt32))
                {
                    return Convert.ToUInt32(stringFromAjaxPost);
                }
                if (type == typeof(Single))
                {
                    return Convert.ToDouble(stringFromAjaxPost);
                }
                if (type == typeof(double))
                {
                    return Convert.ToDouble(stringFromAjaxPost);
                }
                if (type == typeof(string))
                {
                    return Convert.ToString(stringFromAjaxPost);
                }
                else
                {
                    throw new NotImplementedException("Il tipo dato Type passato alla funzione, non è ancora stato implementato");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            //Se non è nessuna del tipo di variabili testate sopra
            return null;
        }
    }
}
