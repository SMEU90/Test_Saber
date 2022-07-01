using System;
using System.IO;

namespace Test_Saber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ListRandom listRandom = new ListRandom();
            ListNode listNode1 = new ListNode
            {
                Data = "str1"
            };
            listRandom.Tail = listNode1;
            listRandom.Count++;
            ListNode listNode2 = new ListNode
            {
                Data = "str2",
                Previous=listNode1,
            };
            
            listRandom.Count++;
            listNode1.Next = listNode2;
            ListNode listNode3 = new ListNode
            {
                Data = "str3",
                Previous = listNode2,
                
            };
            listRandom.Count++;
            listNode3.Random = listNode3;
            listNode2.Next = listNode3;
            listNode1.Random = listNode3;
            ListNode listNode4 = new ListNode
            {
                Data = "str4",
                Previous=listNode3,
                Random=listNode2
            };
            listRandom.Count++;
            listRandom.Head = listNode4;
            listNode3.Next = listNode4;

            using (FileStream fileStream = File.Create("TestSaber.txt"))
            {
                Console.WriteLine("Before serialization:");
                listRandom.DisplayNode();
                listRandom.Serialize(fileStream);
            }
            using (FileStream fileStream = File.OpenRead("TestSaber.txt"))
            {
                Console.WriteLine("After deserialization:");
                listRandom=new ListRandom();
                listRandom.Deserialize(fileStream);
                listRandom.DisplayNode();
            }
        }
    }
}
   
