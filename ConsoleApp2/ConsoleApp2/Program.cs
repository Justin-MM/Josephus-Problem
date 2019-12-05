using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp2
{
    class Program   
    {
        public static int SurvivingPrisoner(int n, int killingInterval)
        {
            // add all prisoners to linkedlist identified by zero index.
            var prisoners = PopulateLinkedList(n); 
            var prisoner = prisoners.Head;

            // start killing them
            int counter = 0;
            while (prisoners.Count != 1)
            {
                if (counter == killingInterval - 1)
                {
                    prisoners.Remove(prisoner.Value);
                    counter = 0;
                }
                else
                {
                    counter++;
                }                
                prisoner = prisoner.Next;                
            }

            var survivor = prisoners.Head.Value;
            return survivor;
        }

        private static CircularLinkedList.LinkedList<int> PopulateLinkedList(int n)
        {
            CircularLinkedList.LinkedList<int> prisoners = new CircularLinkedList.LinkedList<int>();
            
            int i = 0;
            while(i < n)
            {              
                prisoners.AddLast(i);
                i++;
            }            
            return prisoners;
        }

        public static void Main()
        {
            Console.WriteLine(SurvivingPrisoner(13, 2));
            Console.ReadLine();
        }
    }

}
