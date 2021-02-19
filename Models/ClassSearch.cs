using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1_KASR_MAGZ.Models.Data;

namespace Lab1_KASR_MAGZ.Models
{
    public static class ClassSearch
    {
        public static void Looking(string information, int counting,  IEnumerable<Players> NameList, Func<Players, bool> Comparer)
        {
            string respuesta = "";
            string clave = "";
            for(int i = 0; i < counting; i++)
            {
                if(Comparer.Invoke(NameList.ElementAt(i)))
                {
                    respuesta += Convert.ToString(NameList.ElementAt(i).Name);
                }

            }
        }


        
    }
}
