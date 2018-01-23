using ConsoleApp1;
using myBlockChain.dataFile;
using System;
using System.Threading;

namespace myBlockChain.test
{
    class TestJson
    {
        static public void Main()
        {
            Console.WriteLine("my Block Chain");
            BlockChain myBlockChaine = new BlockChain();
            Console.WriteLine(myBlockChaine.getBlockI(0).index + " " + myBlockChaine.getBlockI(0).data);
            for (int i = 1; i < 10; i++)
            {
                //Thread.Sleep(1000);
                myBlockChaine.searchBlock("test");
                //Console.WriteLine(myBlockChaine.getBlockI(i).getIndex() + " " + myBlockChaine.getBlockI(i).getData());
            }

            myBlockChaine.toString();
            /*Console.WriteLine("Json");
            string json = JsonConvert.SerializeObject(myBlockChaine, Formatting.Indented);
            File.WriteAllText("file.json", json);
            Console.Write(json);

            //StreamWriter file = File.CreateText(@"D:\path.txt");
            Console.WriteLine("open");
            StreamReader r = new StreamReader("file.json");
            Console.WriteLine("lecture");
            string jsonr = r.ReadToEnd();
            Console.WriteLine("deserialize");
            BlockChain deserializedProduct = JsonConvert.DeserializeObject<BlockChain>(jsonr);*/

            Json<BlockChain> j = new Json<BlockChain>(@"dataFile/file.json");
            String s = j.serialize(myBlockChaine);
            Console.WriteLine("json string : "+s);

            BlockChain deserializedProduct = j.deserialize();

            deserializedProduct.toString();

            Console.WriteLine("end");
            


            Thread.Sleep(999999999);
        }

    }
}
