using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN.ModelMate.Lib
{
    public static partial class ModelDump
    {
        public static void DisplayTrackedEntities(string label, DbChangeTracker changeTracker)
        {
            Console.WriteLine(label + ":");

            var entries = changeTracker.Entries();
            foreach (var entry in entries)
            {
                Console.WriteLine("Entity Name: {0} [{1}]\tStatus: {2}",
                                    entry.Entity.GetType().FullName, entry.Entity.GetType().Name, entry.State);
                switch (entry.State)
                {
                    case System.Data.Entity.EntityState.Added:
                        {
                            DisplayDBPropertyValues(entry.CurrentValues, null);
                            break;
                        }
                    case System.Data.Entity.EntityState.Deleted:
                        {
                            DisplayDBPropertyValues(entry.CurrentValues, entry.OriginalValues);
                            break;
                        }
                    case System.Data.Entity.EntityState.Modified:
                        {
                            DisplayDBPropertyValues(entry.CurrentValues, entry.OriginalValues);
                            break;
                        }
                    case System.Data.Entity.EntityState.Unchanged:
                        {
                            // DisplayDBPropertyValues(entry.CurrentValues, entry.OriginalValues);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            Console.WriteLine("---------------------------------------");
        }

        public static void DisplayDBPropertyValues(DbPropertyValues currentValues, DbPropertyValues originalValues, int indent = 1)
        {
            DisplayDBPropertyValues("", currentValues, originalValues, indent);
        }
        public static void DisplayDBPropertyValues(string label, DbPropertyValues currentValues, DbPropertyValues originalValues, int indent = 1)
        {
            Console.WriteLine(label + ":");

            foreach (string name in currentValues.PropertyNames)
            {
                var currentValue = currentValues[name];
                if (currentValue is DbPropertyValues)
                {
                    Console.WriteLine("{0}- complex property {1}", String.Empty.PadLeft(indent), name);
                    DisplayDBPropertyValues((DbPropertyValues)currentValue, null, indent + 1);

                }
                else
                {
                    if (originalValues == null)
                    {
                        Console.WriteLine("{0}- {1}:\t{2}", String.Empty.PadLeft(indent), name, currentValue);
                    }
                    else
                    {
                        var originalValue = originalValues[name];
                        Console.WriteLine("{0}- {1}:\t{2}\t[{3}]", String.Empty.PadLeft(indent), name, currentValue, originalValue);
                    }
                }
            }
        }
    }
}
