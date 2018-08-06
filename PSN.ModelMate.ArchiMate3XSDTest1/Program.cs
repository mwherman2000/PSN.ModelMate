using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PSN.ModelMate.ArchiMate3XSDTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dsDiagram = new DataSet();
            DataSet dsModel = new DataSet();
            DataSet dsView = new DataSet();

            dsDiagram.ReadXmlSchema("archimate3_Diagram.xsd");

            dsModel.ReadXmlSchema("archimate3_Model.xsd");

            dsView.ReadXmlSchema("archimate3_View.xsd");

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
    }
}
