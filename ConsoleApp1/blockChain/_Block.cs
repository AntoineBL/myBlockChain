using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace myBlockChain.dataFile
{
    class _Block
    {
        private static byte[] difficult = { 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        public int index { get; set; }
        public Byte[] previousHash { get; set; }
        public DateTime timestamp { get; set; }
        public String data { get; set; }
        public Byte[] hash { get; set; }
        public int value { get; set; }
        public Boolean stopMining { get; set; } = false;

        private int nbThread  = 2;
        private Byte[] hashTread;
        private Boolean hashFind = false;

        public Byte[] multiThreadhash(int index, byte[] previousHash, DateTime timestamp, String data)
        {
            Thread[] tabThread = new Thread[this.nbThread];
            for(int i=0; i<nbThread; i++)
            {
                //Console.WriteLine("htread " + i);
                tabThread[i] = new Thread(delegate () { calculateHash(index, previousHash, timestamp, data); });
                tabThread[i].Start();
            }

            for (int i = 0; i < nbThread; i++)
            {
                if (tabThread[i].IsAlive) // Si le thread n'est pas déjà fini
                {
                    tabThread[i].Join(); // On attend que le thread soit terminé
                    //Console.WriteLine("thread " + i + " est fini");
                }
            }
            return this.hashTread;
        }

        public void calculateHash(int index, byte[] previousHash, DateTime timestamp, String data)
        {

            SHA256 mySHA256 = SHA256Managed.Create();
            Random random = new Random(DateTime.Now.Millisecond);
            //Console.WriteLine(random.GetHashCode());
            byte[] hashValue;
            int val = 0;
            int value;
            do
            {
                val++;
                value = random.Next();
                hashValue = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(index + previousHash.ToString() + timestamp + data + value));
                
            } while (!proofOfWork(difficult, hashValue) && !this.hashFind && !stopMining);
            
            
            if(proofOfWork(difficult, hashValue))
            {
                //Console.WriteLine("   number of iteration for find hash"+ val);
                this.value = value;
                this.hashTread = hashValue;
                this.hashFind = true;
            }
        }

        /**
         * Check if the hash value matche with the difficulty
         */
        public Boolean proofOfWork(Byte[] difficult, Byte[] hasValue)
        {
            int res = 0;
            for (int i = 0; i < difficult.Length; i++)
            {

                res += difficult[i] & hasValue[i];
            }
            if (res != 0) { return false; }
            return true;
        }


        /*
         * Check if the new block is validate
         * 
         */
        public Boolean isValidNewBlock(_Block previousBlock)
        {
            if (previousBlock.index + 1 != this.index)
            {
                Console.WriteLine("invalid index");
                return false;
            }
            else if (previousBlock.hash != this.previousHash)
            {
                Console.WriteLine("invalid previoushash");
                return false;
            }
            else if (this.calculateHashForBlock().Equals(this.hash))
            {
                Console.WriteLine("invalid hash: " + this.calculateHashForBlock().ToString() + " " + this.hash.ToString());
                return false;
            }
            return true;
        }


        public byte[] calculateHashForBlock()
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            return mySHA256.ComputeHash(Encoding.ASCII.GetBytes(this.index + this.previousHash.ToString() + this.timestamp + this.data + this.value));
        }

        public Boolean Equals(_Block block)
        {
            if(this.index == block.index && this.previousHash == block.previousHash && this.hash == block.hash && this.timestamp == block.timestamp && this.data == block.data && this.value == block.value)
            {
                return true;
            }
            return false;
        }
    }
}
