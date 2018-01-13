using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using myBlockChain.blockChain;

namespace ConsoleApp1
{
    class BlockChain
    {
        public List<_Block> blockChain { get; set; }

        public BlockChain()
        {
            this.blockChain = new List<_Block>();
            this.blockChain.Add(new GenesisBlock());
        }

        public BlockChain(List<_Block> blockChain)
        {
            this.blockChain = blockChain;
        }

        /*public void replaceChain(List<Block> newBlocks)
        {
            if (isValidChain(newBlocks) && newBlocks.Count > this.blockChain.Count)
            {
                Console.WriteLine("Received blockchain is valid. Replacing current blockchain with received blockchain");
                this.blockChain = newBlocks;
                //broadcast(responseLatestMsg());
            }
            else
            {
                Console.WriteLine("Received blockchain invalid");
            }
        }*/

        public _Block getLatestBlock()
        {
            return this.blockChain.Last();
        }

        /**
         * Check if the block chain is entirely good
         */
        public Boolean isValidChain () {

            if (this.blockChain[0].Equals(new GenesisBlock())/*JsonConvert.SerializeObject(this.blockChain[0]) != JsonConvert.SerializeObject(new Block())*/)
            {
                return false;
            }
            //List<Block> tempBlocks = new List<Block>();
            //tempBlocks.Add(blockchainToValidate[0]);
            for (int i = 1; i < this.blockChain.Count; i++)
            {
                if (this.blockChain[i].isValidNewBlock(this.blockChain[i-1]))
                {
                    //tempBlocks.Add(blockchainToValidate[i]);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /**
         * Generate a new block for the block chain
         */
        public _Block generateNewBlock(String blockData)
        {
            var previousBlock = getLatestBlock();
            var nextIndex = previousBlock.index + 1;
            var nextTimestamp = DateTime.Now;
            return new Block(nextIndex, previousBlock.hash, nextTimestamp, blockData);
        }

        /**
         * Add one block to the block chain
         */
        public void searchBlock(String data)
        {
            this.blockChain.Add(generateNewBlock(data));            
        }

        public _Block getBlockI(int i)
        {
            return this.blockChain[i];
     
        }

        public void toString()
        {
            for(int i=0; i<this.blockChain.Count; i++)
            {
                Console.WriteLine(this.blockChain[i].index);
            }
        }

        

        
}
}
