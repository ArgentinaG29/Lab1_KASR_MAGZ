using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListLibrary;

namespace Lab1_KASR_MAGZ.Models.Data
{
    public class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public List<Players> PlayerList;
        public GenericList<Players> player_list;
        private Singleton()
        {
            PlayerList = new List<Players>();
            player_list = new DoubleLinkedList<Players>();
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;   
            }
        }
    }
}
