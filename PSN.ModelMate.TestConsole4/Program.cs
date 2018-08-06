using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Data;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace ModelMateLibFindTest1
{
    class Program
    {
        const int tid = 957727837;
        const string tident1 = "id-tTnnt2";
        const string tident2 = "id-tTnnt%";
        const string tname1 = "Tenant 2";
        const string tname2 = "Tenant%";

        const string mident0 = "id-tTnnt2.mMdl22016092342146PM";

        static void Main(string[] args)
        {
            using (var ctx = new ModelMateEFModel9Context())
            {
                ctx.Database.Log = Console.Write;
                Console.WriteLine("LazyLoadingEnabled: " + ctx.Configuration.LazyLoadingEnabled.ToString());

                foreach (tenant t in ctx.tenant)
                {
                    ModelDump.DisplayDBPropertyValues("t", ctx.Entry(t).CurrentValues, null);
                }

                //var query = from t in ctx.tenant
                //            where t.identifier == tident1
                //            select t;
                //tenant tq = query.Single();

                //object[] keysq = { tid };
                //tenant tq = ctx.tenant.Find(keysq);

                //ctx.Entry(tq).Collection(fs => fs.folders).Load(); // Works
                //folders fsq = tq.folders.ElementAt(0);
                //ModelMateLib.DisplayDBPropertyValues("fsq", ctx.Entry(fsq).CurrentValues, null);

                //ctx.Entry(fsq).Collection(f => f.folder).Load(); // Works
                //folder fq = fsq.folder.ElementAt(0);
                //ModelMateLib.DisplayDBPropertyValues("fq", ctx.Entry(fq).CurrentValues, null);

                //ctx.tenant.Load();
                object[] keys = { tid };
                tenant tenant9 = ctx.tenant.Find(keys);
                folders fs0 = tenant9.folders.ElementAt(0);
                ModelDump.DisplayDBPropertyValues("fs0", ctx.Entry(fs0).CurrentValues, null);
                folder f0 = fs0.folder.ElementAt(0);
                ModelDump.DisplayDBPropertyValues("f0", ctx.Entry(f0).CurrentValues, null);



                //folders fs0 = t0.folders.ElementAt(0);
                //ModelMateLib.DisplayDBPropertyValues("fs0", ctx.Entry(fs0).CurrentValues, null);
                //folder f0 = fs0.folder.ElementAt(0);
                //ModelMateLib.DisplayDBPropertyValues("f0", ctx.Entry(f0).CurrentValues, null);

                tenant[] ts2 = new tenant[] { };
                ts2 = ModelFinder.FindTenants(ctx, tident2, "");
                ModelDump.DisplayDBPropertyValues("ts2", ctx.Entry(ts2[0]).CurrentValues, null);

                ts2 = ModelFinder.FindTenants(ctx, "", tname2);
                ModelDump.DisplayDBPropertyValues("ts2", ctx.Entry(ts2[0]).CurrentValues, null);

                tenant t2 = null;
                try
                {
                    t2 = ModelFinder.FindTenant(ctx, "", tname1);
                    ModelDump.DisplayDBPropertyValues("t1", ctx.Entry(t2).CurrentValues, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEPTION: " + ex.ToString());
                }

                //tenant t2 = null;
                try
                {
                    t2 = ModelFinder.FindTenant(ctx, tident1, "");
                    ModelDump.DisplayDBPropertyValues("t1", ctx.Entry(t2).CurrentValues, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEPTION: " + ex.ToString());
                }

                model m2 = ModelFinder.FindModel(ctx, t2, mident0, null);
                ModelDump.DisplayDBPropertyValues("m2", ctx.Entry(m2).CurrentValues, null);

                element[] es0 = ModelFinder.FindElements(ctx, t2, m2, ModelConst.ElementType.ApplicationComponent);
                Console.WriteLine("es0.Count: " + es0.Count<element>().ToString());
                ModelDump.DisplayDBPropertyValues("es0", ctx.Entry(es0[0]).CurrentValues, null);

                es0 = ModelFinder.FindElements(ctx, t2, m2, ModelConst.ElementType.AllElementTypes);
                Console.WriteLine("es0.Count: " + es0.Count<element>().ToString());
                ModelDump.DisplayDBPropertyValues("es0", ctx.Entry(es0[0]).CurrentValues, null);

                relationship[] rs0 = ModelFinder.FindRelationships(ctx, t2, m2, ModelConst.RelationshipType.AllRelationshipTypes);
                Console.WriteLine("es0.Count: " + es0.Count<element>().ToString());
                ModelDump.DisplayDBPropertyValues("es0", ctx.Entry(es0[0]).CurrentValues, null);

                foreach(relationship r in rs0)
                {
                    element eSource = ModelFinder.FindElement(ctx, t2, m2, r.source, null);
                    element eTarget = ModelFinder.FindElement(ctx, t2, m2, r.target, null);
                    ModelDump.DisplayDBPropertyValues("eSource", ctx.Entry(eSource).CurrentValues, null);
                    ModelDump.DisplayDBPropertyValues("r", ctx.Entry(r).CurrentValues, null);
                    ModelDump.DisplayDBPropertyValues("eTarget", ctx.Entry(eTarget).CurrentValues, null);
                }

                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
            }
        }
    }
}
