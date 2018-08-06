using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;
using Parallelspace.Content.Objects;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN.ModelMate.Lib
{
    public class ModelProcessor
    {
        public enum ObjectPostProcessingOption
        {
            Continue,           // Normal processing
            Break,              // Do not process any more objects of this type in this context; process *all* child objects for 
                                //      current object ignoring the post processing option they return
            Return,             // Stop processing any more objects of this type *in this context* (including stop processing all child objects)
            StopAllProcessing   // Stop processing any more objects or child objects (regardless of context)
        }

        public ModelProcessor()
        {
            objectProcessor = ProcessObjectDefault;
        }
        public ModelProcessor(Func<DataSet, DbContext, object, ObjectPostProcessingOption> objectProcessorIn)
        {
            objectProcessor = objectProcessorIn;
        }
        private static Func<DataSet, DbContext, object, ObjectPostProcessingOption> objectProcessor { get; set; }

        public static ObjectPostProcessingOption ProcessObjectDefault(DataSet ds, DbContext ctx, object obj)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            if (ds == null) throw new ArgumentNullException("ds");
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (obj == null) throw new ArgumentNullException("obj");

            ModelDump.DisplayDBPropertyValues(ctx.Entry(obj).Entity.GetType().Name, ctx.Entry(obj).CurrentValues, null);

            return option;
        }

        private static int base_Id = Util.MakeIdInt32();
        private static Int32 SafeId(DataRow dr, string columnName)
        {
            Int32 value;

            string svalue = SafeString(dr, columnName);
            if (String.IsNullOrEmpty(svalue))
            {
                value = 0;
            }
            else
            {
                value = Int32.Parse(svalue) + base_Id;
            }

            return value;
        }
        private static string SafeString(DataRow dr, string columnName)
        {
            string value;
            object obj;

            if (dr.Table.Columns.Contains(columnName))
            {
                obj = dr[columnName];
                if (obj != null)
                {
                    value = obj.ToString();
                }
                else
                {
                    value = ""; // TODO
                }
            }
            else
            {
                value = ""; // TODO
            }

            return value;
        }
        public static ObjectPostProcessingOption ConvertAMEFFDataSet2Model(DataSet ds, DbContext ctx, object objModel)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            if (ds == null) throw new ArgumentNullException("ds");
            //if (ctx == null) throw new ArgumentNullException("ctx");
            if (objModel == null) throw new ArgumentNullException("objModel");

            string objTypeName = Util.MakeBaseObjectTypeName(objModel);

            Console.WriteLine("ConvertAMEFFDataSet2Model:processing " + objModel.GetType().Name);

            //string[] orderedTableNames = new string[]{
            //    Const.MODEL_PREFIX,

            //    Const.METADATA_XPREFIX,

            //    Const.ELEMENTS_XPREFIX, Const.ELEMENT_PREFIX, Const.RELATIONSHIPS_XPREFIX, Const.RELATIONSHIP_PREFIX,

            //    Const.ORGANIZATION_XPREFIX, Const.ITEM_XPREFIX,
            //    Const.VIEWS_XPREFIX, Const.VIEW_PREFIX, Const.NODE_PREFIX, Const.CONNECTION_PREFIX, Const.BENDPOINT_XPREFIX,
            //    Const.STYLE_XPREFIX, Const.FILLCOLOR_XPREFIX, Const.LINECOLOR_XPREFIX, Const.FONT_XPREFIX, Const.COLOR_XPREFIX,

            //    Const.PROPERTIES_XPREFIX, Const.PROPERTY_XPREFIX,
            //    Const.PROPERTYDEFS_XPREFIX, Const.PROPERTYDEF_PREFIX,

            //    Const.NAME_PREFIX, Const.VALUE_PREFIX, Const.DOCUMENTATION_PREFIX, Const.LABEL_PREFIX
            //};
            //foreach (string tableName in orderedTableNames)
            //{
            //}

            ConvertAMEFFDataTable2Model(ds, objModel, ModelConst.MODEL_PREFIX);

            return option;
        }

        public static object[] ConvertAMEFFDataTable2Model(DataSet ds, object objParent, string tableName)
        {
            object[] objectArray = null;
            //ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            if (ds == null) throw new ArgumentNullException("ds"); 
            //if (ctx == null) throw new ArgumentNullException("ctx");
            if (objParent == null) throw new ArgumentNullException("objParent");
            if (String.IsNullOrEmpty(tableName)) throw new ArgumentNullException("tableName");

            model model = null;
            metadata metadata = null;
            elements elements = null;
            element element = null;
            relationships relationships = null;
            relationship relationship = null;
            organization organization = null;
            item item = null;
            views views = null;
            view view = null;
            node node = null;
            connection connection = null;
            bendpoint bendpoint = null;
            style style = null;
            fillColor fillColor = null;
            lineColor lineColor = null;
            font font = null;
            color color = null;
            properties properties = null;
            property property = null;
            propertydefs propertydefs = null;
            propertydef propertydef = null;
            name name = null;
            documentation documentation = null;
            label label = null;
            value value = null;

            switch (tableName)
            {
                case ModelConst.MODEL_PREFIX: //  ASSUMPTION: dataset only contains one model
                    {
                        model = (model)objParent;
                        DataTable dt = ds.Tables[tableName];
                        if (dt.Rows.Count != 1) throw new ArgumentOutOfRangeException("ds.Tables[model].Rows.Count", dt.Rows.Count.ToString(), "Dataset can only contain one model");
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            objectArray[iObject++] = model;
                            model.model_Id = SafeId(dr, "model_Id");
                            model.identifier = SafeString(dr, "identifier");
                            model.version = SafeString(dr, "version");
                            model.operation = PCOOperation.atrest.ToString();

                            metadata[] metadataArray = (metadata[])ConvertAMEFFDataTable2Model(ds, model, ModelConst.METADATA_XPREFIX);
                            foreach (metadata md in metadataArray) model.metadata.Add(md);

                            elements[] elementsArray = (elements[])ConvertAMEFFDataTable2Model(ds, model, ModelConst.ELEMENTS_XPREFIX);
                            foreach (elements es in elementsArray) model.elements.Add(es);
                            relationships[] relationshipsArray = (relationships[])ConvertAMEFFDataTable2Model(ds, model, ModelConst.RELATIONSHIPS_XPREFIX);
                            foreach (relationships rs in relationshipsArray) model.relationships.Add(rs);
                            organization[] organizationArray = (organization[])ConvertAMEFFDataTable2Model(ds, model, ModelConst.ORGANIZATION_XPREFIX);
                            foreach (organization o in organizationArray) model.organization.Add(o);
                            views[] viewsArray = (views[])ConvertAMEFFDataTable2Model(ds, model, ModelConst.VIEWS_XPREFIX);
                            foreach (views vs in viewsArray) model.views.Add(vs);

                            //metadata[] metadataArray = (metadata[])ConvertAMEFFDataTable2Model(ds, model, Const.NAME_PREFIX);
                            //metadata[] metadataArray = (metadata[])ConvertAMEFFDataTable2Model(ds, model, Const.VALUE_PREFIX);
                            //metadata[] metadataArray = (metadata[])ConvertAMEFFDataTable2Model(ds, model, Const.DOCUMENTATION_PREFIX);
                            //metadata[] metadataArray = (metadata[])ConvertAMEFFDataTable2Model(ds, model, Const.LABEL_PREFIX);
                        }
                        break;
                    }

                case ModelConst.METADATA_XPREFIX:
                    {
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            metadata = ModelFactory.NewMetadata((property)null); // TODO
                            metadata.model_Id = SafeId(dr, "model_Id");
                            //metadata.identifier = SafeString(dr, "identifier");
                            //metadata.version = SafeString(dr, "version");
                            metadata.operation = PCOOperation.atrest.ToString();

                            objectArray[iObject++] = model;
                        }
                        break;
                    }

                case ModelConst.ELEMENTS_XPREFIX:
                    {
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements = ModelFactory.NewElements((element)null); // TODO
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                            ConvertAMEFFDataTable2Model(ds, elements, ModelConst.ELEMENT_PREFIX);
                        }
                        break;
                    }
                case ModelConst.ELEMENT_PREFIX:
                    {
                        element = (element)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            element.element_Id = SafeId(dr, "element_Id");
                            element.identifier = SafeString(dr, "identifier");
                            element.type = SafeString(dr, "type");
                            element.operation = PCOOperation.atrest.ToString();
                        }
                            
                        break;
                    }

                case ModelConst.RELATIONSHIPS_XPREFIX:
                    {
                        relationships = (relationships)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.RELATIONSHIP_PREFIX:
                    {
                        relationship = (relationship)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }

                case ModelConst.ORGANIZATION_XPREFIX:
                    {
                        organization = (organization)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.ITEM_PREFIX:
                    {
                        item = (item)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }

                case ModelConst.VIEWS_XPREFIX:
                    {
                        views = (views)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.VIEW_PREFIX:
                    {
                        view = (view)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.NODE_PREFIX:
                    {
                        node = (node)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.CONNECTION_PREFIX:
                    {
                        connection = (connection)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.BENDPOINT_XPREFIX:
                    {
                        bendpoint = (bendpoint)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.STYLE_XPREFIX:
                    {
                        style = (style)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.FILLCOLOR_XPREFIX:
                    {
                        fillColor = (fillColor)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.LINECOLOR_XPREFIX:
                    {
                        lineColor = (lineColor)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.FONT_XPREFIX:
                    {
                        font = (font)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.COLOR_XPREFIX:
                    {
                        color = (color)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }

                case ModelConst.PROPERTIES_XPREFIX:
                    {
                        properties = (properties)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.PROPERTY_XPREFIX:
                    {
                        property = (property)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.PROPERTYDEFS_XPREFIX:
                    {
                        propertydefs = (propertydefs)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.PROPERTYDEF_PREFIX:
                    {
                        propertydef = (propertydef)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }

                case ModelConst.NAME_PREFIX:
                    {
                        name = (name)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.LABEL_PREFIX:
                    {
                        label = (label)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.DOCUMENTATION_PREFIX:
                    {
                        documentation = (documentation)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }
                case ModelConst.VALUE_PREFIX:
                    {
                        value = (value)objParent;
                        DataTable dt = ds.Tables[tableName];
                        objectArray = new object[dt.Rows.Count];
                        int iObject = 0;
                        var dtRows = dt.Rows;
                        foreach (DataRow dr in dtRows)
                        {
                            elements.elements_Id = SafeId(dr, "elements_Id");
                            //model.identifier = dr["identifier"].ToString();
                            //model.version = dr["verson"].ToString();
                            //elements.operation = PCOOperation.atrest.ToString();
                        }
                        break;
                    }

                case ModelConst.TENANT_PREFIX:
                    {
                        throw new ArgumentOutOfRangeException("tableName", tableName, "Unexpected table name '" + tableName + "'");
                        //break;
                    }
                case ModelConst.FOLDER_PREFIX:
                    {
                        throw new ArgumentOutOfRangeException("tableName", tableName, "Unexpected table name '" + tableName + "'");
                        //break;
                    }
                case ModelConst.TIMESNAP_PREFIX:
                    {
                        throw new ArgumentOutOfRangeException("tableName", tableName, "Unexpected table name '" + tableName + "'");
                        //break;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException("tableName", tableName, "Unexpected table name '" + tableName + "'");
                        //break;
                    }
            }

            //ObjectDisplay.DisplayDBPropertyValues(ctx.Entry(obj).Entity.GetType().Name, ctx.Entry(obj).CurrentValues, null);

            return objectArray;
        }

        public static ObjectPostProcessingOption ConvertModel2AMEFFDataSet(DataSet ds, DbContext ctx, object obj)
        {
            // TODO if (ds.GetType().Name == "DataSet" && ds == null) ds.ReadXmlSchema(#RESOURCE#);

            if (ds == null) throw new ArgumentNullException("ds");
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (obj == null) throw new ArgumentNullException("obj");

            string objTypeName = Util.MakeBaseObjectTypeName(obj);

            if ((objTypeName == "processinghistory") ||
                (objTypeName == "usage") ||
                (objTypeName == "performance") ||
                (objTypeName == "management") ||
                (objTypeName == "metadata")
               )
            {
                return ObjectPostProcessingOption.Return;
            }

            return ConvertEntity2AMEFFDataSet(ds, ctx, obj);
        }

        static int nRows = 0;
        public static ObjectPostProcessingOption ConvertEntity2AMEFFDataSet(DataSet ds, DbContext ctx, object obj)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;


            if (ds == null) throw new ArgumentNullException("ds");
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (obj == null) throw new ArgumentNullException("obj");

            //string objTypeName = ctx.Entry(obj).Entity.GetType().Name;
            string objTypeName = Util.MakeBaseObjectTypeName(obj);

            //ModelDump.DisplayDBPropertyValues(ctx.Entry(obj).Entity.GetType().Name, ctx.Entry(obj).CurrentValues, null);
            nRows++;
            Console.WriteLine("Object " + nRows.ToString() + ": " + objTypeName);

            DataTable dt = ds.Tables[objTypeName];
            Console.WriteLine("dt.Rows.Count: " + dt.Rows.Count.ToString());
            //if (dt.Rows.Count >= 87666) System.Diagnostics.Debugger.Break();
            DataRow drNew = null;
            if (objTypeName != "property")
            {
                drNew = dt.NewRow();
            }

            switch (objTypeName)
            {
                case "element":
                    {
                        element e = (element)obj;
                        drNew["operation"] = DBNull.Value;
                        if (dt.Columns.Contains("identifier")) drNew["identifier"] = String.IsNullOrEmpty(e.identifier) ? (object)DBNull.Value : e.identifier;
                        if (dt.Columns.Contains("type")) drNew["type"] = String.IsNullOrEmpty(e.type) ? "" : e.type;

                        if (dt.Columns.Contains("element_Id")) drNew["element_Id"] = e.element_Id;
                        if (dt.Columns.Contains("elements_Id")) drNew["elements_Id"] = e.elements_Id.HasValue ? e.elements_Id : (object)DBNull.Value;
                        break;
                    }
                case "label":
                    {
                        label l = (label)obj;
                        drNew["operation"] = DBNull.Value;
                        if (dt.Columns.Contains("lang")) drNew["lang"] = String.IsNullOrEmpty(l.lang) ? (object)DBNull.Value : l.lang;
                        if (dt.Columns.Contains("label_text")) drNew["label_text"] = String.IsNullOrEmpty(l.label_text) ? "" : l.label_text;

                        if (dt.Columns.Contains("connection_Id")) drNew["connection_Id"] = l.connection_Id.HasValue ? l.connection_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("element_Id")) drNew["element_Id"] = l.element_Id.HasValue ? l.element_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("item_Id")) drNew["item_Id"] = l.item_Id.HasValue ? l.item_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("label_Id")) drNew["label_Id"] = l.label_Id;
                        if (dt.Columns.Contains("node_Id")) drNew["node_Id"] = l.node_Id.HasValue ? l.node_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("relationship_Id")) drNew["relationship_Id"] = l.relationship_Id.HasValue ? l.relationship_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("timesnap_Id")) drNew["timesnap_Id"] = l.timesnap_Id.HasValue ? l.timesnap_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("view_Id")) drNew["view_Id"] = l.view_Id.HasValue ? l.view_Id : (object)DBNull.Value;
                        break;
                    }
                case "name":
                    {
                        name n = (name)obj;
                        drNew["operation"] = DBNull.Value;
                        if (dt.Columns.Contains("lang")) drNew["lang"] = String.IsNullOrEmpty(n.lang) ? (object)DBNull.Value : n.lang;
                        if (dt.Columns.Contains("name_text")) drNew["name_text"] = String.IsNullOrEmpty(n.name_text) ? "" : n.name_text;

                        if (dt.Columns.Contains("tenant_Id")) drNew["tenant_Id"] = n.tenant_Id.HasValue ? n.tenant_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("folder_Id")) drNew["folder_Id"] = n.folder_Id.HasValue ? n.folder_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("model_Id")) drNew["model_Id"] = n.model_Id.HasValue ? n.model_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("name_Id")) drNew["name_Id"] = n.name_Id;
                        break;
                    }
                case "properties":
                    {
                        properties ps = (properties)obj;
                        // drNew["operation"] = DBNull.Value;

                        if (dt.Columns.Contains("properties_Id")) drNew["properties_Id"] = ps.properties_Id;

                        if (dt.Columns.Contains("connection_Id")) drNew["connection_Id"] = ps.connection_Id.HasValue ? ps.connection_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("element_Id")) drNew["element_Id"] = ps.element_Id.HasValue ? ps.element_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("folder_Id")) drNew["folder_Id"] = ps.folder_Id.HasValue ? ps.folder_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("model_Id")) drNew["model_Id"] = ps.model_Id.HasValue ? ps.model_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("node_Id")) drNew["node_Id"] = ps.node_Id.HasValue ? ps.node_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("relationship_Id")) drNew["relationship_Id"] = ps.relationship_Id.HasValue ? ps.relationship_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("tenant_Id")) drNew["tenant_Id"] = ps.tenant_Id.HasValue ? ps.tenant_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("timesnap_Id")) drNew["timesnap_Id"] = ps.timesnap_Id.HasValue ? ps.timesnap_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("view_Id")) drNew["view_Id"] = ps.view_Id.HasValue ? ps.view_Id : (object)DBNull.Value;
                        break;
                    }
                case "property":
                    {
                        property p = (property)obj;

                        //if (dt.Columns.Contains("identifierref")) drNew["identifierref"] = String.IsNullOrEmpty(p.identifierref) ? (object)DBNull.Value : p.identifierref;
                        //drNew["operation"] = DBNull.Value;
                        //if (dt.Columns.Contains("property_Id")) drNew["property_Id"] = p.property_Id;
                        //if (dt.Columns.Contains("properties_Id")) drNew["properties_Id"] = p.properties_Id.HasValue ? p.properties_Id : (object)DBNull.Value;
                        //if (dt.Columns.Contains("metadata_Id")) drNew["metadata_Id"] = p.metadata_Id.HasValue ? p.metadata_Id : (object)DBNull.Value;

                        drNew = dt.Rows.Add(new object[]
                            {
                                String.IsNullOrEmpty(p.identifierref) ? (object)DBNull.Value : p.identifierref,
                                DBNull.Value,
                                p.property_Id,
                                p.properties_Id.HasValue ? p.properties_Id : (object)DBNull.Value,
                                p.metadata_Id.HasValue ? p.metadata_Id : (object)DBNull.Value,
                            });
                        break;
                    }
                case "propertydef":
                    {
                        propertydef pd = (propertydef)obj;
                        drNew["operation"] = DBNull.Value;
                        if (dt.Columns.Contains("identifier")) drNew["identifier"] = String.IsNullOrEmpty(pd.identifier) ? (object)DBNull.Value : pd.identifier;
                        if (dt.Columns.Contains("name")) drNew["name"] = String.IsNullOrEmpty(pd.identifier) ? (object)DBNull.Value : pd.name;
                        if (dt.Columns.Contains("type")) drNew["type"] = String.IsNullOrEmpty(pd.identifier) ? (object)DBNull.Value : pd.type;

                        if (dt.Columns.Contains("propertydefs_Id")) drNew["propertydefs_Id"] = pd.propertydefs_Id.HasValue ? pd.propertydefs_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("propertydef_Id")) drNew["propertydef_Id"] = pd.propertydef_Id;
                        break;
                    }
                case "relationships":
                    {
                        relationships rs = (relationships)obj;
   
                        if (dt.Columns.Contains("model_Id")) drNew["model_Id"] = rs.model_Id.HasValue ? rs.model_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("relationships_Id")) drNew["relationships_Id"] = rs.relationships_Id;
                        break;
                    }
                case "relationship":
                    {
                        relationship r = (relationship)obj;
                        drNew["operation"] = DBNull.Value;
                        if (dt.Columns.Contains("identifier")) drNew["identifier"] = String.IsNullOrEmpty(r.identifier) ? (object)DBNull.Value : r.identifier;
                        if (dt.Columns.Contains("type")) drNew["type"] = String.IsNullOrEmpty(r.type) ? (object)DBNull.Value : r.type;
                        if (dt.Columns.Contains("source")) drNew["source"] = String.IsNullOrEmpty(r.source) ? (object)DBNull.Value : r.source;
                        if (dt.Columns.Contains("target")) drNew["target"] = String.IsNullOrEmpty(r.target) ? (object)DBNull.Value : r.target;

                        if (dt.Columns.Contains("relationships_Id")) drNew["relationships_Id"] = r.relationships_Id.HasValue ? r.relationships_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("relationship_Id")) drNew["relationship_Id"] = r.relationship_Id;
                        break;
                    }
                case "value":
                    {
                        value v = (value)obj;
                        drNew["operation"] = DBNull.Value;
                        if (dt.Columns.Contains("lang")) drNew["lang"] = String.IsNullOrEmpty(v.lang) ? (object)DBNull.Value : v.lang;
                        if (dt.Columns.Contains("value_text")) drNew["value_text"] = String.IsNullOrEmpty(v.value_text) ? "" : v.value_text;

                        if (dt.Columns.Contains("property_Id")) drNew["property_Id"] = v.property_Id.HasValue ? v.property_Id : (object)DBNull.Value;
                        if (dt.Columns.Contains("value_Id")) drNew["value_Id"] = v.value_Id;
                        break;
                    }
                default:
                    {
                        var currentValues = ctx.Entry(obj).CurrentValues;
                        var pns = currentValues.PropertyNames;
                        foreach (string propertyName in pns)
                        {
                            if (dt.Columns.Contains(propertyName))
                            {
                                var propertyValue = currentValues[propertyName];
                                if (propertyValue == null)
                                {
                                    drNew[propertyName] = DBNull.Value;
                                }
                                else
                                {
                                    if ((propertyName == "operation" && propertyValue.ToString() == PCOOperation.atrest.ToString()) ||
                                        (propertyName == "operation" && propertyValue.ToString() == PCOOperation.OtherOrUnknownOrUndefined.ToString()) ||
                                        (propertyName == "viewpoint" && propertyValue.ToString() == ModelConst.ViewType.OtherOrUnknownOrUndefinedViewType.ToString()) ||
                                        (propertyName == "type" && propertyValue.ToString() == ModelConst.PropertyDataType.OtherOrUnknownOrUndefinedDataType.ToString()) ||
                                        (propertyName == "z") ||
                                        (propertyName == "d")
                                       )
                                    {
                                        drNew[propertyName] = DBNull.Value;
                                    }
                                    else
                                    {
                                        drNew[propertyName] = propertyValue;
                                    }
                                }
                            }
                            else
                            {
                                // do nothing - leaf tables don't have xxxx_Id in XSD schema
                            }
                        }
                        break;
                    }
            }

            if (objTypeName != "property")
            {
                dt.Rows.Add(drNew);
            }

            return option;
        }
        public void SaveAMEFFDataSetAsXML(DataSet dsModel, string filename)
        {
            string xmlModel = dsModel.GetXml();

            for (int iPrefix = 1; iPrefix < 20; iPrefix++)
            {
                xmlModel = xmlModel.Replace(String.Format("d{0}p1", iPrefix), "xml");
            }
            xmlModel = xmlModel.Replace("xmlns:xml=\"http://www.w3.org/XML/1998/namespace\"", "");

            xmlModel = xmlModel.Replace("<ModelMate xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.opengroup.org/xsd/archimate\">", "<?xml version=\"1.0\" standalone=\"yes\"?>");
            xmlModel = xmlModel.Replace("</ModelMate>", "");

            string namespaces = "xmlns=\"http://www.opengroup.org/xsd/archimate\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.opengroup.org/xsd/archimate http://www.opengroup.org/xsd/archimate/archimate_v2p1.xsd http://purl.org/dc/elements/1.1/ http://dublincore.org/schemas/xmls/qdc/2008/02/11/dc.xsd\"";
            xmlModel = xmlModel.Replace("<model ", "<model " + namespaces + " ");

            xmlModel = xmlModel.Replace("\" type=\"", "\" xsi:type=\"");
            // xsi:type="string" to type="string"
            foreach (ModelConst.PropertyDataType t in Enum.GetValues(typeof(ModelConst.PropertyDataType)))
            {
                string type = t.ToString().Replace("Type", "");
                xmlModel = xmlModel.Replace("xsi:type=\"" + type + "\"", "type=\"" + type + "\"");
            }

            File.WriteAllText(filename, xmlModel);
        }

        public static ObjectPostProcessingOption ConvertModel2XMLDataSet(DataSet ds, DbContext ctx, object obj)
        {
            // TODO if (ds.GetType().Name == "DataSet" && ds == null) ds.ReadXmlSchema(#RESOURCE#);

            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            if (ds == null) throw new ArgumentNullException("ds");
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (obj == null) throw new ArgumentNullException("obj");

            string objTypeName = Util.MakeBaseObjectTypeName(ctx.Entry(obj).Entity);

            //ModelDump.DisplayDBPropertyValues(ctx.Entry(obj).Entity.GetType().Name, ctx.Entry(obj).CurrentValues, null);
            Console.WriteLine("Object: " + objTypeName);

            DataTable dt = ds.Tables[objTypeName];
            DataRow drNew = dt.NewRow();

            foreach (string propertyName in ctx.Entry(obj).CurrentValues.PropertyNames)
            {
                if (dt.Columns.Contains(propertyName))
                {
                    var propertyValue = ctx.Entry(obj).CurrentValues[propertyName];
                    if (propertyValue == null)
                    {
                        drNew[propertyName] = DBNull.Value;
                    }
                    else
                    {
                        drNew[propertyName] = propertyValue;
                    }
                }
                else
                {
                    // do nothing - e.g. leaf tables don't have xxxx_Id in XSD schema
                }
            }

            dt.Rows.Add(drNew);

            return option;
        }

        public static ObjectPostProcessingOption CountModelObjects(DataSet ds, DbContext ctx, object obj)
        {
            // TODO if (ds.GetType().Name == "DataSet" && ds == null) ds.ReadXmlSchema(#RESOURCE#);

            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            if (ds == null) throw new ArgumentNullException("ds");
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (obj == null) throw new ArgumentNullException("obj");

            string objTypeName = Util.MakeBaseObjectTypeName(ctx.Entry(obj).Entity);

            ModelDump.DisplayDBPropertyValues(ctx.Entry(obj).Entity.GetType().Name, ctx.Entry(obj).CurrentValues, null);

            DataTable dt = ds.Tables[objTypeName];
            DataRow drNew = dt.NewRow();

            foreach (string propertyName in ctx.Entry(obj).CurrentValues.PropertyNames)
            {
                if (dt.Columns.Contains(propertyName))
                {
                    var propertyValue = ctx.Entry(obj).CurrentValues[propertyName];
                    if (propertyValue == null)
                    {
                        drNew[propertyName] = DBNull.Value;
                    }
                    else
                    {
                        drNew[propertyName] = propertyValue;
                    }
                }
                else
                {
                    // do nothing - e.g. leaf tables don't have xxxx_Id in XSD schema
                }
            }

            dt.Rows.Add(drNew);

            return option;
        }

        public ObjectPostProcessingOption ProcessTenant(DataSet ds, DbContext ctx, ICollection<tenant> tscIn)
        { 
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (tscIn == null) throw new ArgumentNullException("tscIn");

            foreach (tenant t in tscIn)
            {
                option = objectProcessor(ds, ctx, t);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                childOption = ProcessAnnotations(ds, ctx, t.metadata, t.name, null, t.documentation);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                childOption = ProcessPropertiesAndDefinitions(ds, ctx, t.properties, t.propertydefs);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                childOption = ProcessFolders(ds, ctx, t.folders);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (objectProcessor != ModelProcessor.ConvertModel2AMEFFDataSet)
                {
                    childOption = ProcessStatistics(ds, ctx, t.processinghistory, t.usage, t.performance, t.management);
                    if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                }

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessAnnotations(DataSet ds, DbContext ctx, ICollection<metadata> mdcIn, ICollection<name> ncIn, ICollection<label> lcIn, ICollection<documentation> dcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");

            if (mdcIn != null) childOption = ProcessMetadata(ds, ctx, mdcIn);
            if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
            if (ncIn != null) childOption = ProcessName(ds, ctx, ncIn);
            if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
            if (lcIn != null) childOption = ProcessLabel(ds, ctx, lcIn);
            if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
            if (dcIn != null) childOption = ProcessDocumentation(ds, ctx, dcIn);
            if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

            return option;
        }
        public ObjectPostProcessingOption ProcessPropertiesAndDefinitions(DataSet ds, DbContext ctx, ICollection<properties> pscIn, ICollection<propertydefs> pdscIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");

            if (pscIn != null) childOption = ProcessProperties(ds, ctx, pscIn);
            if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
            if (pdscIn != null) childOption = ProcessProperydefs(ds, ctx, pdscIn);
            if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

            return option;
        }
        public ObjectPostProcessingOption ProcessStatistics(DataSet ds, DbContext ctx, ICollection<processinghistory> phcIn, ICollection<usage> ucIn, ICollection<performance> pcIn, ICollection<management> mcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");

            if (phcIn != null) childOption = ProcessProcessinghistory(ds, ctx, phcIn);
            if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
            if (ucIn != null) childOption = ProcessUsage(ds, ctx, ucIn);
            if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
            if (pcIn != null) childOption = ProcessPerformance(ds, ctx, pcIn);
            if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
            if (mcIn != null) childOption = ProcessManagement(ds, ctx, mcIn);
            if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

            return option;
        }
        public ObjectPostProcessingOption ProcessProcessinghistory(DataSet ds, DbContext ctx, ICollection<processinghistory> phcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (phcIn == null) throw new ArgumentNullException("phcIn");

            // return; // WORKAROUND

            foreach (processinghistory ph in phcIn)
            {
                option = objectProcessor(ds, ctx, ph);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (ph.timesnap != null) childOption = ProcessTimesnap(ds, ctx, ph.timesnap);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessUsage(DataSet ds, DbContext ctx, ICollection<usage> ucIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (ucIn == null) throw new ArgumentNullException("ucIn");

            // return; // WORKAROUND

            foreach (usage u in ucIn)
            {
                option = objectProcessor(ds, ctx, u);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (u.timesnap != null) childOption = ProcessTimesnap(ds, ctx, u.timesnap);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option; 
        }
        public ObjectPostProcessingOption ProcessPerformance(DataSet ds, DbContext ctx, ICollection<performance> pcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (pcIn == null) throw new ArgumentNullException("pcIn");

            // return; // WORKAROUND

            foreach (performance p in pcIn)
            {
                option = objectProcessor(ds, ctx, p);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (p.timesnap != null) childOption = ProcessTimesnap(ds, ctx, p.timesnap);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessManagement(DataSet ds, DbContext ctx, ICollection<management> mcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (mcIn == null) throw new ArgumentNullException("mcIn");

            //return; // WORKAROUND

            foreach (management m in mcIn)
            {
                option = objectProcessor(ds, ctx, m);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (m.timesnap != null) childOption = ProcessTimesnap(ds, ctx, m.timesnap);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessTimesnap(DataSet ds, DbContext ctx, ICollection<timesnap> tscIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (tscIn == null) throw new ArgumentNullException("tscIn");

            foreach (timesnap ts in tscIn)
            {
                option = objectProcessor(ds, ctx, ts);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                childOption = ProcessAnnotations(ds, ctx, null, null, ts.label, ts.documentation);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                childOption = ProcessPropertiesAndDefinitions(ds, ctx, ts.properties, null);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessMetadata(DataSet ds, DbContext ctx, ICollection<metadata> mdcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (mdcIn == null) throw new ArgumentNullException("mdcIn");

            // return; // WORKAROUND

            foreach (metadata md in mdcIn)
            {
                option = objectProcessor(ds, ctx, md);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (md.property != null) childOption = ProcessProperty(ds, ctx, md.property);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (objectProcessor != ModelProcessor.ConvertModel2AMEFFDataSet)
                {
                    childOption = ProcessStatistics(ds, ctx, md.processinghistory, md.usage, md.performance, md.management);
                    if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                }

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessFolders(DataSet ds, DbContext ctx, ICollection<folders> fscIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (fscIn == null) throw new ArgumentNullException("fscIn");

            foreach (folders fs in fscIn)
            {
                option = objectProcessor(ds, ctx, fs);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (fs.folder != null) childOption = ProcessFolder(ds, ctx, fs.folder);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessFolder(DataSet ds, DbContext ctx, ICollection<folder> fcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (fcIn == null) throw new ArgumentNullException("fcIn");

            foreach (folder f in fcIn)
            {
                option = objectProcessor(ds, ctx, f);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                childOption = ProcessAnnotations(ds, ctx, f.metadata, f.name, null, f.documentation);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                childOption = ProcessPropertiesAndDefinitions(ds, ctx, f.properties, null);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (f.models != null) childOption = ProcessModels(ds, ctx, f.models);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (f.folder1 != null) childOption = ProcessFolder(ds, ctx, f.folder1); // TEST
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessModels(DataSet ds, DbContext ctx, ICollection<models> mscIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (mscIn == null) throw new ArgumentNullException("mscIn");

            foreach (models ms in mscIn)
            {
                option = objectProcessor(ds, ctx, ms);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (ms.model != null) childOption = ProcessModel(ds, ctx, ms.model);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessModel(DataSet ds, DbContext ctx, ICollection<model> mcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (mcIn == null) throw new ArgumentNullException("mcIn");

            foreach (model m in mcIn)
            {
                option = objectProcessor(ds, ctx, m);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                childOption = ProcessAnnotations(ds, ctx, m.metadata, m.name, null, m.documentation);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                childOption = ProcessPropertiesAndDefinitions(ds, ctx, m.properties, m.propertydefs);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (m.elements != null) childOption = ProcessElements(ds, ctx, m.elements);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (m.relationships != null) childOption = ProcessRelationships(ds, ctx, m.relationships);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (m.organization != null) childOption = ProcessOrganization(ds, ctx, m.organization);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (m.views != null) childOption = ProcessViews(ds, ctx, m.views);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (objectProcessor != ModelProcessor.ConvertModel2AMEFFDataSet)
                {
                    childOption = ProcessStatistics(ds, ctx, m.processinghistory, m.usage, m.performance, m.management);
                    if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                }

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessRelationships(DataSet ds, DbContext ctx, ICollection<relationships> rscIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (rscIn == null) throw new ArgumentNullException("rscIn");

            foreach (relationships rs in rscIn)
            {
                option = objectProcessor(ds, ctx, rs);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (rs.relationship != null) childOption = ProcessRelationship(ds, ctx, rs.relationship);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessRelationship(DataSet ds, DbContext ctx, ICollection<relationship> rcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (rcIn == null) throw new ArgumentNullException("rcIn");

            foreach (relationship r in rcIn)
            {
                option = objectProcessor(ds, ctx, r);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                childOption = ProcessAnnotations(ds, ctx, null, null, r.label, r.documentation);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                childOption = ProcessPropertiesAndDefinitions(ds, ctx, r.properties, null);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (objectProcessor != ModelProcessor.ConvertModel2AMEFFDataSet)
                {
                    childOption = ProcessStatistics(ds, ctx, r.processinghistory, r.usage, r.performance, r.management);
                    if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                }

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessOrganization(DataSet ds, DbContext ctx, ICollection<organization> ocIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (ocIn == null) throw new ArgumentNullException("ocIn");

            foreach (organization o in ocIn)
            {
                option = objectProcessor(ds, ctx, o);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (o.item != null) childOption = ProcessItem(ds, ctx, o.item);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessItem(DataSet ds, DbContext ctx, ICollection<item> icIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (icIn == null) throw new ArgumentNullException("icIn");

            foreach (item i in icIn)
            {
                option = objectProcessor(ds, ctx, i);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                childOption = ProcessAnnotations(ds, ctx, null, null, i.label, i.documentation);

                if (i.item1 != null) childOption = ProcessItem(ds, ctx, i.item1); // TEST
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessViews(DataSet ds, DbContext ctx, ICollection<views> vscIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (vscIn == null) throw new ArgumentNullException("vscIn");

            foreach (views vs in vscIn)
            {
                option = objectProcessor(ds, ctx, vs);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (vs.view != null) childOption = ProcessView(ds, ctx, vs.view);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessView(DataSet ds, DbContext ctx, ICollection<view> vcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (vcIn == null) throw new ArgumentNullException("vcIn");

            foreach (view v in vcIn)
            {
                option = objectProcessor(ds, ctx, v);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                childOption = ProcessAnnotations(ds, ctx, null, null, v.label, v.documentation);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                childOption = ProcessPropertiesAndDefinitions(ds, ctx, v.properties, null);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (v.node != null) childOption = ProcessNode(ds, ctx, v.node);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (v.connection != null) childOption = ProcessConnection(ds, ctx, v.connection);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (objectProcessor != ModelProcessor.ConvertModel2AMEFFDataSet)
                {
                    childOption = ProcessStatistics(ds, ctx, v.processinghistory, v.usage, v.performance, v.management);
                    if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                }

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessNode(DataSet ds, DbContext ctx, ICollection<node> ncIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (ncIn == null) throw new ArgumentNullException("ncIn");

            foreach (node n in ncIn)
            {
                option = objectProcessor(ds, ctx, n);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                childOption = ProcessAnnotations(ds, ctx, null, null, n.label, n.documentation);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                childOption = ProcessPropertiesAndDefinitions(ds, ctx, n.properties, null);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (n.style != null) childOption = ProcessStyle(ds, ctx, n.style);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (n.node1 != null) childOption = ProcessNode(ds, ctx, n.node1); // TEST
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessConnection(DataSet ds, DbContext ctx, ICollection<connection> ccIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (ccIn == null) throw new ArgumentNullException("ccIn");

            foreach (connection c in ccIn)
            {
                option = objectProcessor(ds, ctx, c);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                childOption = ProcessAnnotations(ds, ctx, null, null, c.label, c.documentation);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                childOption = ProcessPropertiesAndDefinitions(ds, ctx, c.properties, null);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (c.style != null) childOption = ProcessStyle(ds, ctx, c.style);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (c.bendpoint != null) childOption = ProcessBendpoint(ds, ctx, c.bendpoint);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessStyle(DataSet ds, DbContext ctx, ICollection<style> scIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (scIn == null) throw new ArgumentNullException("scIn");

            foreach (style s in scIn)
            {
                option = objectProcessor(ds, ctx, s);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (s.fillColor != null) childOption = ProcessFillColor(ds, ctx, s.fillColor);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (s.lineColor != null) childOption = ProcessLineColor(ds, ctx, s.lineColor);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (s.font != null) childOption = ProcessFont(ds, ctx, s.font);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessFillColor(DataSet ds, DbContext ctx, ICollection<fillColor> fccIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (fccIn == null) throw new ArgumentNullException("fccIn");

            foreach (fillColor fc in fccIn)
            {
                option = objectProcessor(ds, ctx, fc);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessLineColor(DataSet ds, DbContext ctx, ICollection<lineColor> lccIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (lccIn == null) throw new ArgumentNullException("lccIn");

            foreach (lineColor lc in lccIn)
            {
                option = objectProcessor(ds, ctx, lc);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessFont(DataSet ds, DbContext ctx, ICollection<font> fcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (fcIn == null) throw new ArgumentNullException("fcIn");

            foreach (font f in fcIn)
            {
                option = objectProcessor(ds, ctx, f);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (f.color != null) childOption = ProcessColor(ds, ctx, f.color);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option; 
        }
        public ObjectPostProcessingOption ProcessColor(DataSet ds, DbContext ctx, ICollection<color> ccIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (ccIn == null) throw new ArgumentNullException("ccIn");

            foreach (color c in ccIn)
            {
                option = objectProcessor(ds, ctx, c);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessBendpoint(DataSet ds, DbContext ctx, ICollection<bendpoint> bpcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (bpcIn == null) throw new ArgumentNullException("bpcIn");

            foreach (bendpoint b in bpcIn)
            {
                option = objectProcessor(ds, ctx, b);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessElements(DataSet ds, DbContext ctx, ICollection<elements> escIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (escIn == null) throw new ArgumentNullException("escIn");

            foreach (elements es in escIn)
            {
                option = objectProcessor(ds, ctx, es);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (es.element != null) childOption = ProcessElement(ds, ctx, es.element);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessElement(DataSet ds, DbContext ctx, ICollection<element> ecIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (ecIn == null) throw new ArgumentNullException("ecIn");

            foreach (element e in ecIn)
            {
                option = objectProcessor(ds, ctx, e);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                childOption = ProcessAnnotations(ds, ctx, null, null, e.label, e.documentation);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                childOption = ProcessPropertiesAndDefinitions(ds, ctx, e.properties, null);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }

        public ObjectPostProcessingOption ProcessProperties(DataSet ds, DbContext ctx, ICollection<properties> pscIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (pscIn == null) throw new ArgumentNullException("pscIn");

            foreach (properties ps in pscIn)
            {
                option = objectProcessor(ds, ctx, ps);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (ps.property != null) childOption = ProcessProperty(ds, ctx, ps.property);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessProperty(DataSet ds, DbContext ctx, ICollection<property> pcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (pcIn == null) throw new ArgumentNullException("pcIn");

            foreach (property p in pcIn)
            {
                option = objectProcessor(ds, ctx, p);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (p.value != null) childOption = ProcessValue(ds, ctx, p.value);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessProperydefs(DataSet ds, DbContext ctx, ICollection<propertydefs> pdscIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (pdscIn == null) throw new ArgumentNullException("pdscIn");

            foreach (propertydefs pds in pdscIn)
            {
                option = objectProcessor(ds, ctx, pds);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (pds.propertydef != null) childOption = ProcessPropertydef(ds, ctx, pds.propertydef);
                if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessPropertydef(DataSet ds, DbContext ctx, ICollection<propertydef> pdfcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;
            ObjectPostProcessingOption childOption = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (pdfcIn == null) throw new ArgumentNullException("pdfcIn");

            foreach (propertydef pd in pdfcIn)
            {
                option = objectProcessor(ds, ctx, pd);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (objectProcessor != ModelProcessor.ConvertModel2AMEFFDataSet)
                {
                    childOption = ProcessStatistics(ds, ctx, pd.processinghistory, pd.usage, pd.performance, pd.management);
                    if (childOption == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                }

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessDocumentation(DataSet ds, DbContext ctx, ICollection<documentation> dcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (dcIn == null) throw new ArgumentNullException("dcIn");

            foreach (documentation d in dcIn)
            {
                option = objectProcessor(ds, ctx, d);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessLabel(DataSet ds, DbContext ctx, ICollection<label> lcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (lcIn == null) throw new ArgumentNullException("lcIn");

            foreach (label l in lcIn)
            {
                option = objectProcessor(ds, ctx, l);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessName(DataSet ds, DbContext ctx, ICollection<name> ncIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (ncIn == null) throw new ArgumentNullException("ncIn");

            foreach (name n in ncIn)
            {
                option = objectProcessor(ds, ctx, n);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
        public ObjectPostProcessingOption ProcessValue(DataSet ds, DbContext ctx, ICollection<value> vcIn)
        {
            ObjectPostProcessingOption option = ObjectPostProcessingOption.Continue;

            // if (ds == null) throw new ArgumentNullException("ds");
            // if (ctx == null) throw new ArgumentNullException("ctx");
            if (vcIn == null) throw new ArgumentNullException("vcIn");

            foreach (value v in vcIn)
            {
                option = objectProcessor(ds, ctx, v);
                if (option == ObjectPostProcessingOption.StopAllProcessing) return ObjectPostProcessingOption.StopAllProcessing;
                if (option == ObjectPostProcessingOption.Return) return ObjectPostProcessingOption.Continue;

                if (option == ObjectPostProcessingOption.Break) return ObjectPostProcessingOption.Continue;
                // else option == ObjectPostProcessingOption.Continue
            }

            return option;
        }
    }
}