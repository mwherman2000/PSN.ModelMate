using PSN.ModelMate.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services;

namespace PSN.ModelMate.TestConsole9
{
    class Program
    {
        static void Main(string[] args)
        {
            Type serviceType = typeof(ODataServiceDataService);
            Uri baseAddress = new Uri("http://localhost:6822/ModelMateODataService");
            Uri[] baseAddresses = new Uri[] { baseAddress };

            // Create a new hosting instance for the Northwind 
            // data service at the specified address. 
            DataServiceHost host = new DataServiceHost(serviceType, baseAddresses);
            host.Open();

            // Keep the data service host open while the console is open. 
            Console.WriteLine("Navigate to the following URI to see the service.");
            Console.WriteLine(baseAddress);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            // Close the host. 
            host.Close();
        }
    }
}
