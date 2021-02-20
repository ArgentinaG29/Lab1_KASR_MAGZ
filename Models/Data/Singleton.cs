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
        public LinkedList<Players> PlayerList;//Lista C#
        public GenericList<Players> player_list;//Lista artesanal
        public List<Players> auxiliarList;//Lista auxiliar que guardará los elementos buscados
        public List<TimeDescription> OperationTime;
      
        private Singleton()
        {
            PlayerList = new LinkedList<Players>();
            player_list = new DoubleLinkedList<Players>();
            auxiliarList = new List<Players>();
            OperationTime = new List<TimeDescription>();
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
