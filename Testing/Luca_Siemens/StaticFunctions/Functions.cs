using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luca.Siemens.StaticFunctions
{
    public static class Functions
    {
        /// <summary>
        /// Ritorna on oggetto contenente i dati nel formato corretto, in modo da poter essere scritto sul plc.
        /// Viene eseguito un cast nel tipo dato corretto e restituito l'oggetto
        /// </summary>
        /// <param name="stringFromAjaxPost">Stringa che arriva come parametro dalla chiamata POST di ajax</param>
        /// <param name="type">tipo del dato da castare, che deve essere precedentemente letto ed inviato come parametro alla funzione</param>
        /// <returns>Ritorna un oggetto contenente il valore convertito, oppure null se il tipo dato non è supportato</returns>
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
