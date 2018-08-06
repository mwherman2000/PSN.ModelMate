using Parallelspace.Content.Objects;
using PSN.ModelMate.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;

namespace PSN.ModelMate.Lib
{
    static public class ModelMigrator
    {
        static public void PreloadAllTables(ModelMateEFModel9Context ctxTarget)
        {
            const bool debugTrace = false;

            //ctxTarget.Database.Log = Console.Write;

            ctxTarget.model.Include(o => o.elements).Include(o => o.name).Include(o => o.propertydefs).Include(o => o.properties).Include(o => o.relationships).Include(o => o.views).Include(o => o.organization).Include(o => o.documentation).Include(o => o.metadata).Load();
            ctxTarget.model.Include(o => o.management).Include(o => o.performance).Include(o => o.processinghistory).Include(o => o.usage).Load();
            if (debugTrace) Console.WriteLine("model.Local.Count: " + ctxTarget.model.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("propertydefs.Local.Count: " + ctxTarget.propertydefs.Local.Count().ToString());

            ctxTarget.propertydefs.Include(o => o.propertydef).Load();
            ctxTarget.propertydef.Include(o => o.management).Include(o => o.performance).Include(o => o.processinghistory).Include(o => o.usage).Load();
            if (debugTrace) Console.WriteLine("propertydefs.Local.Count: " + ctxTarget.propertydefs.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("propertydef.Local.Count: " + ctxTarget.propertydef.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("propertydefs.Count: " + ctxTarget.propertydefs.Count().ToString());
            if (debugTrace) Console.WriteLine("propertydef.Count: " + ctxTarget.propertydef.Count().ToString());

            ctxTarget.metadata.Include(o => o.management).Include(o => o.performance).Include(o => o.processinghistory).Include(o => o.usage).Include(o => o.property).Load();
            if (debugTrace) Console.WriteLine("metadata.Local.Count: " + ctxTarget.metadata.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("metadata.Count: " + ctxTarget.metadata.Count().ToString());

            ctxTarget.properties.Include(o => o.property).Load();
            ctxTarget.property.Include(o => o.value).Include(o => o.metadata).Load();
            if (debugTrace) Console.WriteLine("properties.Local.Count: " + ctxTarget.properties.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("property.Local.Count: " + ctxTarget.property.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("properties.Count: " + ctxTarget.properties.Count().ToString());
            if (debugTrace) Console.WriteLine("property.Count: " + ctxTarget.property.Count().ToString());

            ctxTarget.elements.Include(o => o.element).Load();
            ctxTarget.element.Include(o => o.label).Include(o => o.properties).Load();
            ctxTarget.element.Include(o => o.documentation).Include(o => o.management).Include(o => o.performance).Include(o => o.processinghistory).Include(o => o.usage).Load();
            if (debugTrace) Console.WriteLine("elements.Local.Count: " + ctxTarget.elements.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("element.Local.Count: " + ctxTarget.element.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("elements.Count: " + ctxTarget.elements.Count().ToString());
            if (debugTrace) Console.WriteLine("element.Count: " + ctxTarget.element.Count().ToString());

            ctxTarget.relationships.Include(o => o.relationship).Load();
            ctxTarget.relationship.Include(o => o.label).Include(o => o.properties).Include(o => o.documentation).Include(o => o.management).Include(o => o.performance).Include(o => o.processinghistory).Include(o => o.usage).Load();
            if (debugTrace) Console.WriteLine("relationships.Local.Count: " + ctxTarget.relationships.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("relationship.Local.Count: " + ctxTarget.relationship.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("relationships.Count: " + ctxTarget.relationships.Count().ToString());
            if (debugTrace) Console.WriteLine("relationship.Count: " + ctxTarget.relationship.Count().ToString());

            ctxTarget.views.Include(o => o.view).Load();
            ctxTarget.view.Include(o => o.label).Include(o => o.properties).Include(o => o.documentation).Include(o => o.management).Include(o => o.performance).Include(o => o.processinghistory).Include(o => o.usage).Load();
            if (debugTrace) Console.WriteLine("views.Local.Count: " + ctxTarget.views.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("view.Local.Count: " + ctxTarget.view.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("views.Count: " + ctxTarget.views.Count().ToString());
            if (debugTrace) Console.WriteLine("view.Count: " + ctxTarget.view.Count().ToString());

            ctxTarget.organization.Include(o => o.item).Load();
            ctxTarget.item.Include(o => o.label).Include(o => o.item1).Include(o => o.documentation).Load();
            if (debugTrace) Console.WriteLine("organization.Local.Count: " + ctxTarget.organization.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("item.Local.Count: " + ctxTarget.item.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("organization.Count: " + ctxTarget.organization.Count().ToString());
            if (debugTrace) Console.WriteLine("item.Count: " + ctxTarget.item.Count().ToString());

            ctxTarget.usage.Include(o => o.timesnap).Load();
            ctxTarget.performance.Include(o => o.timesnap).Load();
            ctxTarget.processinghistory.Include(o => o.timesnap).Load();
            ctxTarget.management.Include(o => o.timesnap).Load();
            ctxTarget.timesnap.Include(o => o.label).Include(o => o.documentation).Include(o => o.properties).Load();
            if (debugTrace) Console.WriteLine("usage.Local.Count: " + ctxTarget.usage.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("performance.Local.Count: " + ctxTarget.performance.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("processinghistory.Local.Count: " + ctxTarget.processinghistory.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("management.Local.Count: " + ctxTarget.management.Local.Count().ToString());
            if (debugTrace) Console.WriteLine("timesnap.Local.Count: " + ctxTarget.timesnap.Local.Count().ToString());
        }

        static public model MigrateModel(ModelMateEFModel9Context ctxTarget, 
                                        tenant tTarget, folder fTarget, model mIn, PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn, 
                                        bool recurseChildObjectsIn)
        {
            model m = null;

            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (tTarget == null) throw new ArgumentNullException("tTarget");
            if (fTarget == null) throw new ArgumentNullException("fTarget");
            if (mIn == null) throw new ArgumentNullException("mIn");

            model mTarget = null;
            if (String.IsNullOrEmpty(mIn.identifier))
            {
                mTarget = ModelFinder.FindModel(ctxTarget, tTarget, null, mIn.name.ElementAt(0).name_text);
            }
            else
            {
                mTarget = ModelFinder.FindModel(ctxTarget, tTarget, mIn.identifier, null);
            }

            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        // Merge local properties (update object fields), related properties (update related property sets), and substructure (recursively update source child objects) 
                        if (mTarget == null)
                        {
                            ctxTarget.model.Add(mIn);
                            fTarget.models.ElementAt(0).model.Add(mIn); // CONCERN
                            m = mIn;
                        }
                        else
                        {
                            UpdateModelObject(ctxTarget, mTarget, mIn);
                            m = mTarget;
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            switch (opRelatedPropertiesIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.delete:
                case PCOOperation.remove:
                    {
                        // do nothing - the object was deleted
                        break;
                    }
                case PCOOperation.add:
                case PCOOperation.create:
                case PCOOperation.merge:
                case PCOOperation.replace:
                case PCOOperation.update:
                    {
                        // Merge local properties (update object fields), related properties (update related property sets), and substructure (recursively update source child objects) 
                        if (mTarget == null)
                        {
                            // do nothing - newly added object
                        }
                        else
                        {
                            UpdateModelRelatedProperties(ctxTarget, mTarget, mIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn);
                        }
                        break;
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();

            if (recurseChildObjectsIn)
            {
                int nElements = 0;
                if (mTarget.elements.Count() == 0) mTarget.elements.Add(ModelFactory.NewElements());
                var ecctx = ctxTarget.element;
                bool emptyDbSet = ecctx.Count() == 0;
                var ecTarget = mTarget.elements.ElementAt(0).element;
                if (emptyDbSet)
                {
                    ecctx.AddRange(mIn.elements.ElementAt(0).element);

                    foreach (elements esIn in mIn.elements)
                    {
                        var ec = esIn.element;
                        int tElements = ec.Count();
                        DateTime dtStart = DateTime.Now;
                        foreach (element eIn in ec)
                        {
                            nElements++;
                            if (nElements % 100 == 0)
                                Console.WriteLine("Element1 " + nElements.ToString() + " " + tElements.ToString() + " " +
                                                Util.PredictCompletion(dtStart, tElements, nElements));
                            ecTarget.Add(eIn);
                        }
                    }
                }
                else
                {
                    foreach (elements esIn in mIn.elements)
                    {
                        element eNew = null;
                        var ec = esIn.element;
                        int tElements = ec.Count();
                        DateTime dtStart = DateTime.Now;
                        foreach (element eIn in ec)
                        {
                            nElements++;
                            if (nElements % 100 == 0)
                                Console.WriteLine("Element2 " + nElements.ToString() + " " + tElements.ToString() + " " +
                                                Util.PredictCompletion(dtStart, tElements, nElements));

                            eNew = MigrateElement(ctxTarget, tTarget, mIn, eIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn, emptyDbSet);
                            if (eNew != null)
                            {
                                //esIn.element.Add(eNew);
                                ecTarget.Add(eNew);
                                ecctx.Add(eNew);
                            }
                        }
                    }
                }

                ctxTarget.SaveChanges();

                int nRelationships = 0;
                if (mTarget.relationships.Count() == 0) mTarget.relationships.Add(ModelFactory.NewRelationships());
                var rcctx = ctxTarget.relationship;
                emptyDbSet = rcctx.Count() == 0;
                var rcTarget = mTarget.relationships.ElementAt(0).relationship;
                if (emptyDbSet)
                {
                    rcctx.AddRange(mIn.relationships.ElementAt(0).relationship);

                    foreach (relationships rsIn in mIn.relationships)
                    {
                        var rc = rsIn.relationship;
                        int tRelationships = rc.Count();
                        DateTime dtStart = DateTime.Now;
                        foreach (relationship rIn in rc)
                        {
                            nRelationships++;
                            if (nElements % 100 == 0)
                                Console.WriteLine("Relationship1 " + nRelationships.ToString() + " " + tRelationships.ToString() + " " +
                                                Util.PredictCompletion(dtStart, tRelationships, nRelationships));
                            rcTarget.Add(rIn);
                        }
                    }
                }
                else
                {
                    foreach (relationships rsIn in mIn.relationships)
                    {
                        relationship rNew = null;
                        var rc = rsIn.relationship;
                        int tRelatonships = rc.Count();
                        DateTime dtStart = DateTime.Now;
                        foreach (relationship rIn in rc)
                        {
                            nRelationships++;
                            if (nRelationships % 100 == 0)
                                Console.WriteLine("Relationship2 " + nRelationships.ToString() + " " + tRelatonships.ToString() + " " +
                                                Util.PredictCompletion(dtStart, tRelatonships, nRelationships));

                            rNew = MigrateRelationship(ctxTarget, tTarget, mIn, rIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn, emptyDbSet);
                            if (rNew != null)
                            {
                                //rsIn.relationship.Add(rNew);
                                rcTarget.Add(rNew); // CONCERN
                                rcctx.Add(rNew);
                            }
                        }
                    }
                }
                ctxTarget.SaveChanges();

                if (mTarget.views.Count() == 0) mTarget.views.Add(ModelFactory.NewViews());
                var vcctx = ctxTarget.view;
                emptyDbSet = vcctx.Count() == 0;
                var vcTarget = mTarget.views.ElementAt(0).view;
                foreach (views vsIn in mIn.views)
                {
                    view vNew = null;
                    var vc = vsIn.view;
                    foreach (view vIn in vc)
                    {
                        vNew = MigrateView(ctxTarget, tTarget, mIn, vIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn, emptyDbSet);
                        if (vNew != null)
                        {
                            //vsIn.view.Add(vNew);
                            vcTarget.Add(vIn); // CONCERN
                            vcctx.Add(vNew);
                        }
                    }
                }
                ctxTarget.SaveChanges();

                if (mTarget.organization.Count() == 0) mTarget.organization.Add(ModelFactory.NewOrganization());
                var icctx = ctxTarget.item;
                emptyDbSet = icctx.Count() == 0;
                var icTarget = mTarget.organization.ElementAt(0).item;
                foreach (organization oIn in mIn.organization)
                {
                    item iNew = null;
                    var ic = oIn.item;
                    foreach (item iIn in ic)
                    {
                        iNew = MigrateItem(ctxTarget, tTarget, mIn, iIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn, emptyDbSet);
                        if (iNew != null)
                        {
                            //oIn.item.Add(iNew);
                            icTarget.Add(iIn); // CONCERN about nested items
                            icctx.Add(iNew);
                        }
                    }
                }
                ctxTarget.SaveChanges();
            }

            ctxTarget.SaveChanges();

            return m;
        }

        static public element MigrateElement(ModelMateEFModel9Context ctxTarget,
                                            tenant tTarget, model mTarget, element eIn, 
                                            PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn, 
                                            bool recurseChildObjectsIn, bool emptyDbSet)
        {
            element eNew = null;

            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (tTarget == null) throw new ArgumentNullException("tTarget");
            if (mTarget == null) throw new ArgumentNullException("mTarget");
            if (eIn == null) throw new ArgumentNullException("eIn");

            element eTarget = null;
            if (!emptyDbSet)
            {
                if (String.IsNullOrEmpty(eIn.identifier))
                {
                    eTarget = ModelFinder.FindElement(ctxTarget, tTarget, mTarget, null, eIn.label.ElementAt(0).label_text);
                }
                else
                {
                    eTarget = ModelFinder.FindElement(ctxTarget, tTarget, mTarget, eIn.identifier, null);
                }
            }

            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (eTarget == null)
                        {
                            eNew = eIn;
                        }
                        else
                        {
                            UpdateElementObject(ctxTarget, eTarget, eIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn);
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            //ctxTarget.SaveChanges();

            switch (opRelatedPropertiesIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (eTarget == null)
                        {
                            // do nothing - newly added object
                        }
                        else
                        {
                            UpdateElementRelatedProperties(ctxTarget, eTarget, eIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn);
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
            }

            //ctxTarget.SaveChanges();

            if (recurseChildObjectsIn)
            {
                // nothing to do for an element

                //throw new NotImplementedException("recurseChildObjectsIn");
            }

            //ctxTarget.SaveChanges();

            return eNew;
        }

        static public relationship MigrateRelationship(ModelMateEFModel9Context ctxTarget,
                                                        tenant tTarget, model mTarget, relationship rIn, 
                                                        PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn, 
                                                        bool recurseChildObjectsIn, bool emptyDbSet)
        {
            relationship rNew = null;

            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (tTarget == null) throw new ArgumentNullException("tTarget");
            if (mTarget == null) throw new ArgumentNullException("mTarget");
            if (rIn == null) throw new ArgumentNullException("rIn");

            relationship rTarget = null;
            if (!emptyDbSet)
            {
                if (String.IsNullOrEmpty(rIn.identifier))
                {
                    rTarget = ModelFinder.FindRelationship(ctxTarget, tTarget, mTarget, null, rIn.label.ElementAt(0).label_text);
                }
                else
                {
                    rTarget = ModelFinder.FindRelationship(ctxTarget, tTarget, mTarget, rIn.identifier, null);
                }
            }

            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (rTarget == null)
                        {
                            rNew = rIn;
                        }
                        else
                        {
                            UpdateRelationshipObject(ctxTarget, rTarget, rIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn);
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            //ctxTarget.SaveChanges();

            switch (opRelatedPropertiesIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (rTarget == null)
                        {
                            // do nothing - newly added object
                        }
                        else
                        {
                            UpdateRelationshipRelatedProperties(ctxTarget, rTarget, rIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn);
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
            }

            //ctxTarget.SaveChanges();

            if (recurseChildObjectsIn)
            {
                // nothing to do for an element

                //throw new NotImplementedException("recurseChildObjectsIn");
            }

            //ctxTarget.SaveChanges();

            return rNew;
        }

        static public view MigrateView(ModelMateEFModel9Context ctxTarget,
                                        tenant tTarget, model mTarget, view vIn, 
                                        PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn, 
                                        bool recurseChildObjectsIn, bool emptyDbSet)
        {
            view vNew = null;

            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (tTarget == null) throw new ArgumentNullException("tTarget");
            if (mTarget == null) throw new ArgumentNullException("mTarget");
            if (vIn == null) throw new ArgumentNullException("vIn");

            view vTarget = null;
            if (!emptyDbSet)
            {
                if (String.IsNullOrEmpty(vIn.identifier))
                {
                    vTarget = ModelFinder.FindView(ctxTarget, tTarget, mTarget, null, vIn.label.ElementAt(0).label_text);
                }
                else
                {
                    vTarget = ModelFinder.FindView(ctxTarget, tTarget, mTarget, vIn.identifier, null);
                }
            }

            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (vTarget == null)
                        {
                            vNew = vIn;
                        }
                        else
                        {
                            UpdateViewObject(ctxTarget, vTarget, vIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn);
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            //ctxTarget.SaveChanges();

            switch (opRelatedPropertiesIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (vTarget == null)
                        {
                            // do nothing - newly added object
                        }
                        else
                        {
                            UpdateViewRelatedProperties(ctxTarget, vTarget, vIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn);
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
            }

            //ctxTarget.SaveChanges();

            if (recurseChildObjectsIn)
            {
                // nothing to do for an element

                //throw new NotImplementedException("recurseChildObjectsIn");
            }

            //ctxTarget.SaveChanges();

            return vNew;
        }

        static public item MigrateItem(ModelMateEFModel9Context ctxTarget, 
                                        tenant tTarget, model mTarget, item iIn, 
                                        PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn, 
                                        bool recurseChildObjectsIn, bool emptyDbSet)
        {
            item iNew = null;

            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (tTarget == null) throw new ArgumentNullException("tTarget");
            if (mTarget == null) throw new ArgumentNullException("mTarget");
            if (iIn == null) throw new ArgumentNullException("iIn");

            item iTarget = null;
            if (!emptyDbSet)
            {
                if (String.IsNullOrEmpty(iIn.identifier))
                {
                    iTarget = ModelFinder.FindItem(ctxTarget, tTarget, mTarget, null, iIn.label.ElementAt(0).label_text);
                }
                else
                {
                    iTarget = ModelFinder.FindItem(ctxTarget, tTarget, mTarget, iIn.identifier, null);
                }
            }

            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (iTarget == null)
                        {
                            iNew = iIn;
                        }
                        else
                        {
                            UpdateItemObject(ctxTarget, iTarget, iIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn);
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            //ctxTarget.SaveChanges();

            switch (opRelatedPropertiesIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (iTarget == null)
                        {
                            // do nothing - newly added object
                        }
                        else
                        {
                            UpdateItemRelatedProperties(ctxTarget, iTarget, iIn, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn);
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opRelatedPropertiesIn " + opRelatedPropertiesIn.ToString());
                    }
            }

            //ctxTarget.SaveChanges();

            if (recurseChildObjectsIn)
            {
                foreach (item iChild in iIn.item1)
                {
                    MigrateItem(ctxTarget, tTarget, mTarget, iChild, opObjectIn, opRelatedPropertiesIn, recurseChildObjectsIn, emptyDbSet); // CCONCERN about nested items
                }
            }

            //ctxTarget.SaveChanges();

            return iNew;
        }

        static public void UpdateModelObject(ModelMateEFModel9Context ctxTarget, model mTarget, model mIn)
        {
            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (mTarget == null) throw new ArgumentNullException("mTarget");
            if (mIn == null) throw new ArgumentNullException("mIn");

            //mTarget.identifier = mIn.identifier;
            //mTarget.models_Id = mIn.models_Id;
            //mTarget.model_Id = mIn.model_Id;
            mTarget.operation = mIn.operation;
            mTarget.version = mIn.version;

            ctxTarget.SaveChanges();
        }

        static public void UpdateModelRelatedProperties(ModelMateEFModel9Context ctxTarget, model mTarget, model mIn,
                                                        PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn,
                                                        bool recurseChildObjectsIn)
        {
            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (mTarget == null) throw new ArgumentNullException("mTarget");
            if (mIn == null) throw new ArgumentNullException("mIn");

            MigrateName(ctxTarget, mTarget.name, mIn.name, opObjectIn); // lang, name_text
            MigrateDocumentation(ctxTarget, mTarget.documentation, mIn.documentation, opObjectIn); // lang, documentation_text
 
            MigratePropertyDefs(ctxTarget, mTarget.propertydefs, mIn.propertydefs, opObjectIn); // new HashSet<property>()

            MigrateProperties(ctxTarget, mTarget.properties, mIn.properties, opObjectIn); // new HashSet<property>()

            MigrateMetadata(ctxTarget, mTarget.metadata, mIn.metadata, opObjectIn); // new HashSet<property>(), processinghistory, usage, performance, management

            MigrateProcessingHistory(ctxTarget, mTarget.processinghistory, mIn.processinghistory, opObjectIn); // new HashSet<timesnap>()
            MigrateUsage(ctxTarget, mTarget.usage, mIn.usage, opObjectIn); // new HashSet<timesnap>()
            MigratePerformance(ctxTarget, mTarget.performance, mIn.performance, opObjectIn); // new HashSet<timesnap>()
            MigrateManagement(ctxTarget, mTarget.management, mIn.management, opObjectIn); // new HashSet<timesnap>()

            ctxTarget.SaveChanges();
        }
        static public void UpdateElementRelatedProperties(ModelMateEFModel9Context ctxTarget, element eTarget, element eIn,
                                                PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn,
                                                bool recurseChildObjectsIn)
        {
            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (eTarget == null) throw new ArgumentNullException("eTarget");
            if (eIn == null) throw new ArgumentNullException("eIn");

            MigrateLabel(ctxTarget, eTarget.label, eIn.label, opObjectIn); // lang, name_text
            MigrateDocumentation(ctxTarget, eTarget.documentation, eIn.documentation, opObjectIn); // lang, documentation_text

            //MigratePropertyDefs(ctxTarget, eTarget.propertydefs, eIn.propertydefs, opObjectIn); // new HashSet<property>()

            MigrateProperties(ctxTarget, eTarget.properties, eIn.properties, opObjectIn); // new HashSet<property>()

            //MigrateMetadata(ctxTarget, eTarget.metadata, eIn.metadata, opObjectIn); // new HashSet<property>(), processinghistory, usage, performance, management

            MigrateProcessingHistory(ctxTarget, eTarget.processinghistory, eIn.processinghistory, opObjectIn); // new HashSet<timesnap>()
            MigrateUsage(ctxTarget, eTarget.usage, eIn.usage, opObjectIn); // new HashSet<timesnap>()
            MigratePerformance(ctxTarget, eTarget.performance, eIn.performance, opObjectIn); // new HashSet<timesnap>()
            MigrateManagement(ctxTarget, eTarget.management, eIn.management, opObjectIn); // new HashSet<timesnap>()

            ctxTarget.SaveChanges();
        }
        static public void UpdateRelationshipRelatedProperties(ModelMateEFModel9Context ctxTarget, relationship rTarget, relationship rIn,
                                        PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn,
                                        bool recurseChildObjectsIn)
        {
            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (rTarget == null) throw new ArgumentNullException("mTarget");
            if (rIn == null) throw new ArgumentNullException("rIn");

            MigrateLabel(ctxTarget, rTarget.label, rIn.label, opObjectIn); // lang, name_text
            MigrateDocumentation(ctxTarget, rTarget.documentation, rIn.documentation, opObjectIn); // lang, documentation_text

            //MigratePropertyDefs(ctxTarget, rTarget.propertydefs, rIn.propertydefs, opObjectIn); // new HashSet<property>()

            MigrateProperties(ctxTarget, rTarget.properties, rIn.properties, opObjectIn); // new HashSet<property>()

            //MigrateMetadata(ctxTarget, rTarget.metadata, rIn.metadata, opObjectIn); // new HashSet<property>(), processinghistory, usage, performance, management

            MigrateProcessingHistory(ctxTarget, rTarget.processinghistory, rIn.processinghistory, opObjectIn); // new HashSet<timesnap>()
            MigrateUsage(ctxTarget, rTarget.usage, rIn.usage, opObjectIn); // new HashSet<timesnap>()
            MigratePerformance(ctxTarget, rTarget.performance, rIn.performance, opObjectIn); // new HashSet<timesnap>()
            MigrateManagement(ctxTarget, rTarget.management, rIn.management, opObjectIn); // new HashSet<timesnap>()

            ctxTarget.SaveChanges();
        }
        static public void UpdateViewRelatedProperties(ModelMateEFModel9Context ctxTarget, view vTarget, view vIn,
                                PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn,
                                bool recurseChildObjectsIn)
        {
            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (vTarget == null) throw new ArgumentNullException("mTarget");
            if (vIn == null) throw new ArgumentNullException("vIn");

            MigrateLabel(ctxTarget, vTarget.label, vIn.label, opObjectIn); // lang, name_text
            MigrateDocumentation(ctxTarget, vTarget.documentation, vIn.documentation, opObjectIn); // lang, documentation_text

            //MigratePropertyDefs(ctxTarget, vTarget.propertydefs, vIn.propertydefs, opObjectIn); // new HashSet<property>()

            MigrateProperties(ctxTarget, vTarget.properties, vIn.properties, opObjectIn); // new HashSet<property>()

            //MigrateMetadata(ctxTarget, vTarget.metadata, vIn.metadata, opObjectIn); // new HashSet<property>(), processinghistory, usage, performance, management

            MigrateProcessingHistory(ctxTarget, vTarget.processinghistory, vIn.processinghistory, opObjectIn); // new HashSet<timesnap>()
            MigrateUsage(ctxTarget, vTarget.usage, vIn.usage, opObjectIn); // new HashSet<timesnap>()
            MigratePerformance(ctxTarget, vTarget.performance, vIn.performance, opObjectIn); // new HashSet<timesnap>()
            MigrateManagement(ctxTarget, vTarget.management, vIn.management, opObjectIn); // new HashSet<timesnap>()

            ctxTarget.SaveChanges();
        }
        static public void UpdateItemRelatedProperties(ModelMateEFModel9Context ctxTarget, item iTarget, item iIn,
                                PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn,
                                bool recurseChildObjectsIn)
        {
            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (iTarget == null) throw new ArgumentNullException("mTarget");
            if (iIn == null) throw new ArgumentNullException("iIn");

            MigrateLabel(ctxTarget, iTarget.label, iIn.label, opObjectIn); // lang, name_text
            MigrateDocumentation(ctxTarget, iTarget.documentation, iIn.documentation, opObjectIn); // lang, documentation_text

            //MigratePropertyDefs(ctxTarget, iTarget.propertydefs, iIn.propertydefs, opObjectIn); // new HashSet<property>()

            //MigrateProperties(ctxTarget, iTarget.properties, iIn.properties, opObjectIn); // new HashSet<property>()

            //MigrateMetadata(ctxTarget, iTarget.metadata, iIn.metadata, opObjectIn); // new HashSet<property>(), processinghistory, usage, performance, management

            //MigrateProcessingHistory(ctxTarget, iTarget.processinghistory, iIn.processinghistory, opObjectIn); // new HashSet<timesnap>()
            //MigrateUsage(ctxTarget, iTarget.usage, iIn.usage, opObjectIn); // new HashSet<timesnap>()
            //MigratePerformance(ctxTarget, iTarget.performance, iIn.performance, opObjectIn); // new HashSet<timesnap>()
            //MigrateManagement(ctxTarget, iTarget.management, iIn.management, opObjectIn); // new HashSet<timesnap>()

            ctxTarget.SaveChanges();
        }

        static public void MigrateName(ModelMateEFModel9Context ctxTarget, 
                                        ICollection<name> ncTarget, ICollection<name> ncIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (ncTarget.Count == 0)
                        {
                            var ncctx = ctxTarget.name;
                            foreach (name n in ncIn)
                            {
                                name iNew = ModelFactory.NewName(n.name_text, n.lang);
                                ncctx.Add(iNew);
                                ncTarget.Add(iNew);
                            }
                        }
                        else
                        {
                            var ncctx = ctxTarget.name;
                            foreach (name n in ncIn)
                            {
                                var ncMatching = (from match in ncTarget
                                                where (match.lang == n.lang && match.name_text == n.name_text)
                                                select match);
                                switch(ncMatching.Count<name>())
                                {
                                    case 0: // might be same name but different language
                                        {
                                            name iNew = ModelFactory.NewName(n.name_text, n.lang);
                                            ncctx.Add(iNew);
                                            ncTarget.Add(iNew);
                                            break;
                                        }
                                    case 1:
                                        {
                                            // exact match - ignore it - do nothing
                                            break;
                                        }
                                    default:
                                        {
                                            throw new ArgumentOutOfRangeException("name", "More than 1 match is illegal");
                                        }
                                }
                            }
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }
            ctxTarget.SaveChanges();
        }
        static public void MigrateLabel(ModelMateEFModel9Context ctxTarget,
                                ICollection<label> lcTarget, ICollection<label> lcIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (lcTarget.Count == 0)
                        {
                            var lcctx = ctxTarget.label;
                            foreach (label l in lcIn)
                            {
                                label lNew = ModelFactory.NewLabel(l.label_text, l.lang);
                                lcctx.Add(lNew);
                                lcTarget.Add(lNew);
                            }
                        }
                        else
                        {
                            var lcctx = ctxTarget.label;
                            foreach (label l in lcIn)
                            {
                                var lcMatching = (from match in lcTarget
                                                where (match.lang == l.lang && match.label_text == l.label_text)
                                                select match);
                                switch (lcMatching.Count<label>())
                                {
                                    case 0: // might be same name but different language
                                        {
                                            label lNew = ModelFactory.NewLabel(l.label_text, l.lang);
                                            lcctx.Add(lNew);
                                            lcTarget.Add(lNew);
                                            break;
                                        }
                                    case 1:
                                        {
                                            // exact match - ignore it - do nothing
                                            break;
                                        }
                                    default:
                                        {
                                            throw new ArgumentOutOfRangeException("label", "More than 1 match is illegal");
                                        }
                                }
                            }
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }
            ctxTarget.SaveChanges();
        }
        static public void MigrateDocumentation(ModelMateEFModel9Context ctxTarget, 
                                                ICollection<documentation> dcTarget, ICollection<documentation> dcIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (dcTarget.Count == 0)
                        {
                            var dcctx = ctxTarget.documentation;
                            foreach (documentation d in dcIn)
                            {
                                documentation dNew = ModelFactory.NewDocumentation(d.documentation_text, d.lang);
                                dcctx.Add(dNew);
                                dcTarget.Add(dNew);
                            }
                        }
                        else
                        {
                            var dcctx = ctxTarget.documentation;
                            foreach (documentation d in dcIn)
                            {
                                var dcMatching = (from match in dcTarget
                                                where (match.lang == d.lang && match.documentation_text == d.documentation_text)
                                                select match);
                                switch (dcMatching.Count<documentation>())
                                {
                                    case 0:
                                        {
                                            documentation dNew = ModelFactory.NewDocumentation(d.documentation_text, d.lang);
                                            dcctx.Add(dNew);
                                            dcTarget.Add(dNew);
                                            break;
                                        }
                                    case 1:
                                        {
                                            // exact match - ignore it - do nothing
                                            break;
                                        }
                                    default:
                                        {
                                            throw new ArgumentOutOfRangeException("name", "More than 1 match is illegal");
                                        }
                                }
                            }
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();
        }
        static public void MigratePropertyDefs(ModelMateEFModel9Context ctxTarget, 
                                            ICollection<propertydefs> pdscTarget, ICollection<propertydefs> pdscIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        switch (pdscTarget.Count)
                        {
                            case 0:
                                {
                                    var pdscctx = ctxTarget.propertydefs;
                                    foreach (propertydefs pds in pdscIn)
                                    {
                                        pdscctx.Add(pds);
                                        pdscTarget.Add(pds);
                                    }
                                    break; // pdsTarget.Count case 0
                                }
                            case 1:
                                {
                                    if (pdscIn.Count > 1) throw new ArgumentOutOfRangeException("pdscIn", "Should have 0 or 1 children");

                                    if (pdscIn.Count == 1) MigratePropertyDef(ctxTarget, 
                                                        pdscTarget.ElementAt(0).propertydef, 
                                                        pdscIn.ElementAt(0).propertydef,
                                                        opObjectIn);
                                    break; // pdsTarget.Count case 1
                                }
                            default:
                                {
                                    throw new ArgumentOutOfRangeException("propertydefs", "More than 1 is illegal");
                                }
                        }
                        break; // case PCOOperation.merge
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();
        }
        private static void MigratePropertyDef(ModelMateEFModel9Context ctxTarget, 
                                                ICollection<propertydef> pdcTarget, ICollection<propertydef> pdcIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (pdcTarget.Count == 0)
                        {
                            var pdcctx = ctxTarget.propertydef;
                            foreach (propertydef pd in pdcIn)
                            {
                                pdcctx.Add(pd);
                                pdcTarget.Add(pd);
                            }
                        }
                        else
                        {
                            var pdcctx = ctxTarget.propertydef;
                            foreach (propertydef pd in pdcIn)
                            {
                                var pdcMatching = (from match in pdcTarget
                                                where (match.name == pd.name)
                                                select match);
                                switch (pdcMatching.Count<propertydef>())
                                {
                                    case 0:
                                        {
                                            pdcctx.Add(pd);
                                            pdcTarget.Add(pd);
                                            // management, performance, processinghistory and usage will be pulled in automatically if they exist
                                            break;
                                        }
                                    case 1:
                                        {
                                            var pdcMatching0 = pdcMatching.ElementAt(0);
                                            if (pdcMatching0.type == pd.type)
                                            {
                                                MigrateManagement(ctxTarget, pdcMatching0.management, pd.management, opObjectIn);
                                                MigratePerformance(ctxTarget, pdcMatching0.performance, pd.performance, opObjectIn);
                                                MigrateProcessingHistory(ctxTarget, pdcMatching0.processinghistory, pd.processinghistory, opObjectIn);
                                                MigrateUsage(ctxTarget, pdcMatching0.usage, pd.usage, opObjectIn);
                                            }
                                            else
                                            {
                                                throw new ArgumentException(pdcMatching0.type + " and " + pd.type, "Cannot change the type of an existing properydef");
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            throw new ArgumentOutOfRangeException("propertydef", "More than 1 match is illegal");
                                        }
                                }
                            }
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();
        }
        private static void MigrateProperties(ModelMateEFModel9Context ctxTarget, 
                                                ICollection<properties> pscTarget, ICollection<properties> pscIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        switch (pscTarget.Count)
                        {
                            case 0:
                                {
                                    var pscctx = ctxTarget.properties;
                                    foreach (properties ps in pscIn)
                                    {
                                        pscctx.Add(ps);
                                        pscTarget.Add(ps);
                                    }
                                    break; // psTarget.Count case 0
                                }
                            case 1:
                                {
                                    if (pscIn.Count != 1) throw new ArgumentOutOfRangeException("pscIn", "Should have 0 or 1 children");

                                    if (pscIn.Count == 1) MigrateProperty(ctxTarget,
                                                        pscTarget.ElementAt(0).property,
                                                        pscIn.ElementAt(0).property,
                                                        opObjectIn);
                                    break; // psTarget.Count case 1
                                }
                            default:
                                {
                                    throw new ArgumentOutOfRangeException("propertydefs", "More than 1 is illegal");
                                }
                        }
                        break; // case PCOOperation.merge
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();
        }

        private static void MigrateProperty(ModelMateEFModel9Context ctxTarget, 
                                            ICollection<property> pcTarget, ICollection<property> pcIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        if (pcTarget.Count == 0)
                        {
                            var pcctx = ctxTarget.property;
                            foreach (property p in pcIn)
                            {
                                pcctx.Add(p);
                                pcTarget.Add(p);
                            }
                        }
                        else // else one or more
                        {
                            var pcctx = ctxTarget.property;
                            foreach (property p in pcIn)
                            {
                                var pMatching = (from match in pcTarget
                                                where (match.identifierref == p.identifierref) // CONCERN
                                                select match);
                                switch (pMatching.Count<property>())
                                {
                                    case 0:
                                        {
                                            pcctx.Add(p);
                                            pcTarget.Add(p);
                                            break;
                                        }
                                    case 1:
                                        {
                                            // update existing values
                                            var vcctx = ctxTarget.value;
                                            foreach (value vIn in p.value)
                                            {
                                                var vcTarget = pMatching.ElementAt(0).value;
                                                var vsMatching = (from match in vcTarget
                                                                  where (match.lang == vIn.lang)
                                                                 select match);
                                                switch (vsMatching.Count<value>())
                                                {
                                                    case 0:
                                                        {
                                                            vcctx.Add(vIn);
                                                            vcTarget.Add(vIn);
                                                            break;
                                                        }
                                                    case 1:
                                                        {
                                                            var vsMatching0 = vsMatching.ElementAt(0);
                                                            vsMatching0.lang = vIn.lang;
                                                            vsMatching0.operation = vIn.operation;
                                                            vsMatching0.value_text = vIn.value_text;
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            throw new ArgumentOutOfRangeException(vsMatching.ElementAt(0).value_Id + " and " + vsMatching.ElementAt(0).lang, "Value should have 1 value per language");
                                                        }
                                                }
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            throw new ArgumentOutOfRangeException("property", "More than 1 match is illegal");
                                        }
                                }
                            }
                        }
                        break;
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();
        }

        private static void MigrateMetadata(ModelMateEFModel9Context ctxTarget, 
                                            ICollection<metadata> mdcTarget, ICollection<metadata> mdcIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        switch (mdcTarget.Count)
                        {
                            case 0: // mdsTarget.Count case 0
                                {
                                    var mcctx = ctxTarget.metadata;
                                    foreach (metadata md in mdcIn)
                                    {
                                        mcctx.Add(md);
                                        mdcTarget.Add(md);
                                    }
                                    break;
                                }
                            case 1: // mdsTarget.Count case 1
                                {
                                    if (mdcIn.Count > 1) throw new ArgumentOutOfRangeException("mdcIn", "Should have 0 or 1 children");

                                    var mcctx = ctxTarget.metadata;
                                    foreach (metadata md in mdcIn)
                                    {
                                        var mdsMatching = (from match in mdcTarget
                                                        where (match.schema == md.schema && match.schemaversion == md.schemaversion)
                                                        select match);
                                        switch (mdsMatching.Count<metadata>())
                                        {
                                            case 0: // CONCERN
                                                {
                                                    mcctx.Add(md);
                                                    mdcTarget.Add(md);
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    var mdsMatching0 = mdsMatching.ElementAt(0);

                                                    mdsMatching0.operation = md.operation;

                                                    MigrateManagement(ctxTarget, mdsMatching0.management, md.management, opObjectIn);
                                                    MigratePerformance(ctxTarget, mdsMatching0.performance, md.performance, opObjectIn);
                                                    MigrateProcessingHistory(ctxTarget, mdsMatching0.processinghistory, md.processinghistory, opObjectIn);
                                                    MigrateUsage(ctxTarget, mdsMatching0.usage, md.usage, opObjectIn);
                                                    break;
                                                }
                                            default:
                                                {
                                                    throw new ArgumentOutOfRangeException("metadata", "More than 1 match is illegal");
                                                }
                                        }
                                    } // foreach (metadata md in mdIn)
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    } // case PCOOperation.merge
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();
        }
        private static void MigrateManagement(ModelMateEFModel9Context ctxTarget,
                                                ICollection<management> mcTarget, ICollection<management> mcIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        switch (mcTarget.Count)
                        {
                            case 0:
                                {
                                    var mcctx = ctxTarget.management;
                                    foreach (management m in mcIn)
                                    {
                                        mcctx.Add(m);
                                        mcTarget.Add(m); // CONCERN
                                    }
                                    break; // mcTarget.Count case 0
                                }
                            case 1:
                                {
                                    if (mcIn.Count > 1) throw new ArgumentOutOfRangeException("mcIn", "Should have 0 or 1 children");

                                    if (mcIn.Count == 1) MigrateTimesnap(ctxTarget,
                                                        mcTarget.ElementAt(0).timesnap,
                                                        mcIn.ElementAt(0).timesnap,
                                                        opObjectIn);
                                    break; // mcTarget.Count case 1
                                }
                            default:
                                {
                                    throw new ArgumentOutOfRangeException("management", "More than 1 is illegal");
                                }
                        }
                        break; // case PCOOperation.merge
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();
        }
        private static void MigratePerformance(ModelMateEFModel9Context ctxTarget,
                                                ICollection<performance> pcTarget, ICollection<performance> pcIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        switch (pcTarget.Count)
                        {
                            case 0:
                                {
                                    var pcctx = ctxTarget.performance;
                                    foreach (performance p in pcIn)
                                    {
                                        pcctx.Add(p);
                                        pcTarget.Add(p); // CONCERN
                                    }
                                    break; // psTarget.Count case 0
                                }
                            case 1:
                                {
                                    if (pcIn.Count > 1) throw new ArgumentOutOfRangeException("pcIn", "Should have 0 or 1 children");

                                    if (pcIn.Count == 1) MigrateTimesnap(ctxTarget,
                                                        pcTarget.ElementAt(0).timesnap,
                                                        pcIn.ElementAt(0).timesnap,
                                                        opObjectIn);
                                    break; // psTarget.Count case 1
                                }
                            default:
                                {
                                    throw new ArgumentOutOfRangeException("performance", "More than 1 is illegal");
                                }
                        }
                        break; // case PCOOperation.merge
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();
        }
        private static void MigrateProcessingHistory(ModelMateEFModel9Context ctxTarget, 
                                                    ICollection<processinghistory> phcTarget, ICollection<processinghistory> phcIn,
                                                    PCOOperation opObjectIn)
        { 
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        switch (phcTarget.Count)
                        {
                            case 0:
                                {
                                    var phcctx = ctxTarget.processinghistory;
                                    foreach (processinghistory ps in phcIn)
                                    {
                                        phcctx.Add(ps);
                                        phcTarget.Add(ps); // CONCERN
                                    }
                                    break; // psTarget.Count case 0
                                }
                            case 1:
                                {
                                    if (phcIn.Count > 1) throw new ArgumentOutOfRangeException("phcIn", "Should have 0 or 1 children");

                                    if (phcIn.Count == 1) MigrateTimesnap(ctxTarget,
                                                        phcTarget.ElementAt(0).timesnap,
                                                        phcIn.ElementAt(0).timesnap,
                                                        opObjectIn);
                                    break; // psTarget.Count case 1
                                }
                            default:
                                {
                                    throw new ArgumentOutOfRangeException("processinghistory", "More than 1 is illegal");
                                }
                        }
                        break; // case PCOOperation.merge
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();
        }
        private static void MigrateUsage(ModelMateEFModel9Context ctxTarget, 
                                            ICollection<usage> ucTarget, ICollection<usage> ucIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        switch (ucTarget.Count)
                        {
                            case 0:
                                {
                                    var ucctx = ctxTarget.usage;
                                    foreach (usage u in ucIn)
                                    {
                                        ucctx.Add(u);
                                        ucTarget.Add(u); // CONCERN
                                    }
                                    break; // psTarget.Count case 0
                                }
                            case 1:
                                {
                                    if (ucIn.Count > 1) throw new ArgumentOutOfRangeException("ucIn", "Should have 0 or 1 children");

                                    if (ucIn.Count == 1) MigrateTimesnap(ctxTarget,
                                                        ucTarget.ElementAt(0).timesnap,
                                                        ucIn.ElementAt(0).timesnap,
                                                        opObjectIn);
                                    break; // psTarget.Count case 1
                                }
                            default:
                                {
                                    throw new ArgumentOutOfRangeException("usage", "More than 1 is illegal");
                                }
                        }
                        break; // case PCOOperation.merge
                    }
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();
        }
        
        private static void MigrateTimesnap(ModelMateEFModel9Context ctxTarget, 
                                                ICollection<timesnap> tscTarget, ICollection<timesnap> tscIn, PCOOperation opObjectIn)
        {
            switch (opObjectIn)
            {
                case PCOOperation.ignore:
                    {
                        break;
                    }
                case PCOOperation.add:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.create:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.delete:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.merge:
                    {
                        switch (tscTarget.Count)
                        {
                            case 0: // mdsTarget.Count case 0
                                {
                                    var tscctx = ctxTarget.timesnap;
                                    foreach (timesnap ts in tscIn)
                                    {
                                        tscctx.Add(ts);
                                        tscTarget.Add(ts);
                                    }
                                    break;
                                }
                            case 1: // mdsTarget.Count case 1
                                {
                                    if (tscIn.Count > 1) throw new ArgumentOutOfRangeException("tscIn", "Should have 0 or 1 children");

                                    var tscctx = ctxTarget.timesnap;
                                    foreach (timesnap ts in tscIn)
                                    {
                                        var tsMatching = (from match in tscTarget
                                                          where (match.schema == ts.schema && match.schemaversion == ts.schemaversion)
                                                          select match);
                                        switch (tsMatching.Count<timesnap>())
                                        {
                                            case 0: // CONCERN
                                                {
                                                    tscctx.Add(ts);
                                                    tscTarget.Add(ts);
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    var tsMatching0 = tsMatching.ElementAt(0);

                                                    tsMatching0.operation = ts.operation;
                                                    tsMatching0.category = ts.category;
                                                    tsMatching0.schema = ts.schema;
                                                    tsMatching0.schemaversion = ts.schemaversion;
                                                    tsMatching0.subcategory = ts.subcategory;
                                                    tsMatching0.timestamp = ts.timestamp;

                                                    MigrateDocumentation(ctxTarget, tsMatching0.documentation, ts.documentation, opObjectIn);
                                                    MigrateLabel(ctxTarget, tsMatching0.label, ts.label, opObjectIn);
                                                    MigrateProperties(ctxTarget, tsMatching0.properties, ts.properties, opObjectIn);
                                                    break;
                                                }
                                            default:
                                                {
                                                    throw new ArgumentOutOfRangeException("metadata", "More than 1 match is illegal");
                                                }
                                        }
                                    } // foreach (metadata md in tscIn)
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    } // case PCOOperation.merge
                case PCOOperation.remove:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.replace:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.update:
                    {
                        throw new NotImplementedException("case opObjectIn " + opObjectIn.ToString());
                    }
                case PCOOperation.OtherOrUnknownOrUndefined:
                default:
                    {
                        throw new NotImplementedException("opObjectIn " + opObjectIn.ToString());
                    }
            }

            ctxTarget.SaveChanges();
        }

        static public void UpdateElementObject(ModelMateEFModel9Context ctxTarget, element eTarget, element eIn,
                                               PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn,
                                               bool recurseChildObjectsIn)
        {
            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (eTarget == null) throw new ArgumentNullException("eTarget");
            if (eIn == null) throw new ArgumentNullException("eIn");

            //eTarget.identifier = eIn.identifier;
            //eTarget.elements_Id = eIn.elements_Id;
            //eTarget.element_Id = eIn.element_Id;
            eTarget.operation = eIn.operation;
            eTarget.type = eIn.type;
        }

        static public void UpdateRelationshipObject(ModelMateEFModel9Context ctxTarget, relationship rTarget, relationship rIn,
                                                    PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn,
                                                    bool recurseChildObjectsIn)
        {
            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (rTarget == null) throw new ArgumentNullException("rTarget");
            if (rIn == null) throw new ArgumentNullException("rIn");

            //rTarget.identifier = rIn.identifier;
            //rTarget.relationships_Id = rIn.relationships_Id;
            //rTarget.relationship_Id = rIn.relationship_Id;
            rTarget.operation = rIn.operation;
            rTarget.type = rIn.type;
            rTarget.source = rIn.source;
            rTarget.target = rIn.target;
            rTarget.type = rIn.type;
        }

        static public void UpdateViewObject(ModelMateEFModel9Context ctxTarget, view vTarget, view vIn,
                                            PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn,
                                            bool recurseChildObjectsIn)
        {
            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (vTarget == null) throw new ArgumentNullException("eTarget");
            if (vIn == null) throw new ArgumentNullException("eIn");

            //vTarget.identifier = vIn.identifier;
            //vTarget.views_Id = vIn.views_Id;
            //vTarget.view_Id = vIn.view_Id;
            vTarget.operation = vIn.operation;
            vTarget.viewpoint = vIn.viewpoint;
        }

        static public void UpdateItemObject(ModelMateEFModel9Context ctxTarget, item iTarget, item iIn,
                                            PCOOperation opObjectIn, PCOOperation opRelatedPropertiesIn,
                                            bool recurseChildObjectsIn)
        {
            if (ctxTarget == null) throw new ArgumentNullException("ctxTarget");
            if (iTarget == null) throw new ArgumentNullException("iTarget");
            if (iIn == null) throw new ArgumentNullException("iIn");

            //iTarget.identifier = iIn.identifier;
            //iTarget.items_Id = iIn.items_Id;
            //iTarget.item_Id = iIn.item_Id;
            iTarget.operation = iIn.operation;
            iTarget.identifierref = iIn.identifierref;
        }
    }
}
