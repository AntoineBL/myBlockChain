using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myBlockChain.dataFile
{
    sealed  class GenesisBlock : _Block
    {
        public GenesisBlock()
        {
            this.index = 0;
            this.previousHash = Enumerable.Repeat((byte)0x0, 256).ToArray(); ;
            this.timestamp = DateTime.Now;
            this.data = "816534932c2b7154836da6afc367695e6337db8a921823784c14378abed4f7d7";
            this.hash = multiThreadhash(this.index, this.previousHash, this.timestamp, this.data);
        }

        public GenesisBlock(int index, byte[] previousHash, DateTime timestamp, string data, byte[] hash, int value)
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
