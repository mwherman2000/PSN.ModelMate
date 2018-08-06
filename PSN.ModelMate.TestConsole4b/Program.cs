using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;
using Parallelspace.Content.Objects;
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
            const string tTargetName = "Tenant 2 Test";
            string tTargetIdentifier = Util.MakeIdentifierRootIdentifier(typeof(tenant).Name, Util.TrimWhitespace(tTargetName));

            const string mTargetName = "Model 2 Test";
            string mTargetIdentifier = Util.MakeIdentifierFromParentIdentifier(
                    Util.MakeIdentifierRootIdentifier(typeof(tenant).Name, tTargetName),
                    typeof(model).Name, Util.TrimWhitespace(mTargetName));

            tenant tTarget = null;
            model mTarget = null;

            using (var ctxTarget = new ModelMateEFModel9Context())
            {
                ctxTarget.Database.Log = Console.Write;
                Console.WriteLine("LazyLoadingEnabled: " + ctxTarget.Configuration.LazyLoadingEnabled.ToString());

                tTarget = ModelFinder.FindTenant(ctxTarget, tTargetIdentifier, "");
                ModelDump.DisplayDBPropertyValues("tTarget", ctxTarget.Entry(tTarget).CurrentValues, null);

                mTarget = ModelFinder.FindModel(ctxTarget, tTarget, mTargetIdentifier, null);
                ModelDump.DisplayDBPropertyValues("mTarget", ctxTarget.Entry(mTarget).CurrentValues, null);

                element[] es0 = ModelFinder.FindElements(ctxTarget, tTarget, mTarget, ModelConst.ElementType.ApplicationComponent);
                Console.WriteLine("es0.Count: " + es0.Count<element>().ToString());
                ModelDump.DisplayDBPropertyValues("es0", ctxTarget.Entry(es0[0]).CurrentValues, null);

                es0 = ModelFinder.FindElements(ctxTarget, tTarget, mTarget, ModelConst.ElementType.AllElementTypes);
                Console.WriteLine("es0.Count: " + es0.Count<element>().ToString());
                ModelDump.DisplayDBPropertyValues("es0", ctxTarget.Entry(es0[0]).CurrentValues, null);

                relationship[] rs0 = ModelFinder.FindRelationships(ctxTarget, tTarget, mTarget, ModelConst.RelationshipType.AllRelationshipTypes);
                Console.WriteLine("es0.Count: " + es0.Count<element>().ToString());
                ModelDump.DisplayDBPropertyValues("es0", ctxTarget.Entry(es0[0]).CurrentValues, null);

                foreach (relationship r in rs0)
                {
                    element eSource = ModelFinder.FindElement(ctxTarget, tTarget, mTarget, r.source, null);
                    element eTarget = ModelFinder.FindElement(ctxTarget, tTarget, mTarget, r.target, null);
                    ModelDump.DisplayDBPropertyValues("eSource", ctxTarget.Entry(eSource).CurrentValues, null);
                    ModelDump.DisplayDBPropertyValues("r", ctxTarget.Entry(r).CurrentValues, null);
                    ModelDump.DisplayDBPropertyValues("eTarget", ctxTarget.Entry(eTarget).CurrentValues, null);
                }
            }

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();

            string tSourceName = "Tenant 2";
            ModelFactory.TenantName = tSourceName;

            string mSourceIdentifier = mTargetIdentifier;
            string mSourceName = mTargetName; // "Model 2 " + DateTime.Now.ToString();
            ModelFactory.ModelName = mSourceName;

            tenant tSource = null;
            model mSource = null;

            propertydef pdef1 = ModelFactory.NewPropertyDef("String Propertydef 1", ModelConst.PropertyDataType.stringType);
            propertydef pdef2 = ModelFactory.NewPropertyDef("Boolean Propertydef 2", ModelConst.PropertyDataType.booleanType);
            propertydef pdef3 = ModelFactory.NewPropertyDef("Number Propertydef 3", ModelConst.PropertyDataType.numberType);
            propertydef[] pdfArray = new propertydef[] { pdef1, pdef2, pdef3 };
            propertydefs pdefs = ModelFactory.NewPropertyDefs(pdfArray);

            ModelConst.ElementType etAC = ModelConst.ElementType.ApplicationComponent;
            element[] elementArray = new element[10];
            for (int iElement = 0; iElement < 10; iElement++)
            {
                property p1 = ModelFactory.NewProperty(ModelFactory.NewValue("Element Property Value " + DateTime.Now.ToString()),
                                        pdfArray[iElement % 3]);
                property p2 = ModelFactory.NewProperty(ModelFactory.NewValue("Element Property Value " + DateTime.Now.ToString()),
                                        pdfArray[(iElement+1) % 3]);
                properties ps = ModelFactory.NewProperties(new property[] { p1, p2 });
                elementArray[iElement] = ModelFactory.NewElement("Element " + iElement.ToString(), etAC, ps);
            }
            elements elements = ModelFactory.NewElements(elementArray);

            tSource = ModelFactory.NewTenant(tSourceName,
                ModelFactory.NewFolder("/",
                            mSource = ModelFactory.NewModel(mSourceName, ModelConst.LANG_EN,
                                                            elements, pdefs
                            )
                        )
              );

            mSource.version = "9.0";

            var properties = ModelFactory.NewProperties(ModelFactory.NewProperty(ModelFactory.NewValue("Model 2 Value 1"), pdef1));
            mSource.properties.Add(properties);

            //var name = ModelFactory.NewName("Model 2 Name " + DateTime.Now.ToString());
            //model3.name.Add(name);

            var relationship1 = ModelFactory.NewRelationship("Association Relationship 1",
                                    ModelConst.RelationshipType.AssociationRelationship,
                                    elementArray[0], elementArray[1]
                                );
            var relationship2 = ModelFactory.NewRelationship("Association Relationship 2",
                                    ModelConst.RelationshipType.AssociationRelationship,
                                    elementArray[2], elementArray[3]
                                );
            var relationships = ModelFactory.NewRelationships(new relationship[] { relationship1, relationship2 });
            mSource.relationships.Add(relationships);

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();

            //var organization = ModelFactory.NewOrganization(
            //            ModelFactory.NewItem(elementArray[0],
            //                ModelFactory.NewItem(null,
            //                    ModelFactory.NewItem(elementArray[1]
            //                    )
            //                )
            //            )
            //       );
            //model3.organization.Add(organization);

            //var documentation = ModelFactory.NewDocumentation("Model 2 Documentation " + DateTime.Now.ToString());
            //model3.documentation.Add(documentation);

            //var metadata = ModelFactory.NewMetadata(
            //            ModelFactory.NewProperty(ModelFactory.NewValue("Model 2 Metadata Value " + DateTime.Now.ToString()), pdefString)
            //        );
            //model3.metadata.Add(metadata);

            //var style1 = ModelFactory.NewStyle(
            //                ModelFactory.NewFillColor(255, 0, 0),
            //                ModelFactory.NewLineColor(0, 255, 0),
            //                ModelFactory.NewFont("Times Roman", (float)10.5, "bold", ModelFactory.NewColor(0, 0, 255)), 3
            //            );
            //var style2 = ModelFactory.NewStyle(
            //    ModelFactory.NewFillColor(255, 0, 0),
            //    ModelFactory.NewLineColor(0, 255, 0),
            //    ModelFactory.NewFont("Times Roman", (float)12.5, "bold", ModelFactory.NewColor(0, 0, 255)), 3
            //);

            //var node0 = ModelFactory.NewNode("Node 0 " + DateTime.Now.ToString(),
            //                            0, 0, 0, 50, 50, 50,
            //                            elementArray[0], null, null, style1,
            //                            new node[] { ModelFactory.NewNode("Sub Node " + DateTime.Now.ToString(),
            //                                            10, 10, 10, 25, 25, 25,
            //                                            elementArray[3])
            //                            }
            //            );
            //var node1 = ModelFactory.NewNode("Node 1 " + DateTime.Now.ToString(),
            //                            100, 100, 100, 50, 50, 50,
            //                            elementArray[1], null, null, style2,
            //                            null
            //            );

            //var bendpoint1 = ModelFactory.NewBendPoint(25, 25, 25);
            //var connection1 = ModelFactory.NewConnection("Connection 1 " + DateTime.Now.ToString(),
            //                    node0, node1, relationship1
            //                  );
            //connection1.bendpoint.Add(bendpoint1);

            //view view2;
            //var views = ModelFactory.NewViews(
            //                view2 = ModelFactory.NewView("View 2 " + DateTime.Now.ToString(), ModelConst.ViewType.Layered,
            //                    new node[] {
            //                        node0, node1
            //                    },
            //                    new connection[] { connection1 }
            //                )
            //            );
            //model2.views.Add(views);

            //tenant2.processinghistory.Add(
            //ModelFactory.NewProcessinghistory(
            //    ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Processinghistory " + DateTime.Now.ToString(),
            //        ModelConst.TimesnapCategory.processinghistory, "Subcategory " + DateTime.Now.ToString(),
            //        null, null,
            //        ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //        )
            //    )
            //)
            //);
            //tenant2.usage.Add(
            //            ModelFactory.NewUsage(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.usage, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );
            //tenant2.performance.Add(
            //            ModelFactory.NewPerformance(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.performance, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );
            //tenant2.management.Add(
            //            ModelFactory.NewManagement(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Management " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.management, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );

            //model2.processinghistory.Add(
            //            ModelFactory.NewProcessinghistory(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Processinghistory " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.processinghistory, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );
            //model2.usage.Add(
            //            ModelFactory.NewUsage(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.usage, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );
            //model2.performance.Add(
            //            ModelFactory.NewPerformance(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.performance, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );
            //model2.management.Add(
            //            ModelFactory.NewManagement(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Management " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.management, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );

            //elementArray[0].processinghistory.Add(
            //ModelFactory.NewProcessinghistory(
            //    ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Processinghistory " + DateTime.Now.ToString(),
            //        ModelConst.TimesnapCategory.processinghistory, "Subcategory " + DateTime.Now.ToString(),
            //        null, null,
            //        ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //        )
            //    )
            //)
            //);
            //elementArray[0].usage.Add(
            //            ModelFactory.NewUsage(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.usage, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );
            //elementArray[0].performance.Add(
            //            ModelFactory.NewPerformance(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.performance, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );
            //elementArray[0].management.Add(
            //            ModelFactory.NewManagement(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Management " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.management, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );

            //view2.processinghistory.Add(
            //            ModelFactory.NewProcessinghistory(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Processinghistory " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.processinghistory, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );
            //view2.usage.Add(
            //            ModelFactory.NewUsage(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.usage, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );
            //view2.performance.Add(
            //            ModelFactory.NewPerformance(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Performance " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.performance, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );
            //view2.management.Add(
            //            ModelFactory.NewManagement(
            //                ModelFactory.NewTimesnap(DateTime.Now, "Timesnap Management " + DateTime.Now.ToString(),
            //                    ModelConst.TimesnapCategory.management, "Subcategory " + DateTime.Now.ToString(),
            //                    null, null,
            //                    ModelFactory.NewProperty(ModelFactory.NewValue("Value " + DateTime.Now.ToString()), pdefString
            //                    )
            //                )
            //            )
            //            );

            using (var ctxTarget = new ModelMateEFModel9Context())
            {
                ctxTarget.Database.Log = Console.Write;

                tTarget = ModelFinder.FindTenant(ctxTarget, tTargetIdentifier, "");
                ModelDump.DisplayDBPropertyValues("tTarget", ctxTarget.Entry(tTarget).CurrentValues, null);

                folders fs0 = tTarget.folders.ElementAt(0);
                ModelDump.DisplayDBPropertyValues("fs0", ctxTarget.Entry(fs0).CurrentValues, null);
                folder f0 = fs0.folder.ElementAt(0);
                ModelDump.DisplayDBPropertyValues("f0", ctxTarget.Entry(f0).CurrentValues, null);

                mTarget = ModelFinder.FindModel(ctxTarget, tTarget, mTargetIdentifier, null);
                ModelDump.DisplayDBPropertyValues("mTarget", ctxTarget.Entry(mTarget).CurrentValues, null);

                ModelMigrator.MigrateModel(ctxTarget, tTarget, f0, 
                                                mSource, PCOOperation.merge, PCOOperation.merge, true);
            }

            Console.WriteLine("Press enter to exist...");
            Console.ReadLine();
        }
    }
}
