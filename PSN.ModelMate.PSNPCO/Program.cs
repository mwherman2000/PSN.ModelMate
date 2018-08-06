using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace PSN.ModelMate.PSNPCO
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dsPCO = new DataSet();

            const string fileName = "../../PSNPCODoc3.xml";

            dsPCO.ReadXml(fileName);

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
