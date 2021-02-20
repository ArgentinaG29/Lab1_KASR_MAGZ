using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1_KASR_MAGZ.Models.Data;

namespace Lab1_KASR_MAGZ.Models
{
    public class ClassSearch
    {
        public static void Looking(string information, int counting, IEnumerable<Players> NameList, Func<Players, bool> Comparer)
        {
            for (int i = 0; i < counting; i++)
            {
                if (Comparer.Invoke(NameList.ElementAt(i)))
                {
                    Singleton.Instance.auxiliarList.Add(NameList.ElementAt(i));
                }
            }                                   
        }


        public static int CompareTo(int comparer, object obj)
        {
            var the_salary = ((Players)obj).Salary;
            return the_salary < comparer ? -1 : comparer == the_salary ? 0 : 1;
        }

        public static void LookingSalary(int comparerNumber, int counting, IEnumerable<Players> NameList, int option)
        {
            for(int i =0; i<counting; i++)
            {
                if(CompareTo(comparerNumber, NameList.ElementAt(i)) == -1 && option == -1)
                {
                    Singleton.Instance.auxiliarList.Add(NameList.ElementAt(i));
                }
                else
                {
                    if(CompareTo(comparerNumber, NameList.ElementAt(i)) == 0 && option == 0)
                    {
                        Singleton.Instance.auxiliarList.Add(NameList.ElementAt(i));
                    }
                    else
                    {
                        if(CompareTo(comparerNumber, NameList.ElementAt(i)) == 1 && option == 1)
                        {
                            Singleton.Instance.auxiliarList.Add(NameList.ElementAt(i));
                        }
                    }
                }
            }
        }



    }
}

