using ConsoleApp1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace myBlockChain.blockChain
{
    class Json
    {
        private String fileName;
        private BlockChain blockChain;

        public Json(String fileName, BlockChain blockChaine)
        {
            this.fileName = fileName;
            this.blockChain = blockChaine;
        }

        public String serialize()
        {
            string json = JsonConvert.SerializeObject(this.blockChain, Formatting.Indented);
            File.WriteAllText(this.fileName, json);

            return json;
        }

        public BlockChain deserialize()
        {
            StreamReader r = new StreamReader(this.fileName);
            string jsonr = r.ReadToEnd();
            BlockChain deserializedBlockChain = JsonConvert.DeserializeObject<BlockChain>(jsonr);

            return deserializedBlockChain;
        }
        

    }
}
