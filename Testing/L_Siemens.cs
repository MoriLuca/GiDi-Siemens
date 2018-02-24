﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luca
{
    public class L_Siemens
    {
        public static void BuildCharArrayForSiemens(string stringToConvert, byte[] charArray)
        {
            int stringLenght = stringToConvert.Length;
            charArray[0] = charArray[0];
            charArray[1] = (byte)stringLenght;
            for (int i = 0; i < stringLenght; i++)
            {
                charArray[i + 2] = (byte)stringToConvert[i];
            }
        }

        /// <summary>
        /// Probabilmente Inutile al momento, è stato risolto andando a chiamare la funzione di conversione direttamente all'interno
        /// della funzione di scrittura al PLC
        /// </summary>
        /// <param name="stringToConvert"></param>
        /// <returns></returns>
        public static object BuildCharArrayForSiemens(string stringToConvert)
        {
            int stringLenght = stringToConvert.Length;
            byte[] b = new byte[stringLenght + 2];
            b[1] = (byte)stringLenght;
            for (int i = 0; i < stringLenght; i++)
            {
                b[i + 2] = (byte)stringToConvert[i];
            }
            return b;
        }

        public static string BuildStringFromSiemensCharArray(byte[] charArray)
        {
            int arrayLenght = Convert.ToInt32(charArray[0]);
            int wordLenght = Convert.ToInt32(charArray[1]);
            int startingOffset = 2;
            int endingOffset = (startingOffset + wordLenght);
            byte[] cp = new byte[wordLenght];
            Array.Copy(charArray, startingOffset, cp, 0, wordLenght);
            return System.Text.Encoding.Default.GetString(cp);
        }

        public static void WriteSingleVaraible(GiDiSiemens.Models.L_SiemensData data)
        {
            GiDiSiemens.Repo.SiemensRepo.PLC.Open();
            GiDiSiemens.Repo.SiemensRepo.PLC.Write(data.DataType, data.DBNumber, data.DBOffset, data.RawContent);
            GiDiSiemens.Repo.SiemensRepo.PLC.Close();
        }

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
                    return Convert.ToSingle(stringFromAjaxPost);
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