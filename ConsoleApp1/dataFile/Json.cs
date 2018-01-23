using ConsoleApp1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace myBlockChain.dataFile
{
    class Json <T>
    {
        private String fileName;

        /*public Json(String fileName, BlockChain blockChaine)
        {
            this.fileName = fileName;
            this.blockChain = blockChaine;
        }*/

        public Json(String fileName)
        {
            this.fileName = fileName;
        }



        public String serialize(T format)
        {
            string json = JsonConvert.SerializeObject(format, Formatting.Indented);
            File.WriteAllText(this.fileName, json);

            return json;
        }

        public T deserialize()
        {
            StreamReader r = new StreamReader(this.fileName);
            string jsonr = r.ReadToEnd();
            T deserializedBlockChain = JsonConvert.DeserializeObject<T>(jsonr);

            return deserializedBlockChain;
        }
        

    }
}
