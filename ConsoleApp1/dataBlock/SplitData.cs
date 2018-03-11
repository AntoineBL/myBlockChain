using System;
using System.Collections.Generic;
using System.Text;

namespace myBlockChain.dataBlock
{
    public class SplitData
    {
        String[] dataSplit;

        public SplitData(String data)
        {
            split(data);
        }

        public void split(String data)
        {
            dataSplit = data.Split("@@");
        }

        public String getGoal()
        {
            return dataSplit[0];
        }

        public String getHash()
        {
            return dataSplit[1];
        }

        public String getData()
        {
            return dataSplit[2];
        }

    }
}
