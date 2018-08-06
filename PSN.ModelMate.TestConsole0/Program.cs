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

namespace ModelMateLibTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            string modelName = "Model 2 Test";
            ModelFactory.ModelName = modelName;

            //model modelArchisurance = ObjectFactory.NewModel("Archisurance");
            //DataSet dsArchisurance = new DataSet();
            //string xmlArchiSurance = @"..\..\..\PSN.ModelMate.Schema\Samples\Archisurance\Archisurance-psn.xml";
            //dsArchisurance.ReadXml(xmlArchiSurance);
            ////dsArchisurance.WriteXmlSchema("xmlArchiSurance.xsd");
            ////dsArchisurance.WriteXml("xmlArchiSurance.xml");
            //var mmpArchisurance = new ModelMateProcessor(ModelMateProcessor.ConvertAMEFFDataSet2Model);
            //// TODO TODO mmpArchisurance.ProcessModel(dsArchisurance, (DbContext)null, new Collection<model> { modelArchisurance });

            string tenantName = "Tenant 0 " + DateTime.Now.ToString();
            ModelFactory.TenantName = tenantName;

            var tenant0 = ModelFactory.NewTenantWithRootFolder(tenantName);

            tenantName = "Tenant 1 " + DateTime.Now.ToString();
            ModelFactory.TenantName = tenantName;

            var tenant1 = ModelFactory.NewTenant(tenantName,
                            ModelFactory.NewFolder("/",
                                new folder[] { ModelFactory.NewFolder("Model Templates"), ModelFactory.NewFolder("Production Models") }
                            )
                          );

            ModelConst.PropertyDataType dtStringType = ModelConst.PropertyDataType.stringType;
            propertydef pdefString = ModelFactory.NewPropertyDef("String Propertydef " + DateTime.Now.ToString(), dtStringType);
            propertydefs pdefs = ModelFactory.NewPropertyDefs(pdefString);

            ModelConst.ElementType etAC = ModelConst.ElementType.ApplicationComponent;
            element[] elementArray = new element[10];
            for (int iElement = 0; iElement < 10; iElement++)
            {
                property p = ModelFactory.NewProperty(ModelFactory.NewValue("Element Property Value " + DateTime.Now.ToString()),
                                        pdefString);
                elementArray[iElement] = ModelFactory.NewElement("Element " + iElement.ToString() + " " + DateTime.Now.ToString(), etAC, p);
            }
            elements elements = ModelFactory.NewElements(elementArray);

            tenantName = "Tenant 2 Test";
            ModelFactory.TenantName = tenantName;

            model model2;
            //var tenant2 = ModelFactory.NewTenant(tenantName,
            //                ModelFactory.NewFolder("/",
            //                    ModelFactory.NewFolder("Production Models",
            //                        ModelFactory.NewFolder("HQ Models Folder " + DateTime.Now.ToString(),
            //                            model2 = ModelFactory.NewModel(modelName, ModelConst.LANG_EN,
            //                                                            elements, pdefs
            //                            )
            //                        )
            //                    )
            //                )
            //              );

            var tenant2 = ModelFactory.NewTenant(tenantName,
                ModelFactory.NewFolder("/",
                            model2 = ModelFactory.NewModel(modelName, ModelConst.LANG_EN,
                                                            elements, pdefs
                            )
                        )
              );

            Console.WriteLine("Tenant.tenant_Id " + tenant2.tenant_Id.ToString());
            foreach (folders fs in tenant2.folders)
            {
                Console.WriteLine("Folders.tenant_Id " + fs.tenant_Id.ToString());
                Console.WriteLine("Folders.folders_Id " + fs.folders_Id.ToString());

                foreach (folder f in fs.folder)
                {
                    Console.WriteLine("Folder " + f.folders_Id.ToString());
                    Console.WriteLine("Folder " + f.folder_Id.ToString());
                }
            }

            var name = ModelFactory.NewName("Model 2 Name " + DateTime.Now.ToString());
            model2.name.Add(name);
            var documentation = ModelFactory.NewDocumentation("Model 2 Documentation " + DateTime.Now.ToString());
            model2.documentation.Add(documentation);
            var metadata = ModelFactory.NewMetadata(
                        ModelFactory.NewProperty(ModelFactory.NewValue("Model 2 Metadata Value " + DateTime.Now.ToString()), pdefString)
                    );
            model2.metadata.Add(metadata);

            var properties = ModelFactory.NewProperties(
                                ModelFactory.NewProperty(ModelFactory.NewValue("Model 2 Property Value " + DateTime.Now.ToString()), pdefString)
                            );
            model2.properties.Add(properties);

            var relationship1 = ModelFactory.NewRelationship("Association Relationship " + DateTime.Now.ToString(),
                                    ModelConst.RelationshipType.AssociationRelationship,
                                    elementArray[0], elementArray[1]
                                );
            var relationships = ModelFactory.NewRelationships(relationship1);
            model2.relationships.Add(relationships);

            var organization = ModelFactory.NewOrganization(
                        ModelFactory.NewItem(elementArray[0],
                            ModelFactory.NewItem(null,
                                ModelFactory.NewItem(elementArray[1]
                                )
                            )
                        )
                   );
            model2.organization.Add(organization);

            var style1 = ModelFactory.NewStyle(
                            ModelFactory.NewFillColor(255, 0, 0),
                            ModelFactory.NewLineColor(0, 255, 0),
                            ModelFactory.NewFont("Times Roman", (float)10.5, "bold", ModelFactory.NewColor(0, 0, 255)), 3
                        );
            var style2 = ModelFactory.NewStyle(
                ModelFactory.NewFillColor(255, 0, 0),
                ModelFactory.NewLineColor(0, 255, 0),
                ModelFactory.NewFont("Times Roman", (float)12.5, "bold", ModelFactory.NewColor(0, 0, 255)), 3
            );

            var node0 = ModelFactory.NewNode("Node 0 " + DateTime.Now.ToString(),
                                        0, 0, 0, 50, 50, 50,
                                        elementArray[0], null, null, style1,
                                        new node[] { ModelFactory.NewNode("Sub Node " + DateTime.Now.ToString(),
                                                        10, 10, 10, 25, 25, 25,
                                                        elementArray[3])
                                        }
                        );
            var node1 = ModelFactory.NewNode("Node 1 " + DateTime.Now.ToString(),
                                        100, 100, 100, 50, 50, 50,
                                        elementArray[1], null, null, style2,
                                        null
                        );

            var bendpoint1 = ModelFactory.NewBendPoint(25, 25, 25);
            var connection1 = ModelFactory.NewConnection("Connection 1 " + DateTime.Now.ToString(),
                                node0, node1, relationship1
                              );
            connection1.bendpoint.Add(bendpoint1);

            view view2;
            var views = ModelFactory.NewViews(
                            view2 = ModelFactory.NewView("View 2 " + DateTime.Now.ToString(), ModelConst.ViewType.Layered,
                                new node[] {
                                    node0, node1
                                },
                                new connection[] { connection1 }
                            )
                        );
            model2.views.Add(views);

            tenant2.processinghistory.Add(
            ModelFactory.NewProcessinghistory(
                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Processinghistory " + DateTime.Now.ToString(),
                    ModelConst.TimesnapCategory.processinghistory, "Subcategory " + DateTime.Now.ToString(),
                    null, null,
                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                    )
                )
            )
            );
            tenant2.usage.Add(
                        ModelFactory.NewUsage(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.usage, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );
            tenant2.performance.Add(
                        ModelFactory.NewPerformance(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.performance, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );
            tenant2.management.Add(
                        ModelFactory.NewManagement(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Management " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.management, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );

            model2.processinghistory.Add(
                        ModelFactory.NewProcessinghistory(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Processinghistory " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.processinghistory, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );
            model2.usage.Add(
                        ModelFactory.NewUsage(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.usage, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );
            model2.performance.Add(
                        ModelFactory.NewPerformance(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.performance, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );
            model2.management.Add(
                        ModelFactory.NewManagement(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Management " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.management, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );

            elementArray[0].processinghistory.Add(
            ModelFactory.NewProcessinghistory(
                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Processinghistory " + DateTime.Now.ToString(),
                    ModelConst.TimesnapCategory.processinghistory, "Subcategory " + DateTime.Now.ToString(),
                    null, null,
                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                    )
                )
            )
            );
            elementArray[0].usage.Add(
                        ModelFactory.NewUsage(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.usage, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );
            elementArray[0].performance.Add(
                        ModelFactory.NewPerformance(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.performance, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );
            elementArray[0].management.Add(
                        ModelFactory.NewManagement(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Management " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.management, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );

            view2.processinghistory.Add(
                        ModelFactory.NewProcessinghistory(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Processinghistory " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.processinghistory, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );
            view2.usage.Add(
                        ModelFactory.NewUsage(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.usage, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );
            view2.performance.Add(
                        ModelFactory.NewPerformance(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.performance, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );
            view2.management.Add(
                        ModelFactory.NewManagement(
                            ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Management " + DateTime.Now.ToString(),
                                ModelConst.TimesnapCategory.management, "Subcategory " + DateTime.Now.ToString(),
                                null, null,
                                ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
                                )
                            )
                        )
                        );

            //using (XmlWriter writer = XmlWriter.Create("model2.xml"))
            //{
            //    //XmlSerializer serializer = new XmlSerializer(typeof(model));
            //    DataContractSerializerSettings settings = new DataContractSerializerSettings();
            //    DataContractSerializer serializer = new DataContractSerializer(typeof(model));
            //    serializer.WriteObject(writer, model2);
            //    writer.Close();
            //}

            string xmlSchemaFile = @"..\..\..\PSN.ModelMate.Schema\Schema\Reference ModelGood9.xsd";
            // DataSet ds = new DataSet();
            // ds.ReadXmlSchema(xmlSchemaFile);
            // BUG DataSet ds2 = new DataSet();
            // BUG ds2.ReadXmlSchema(xmlSchemaFile);

            //DataTable dttenant = ds.Tables["tenant"];
            //DataRow drtentant1 = dttenant.NewRow();
            //DataRow drtentant2 = ds.Tables["tenant"].NewRow();

            //DataTable dtfolders = ds.Tables["folders"];
            //DataTable dtfolder = ds.Tables["folder"];
            //DataTable dtmodels = ds.Tables["models"];
            //DataTable dtmodel = ds.Tables["model"];

            //DataTable dtprocessinghistory = ds.Tables["processinghistory"];
            //DataTable dtusage = ds.Tables["usage"];
            //DataTable dtperformance = ds.Tables["performance"];
            //DataTable dtmanagement = ds.Tables["management"];
            //DataTable dttimesnap = ds.Tables["timesnap"];

            //DataTable dtproperties = ds.Tables["properties"];
            //DataTable dtproperty = ds.Tables["property"];

            //DataTable dtpropertydefs = ds.Tables["propertydefs"];
            //DataTable dtpropertydef = ds.Tables["propertydef"];

            //DataTable dtelements = ds.Tables["elements"];
            //DataTable dtelement = ds.Tables["element"];

            //DataTable dtrelationships = ds.Tables["relationships"];
            //DataTable dtrelationship = ds.Tables["relationship"];

            //DataTable dtviews = ds.Tables["views"];
            //DataTable dtview = ds.Tables["view"];
            //DataTable dtconnection = ds.Tables["connection"];
            //DataTable dtnode = ds.Tables["node"];

            //DataTable dtstyle = ds.Tables["style"];
            //DataTable dtfont = ds.Tables["font"];
            //DataTable dtfillColor = ds.Tables["fillColor"];
            //DataTable dtlineColor = ds.Tables["lineColor"];
            //DataTable dtcolor = ds.Tables["color"];
            //DataTable dtbendpoint = ds.Tables["bendpoint"];

            //DataTable dtmetadata = ds.Tables["metadata"];

            //DataTable dtorganization = ds.Tables["organization"];
            //DataTable dtitem = ds.Tables["item"];

            //DataTable dtname = ds.Tables["name"];
            //DataTable dtvalue = ds.Tables["value"];
            //DataTable dtdocumentation = ds.Tables["documentation"];
            //DataTable dtlabel = ds.Tables["label"];

            using (var ctx = new ModelMateEFModel9Context())
            {
                ctx.Database.Log = Console.Write;

                ctx.tenant.Add(tenant0);
                ModelDump.DisplayTrackedEntities("Add tenant0", ctx.ChangeTracker);
                ctx.SaveChanges();
                ModelDump.DisplayTrackedEntities("Save tenant0", ctx.ChangeTracker);

                ctx.tenant.Add(tenant1);
                ctx.SaveChanges();

                ctx.tenant.Add(tenant2);
                ModelDump.DisplayTrackedEntities("Add tenant2", ctx.ChangeTracker);
                ctx.SaveChanges();
                ModelDump.DisplayTrackedEntities("Save tenant2", ctx.ChangeTracker);
                ModelDump.DisplayDBPropertyValues("tenant2", ctx.Entry(tenant2).CurrentValues, null);
                ModelDump.DisplayDBPropertyValues("model2", ctx.Entry(model2).CurrentValues, null);
                Console.WriteLine("Tenant.tenant_Id " + tenant2.tenant_Id.ToString());
                foreach (folders fs in tenant2.folders)
                {
                    Console.WriteLine("Folders.tenant_Id " + fs.tenant_Id.ToString());
                    Console.WriteLine("Folders.folders_Id " + fs.folders_Id.ToString());

                    foreach (folder f in fs.folder)
                    {
                        Console.WriteLine("Folder " + f.folders_Id.ToString());
                        Console.WriteLine("Folder " + f.folder_Id.ToString());
                    }
                }

                folders fs0 = tenant2.folders.ElementAt(0);
                ModelDump.DisplayDBPropertyValues("fs0", ctx.Entry(fs0).CurrentValues, null);

                folder f0 = fs0.folder.ElementAt(0);
                ModelDump.DisplayDBPropertyValues("f0", ctx.Entry(f0).CurrentValues, null);

                Console.WriteLine("RECURSION ==================================================");
                //DataSet dsTenant = new DataSet();
                //dsTenant.ReadXmlSchema(xmlSchemaFile);
                ////dsTenant.Tables["tenant"].Namespace = "http://www.w3.org/XML/1998/namespace";
                ////dsTenant.Tables["tenant"].Prefix = "xml";
                //var tenant = ctx.tenant.Find(new object[] { tenant2.tenant_Id });
                //var mmp = new ModelMateProcessor(ModelMateProcessor.ProcessObjectPopulateXMLDataSet);
                //mmp.ProcessTenant(dsTenant, (DbContext)ctx, new Collection<tenant> { tenant });
                //dsTenant.WriteXml("tentantds.xml");

                string modelSchemaFile = @"..\..\..\PSN.ModelMate.Schema\Schema\ModelMateModel9.xsd";
                DataSet dsModel = new DataSet();
                dsModel.ReadXmlSchema(modelSchemaFile);
                //DataSet ds3 = dsModel.Copy();
                //var mmpModel = new ModelMateProcessor();
                var mmpModel = new ModelProcessor(ModelProcessor.ConvertModel2AMEFFDataSet);
                mmpModel.ProcessModel(dsModel, (DbContext)ctx, new Collection<model> { model2 });
                dsModel.WriteXml("modelds.xml", XmlWriteMode.IgnoreSchema);
                string filename = "modelds2.xml";
                mmpModel.SaveAMEFFDataSetAsXML(dsModel, filename);

                tenant2.version = ModelConst.TENANT_TESTVERSION;
                var name2 = ModelFactory.NewName("Model Name FR " + DateTime.Now.ToString(), ModelConst.LANG_FR);
                model2.name.Add(name2);
                var name3 = ModelFactory.NewName("Model Name EN " + DateTime.Now.ToString());
                model2.name.Add(name3);
                ModelDump.DisplayTrackedEntities("Change tenant2", ctx.ChangeTracker);
                ctx.SaveChanges();
                ModelDump.DisplayTrackedEntities("Save tenant2", ctx.ChangeTracker);

                object[] keys = { -1702823521 };
                tenant tenant9 = ctx.tenant.Find(keys);
                //ctx.tenant.Remove(tenantDelete);
                //ctx.SaveChanges();
            }

            Console.WriteLine("Press enter to exist...");
            Console.ReadLine();
        }

        // TODO Autolog to Processing History
        // TODO Load Dataset from EntityFramework

    }
}
