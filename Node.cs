using System;
using System.Collections.Generic;
using System.IO;

namespace Test_Saber
{
    public class ListNode
    {
        public ListNode Previous;
        public ListNode Next;
        public ListNode Random; // произвольный элемент внутри списка
        public string Data;
    }
    public class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;
        public void Serialize(Stream s)
        { 
            if(s.CanWrite)
            {
                using (var sw = new StreamWriter(s))
                {
                    int count = 0;
                    ListNode oldTail = new ListNode();
                    oldTail = Tail;
                    while (oldTail != null)
                    {
                        sw.WriteLine(oldTail.Data.Length.ToString(),0,sizeof(int));//size
                        sw.WriteLine(oldTail.Data);//data
                        if (oldTail.Random != null)//id Random
                        {
                            ListNode searchTail = new ListNode();
                            searchTail = Tail;
                            int randCount = 0;
                            while (!ReferenceEquals(oldTail.Random, searchTail))
                            {
                                randCount++;
                                searchTail = searchTail.Next;
                            }
                            sw.WriteLine(randCount.ToString(), 0, sizeof(int));
                        }
                        else
                            sw.WriteLine("-1", 0, sizeof(int));

                        count++;
                        oldTail = oldTail.Next;
                    }
                }
            }
            
        }
        public void Deserialize(Stream s)
        {
            if(s.CanRead && s.Length > 0)
            {
                using (var sr = new StreamReader(s))
                {
                    List<int> listInt = new List<int>();
                    ListNode oldNode = new ListNode();
                    while (sr.Peek() >= 0)
                    {
                        ListNode listNode = new ListNode();
                        char[] data = new char[sizeof(int)];
                        int sizeOfString = Convert.ToInt32(sr.ReadLine());

                        sr.Read(data, 0, sizeOfString);
                        listNode.Data = new string(data);

                        if (Count == 0)
                        {
                            Head = listNode;
                            Tail = listNode;
                            oldNode=listNode;

                        } else
                        {
                            oldNode.Next = listNode;
                            listNode.Previous = oldNode;
                            Head = listNode;
                            oldNode = listNode;
                        }
                        sr.ReadLine();
                        sizeOfString = Convert.ToInt32(sr.ReadLine());
                        listInt.Add(sizeOfString);
                        Count++;
                    }
                    for(int i=0;i<Count;i++)
                    {
                        if(listInt.Count > 0)
                            if(listInt[i]!=-1)
                            {
                                ListNode searchNode = new ListNode();
                                ListNode currentNode = new ListNode();
                                currentNode = Tail;
                                searchNode = Tail;
                                for (int j=0;j< listInt[i]; j++)
                                {
                                    currentNode = currentNode.Next;
                                }
                                for(int g=0;g<i;g++)
                                {   
                                    searchNode= searchNode.Next;
                                }
                                searchNode.Random = currentNode;
                            }
                    } 
                }
            }
        }
        public void DisplayNode()
        {
            ListNode listNode = new ListNode();
            listNode = Tail;
            while(listNode!=null)
            {
                Console.WriteLine($"Node: {listNode.Data}");
                if (listNode.Random!=null)
                    Console.WriteLine($"Random: {listNode.Random.Data}");
                else
                    Console.WriteLine("Random: -----");
                listNode = listNode.Next;
            }
        }
    }
}
