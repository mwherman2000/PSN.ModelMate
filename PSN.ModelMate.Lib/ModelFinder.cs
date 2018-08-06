using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;
using Parallelspace.Content.Objects;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PSN.ModelMate.Lib
{
    public static partial class ModelFinder
    {
        // TODO Finders should include tenant as parameter for security reasons
        // TODO Object should include tenant_Id as field to make it easier to implement tenant security

        const string sqlParentField = "SELECT {0} * from dbo.{1} AS PARENT " +
                                            " WHERE PARENT.{2} LIKE '{3}'";
        const string sqlParentNameOrLabelField = "SELECT {0} * from dbo.{1} AS PARENT " +
                                            "  JOIN dbo.{2} AS NAMEORLABEL ON PARENT.{1}_Id = NAMEORLABEL.{1}_Id " +
                                            " WHERE NAMEORLABEL.{2}_text LIKE '{3}'";

        const string sqlParentFieldVersion = "SELECT {0} * from dbo.{1} AS PARENT " +
                                            " WHERE PARENT.{2} LIKE '{3}' AND PARENT.version LIKE '{4}'";
        const string sqlParentNameOrLabelFieldVersion = "SELECT {0} * from dbo.{1} AS PARENT " +
                                            "  JOIN dbo.{2} AS NAMEORLABEL ON PARENT.{1}_Id = NAMEORLABEL.{1}_Id " +
                                            " WHERE NAMEORLABEL.{2}_text LIKE '{3}' AND PARENT.version LIKE '{4}'";

        static public tenant FindTenant(ModelMateEFModel9Context ctx, string identifierLikeClause, string nameLikeClause)
        {
            tenant t = null;
            string type = typeof(tenant).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(nameLikeClause)) throw new ArgumentNullException("identifierLikeClause or nameLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlParentField, "TOP 2", type, "identifier", identifierLikeClause);
            if (!String.IsNullOrEmpty(nameLikeClause)) sql = String.Format(sqlParentNameOrLabelField, "TOP 2", type, "name", nameLikeClause);
            DbParameter[] parms = new DbParameter[] { };
            var tenants = ctx.tenant.SqlQuery(sql, parms).ToList<tenant>();

            switch (tenants.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        t = tenants[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException("identifierLikeClause or nameLikeClause", "Matched too many entities");
                    }
            }

            return t;
        }
        static public tenant FindTenant(ModelMateEFModel9Context ctx, string identifierLikeClause, string nameLikeClause, string version)
        {
            tenant t = null;
            string type = typeof(tenant).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(nameLikeClause)) throw new ArgumentNullException("identifierLikeClause or nameLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlParentFieldVersion, "TOP 2", type, "identifier", identifierLikeClause, version);
            if (!String.IsNullOrEmpty(nameLikeClause)) sql = String.Format(sqlParentNameOrLabelFieldVersion, "TOP 2", type, "name", nameLikeClause, version);
            DbParameter[] parms = new DbParameter[] { };
            var tenants = ctx.tenant.SqlQuery(sql, parms).ToList<tenant>();

            switch (tenants.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        t = tenants[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException("identifierLikeClause or nameLikeClause", "Matched too many entities");
                    }
            }

            return t;
        }
        static public tenant[] FindTenants(ModelMateEFModel9Context ctx, string identifierLikeClause, string nameLikeClause)
        {
            tenant[] ts = new tenant[] { }; // empty list

            string type = typeof(tenant).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(nameLikeClause)) throw new ArgumentNullException("identifierLikeClause or nameLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlParentField, "", type, "identifier", identifierLikeClause, "");
            if (!String.IsNullOrEmpty(nameLikeClause)) sql = String.Format(sqlParentNameOrLabelField, "", type, "name", nameLikeClause, "");
            DbParameter[] parms = new DbParameter[] { };
            var tenants = ctx.tenant.SqlQuery(sql, parms).ToList<tenant>();

            switch (tenants.Count)
            {
                case 0:
                    {
                        // do nothing - return empty list
                        break;
                    }
                default: //  return all objects
                    {
                        ts = tenants.ToArray<tenant>();
                        break;
                    }
            }

            return ts;
        }

        static public folder FindFolder(ModelMateEFModel9Context ctx, tenant t, string identifierLikeClause, string nameLikeClause)
        {
            folder f = null;

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(nameLikeClause)) throw new ArgumentNullException("identifierLikeClause or nameLikeClause", "Both parameters cannot be null");

            throw new NotImplementedException("FindFolder");

            return f;
        }
        static public folder FindFolder(ModelMateEFModel9Context ctx, tenant t, folder parentFolder, string identifierLikeClause, string nameLikeClause)
        {
            folder f = null;

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(nameLikeClause)) throw new ArgumentNullException("identifierLikeClause or nameLikeClause", "Both parameters cannot be null");

            throw new NotImplementedException("FindFolder");

            return f;
        }
        static public folder[] FindFolders(ModelMateEFModel9Context ctx, tenant t, folder parentFolder, string identifierLikeClause, string nameLikeClause)
        {
            folder[] fs = new folder[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");

            throw new NotImplementedException("FindFolders");

            return fs;
        }

        //        SELECT dbo.model.identifier, dbo.model.version, dbo.model.operation, dbo.model.model_Id, dbo.model.models_Id,
        //               dbo.tenant.tenant_Id, dbo.name.name_text
        //          FROM ((((dbo.tenant
        //          INNER JOIN dbo.folders ON dbo.tenant.tenant_Id = dbo.folders.tenant_Id)
        //          INNER JOIN dbo.folder ON dbo.folders.folders_Id = dbo.folder.folders_Id) 
        //          INNER JOIN dbo.models ON dbo.folder.folder_Id = dbo.models.folder_Id) 
        //          INNER JOIN dbo.model ON dbo.models.models_Id = dbo.model.models_Id) 
        //          INNER JOIN dbo.name ON dbo.model.model_Id = dbo.name.model_Id
        //          WHERE dbo.tenant.tenant_Id = 1601948856 AND dbo.name.name_text LIKE 'Test6 Model'

        const string sqlModelFieldByTenant = "SELECT {0} " +
                                            " dbo.model.identifier, dbo.model.version, dbo.model.operation, dbo.model.model_Id, dbo.model.models_Id" +
                                            "          FROM ((((dbo.tenant " +
                                            "          INNER JOIN dbo.folders ON dbo.tenant.tenant_Id = dbo.folders.tenant_Id) " +
                                            "          INNER JOIN dbo.folder ON dbo.folders.folders_Id = dbo.folder.folders_Id) " +
                                            "          INNER JOIN dbo.models ON dbo.folder.folder_Id = dbo.models.folder_Id) " +
                                            "          INNER JOIN dbo.model ON dbo.models.models_Id = dbo.model.models_Id) " +
                                            " WHERE dbo.tenant.identifier LIKE @TenantIdentifier AND dbo.model.{2} LIKE '{3}'";
        const string sqlModelNameOrLabelFieldByTenant = "SELECT {0} " +
                                            " dbo.model.identifier, dbo.model.version, dbo.model.operation, dbo.model.model_Id, dbo.model.models_Id" +
                                            "          FROM ((((dbo.tenant " +
                                            "          INNER JOIN dbo.folders ON dbo.tenant.tenant_Id = dbo.folders.tenant_Id) " +
                                            "          INNER JOIN dbo.folder ON dbo.folders.folders_Id = dbo.folder.folders_Id) " +
                                            "          INNER JOIN dbo.models ON dbo.folder.folder_Id = dbo.models.folder_Id) " +
                                            "          INNER JOIN dbo.model ON dbo.models.models_Id = dbo.model.models_Id) " +
                                            "          INNER JOIN dbo.name ON dbo.model.model_Id = dbo.name.model_Id " +
                                            " WHERE dbo.tenant.identifier LIKE @TenantIdentifier AND dbo.{2}.{2}_text LIKE '{3}'";

        static public model FindModel(ModelMateEFModel9Context ctx, tenant t, string identifierLikeClause, string nameLikeClause)
        {
            model m = null;
            string type = typeof(model).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (t == null) throw new ArgumentNullException("tenant"); // TODO allow searching across tenants (e.g. t == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(nameLikeClause)) throw new ArgumentNullException("identifierLikeClause or nameLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlModelFieldByTenant, "TOP 2", t.identifier, "identifier", identifierLikeClause);
            if (!String.IsNullOrEmpty(nameLikeClause)) sql = String.Format(sqlModelNameOrLabelFieldByTenant, "TOP 2", t.identifier, "name", nameLikeClause);
            SqlParameter mParm = new SqlParameter("@TenantIdentifier", t.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var models = ctx.model.SqlQuery(sql, parms).ToList<model>();

            switch (models.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        m = models[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException("identifierLikeClause or nameLikeClause", "Matched too many entities");
                    }
            }

            return m;
        }
        static public model FindModel(ModelMateEFModel9Context ctx, tenant t, folder f, string identifierLikeClause, string nameLikeClause)
        {
            model m = null;

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(nameLikeClause)) throw new ArgumentNullException("identifierLikeClause or nameLikeClause", "Both parameters cannot be null");

            if (t == null)
            {
                throw new NotImplementedException("t == null");
            }
            else
            {
                if (f == null)
                {
                    throw new NotImplementedException("f == null");
                }
                else
                {
                    throw new NotImplementedException("FindModel");
                }
            }

            return m;
        }
        static public model[] FindModels(ModelMateEFModel9Context ctx, tenant t, folder f, string identifierLikeClause, string nameLikeClause)
        {
            model[] ms = new model[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(nameLikeClause)) throw new ArgumentNullException("identifierLikeClause or nameLikeClause", "Both parameters cannot be null");

            if (t == null)
            {
                throw new NotImplementedException("t == null");
            }
            else
            {
                throw new NotImplementedException("FindModels");
            }

            return ms;
        }
        static public model[] FindModels(ModelMateEFModel9Context ctx, tenant t, string identifierLikeClause, string nameLikeClause)
        {
            model[] ms = new model[] { };

            string type = typeof(model).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (t == null) throw new ArgumentNullException("tenant"); // TODO allow searching across tenants (e.g. t == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(nameLikeClause)) throw new ArgumentNullException("identifierLikeClause or nameLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlModelFieldByTenant, "", t.identifier, "identifier", identifierLikeClause);
            if (!String.IsNullOrEmpty(nameLikeClause)) sql = String.Format(sqlModelNameOrLabelFieldByTenant, "", t.identifier, "name", nameLikeClause);
            SqlParameter mParm = new SqlParameter("@TenantIdentifier", t.identifier);
            //mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var models = ctx.model.SqlQuery(sql, parms).ToList<model>();

            switch (models.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: //  return all objects
                    {
                        ms = models.ToArray<model>();
                        break;
                    }
            }

            return ms;
        }

        const string sqlElementFieldByModel =
            "SELECT {0} dbo.element.identifier, dbo.element.type, dbo.element.operation, dbo.element.element_Id, dbo.element.elements_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.elements ON dbo.model.model_Id = dbo.elements.model_Id) " +
            "INNER JOIN dbo.element ON dbo.elements.elements_Id = dbo.element.elements_Id) " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.element.{2} LIKE '{3}'";
        const string sqlElementFieldByModelByType =
            "SELECT {0} dbo.element.identifier, dbo.element.type, dbo.element.operation, dbo.element.element_Id, dbo.element.elements_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.elements ON dbo.model.model_Id = dbo.elements.model_Id) " +
            "INNER JOIN dbo.element ON dbo.elements.elements_Id = dbo.element.elements_Id) " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.element.{2} LIKE '{3}' AND dbo.element.type LIKE '{4}'";
        const string sqlElementNameOrLabelFieldByModel =
            "SELECT {0} dbo.element.identifier, dbo.element.type, dbo.element.operation, dbo.element.element_Id, dbo.element.elements_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.elements ON dbo.model.model_Id = dbo.elements.model_Id) " +
            "INNER JOIN dbo.element ON dbo.elements.elements_Id = dbo.element.elements_Id) " +
            "INNER JOIN dbo.label ON dbo.element.element_Id = dbo.label.element_Id " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.{2}.{2}_text LIKE '{3}'";
        const string sqlElementNameOrLabelFieldByModelByType =
            "SELECT {0} dbo.element.identifier, dbo.element.type, dbo.element.operation, dbo.element.element_Id, dbo.element.elements_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.elements ON dbo.model.model_Id = dbo.elements.model_Id) " +
            "INNER JOIN dbo.element ON dbo.elements.elements_Id = dbo.element.elements_Id) " +
            "INNER JOIN dbo.label ON dbo.element.element_Id = dbo.label.element_Id " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.{2}.{2}_text LIKE '{3}' AND dbo.element.type LIKE '{4}'";

        static public element FindElement(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause)
        {
            element e = null;

            string type = typeof(element).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlElementFieldByModel, "TOP 2", m.identifier, "identifier", identifierLikeClause);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlElementNameOrLabelFieldByModel, "TOP 2", m.identifier, "label", labelLikeClause);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var elements = ctx.element.SqlQuery(sql, parms).ToList<element>();

            switch (elements.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        e = elements[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException("identifierLikeClause or labelLikeClause", "Matched too many entities");
                    }
            }

            return e;
        }
        static public element FindElement(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause, ModelConst.ElementType elementType)
        {
            element e = null;

            string type = typeof(element).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string etParameter = (elementType == ModelConst.ElementType.AllElementTypes ? "%" : elementType.ToString());

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlElementFieldByModelByType, "TOP 2", m.identifier, "identifier", identifierLikeClause, etParameter);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlElementNameOrLabelFieldByModelByType, "TOP 2", m.identifier, "label", labelLikeClause, etParameter);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var elements = ctx.element.SqlQuery(sql, parms).ToList<element>();

            switch (elements.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        e = elements[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException("identifierLikeClause or labelLikeClause", "Matched too many entities");
                    }
            }

            return e;
        }
        static public element[] FindElements(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause)
        {
            element[] es = new element[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlElementFieldByModel, "", m.identifier, "identifier", identifierLikeClause);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlElementNameOrLabelFieldByModel, "", m.identifier, "label", labelLikeClause);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var elements = ctx.element.SqlQuery(sql, parms).ToList<element>();

            switch (elements.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: //  return all objects
                    {
                        es = elements.ToArray<element>();
                        break;
                    }
            }

            return es;
        }
        static public element[] FindElements(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause, ModelConst.ElementType elementType)
        {
            element[] es = new element[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string etParameter = (elementType == ModelConst.ElementType.AllElementTypes ? "%" : elementType.ToString());

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlElementFieldByModelByType, "", m.identifier, "identifier", identifierLikeClause, etParameter);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlElementNameOrLabelFieldByModelByType, "", m.identifier, "label", labelLikeClause, etParameter);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var elements = ctx.element.SqlQuery(sql, parms).ToList<element>();

            switch (elements.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: //  return all objects
                    {
                        es = elements.ToArray<element>();
                        break;
                    }
            }

            return es;
        }
        static public element[] FindElements(ModelMateEFModel9Context ctx, tenant t, model m, ModelConst.ElementType elementType)
        {
            element[] es = new element[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)

            string etParameter = (elementType == ModelConst.ElementType.AllElementTypes ? "%" : elementType.ToString());

            string sql = "";
            sql = String.Format(sqlElementFieldByModel, "", m.identifier, "type", etParameter);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var elements = ctx.element.SqlQuery(sql, parms).ToList<element>();

            switch (elements.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: //  return all objects
                    {
                        es = elements.ToArray<element>();
                        break;
                    }
            }

            return es;
        }

        const string sqlRelationshipFieldByModel =
            "SELECT dbo.relationship.identifier, dbo.relationship.source, dbo.relationship.target, dbo.relationship.type, dbo.relationship.operation, dbo.relationship.relationship_Id, dbo.relationship.relationships_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.relationships ON dbo.model.model_Id = dbo.relationships.model_Id) " +
            "INNER JOIN dbo.relationship ON dbo.relationships.relationships_Id = dbo.relationship.relationships_Id) " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.relationship.{2} LIKE '{3}'";
        const string sqlRelationshipFieldByModelByType =
            "SELECT dbo.relationship.identifier, dbo.relationship.source, dbo.relationship.target, dbo.relationship.type, dbo.relationship.operation, dbo.relationship.relationship_Id, dbo.relationship.relationships_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.relationships ON dbo.model.model_Id = dbo.relationships.model_Id) " +
            "INNER JOIN dbo.relationship ON dbo.relationships.relationships_Id = dbo.relationship.relationships_Id) " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.relationship.{2} LIKE '{3}' AND dbo.relationship.type LIKE '{4}'";
        const string sqlRelationshipNameOrLabelFieldByModel =
            "SELECT dbo.relationship.identifier, dbo.relationship.source, dbo.relationship.target, dbo.relationship.type, dbo.relationship.operation, dbo.relationship.relationship_Id, dbo.relationship.relationships_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.relationships ON dbo.model.model_Id = dbo.relationships.model_Id) " +
            "INNER JOIN dbo.relationship ON dbo.relationships.relationships_Id = dbo.relationship.relationships_Id) " +
            "INNER JOIN dbo.label ON dbo.relationship.relationship_Id = dbo.label.relationship_Id " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.{2}.{2}_text LIKE '{3}'";
        const string sqlRelationshipNameOrLabelFieldByModelByType =
            "SELECT dbo.relationship.identifier, dbo.relationship.source, dbo.relationship.target, dbo.relationship.type, dbo.relationship.operation, dbo.relationship.relationship_Id, dbo.relationship.relationships_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.relationships ON dbo.model.model_Id = dbo.relationships.model_Id) " +
            "INNER JOIN dbo.relationship ON dbo.relationships.relationships_Id = dbo.relationship.relationships_Id) " +
            "INNER JOIN dbo.label ON dbo.relationship.relationship_Id = dbo.label.relationship_Id " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.{2}.{2}_text LIKE '{3}' AND dbo.relationship.type LIKE '{4}'";

        static public relationship FindRelationship(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause)
        {
            relationship r = null;

            string type = typeof(relationship).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlRelationshipFieldByModel, "TOP 2", m.identifier, "identifier", identifierLikeClause);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlRelationshipNameOrLabelFieldByModel, "TOP 2", m.identifier, "label", labelLikeClause);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var relationships = ctx.relationship.SqlQuery(sql, parms).ToList<relationship>();

            switch (relationships.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        r = relationships[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException("identifierLikeClause or labelLikeClause", "Matched too many entities");
                    }
            }

            return r;
        }
        static public relationship FindRelationship(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause, ModelConst.RelationshipType relationshipType)
        {
            relationship r = null;

            string type = typeof(relationship).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string rtParameter = (relationshipType == ModelConst.RelationshipType.AllRelationshipTypes ? "%" : relationshipType.ToString());

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlRelationshipFieldByModelByType, "TOP 2", m.identifier, "identifier", identifierLikeClause, rtParameter);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlRelationshipNameOrLabelFieldByModelByType, "TOP 2", m.identifier, "label", labelLikeClause, rtParameter);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var relationships = ctx.relationship.SqlQuery(sql, parms).ToList<relationship>();

            switch (relationships.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        r = relationships[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException("identifierLikeClause or labelLikeClause", "Matched too many entities");
                    }
            }

            return r;
        }
        static public relationship[] FindRelationships(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause)
        {
            relationship[] rs = new relationship[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlRelationshipFieldByModel, "", m.identifier, "identifier", identifierLikeClause);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlRelationshipNameOrLabelFieldByModel, "", m.identifier, "label", labelLikeClause);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var relationships = ctx.relationship.SqlQuery(sql, parms).ToList<relationship>();

            switch (relationships.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: //  return all objects
                    {
                        rs = relationships.ToArray<relationship>();
                        break;
                    }
            }

            return rs;
        }
        static public relationship[] FindRelationships(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause, ModelConst.RelationshipType relationshipType)
        {
            relationship[] rs = new relationship[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string rtParameter = (relationshipType == ModelConst.RelationshipType.AllRelationshipTypes ? "%" : relationshipType.ToString());

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlRelationshipFieldByModelByType, "", m.identifier, "identifier", identifierLikeClause, rtParameter);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlRelationshipNameOrLabelFieldByModelByType, "", m.identifier, "label", labelLikeClause, rtParameter);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var relationships = ctx.relationship.SqlQuery(sql, parms).ToList<relationship>();

            switch (relationships.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        rs = relationships.ToArray<relationship>();
                        break;
                    }
            }

            return rs;
        }
        static public relationship[] FindRelationships(ModelMateEFModel9Context ctx, tenant t, model m, ModelConst.RelationshipType relationshipType)
        {
            relationship[] rs = new relationship[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)

            string rtParameter = (relationshipType == ModelConst.RelationshipType.AllRelationshipTypes ? "%" : relationshipType.ToString());

            string sql = "";
            sql = String.Format(sqlRelationshipFieldByModel, "", m.identifier, "type", rtParameter);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var relationships = ctx.relationship.SqlQuery(sql, parms).ToList<relationship>();

            switch (relationships.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        rs = relationships.ToArray<relationship>();
                        break;
                    }
            }

            return rs;
        }

        const string sqlViewFieldByModel =
            "SELECT dbo.view.identifier, dbo.view.viewpoint, dbo.view.operation, dbo.view.view_Id, dbo.view.views_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.views ON dbo.model.model_Id = dbo.views.model_Id) " +
            "INNER JOIN dbo.view ON dbo.views.views_Id = dbo.view.views_Id) " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.view.{2} LIKE '{3}'";
        const string sqlViewFieldByModelByType =
            "SELECT dbo.view.identifier, dbo.view.viewpoint, dbo.view.operation, dbo.view.view_Id, dbo.view.views_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.views ON dbo.model.model_Id = dbo.views.model_Id) " +
            "INNER JOIN dbo.view ON dbo.views.views_Id = dbo.view.views_Id) " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.view.{2} LIKE '{3}' AND dbo.view.viewpoint LIKE '{4}'";
        const string sqlViewNameOrLabelFieldByModel =
            "SELECT dbo.view.identifier, dbo.view.viewpoint, dbo.view.operation, dbo.view.view_Id, dbo.view.views_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.views ON dbo.model.model_Id = dbo.views.model_Id) " +
            "INNER JOIN dbo.view ON dbo.views.views_Id = dbo.view.views_Id) " +
            "INNER JOIN dbo.label ON dbo.view.view_Id = dbo.label.view_Id " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.{2}.{2}_text LIKE '{3}'";
        const string sqlViewNameOrLabelFieldByModelByType =
            "SELECT dbo.view.identifier, dbo.view.viewpoint, dbo.view.operation, dbo.view.view_Id, dbo.view.views_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.views ON dbo.model.model_Id = dbo.views.model_Id) " +
            "INNER JOIN dbo.view ON dbo.views.views_Id = dbo.view.views_Id) " +
            "INNER JOIN dbo.label ON dbo.view.view_Id = dbo.label.view_Id " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.{2}.{2}_text LIKE '{3}' AND dbo.view.viewpoint LIKE '{4}'";

        static public view FindView(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause)
        {
            view v = null;

            string type = typeof(view).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlViewFieldByModel, "TOP 2", m.identifier, "identifier", identifierLikeClause);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlViewNameOrLabelFieldByModel, "TOP 2", m.identifier, "label", labelLikeClause);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var views = ctx.view.SqlQuery(sql, parms).ToList<view>();

            switch (views.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        v = views[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException("identifierLikeClause or labelLikeClause", "Matched too many entities");
                    }
            }

            return v;
        }
        static public view FindView(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause, ModelConst.ViewType viewType)
        {
            view v = null;

            string type = typeof(view).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string vtParameter = (viewType == ModelConst.ViewType.AllViewTypes ? "%" : viewType.ToString());

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlViewFieldByModelByType, "TOP 2", m.identifier, "identifier", identifierLikeClause, vtParameter);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlViewNameOrLabelFieldByModelByType, "TOP 2", m.identifier, "label", labelLikeClause, vtParameter);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var views = ctx.view.SqlQuery(sql, parms).ToList<view>();

            switch (views.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        v = views[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException("identifierLikeClause or labelLikeClause", "Matched too many entities");
                    }
            }

            return v;
        }
        static public view[] FindViews(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause)
        {
            view[] vs = new view[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlViewFieldByModel, "", m.identifier, "identifier", identifierLikeClause);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlViewNameOrLabelFieldByModel, "", m.identifier, "label", labelLikeClause);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var views = ctx.view.SqlQuery(sql, parms).ToList<view>();

            switch (views.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        vs = views.ToArray<view>();
                        break;
                    }
            }

            return vs;
        }
        static public view[] FindViews(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause, ModelConst.ViewType viewType)
        {
            view[] vs = new view[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string vtParameter = (viewType == ModelConst.ViewType.AllViewTypes ? "%" : viewType.ToString());

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlViewFieldByModelByType, "", m.identifier, "identifier", identifierLikeClause, vtParameter);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlViewNameOrLabelFieldByModelByType, "", m.identifier, "label", labelLikeClause, vtParameter);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var views = ctx.view.SqlQuery(sql, parms).ToList<view>();

            switch (views.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        vs = views.ToArray<view>();
                        break;
                    }
            }

            return vs;
        }
        static public view[] FindViews(ModelMateEFModel9Context ctx, tenant t, model m, ModelConst.ViewType viewType)
        {
            view[] vs = new view[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)

            string vtParameter = (viewType == ModelConst.ViewType.AllViewTypes ? "%" : viewType.ToString());

            string sql = "";
            sql = String.Format(sqlViewFieldByModel, "", m.identifier, "viewpoint", vtParameter);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var views = ctx.view.SqlQuery(sql, parms).ToList<view>();

            switch (views.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        vs = views.ToArray<view>();
                        break;
                    }
            }

            return vs;
        }

        const string sqlPropertyByParentByName =
            "SELECT {0} dbo.property.identifierref, dbo.property.operation, dbo.property.property_Id, dbo.property.metadata_Id, dbo.propertydef.name, dbo.propertydef.type, dbo.value.lang, dbo.value.value_text " +
            "FROM (((dbo.{1} " +
            "INNER JOIN dbo.properties ON dbo.{1}.{1}_Id = dbo.properties.{1}_Id) " +
            "INNER JOIN dbo.property ON dbo.properties.properties_Id = dbo.property.properties_Id) " +
            "INNER JOIN dbo.value ON dbo.property.property_Id = dbo.value.property_Id) " +
            "INNER JOIN dbo.propertydef ON CONVERT(VARCHAR(MAX),dbo.property.identifierref) = CONVERT(VARCHAR(MAX),dbo.propertydef.identifier) " +
            "WHERE dbo.{1}.identifier LIKE '{2}' AND dbo.propertydef.name LIKE '{3}'";
        const string sqlPropertyByParentByType =
            "SELECT {0} dbo.property.identifierref, dbo.property.operation, dbo.property.property_Id, dbo.property.metadata_Id, dbo.propertydef.name, dbo.propertydef.type, dbo.value.lang, dbo.value.value_text " +
            "FROM (((dbo.{1} " +
            "INNER JOIN dbo.properties ON dbo.{1}.{1}_Id = dbo.properties.{1}_Id) " +
            "INNER JOIN dbo.property ON dbo.properties.properties_Id = dbo.property.properties_Id) " +
            "INNER JOIN dbo.value ON dbo.property.property_Id = dbo.value.property_Id) " +
            "INNER JOIN dbo.propertydef ON CONVERT(VARCHAR(MAX),dbo.property.identifierref) = CONVERT(VARCHAR(MAX),dbo.propertydef.identifier) " +
            "WHERE dbo.{1}.identifier LIKE '{2}' AND dbo.propertydef.type LIKE '{3}'";
        const string sqlPropertyByParentByValue =
            "SELECT {0} dbo.property.identifierref, dbo.property.operation, dbo.property.property_Id, dbo.property.metadata_Id, dbo.propertydef.name, dbo.propertydef.type, dbo.value.lang, dbo.value.value_text " +
            "FROM (((dbo.{1} " +
            "INNER JOIN dbo.properties ON dbo.{1}.{1}_Id = dbo.properties.{1}_Id) " +
            "INNER JOIN dbo.property ON dbo.properties.properties_Id = dbo.property.properties_Id) " +
            "INNER JOIN dbo.value ON dbo.property.property_Id = dbo.value.property_Id) " +
            "INNER JOIN dbo.propertydef ON CONVERT(VARCHAR(MAX),dbo.property.identifierref) = CONVERT(VARCHAR(MAX),dbo.propertydef.identifier) " +
            "WHERE dbo.{1}.identifier LIKE '{2}' AND dbo.value.value_text LIKE '{3}'";

        static public property FindProperty(ModelMateEFModel9Context ctx, string parentType, string parentIdentifier, string propertyName)
        {
            property p = null;

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (String.IsNullOrEmpty(parentType)) throw new ArgumentNullException("parentType");
            if (String.IsNullOrEmpty(parentIdentifier)) throw new ArgumentNullException("parentIdentifier");
            if (String.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("propertyName");

            int underPos = parentType.IndexOf("_");
            if (underPos > 0) parentType = parentType.Substring(0, underPos);

            string sql = "";
            sql = String.Format(sqlPropertyByParentByName, "TOP 2", parentType, parentIdentifier, propertyName);
            DbParameter[] parms = new DbParameter[] { };
            var properties = ctx.property.SqlQuery(sql, parms).ToList<property>();

            switch (properties.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        p = properties[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException(parentType + " " + parentIdentifier + " '" + propertyName + "'", "Matched too many entities");
                    }
            }

            return p;
        }
        static public property[] FindProperties(ModelMateEFModel9Context ctx, string parentType, string parentIdentifier, string propertyName)
        {
            property[] ps = new property[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (String.IsNullOrEmpty(parentType)) throw new ArgumentNullException("parentType");
            if (String.IsNullOrEmpty(parentIdentifier)) throw new ArgumentNullException("parentIdentifier");
            if (String.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("propertyName");

            int underPos = parentType.IndexOf("_");
            if (underPos > 0) parentType = parentType.Substring(0, underPos);

            string sql = "";
            sql = String.Format(sqlPropertyByParentByName, "", parentType, parentIdentifier, propertyName);
            DbParameter[] parms = new DbParameter[] { };
            var properties = ctx.property.SqlQuery(sql, parms).ToList<property>();

            switch (properties.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        ps = properties.ToArray<property>();
                        break;
                    }
            }

            return ps;
        }
        static public property[] FindPropertiesByType(ModelMateEFModel9Context ctx, string parentType, string parentIdentifier, ModelConst.PropertyDataType dataType)
        {
            property[] ps = new property[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (String.IsNullOrEmpty(parentType)) throw new ArgumentNullException("parentType");
            if (String.IsNullOrEmpty(parentIdentifier)) throw new ArgumentNullException("parentIdentifier");

            int underPos = parentType.IndexOf("_");
            if (underPos > 0) parentType = parentType.Substring(0, underPos);

            string sql = "";
            sql = String.Format(sqlPropertyByParentByType, "", parentType, parentIdentifier, dataType.ToString());
            DbParameter[] parms = new DbParameter[] { };
            var properties = ctx.property.SqlQuery(sql, parms).ToList<property>();

            switch (properties.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        ps = properties.ToArray<property>();
                        break;
                    }
            }

            return ps;
        }
        static public property[] FindPropertiesByValue(ModelMateEFModel9Context ctx, string parentType, string parentIdentifier, string propertyValue)
        {
            property[] ps = new property[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (String.IsNullOrEmpty(parentType)) throw new ArgumentNullException("parentType");
            if (String.IsNullOrEmpty(parentIdentifier)) throw new ArgumentNullException("parentIdentifier");
            if (String.IsNullOrEmpty(propertyValue)) throw new ArgumentNullException("propertyValue");

            int underPos = parentType.IndexOf("_");
            if (underPos > 0) parentType = parentType.Substring(0, underPos);

            string sql = "";
            sql = String.Format(sqlPropertyByParentByValue, "", parentType, parentIdentifier, propertyValue.ToString()); // TODO DateTime
            DbParameter[] parms = new DbParameter[] { };
            var properties = ctx.property.SqlQuery(sql, parms).ToList<property>();

            switch (properties.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        ps = properties.ToArray<property>();
                        break;
                    }
            }

            return ps;
        }

        const string sqlPropertyDefByModel =
            "SELECT {0} dbo_propertydef.identifier, dbo_propertydef.name, dbo_propertydef.type, dbo_propertydef.propertydefs_Id, dbo_propertydef.propertydef_Id " +
            "FROM (dbo_model " +
            "INNER JOIN dbo_propertydefs ON dbo_model.model_Id = dbo_propertydefs.model_Id) " +
            "INNER JOIN dbo_propertydef ON dbo_propertydefs.propertydefs_Id = dbo_propertydef.propertydefs_Id " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo_propertydef.{1} LIKE '{2}'";

        static public propertydef FindPropertyDef(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string nameLikeClause)
        {
            propertydef v = null;

            string type = typeof(propertydef).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(nameLikeClause)) throw new ArgumentNullException("identifierLikeClause or nameLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlPropertyDefByModel, "TOP 2", "identifier", identifierLikeClause);
            if (!String.IsNullOrEmpty(nameLikeClause)) sql = String.Format(sqlPropertyDefByModel, "TOP 2", "name", nameLikeClause);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var propertydefs = ctx.propertydef.SqlQuery(sql, parms).ToList<propertydef>();

            switch (propertydefs.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        v = propertydefs[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException("identifierLikeClause or nameLikeClause", "Matched too many entities");
                    }
            }

            return v;
        }
        static public propertydef[] FindPropertyDefs(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string nameLikeClause)
        {
            propertydef[] ps = new propertydef[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(nameLikeClause)) throw new ArgumentNullException("identifierLikeClause or nameLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlPropertyDefByModel, "", "identifier", identifierLikeClause);
            if (!String.IsNullOrEmpty(nameLikeClause)) sql = String.Format(sqlPropertyDefByModel, "", "name", nameLikeClause);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var propertydefs = ctx.propertydef.SqlQuery(sql, parms).ToList<propertydef>();

            switch (propertydefs.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        ps = propertydefs.ToArray<propertydef>();
                        break;
                    }
            }

            return ps;
        }
        static public propertydef[] FindPropertyDefs(ModelMateEFModel9Context ctx, tenant t, model m, ModelConst.PropertyDataType dataType)
        {
            propertydef[] ps = new propertydef[] { };

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)

            string sql = "";
            sql = String.Format(sqlPropertyDefByModel, "", "type", dataType.ToString());
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var propertydefs = ctx.propertydef.SqlQuery(sql, parms).ToList<propertydef>();

            switch (propertydefs.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        ps = propertydefs.ToArray<propertydef>();
                        break;
                    }
            }

            return ps;
        }

        const string sqlItemByModel =
            "SELECT {0} dbo.item.identifier, dbo.item.identifierref, dbo.item.item_Id, dbo.item.item_Id_0, dbo.item.organization_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.organization ON dbo.model.model_Id = dbo.organization.model_Id) " +
            "INNER JOIN dbo.item ON dbo.organization.organization_Id = dbo.item.organization_Id) " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.item.identifier LIKE '{1}'";
        const string sqlItemNameOrLabelFieldByModel =
            "SELECT {0} dbo.item.identifier, dbo.item.identifierref, dbo.item.item_Id, dbo.item.item_Id_0, dbo.item.organization_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.organization ON dbo.model.model_Id = dbo.organization.model_Id) " +
            "INNER JOIN dbo.item ON dbo.organization.organization_Id = dbo.item.organization_Id) " +
            "INNER JOIN dbo.label ON dbo.item.item_Id = dbo.label.item_Id " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.label.label_text LIKE '{1}'";

        static public item FindItem(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause)
        {
            item i = null;

            string type = typeof(element).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlItemByModel, "TOP 2", identifierLikeClause);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlItemNameOrLabelFieldByModel, "TOP 2", labelLikeClause);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var items = ctx.item.SqlQuery(sql, parms).ToList<item>();

            switch (items.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                case 1:
                    {
                        i = items[0];
                        break;
                    }
                default: // case 2 because of TOP 2 in SELECT query
                    {
                        throw new ArgumentOutOfRangeException("identifierLikeClause or labelLikeClause", "Matched too many entities");
                    }
            }

            return i;
        }
        static public item[] FindItems(ModelMateEFModel9Context ctx, tenant t, model m, string identifierLikeClause, string labelLikeClause)
        {
            item[] iis = new item[] { };

            string type = typeof(item).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlItemByModel, "", identifierLikeClause);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlItemNameOrLabelFieldByModel, "", labelLikeClause);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm };
            var items = ctx.item.SqlQuery(sql, parms).ToList<item>();

            switch (items.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        iis = items.ToArray<item>();
                        break;
                    }
            }

            return iis;
        }

        const string sqlItemByModelByItem =
            "SELECT {0} dbo.item.identifier, dbo.item.identifierref, dbo.item.item_Id, dbo.item.item_Id_0, dbo.item.organization_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.organization ON dbo.model.model_Id = dbo.organization.model_Id) " +
            "INNER JOIN dbo.item ON dbo.organization.organization_Id = dbo.item.organization_Id) " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.item.identifier LIKE @ItemIdentifier AND dbo.item.identifier LIKE '{1}'";
        const string sqlItemNameOrLabelFieldByModelByItem =
            "SELECT {0} dbo.item.identifier, dbo.item.identifierref, dbo.item.item_Id, dbo.item.item_Id_0, dbo.item.organization_Id " +
            "FROM ((dbo.model " +
            "INNER JOIN dbo.organization ON dbo.model.model_Id = dbo.organization.model_Id) " +
            "INNER JOIN dbo.item ON dbo.organization.organization_Id = dbo.item.organization_Id) " +
            "INNER JOIN dbo.label ON dbo.item.item_Id = dbo.label.item_Id " +
            "WHERE dbo.model.identifier LIKE @ModelIdentifier AND dbo.item.identifier LIKE @ItemIdentifier AND dbo.label.label_text LIKE '{1}'";

        static public item[] FindItems(ModelMateEFModel9Context ctx, tenant t, model m, item i, string identifierLikeClause, string labelLikeClause)
        {
            item[] iis = new item[] { };

            string type = typeof(item).Name; // short name

            if (ctx == null) throw new ArgumentNullException("ctx");
            if (m == null) throw new ArgumentNullException("model"); // TODO allow searching across models (e.g. m == null)
            if (String.IsNullOrEmpty(identifierLikeClause) && String.IsNullOrEmpty(labelLikeClause)) throw new ArgumentNullException("identifierLikeClause or labelLikeClause", "Both parameters cannot be null");

            string sql = "";
            if (!String.IsNullOrEmpty(identifierLikeClause)) sql = String.Format(sqlItemByModelByItem, "", identifierLikeClause);
            if (!String.IsNullOrEmpty(labelLikeClause)) sql = String.Format(sqlItemNameOrLabelFieldByModelByItem, "", labelLikeClause);
            SqlParameter mParm = new SqlParameter("@ModelIdentifier", m.identifier);
            SqlParameter iParm = new SqlParameter("@ItemIdentifier", i.identifier);
            // mParm.SqlDbType = SqlDbType.Text;
            DbParameter[] parms = new DbParameter[] { mParm, iParm };
            var items = ctx.item.SqlQuery(sql, parms).ToList<item>();

            switch (items.Count)
            {
                case 0:
                    {
                        // do nothing - return null
                        break;
                    }
                default: // return all objects
                    {
                        iis = items.ToArray<item>();
                        break;
                    }
            }

            return iis;
        }

    }
}
