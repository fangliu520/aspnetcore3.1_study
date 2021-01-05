using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.CommandPattern
{
    public interface IReceive
    {
        void Read();

        void Write();

        void Save();

    }
    public class Receive:IReceive
    {
        public void Read()
        {
            Console.WriteLine("Read");
        }
        public void Write()
        {
            Console.WriteLine("Write");
        }
        public void Save()
        {
            Console.WriteLine("Save");
        }
    }
    public class ReceiveNew : IReceive
    {
        public void Read()
        {
            Console.WriteLine("Read1");
        }
        public void Write()
        {
            Console.WriteLine("Write1");
        }
        public void Save()
        {
            Console.WriteLine("Save1");
        }
    }
}
