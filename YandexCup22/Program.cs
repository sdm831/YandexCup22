using System;
using System.Collections.Generic;
using System.Linq;

namespace YandexCup22
{
    internal class Program
    {
        public static int[] arrNMC = new int[3];
        public static List<DataCenter> dcList = new List<DataCenter>();

        static void Main(string[] args)
        {
            arrNMC = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray();

            for (int n = 1; n <= arrNMC[0]; n++)
            {
                var dc = new DataCenter(n, arrNMC[1]);
                var servers = new Server[arrNMC[1]];
                for (int m = 0; m < arrNMC[1]; m++) 
                {
                    servers[m] = new Server(m+1); 
                }
                dc.servers = servers;
                dcList.Add(dc);                
            }

            for (var c = 0; c < arrNMC[2]; c++)
            {
                ParseCommand(Console.ReadLine());
            }
        }

        private static void ParseCommand(string? command)
        {
            var arrCom = command.Split();

            switch (arrCom[0].ToLower())
            {
                case "disable":
                    DisableServer(int.Parse(arrCom[1]), int.Parse(arrCom[2]));
                    break;

                case "reset":
                    ResetDc(int.Parse(arrCom[1]));
                    break;                

                case "getmax":
                    GetMax();
                    break;

                case "getmin":
                    GetMin();
                    break;
            }
        }

        public static void ResetDc(int dcNumber)
        {
            var dc = dcList.FirstOrDefault(x => x.Number == dcNumber);
            
            foreach (var serv in dc.servers) 
            { 
                if(serv.State == false) serv.State = true; 
            }            
            dc.ResetCount++;
            dc.RA = dc.ResetCount * arrNMC[1];
        }

        public static void DisableServer(int dcNumber, int servNumber)
        {
            dcNumber--;
            servNumber--;
            dcList[dcNumber].servers[servNumber].State = false;
            dcList[dcNumber].RA = dcList[dcNumber].ResetCount * dcList[dcNumber].servers.Count(x => x.State == true);            
        }

        public static void GetMax()
        {
            Console.WriteLine(dcList.FirstOrDefault(x => x.RA == dcList.Max(x => x.RA)).Number);
        }

        public static void GetMin()
        {
            Console.WriteLine(dcList.FirstOrDefault(x => x.RA == dcList.Min(x => x.RA)).Number);
        }
    }

    public class DataCenter
    {
        public DataCenter(int number, int m)
        {
            Number = number;
            servers = new Server[m];
        }

        public int Number { get; set; }
        public int ResetCount { get; set; } = 0;
        public Server[] servers { get; set; } 
        public int RA { get; set; } = 0;
    }

    public class Server
    {
        public Server(int number)
        {
            Number = number;
        }

        public int Number { get; set; }
        public bool State { get; set; } = true;      
    }
}