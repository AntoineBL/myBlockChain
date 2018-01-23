using myBlockChain.dataFile;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{

    class Block : _Block
    {
       
        
        public Block(int index, byte[] previousHash, DateTime timestamp, string data) 
        {
            this.index = index;
            this.previousHash = previousHash;
            this.timestamp = timestamp;
            this.data = data;
            this.hash = multiThreadhash(index, previousHash, timestamp, data);
        }

        public Block(int index, byte[] previousHash, DateTime timestamp, string data, byte[] hash, int value)
        {
            this.index = index;
            this.previousHash = previousHash;
            this.timestamp = timestamp;
            this.data = data;
            this.hash = hash;
            this.value = value;
        }

        
    
    }
}
