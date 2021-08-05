using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeAPI.Helpers
{
    public static class DateHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="youtubeDate">Exemple : 5 avr 2014</param>
        /// <returns></returns>
        public static DateTime? TranslateYoutubeDateInDateTime(string youtubeDate)
        {
            try
            {
                var dateParts = youtubeDate.Split(' ');
                var daysInNumber = dateParts[0];
                var monthInString = dateParts[1].Replace(".", string.Empty);
                var yearhInNumber = dateParts[2];

                var monthInNumber = -1;
                switch (monthInString)
                {
                    case "jan":
                    case "janv":
                    case "janvier":
                        monthInNumber = 1;
                        break;
                    case "fev":
                    case "févr":
                    case "fevrier":
                        monthInNumber = 2;
                        break;
                    case "mar":
                    case "mars":
                        monthInNumber = 3;
                        break;
                    case "avr":
                    case "avril":
                        monthInNumber = 4;
                        break;
                    case "mai":
                        monthInNumber = 5;
                        break;
                    case "jui":
                    case "juin":
                        monthInNumber = 6;
                        break;
                    case "juil":
                    case "juillet":
                        monthInNumber = 7;
                        break;
                    case "aou":
                    case "aout":
                    case "août":
                        monthInNumber = 8;
                        break;
                    case "sep":
                    case "sept":
                    case "septembre":
                        monthInNumber = 9;
                        break;
                    case "oct":
                    case "octobre":
                        monthInNumber = 10;
                        break;
                    case "nov":
                    case "novembre":
                        monthInNumber = 11;
                        break;
                    case "dec":
                    case "déc":
                    case "decembre":
                        monthInNumber = 12;
                        break;
                    default:
                        throw new Exception(string.Format(Resources.Strings.Err_UnknownMonth, monthInString));
                }

                return new DateTime(int.Parse(yearhInNumber), monthInNumber, int.Parse(daysInNumber));
            }
            catch
            {
                return null;
            }
        }
    }
}
