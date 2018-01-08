using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class BlockChain
    {
        List<Block> blockChain;

        public BlockChain()
        {
            this.blockChain = new List<Block>();
            this.blockChain.Add(new Block());
        }

        public BlockChain(List<Block> blockChain)
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

        public Block getLatestBlock()
        {
            return this.blockChain.Last();
        }

        /**
         * Check if the block chain is entirely good
         */
        public Boolean isValidChain () {

            if (this.blockChain[0].Equals(new Block())/*JsonConvert.SerializeObject(this.blockChain[0]) != JsonConvert.SerializeObject(new Block())*/)
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
        public Block generateNewBlock(String blockData)
        {
            var previousBlock = getLatestBlock();
            var nextIndex = previousBlock.getIndex() + 1;
            var nextTimestamp = DateTime.Now;
            return new Block(nextIndex, previousBlock.getHash(), nextTimestamp, blockData);
        }

        /**
         * Add one block to the block chain
         */
        public void searchBlock(String data)
        {
            Block newBlock = generateNewBlock(data);
            if (newBlock.isValidNewBlock(getBlockI(this.blockChain.Count - 2)))
            {
                this.blockChain.Add(newBlock);
            }
                
        }

        public Block getBlockI(int i)
        {
            return this.blockChain[i];
     
        }

        

        
}
}
