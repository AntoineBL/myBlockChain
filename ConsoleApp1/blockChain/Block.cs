using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    class Block
    {
        private int index;
        private Byte[] previousHash;
        private DateTime timestamp;
        private String data;
        private Byte[] hash;
        private int value;

        public DateTime getTimestamp()
        {
            return this.timestamp;
        }

        public String getData()
        {
            return this.data;
        }

        /**
         * Block Genesis
         */
        public Block()
        {
            
            this.index = 0;
            this.previousHash = Enumerable.Repeat((byte)0x0, 256).ToArray(); ;
            this.timestamp = DateTime.Now;
            this.data = "816534932c2b7154836da6afc367695e6337db8a921823784c14378abed4f7d7";
            this.hash = calculateHash(this.index, this.previousHash, this.timestamp, this.data);
        }

        public Block(int index, Byte[] previousHash, DateTime timestamp, String data) {
            this.index = index;
            this.previousHash = previousHash;
            this.timestamp = timestamp;
            this.data = data;
            this.hash = calculateHash(index, previousHash, timestamp, data);
        }

        /**
         * Calculate the hash for one block
         * 
         */
        public Byte[] calculateHash(int index, byte[] previousHash, DateTime timestamp, String data)
        {

            SHA256 mySHA256 = SHA256Managed.Create();
            Random random = new Random(5);
            byte[] difficult = Enumerable.Repeat((byte)0x00, 32).ToArray();
            difficult[31] = 0xFF;
            difficult[30] = 0xFF;
            difficult[29] = 0xFF;
            /*difficult[28] = 0xFF;
            difficult[27] = 0xFF;
            difficult[26] = 0xFF;
            difficult[25] = 0xFF;
            difficult[24] = 0xFF;
            difficult[23] = 0xFF;
            difficult[22] = 0xFF;
            difficult[21] = 0xFF;
            difficult[20] = 0xFF;
            difficult[19] = 0xFF;
            difficult[18] = 0xFF;
            difficult[17] = 0xFF;
            difficult[16] = 0xFF;
            difficult[15] = 0xFF;
            difficult[14] = 0xFF;
            difficult[13] = 0xFF;
            difficult[12] = 0xFF;
            difficult[11] = 0xFF;
            difficult[10] = 0xFF;
            difficult[9] = 0xFF;
            difficult[8] = 0xFF;
            difficult[7] = 0xFF;
            difficult[6] = 0xFF;
            difficult[5] = 0xFF;
            difficult[4] = 0xFF;
            difficult[3] = 0xFF;
            difficult[2] = 0xFF;*/
            //Console.WriteLine(difficult[31]);
            byte[] hashValue;
            int val = 0;
            int value;
            do
            {
                val++;
                value = random.Next();
                hashValue = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(index.ToString() + previousHash.ToString() + timestamp + data + ));
            } while (!proofOfWork(difficult, hashValue));
            Console.WriteLine("   number of iteration for find hash"+ val);
            this.value = value;
            return hashValue;
        }

        /**
         * Check if the hash value matche with the difficulty
         */
        public Boolean proofOfWork(Byte[] difficult,  Byte[] hasValue)
        {
            int res = 0;
            for(int i=0; i<difficult.Length; i++)
            {
                
                res += difficult[i] & hasValue[i];
            }
            if(res != 0) { return false; }
            return true; 
        }


        /*
         * Check if the new block is validate
         * 
         */
        public Boolean isValidNewBlock (Block previousBlock) {
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
            return calculateHash(this.index, this.previousHash, this.timestamp, this.data);
        }

        
        public Byte[] getPreviousHash()
        {
            return this.previousHash;
        }

        public Byte[] getHash()
        {
            return this.hash;
        }

        public int getIndex()
        {
            return this.index;
        }


   


    }
}
