using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace PSN.ModelMate.ArchiMate3XSDTest2
{
    class Program
    {
        static void Main()
        {
            try
            {
                XmlTextReader readerDiagram = new XmlTextReader("archimate3_Diagram.xsd");
                XmlSchema schemaDiagram = XmlSchema.Read(readerDiagram, ValidationCallback);
                schemaDiagram.Write(Console.Out);

                FileStream file = new FileStream("archimate3_Diagram-out.xsd", FileMode.Create, FileAccess.ReadWrite);
                XmlTextWriter xwriter = new XmlTextWriter(file, new UTF8Encoding());
                xwriter.Formatting = Formatting.Indented;
                schemaDiagram.Write(xwriter);
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("archimate3_Diagram.xsd: press enter to continue...");
            Console.ReadLine();

            try
            {
                XmlTextReader readerModel = new XmlTextReader("archimate3_Model.xsd");
                XmlSchema schemaModel = XmlSchema.Read(readerModel, ValidationCallback);
                schemaModel.Write(Console.Out);

                FileStream file = new FileStream("archimate3_Model-out.xsd", FileMode.Create, FileAccess.ReadWrite);
                XmlTextWriter xwriter = new XmlTextWriter(file, new UTF8Encoding());
                xwriter.Formatting = Formatting.Indented;
                schemaModel.Write(xwriter);
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("archimate3_Model.xsd: press enter to continue...");
            Console.ReadLine();

            try
            {
                XmlTextReader readerView = new XmlTextReader("archimate3_View.xsd");
                XmlSchema schemaView = XmlSchema.Read(readerView, ValidationCallback);
                schemaView.Write(Console.Out);

                FileStream file = new FileStream("archimate3_View-out.xsd", FileMode.Create, FileAccess.ReadWrite);
                XmlTextWriter xwriter = new XmlTextWriter(file, new UTF8Encoding());
                xwriter.Formatting = Formatting.Indented;
                schemaView.Write(xwriter);
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("archimate3_View.xsd: press enter to continue...");
            Console.ReadLine();
        }

        static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.Write("WARNING: ");
            else if (args.Severity == XmlSeverityType.Error)
                Console.Write("ERROR: ");

            Console.WriteLine(args.Message);
        }
    }
}
