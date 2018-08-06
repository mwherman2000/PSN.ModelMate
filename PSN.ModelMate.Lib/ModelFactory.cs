using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;
using Parallelspace.Content.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN.ModelMate.Lib
{
    public static partial class ModelFactory
    {
        private static string tenantName = "Unknown";
        public static string TenantName
        {
            get
            {
                return tenantName;
            }

            set
            {
                tenantName = value.TrimStart(new char[] { ' ' }).TrimEnd(new char[] { ' ' });
            }
        }

        private static string modelName = "Unknown";
        public static string ModelName
        {
            get
            {
                return modelName;
            }

            set
            {
                modelName = value.TrimStart(new char[] { ' ' }).TrimEnd(new char[] { ' ' });
            }
        }

        public static processinghistory NewProcessinghistory(timesnap timesnapIn)
        {
            return NewProcessinghistory(new timesnap[] { timesnapIn });
        }
        public static processinghistory NewProcessinghistory(timesnap[] timesnapsIn)
        {
            processinghistory processinghistory = new processinghistory()
            {
                processinghistory_Id = Util.MakeIdInt32(),
            };

            if (timesnapsIn != null)
            {
                foreach (timesnap t in timesnapsIn)
                {
                    t.category = ModelConst.TimesnapCategory.processinghistory.ToString();
                    processinghistory.timesnap.Add(t);
                }
            }

            return processinghistory;
        }

        public static usage NewUsage(timesnap timesnapIn)
        {
            return NewUsage(new timesnap[] { timesnapIn });
        }
        public static usage NewUsage(timesnap[] timesnapsIn)
        {
            usage usage = new usage()
            {
                usage_Id = Util.MakeIdInt32(),
            };

            if (timesnapsIn != null)
            {
                foreach (timesnap t in timesnapsIn)
                {
                    t.category = ModelConst.TimesnapCategory.usage.ToString();
                    usage.timesnap.Add(t);
                }
            }

            return usage;
        }

        public static performance NewPerformance(timesnap timesnapIn)
        {
            return NewPerformance(new timesnap[] { timesnapIn });
        }
        public static performance NewPerformance(timesnap[] timesnapsIn)
        {
            performance performance = new performance()
            {
                performance_Id = Util.MakeIdInt32(),
            };

            if (timesnapsIn != null)
            {
                foreach (timesnap t in timesnapsIn)
                {
                    t.category = ModelConst.TimesnapCategory.performance.ToString();
                    performance.timesnap.Add(t);
                }
            }

            return performance;
        }

        public static management NewManagement(timesnap timesnapIn)
        {
            return NewManagement(new timesnap[] { timesnapIn });
        }
        public static management NewManagement(timesnap[] timesnapsIn)
        {
            management management = new management()
            {
                management_Id = Util.MakeIdInt32(),
            };

            if (timesnapsIn != null)
            {
                foreach (timesnap t in timesnapsIn)
                {
                    t.category = ModelConst.TimesnapCategory.management.ToString();
                    management.timesnap.Add(t);
                }
            }

            return management;
        }

        public static timesnap NewTimesnap(DateTime timestampIn, string labelIn, ModelConst.TimesnapCategory categoryIn, string subcategoryIn, string schemaIn, string schemaversionIn, property propertyIn)
        {
            return NewTimesnap(timestampIn, labelIn, null, categoryIn, subcategoryIn, schemaIn, schemaversionIn, NewProperties(propertyIn), null);
        }
        public static timesnap NewTimesnap(DateTime timestampIn, string labelIn, string langIn, ModelConst.TimesnapCategory categoryIn, string subcategoryIn, string schemaIn, string schemaversionIn, property propertyIn)
        {
            return NewTimesnap(timestampIn, labelIn, langIn, categoryIn, subcategoryIn, schemaIn, schemaversionIn, NewProperties(propertyIn), null);
        }
        public static timesnap NewTimesnap(DateTime timestampIn, string labelIn, ModelConst.TimesnapCategory categoryIn, string subcategoryIn, string schemaIn, string schemaversionIn, properties propertiesIn )
        {
            return NewTimesnap(timestampIn, labelIn, null, categoryIn, subcategoryIn, schemaIn, schemaversionIn, propertiesIn, null);
        }
        public static timesnap NewTimesnap(DateTime timestampIn, string labelIn, string langIn, ModelConst.TimesnapCategory categoryIn, string subcategoryIn, string schemaIn, string schemaversionIn, properties propertiesIn )
        {
            return NewTimesnap(timestampIn, labelIn, langIn, categoryIn, subcategoryIn, schemaIn, schemaversionIn, propertiesIn, null);
        }
        public static timesnap NewTimesnap(DateTime timestampIn, string labelIn, ModelConst.TimesnapCategory categoryIn, string subcategoryIn, string schemaIn, string schemaversionIn, properties propertiesIn, documentation documentationIn)
        {
            return NewTimesnap(timestampIn, labelIn, null, categoryIn, subcategoryIn, schemaIn, schemaversionIn, propertiesIn, documentationIn);
        }
        public static timesnap NewTimesnap(DateTime timestampIn, string labelIn, string langIn, ModelConst.TimesnapCategory categoryIn, string subcategoryIn, string schemaIn, string schemaversionIn, properties propertiesIn, documentation documentationIn)
        {
            if (timestampIn == null) throw new ArgumentNullException("timestamp");

            timesnap timesnap = new timesnap()
            {
                timesnap_Id = Util.MakeIdInt32(),
                identifier = Util.MakeIdentifierTimestamped(ModelConst.TIMESNAP_PREFIX, DateTime.Now),
                //identifier = Util.MakeIdentifierFromParentIdentifier(
                //    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName), 
                //    typeof(timesnap).Name, Util.TrimWhitespace(labelIn)),
                operation = PCOOperation.atrest.ToString(),
            };

            timesnap.timestamp = timestampIn;

            timesnap.category = categoryIn.ToString();
            if (!String.IsNullOrEmpty(subcategoryIn)) timesnap.subcategory = subcategoryIn;

            if (!String.IsNullOrEmpty(labelIn))
            {
                timesnap.label.Add(NewLabel(labelIn, langIn));

                timesnap.identifier = Util.MakeIdentifierFromParentIdentifier(
                                            Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                                            typeof(timesnap).Name, Util.TrimWhitespace(labelIn));
            }

            if (!String.IsNullOrEmpty(schemaIn)) timesnap.schema = schemaIn;
            if (!String.IsNullOrEmpty(schemaIn)) timesnap.schemaversion = schemaversionIn;

            if (propertiesIn != null)
            {
                timesnap.properties.Add(propertiesIn);
            }

            if (documentationIn != null)
            {
                timesnap.documentation.Add(documentationIn);
            }

            return timesnap;
        }

        public static node NewNode(string labelIn, int x, int y, int w, int h, element elementrefIn)
        {
            return NewNode(labelIn, null, x, y, h, w, elementrefIn, null, null, null, null);
        }
        public static node NewNode(string labelIn, string langIn, int x, int y, int w, int h, element elementrefIn)
        {
            return NewNode(labelIn, langIn, x, y, h, w, elementrefIn, null, null, null, null);
        }
        public static node NewNode(string labelIn, int x, int y, int w, int h, element elementrefIn, properties propertiesIn, string typeIn, style styleIn, node[] nodesIn)
        {
            return NewNode(labelIn, null, x, y, h, w, elementrefIn, propertiesIn, typeIn, styleIn, nodesIn);
        }
        public static node NewNode(string labelIn, string langIn, int x, int y, int w, int h, element elementrefIn, properties propertiesIn, string typeIn, style styleIn, node[] nodesIn )
        {
            if (elementrefIn != null & String.IsNullOrEmpty(elementrefIn.identifier)) throw new ArgumentNullException("elementrefIn.identifier");

            node node = new node()
            {
                node_Id = Util.MakeIdInt32(),
                identifier = Util.MakeIdentifierTimestamped(ModelConst.NODE_PREFIX, DateTime.Now),
                //identifier = Util.MakeIdentifierFromParentIdentifier(
                //    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                //    typeof(node).Name, Util.TrimWhitespace(labelIn)),
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(labelIn))
            {
                node.label.Add(NewLabel(labelIn, langIn));

                node.identifier = Util.MakeIdentifierFromParentIdentifier(
                                    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                                    typeof(node).Name, labelIn);
            }

            node.x = x;
            node.y = y;
            //node.z = z;
            node.w = w;
            node.h = h;
            //node.d = d;

            if (!String.IsNullOrWhiteSpace(typeIn))
            {
                node.type = typeIn;
            }

            if (elementrefIn != null)
            {
                node.elementref = elementrefIn.identifier;
            }

            if (nodesIn != null)
            {
                foreach (node n in nodesIn)
                {
                    node.node1.Add(n);
                }
            }

            if (propertiesIn != null)
            {
                node.properties.Add(propertiesIn);
            }

            if (styleIn != null)
            {
                node.style.Add(styleIn);
            }

            return node;
        }
        public static node NewNode(string labelIn, int x, int y, int z, int w, int h, int d, element elementrefIn )
        {
            return NewNode(labelIn, null, x, y, z, h, w, d, elementrefIn, null, null, null, null);
        }
        public static node NewNode(string labelIn, string langIn, int x, int y, int z, int w, int h, int d, element elementrefIn)
        {
            return NewNode(labelIn, langIn, x, y, z, h, w, d, elementrefIn, null, null, null, null);
        }
        public static node NewNode(string labelIn, int x, int y, int z, int w, int h, int d, element elementrefIn, properties propertiesIn, string typeIn, style styleIn, node[] nodesIn )
        {
            return NewNode(labelIn, null, x, y, z, h, w, d, elementrefIn, propertiesIn, typeIn, styleIn, nodesIn);
        }
        public static node NewNode(string labelIn, string langIn, int x, int y, int z, int w, int h, int d, element elementrefIn, properties propertiesIn, string typeIn, style styleIn, node[] nodesIn )
        {
            if (elementrefIn != null & String.IsNullOrEmpty(elementrefIn.identifier)) throw new ArgumentNullException("elementrefIn.identifier");

            node node = new node()
            {
                node_Id = Util.MakeIdInt32(),
                identifier = Util.MakeIdentifierTimestamped(ModelConst.NODE_PREFIX, DateTime.Now),
                //identifier = Util.MakeIdentifierFromParentIdentifier(
                //    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                //    typeof(node).Name, Util.TrimWhitespace(labelIn)),
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(labelIn))
            {
                node.label.Add(NewLabel(labelIn, langIn));

                node.identifier = Util.MakeIdentifierFromParentIdentifier(
                                    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                                    typeof(node).Name, Util.TrimWhitespace(labelIn));
            }

            node.x = x;
            node.y = y;
            node.z = z;
            node.w = w;
            node.h = h;
            node.d = d;

            if (!String.IsNullOrWhiteSpace(typeIn))
            {
                node.type = typeIn;
            }

            if (elementrefIn != null)
            {
                node.elementref = elementrefIn.identifier;
            }

            if (nodesIn != null)
            {
                foreach (node n in nodesIn)
                {
                    node.node1.Add(n);
                }
            }

            if (propertiesIn != null)
            {
                node.properties.Add(propertiesIn);
            }

            if (styleIn != null)
            {
                node.style.Add(styleIn);
            }

            return node;
        }

        public static connection NewConnection(string labelIn, node nSourceIn, node nTargetIn, relationship relationshiprefIn)
        {
            return NewConnection(labelIn, null, nSourceIn, nTargetIn, relationshiprefIn, null, null);
        }
        public static connection NewConnection(string labelIn, string langIn, node nSourceIn, node nTargetIn, relationship relationshiprefIn)
        {
            return NewConnection(labelIn, langIn, nSourceIn, nTargetIn, relationshiprefIn, null, null);
        }
        public static connection NewConnection(string labelIn, node nSourceIn, node nTargetIn, relationship relationshiprefIn, properties propertiesIn, style styleIn)
        {
            return NewConnection(labelIn, null, nSourceIn, nTargetIn, relationshiprefIn, propertiesIn, styleIn);
        }
        public static connection NewConnection(string labelIn, string langIn, node nSourceIn, node nTargetIn, relationship relationshiprefIn, properties propertiesIn, style styleIn)
        {
            if (nSourceIn == null) throw new ArgumentNullException("nSourceIn");
            if (nTargetIn == null) throw new ArgumentNullException("targetIn");

            if (String.IsNullOrEmpty(nSourceIn.identifier)) throw new ArgumentNullException("nSourceIn.identifier");
            if (String.IsNullOrEmpty(nTargetIn.identifier)) throw new ArgumentNullException("nTargetIn.identifier");

            if (relationshiprefIn != null && String.IsNullOrEmpty(relationshiprefIn.identifier)) throw new ArgumentNullException("relationshipref.identifier");

            connection connection = new connection
            {
                connection_Id = Util.MakeIdInt32(),
                identifier = Util.MakeIdentifierTimestamped(ModelConst.CONNECTION_PREFIX, DateTime.Now),
                //identifier = Util.MakeIdentifierFromParentIdentifier(
                //    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                //    typeof(connection).Name, Util.TrimWhitespace(labelIn));
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(labelIn))
            {
                connection.label.Add(NewLabel(labelIn, langIn));

                connection.identifier = Util.MakeIdentifierFromParentIdentifier(
                                        Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                                        typeof(connection).Name, Util.TrimWhitespace(labelIn));
            }

            connection.source = nSourceIn.identifier;
            connection.target = nTargetIn.identifier;

            if (relationshiprefIn != null)
            {
                connection.relationshipref = relationshiprefIn.identifier;
            }

            if (propertiesIn != null)
            {
                connection.properties.Add(propertiesIn);
            }

            if (styleIn != null)
            {
                connection.style.Add(styleIn);
            }

            return connection;
        }

        public static style NewStyle(fillColor fillColorIn, lineColor lineColorIn, font fontIn)
        {
            style style = new style()
            {
                style_Id = Util.MakeIdInt32(),
                operation = PCOOperation.atrest.ToString(),
            };

            if (fillColorIn != null)
            {
                style.fillColor.Add(fillColorIn);
            }

            if (lineColorIn != null)
            {
                style.lineColor.Add(lineColorIn);
            }

            if (fontIn != null)
            {
                style.font.Add(fontIn);
            }

            //style.lineWidth = lineWidthIn;

            return style;
        }
        public static style NewStyle(fillColor fillColorIn, lineColor lineColorIn, font fontIn, long lineWidthIn)
        {
            style style = new style()
            {
                style_Id = Util.MakeIdInt32(),
                operation = PCOOperation.atrest.ToString(),
            };

            if (fillColorIn != null)
            {
                style.fillColor.Add(fillColorIn);
            }

            if (lineColorIn != null)
            {
                style.lineColor.Add(lineColorIn);
            }

            if (fontIn != null)
            {
                style.font.Add(fontIn);
            }

            style.lineWidth = lineWidthIn;

            return style;
        }

        public static font NewFont(string nameTextIn, float sizeIn, string styleIn)
        {
            return NewFont(nameTextIn, sizeIn, styleIn, null);
        }
        public static font NewFont(string nameTextIn, float sizeIn, string styleIn, color colorIn)
        {
            font font = new font()
            {
                font_Id = Util.MakeIdInt32(),
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(nameTextIn)) font.name = Util.TrimWhitespace(nameTextIn); 

            font.size = sizeIn;

            if (!String.IsNullOrEmpty(styleIn)) font.style = Util.TrimWhitespace(styleIn); 

            if (colorIn != null)
            {
                font.color.Add(colorIn);
            }

            return font;
        }

        public static fillColor NewFillColor(int r, int g, int b)
        {
            return NewFillColor(r, g, b, ModelConst.ALPHA_MAX);
        }
        public static fillColor NewFillColor(int r, int g, int b, int a)
        {
            fillColor fillColor = new fillColor()
            {
                fillColor_Id = Util.MakeIdInt32(),
                operation = PCOOperation.atrest.ToString(),
            };

            fillColor.r = r;
            fillColor.g = g;
            fillColor.b = b;
            fillColor.a = a;

            return fillColor;
        }
        public static lineColor NewLineColor(int r, int g, int b)
        {
            return NewLineColor(r, g, b, ModelConst.ALPHA_MAX);
        }
        public static lineColor NewLineColor(int r, int g, int b, int a)
        {
            lineColor lineColor = new lineColor()
            {
                lineColor_Id = Util.MakeIdInt32(),
                operation = PCOOperation.atrest.ToString(),
            };

            lineColor.r = r;
            lineColor.g = g;
            lineColor.b = b;
            lineColor.a = a;

            return lineColor;
        }
        public static color NewColor(int r, int g, int b)
        {
            return NewColor(r, g, b, ModelConst.ALPHA_MAX);
        }
        public static color NewColor(int r, int g, int b, int a)
        {
            color color = new color()
            {
                color_Id = Util.MakeIdInt32(),
                operation = PCOOperation.atrest.ToString(),
            };

            color.r = r;
            color.g = g;
            color.b = b;
            color.a =a;

            return color;
        }

        public static bendpoint NewBendPoint(int x, int y)
        {
            return NewBendPoint(x, y, 0);
        }
        public static bendpoint NewBendPoint(int x, int y, int z)
        {
            bendpoint bendpoint = new bendpoint()
            {
                bendpoint_Id = Util.MakeIdInt32(),
                operation = PCOOperation.atrest.ToString(),
            };

            bendpoint.x = x;
            bendpoint.y = y;
            bendpoint.z = z;

            return bendpoint;
        }

        public static views NewViews()
        {
            return NewViews((view[])null);
        }
        public static views NewViews(view viewIn)
        {
            if (viewIn != null)
            {
                return NewViews(new view[] { viewIn });
            }
            else
            {
                return NewViews((view[])null);
            }
        }
        public static views NewViews(view[] viewsIn)
        {
            views views = new views()
            {
                views_Id = Util.MakeIdInt32(),
                // TODO
            };

            if (viewsIn != null)
            {
                foreach (view p in viewsIn)
                {
                    views.view.Add(p);
                }
            }

            return views;
        }
        public static view NewView(string labelIn)
        {
            return NewView(labelIn, null, ModelConst.ViewType.OtherOrUnknownOrUndefinedViewType, null, null, null);
        }
        public static view NewView(string labelIn, string langIn)
        {
            return NewView(labelIn, langIn, ModelConst.ViewType.OtherOrUnknownOrUndefinedViewType, null, null, null);
        }
        public static view NewView(string labelIn, ModelConst.ViewType viewTypeIn)
        {
            return NewView(labelIn, null, viewTypeIn, null, null, null);
        }
        public static view NewView(string labelIn, string langIn, ModelConst.ViewType viewTypeIn)
        {
            return NewView(labelIn, langIn, viewTypeIn, null, null, null);
        }
        //public static view NewView(Const.ViewpointType viewTypeIn, node[] nodesIn, connection[] connectionsIn)
        //{
        //    return NewView(null, null, viewTypeIn, nodesIn, connectionsIn, null);
        //}
        //public static view NewView(Const.ViewpointType viewTypeIn, node[] nodesIn, connection[] connectionsIn, properties propertiesIn)
        //{
        //    return NewView(null, null, viewTypeIn, nodesIn, connectionsIn, propertiesIn);
        //}
        public static view NewView(string labelIn, ModelConst.ViewType viewTypeIn, node[] nodesIn, connection[] connectionsIn)
        {
            return NewView(labelIn, null, viewTypeIn, nodesIn, connectionsIn, null);
        }
        public static view NewView(string labelIn, string langIn, ModelConst.ViewType viewTypeIn, node[] nodesIn, connection[] connectionsIn)
        {
            return NewView(labelIn, langIn, viewTypeIn, nodesIn, connectionsIn, null);
        }
        public static view NewView(string labelIn, ModelConst.ViewType viewTypeIn, node[] nodesIn, connection[] connectionsIn, properties propertiesIn)
        {
            return NewView(labelIn, null, viewTypeIn, nodesIn, connectionsIn, propertiesIn);
        }
        public static view NewView(string labelIn, string langIn, ModelConst.ViewType viewTypeIn, node[] nodesIn, connection[] connectionsIn, properties propertiesIn)
        {
            view view = new view()
            {
                view_Id = Util.MakeIdInt32(),
                identifier = Util.MakeIdentifierTimestamped(ModelConst.VIEW_PREFIX, DateTime.Now),
                //identifier = Util.MakeIdentifierFromParentIdentifier(
                //    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                //    typeof(view).Name, viewTypeIn.ToString(), Util.TrimWhitespace(labelIn)),
                operation = PCOOperation.atrest.ToString(),
            };

            view.viewpoint = viewTypeIn.ToString();

            if (!String.IsNullOrEmpty(labelIn))
            {
                view.label.Add(NewLabel(labelIn, langIn));

                view.identifier = Util.MakeIdentifierFromParentIdentifier(
                                        Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                                        typeof(view).Name, viewTypeIn.ToString(), Util.TrimWhitespace(labelIn));
            }

            if (propertiesIn != null)
            {
                view.properties.Add(propertiesIn);
            }

            if (nodesIn != null)
            {
                foreach (node n in nodesIn)
                {
                    view.node.Add(n);
                }
            }

            if (connectionsIn != null)
            {
                foreach (connection c in connectionsIn)
                {
                    view.connection.Add(c);
                }
            }

            return view;
        }

        public static metadata NewMetadata(property propertyIn)
        {
            return NewMetadata(null, null, new property[] { propertyIn });
        }
        public static metadata NewMetadata(property[] propertiesIn)
        {
            return NewMetadata(null, null, propertiesIn);
        }
        public static metadata NewMetadata(string schema, string schemaversion)
        {
            return NewMetadata(schema, schemaversion, (property[])null);
        }
        public static metadata NewMetadata(string schema, string schemaversion, property propertyIn)
        {
            return NewMetadata(schema, schemaversion, new property[] { propertyIn });
        }
        public static metadata NewMetadata(string schema, string schemaversion, property[] propertiesIn)
        {
            metadata metadata = new metadata()
            {
                metadata_Id = Util.MakeIdInt32(),
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(schema)) metadata.schema = schema;
            if (!String.IsNullOrEmpty(schemaversion)) metadata.schemaversion = schemaversion;

            if (propertiesIn != null)
            {
                foreach (property p in propertiesIn)
                {
                    metadata.property.Add(p);
                }

            }

            return metadata;
        }

        public static relationships NewRelationships()
        {
            return NewRelationships((relationship[])null);
        }
        public static relationships NewRelationships(relationship relationshipIn)
        {
            if (relationshipIn != null)
            {
                return NewRelationships(new relationship[] { relationshipIn });
            }
            else
            {
                return NewRelationships((relationship[])null);
            }
        }
        public static relationships NewRelationships(relationship[] relationshipsIn)
        {
            relationships relationships = new relationships()
            {
                relationships_Id = Util.MakeIdInt32(),
                // TODO
            };

            if (relationshipsIn != null)
            {
                foreach (relationship p in relationshipsIn)
                {
                    relationships.relationship.Add(p);
                }
            }

            return relationships;
        }
        public static relationship NewRelationship(ModelConst.RelationshipType typeIn, element eSourceIn, element eTargetIn)
        {
            return NewRelationship(null, null, typeIn, eSourceIn, eTargetIn, null);
        }
        public static relationship NewRelationship(ModelConst.RelationshipType typeIn, element eSourceIn, element eTargetIn, properties propertiesIn)
        {
            return NewRelationship(null, null, typeIn, eSourceIn, eTargetIn, propertiesIn);
        }
        public static relationship NewRelationship(string labelIn, ModelConst.RelationshipType typeIn, element eSourceIn, element eTargetIn)
        {
            return NewRelationship(labelIn, null, typeIn, eSourceIn, eTargetIn, null);
        }
        public static relationship NewRelationship(string labelIn, string langIn, ModelConst.RelationshipType typeIn, element eSourceIn, element eTargetIn)
        {
            return NewRelationship(labelIn, langIn, typeIn, eSourceIn, eTargetIn, null);
        }
        public static relationship NewRelationship(string labelIn, ModelConst.RelationshipType typeIn, element eSourceIn, element eTargetIn, properties propertiesIn)
        {
            return NewRelationship(labelIn, null, typeIn, eSourceIn, eTargetIn, propertiesIn);
        }
        public static relationship NewRelationship(string labelIn, string langIn, ModelConst.RelationshipType typeIn, element eSourceIn, element eTargetIn, properties propertiesIn)
        {
            relationship r = null;

            if (eSourceIn == null) throw new ArgumentNullException("eSourceIn");
            if (eTargetIn == null) throw new ArgumentNullException("eTargetIn");

            r = NewRelationship(labelIn, langIn, typeIn, eSourceIn.identifier, eTargetIn.identifier, propertiesIn);

            return r;
        }

        public static relationship NewRelationship(string labelIn, ModelConst.RelationshipType typeIn, string eSourceIdentifierIn, string eTargetIdentifierIn, properties propertiesIn)
        {
           return NewRelationship(labelIn, null, typeIn, eSourceIdentifierIn, eTargetIdentifierIn, propertiesIn);
        }
        public static relationship NewRelationship(string labelIn, string langIn, ModelConst.RelationshipType typeIn, string eSourceIdentifierIn, string eTargetIdentifierIn, properties propertiesIn)
        {
            if (String.IsNullOrEmpty(eSourceIdentifierIn)) throw new ArgumentNullException("eSourceIdentifierIn");
            if (String.IsNullOrEmpty(eTargetIdentifierIn)) throw new ArgumentNullException("eTargetIdentifierIn");

            relationship relationship = new relationship()
            {
                relationship_Id = Util.MakeIdInt32(),
                //identifier = Util.MakeIdentifierTimestamped(ModelConst.RELATIONSHIP_PREFIX, DateTime.Now),
                //identifier = Util.MakeModelRelationshipIdentifier(Util.MakeModelRelationshipIdentifier(modelName, labelIn, typeIn);
                //identifier = Util.MakeIdentifierFromParentIdentifier(
                //    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                //    typeof(relationship).Name, typeIn.ToString(), Util.TrimWhitespace(labelIn)),
                identifier = Util.MakeModelRelationshipIdentifier(modelName, typeIn, eSourceIdentifierIn, eTargetIdentifierIn),
            type = typeIn.ToString(),
                operation = PCOOperation.atrest.ToString(),
            };

            relationship.source = eSourceIdentifierIn;
            relationship.target = eTargetIdentifierIn;

            if (!String.IsNullOrEmpty(labelIn))
            {
                relationship.label.Add(NewLabel(labelIn, langIn));

                //relationship.identifier = Util.MakeIdentifierFromParentIdentifier(
                //                                Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                //                                typeof(relationship).Name, typeIn.ToString(), Util.TrimWhitespace(labelIn));
                relationship.identifier = Util.MakeModelRelationshipIdentifier(modelName, labelIn, typeIn, eSourceIdentifierIn, eTargetIdentifierIn); // TODO consider lang?
            }

            if (propertiesIn != null)
            {
                relationship.properties.Add(propertiesIn);
            }

            return relationship;
        }

        public static item NewItem()
        {
            return NewItem(null, null, (element)null, (item[])null);
        }
        public static item NewItem(string labelIn)
        {
            return NewItem(labelIn, null, null, null);
        }
        public static item NewItem(string labelIn, string langIn)
        {
            return NewItem(labelIn, langIn, null, null);
        }
        public static item NewItem(view objectref)
        {
            return NewItem(null, null, objectref);
        }
        public static item NewItem(relationship objectref)
        {
            return NewItem(null, null, objectref);
        }
        public static item NewItem(element objectref)
        {
            return NewItem(null, null, objectref, (item[])null);
        }
        public static item NewItem(element objectref, item itemIn)
        {
            return NewItem(null, null, objectref, new item[] { itemIn });
        }
        public static item NewItem(element objectref, item[] itemsIn)
        {
            return NewItem(null, null, objectref, itemsIn);
        }
        public static item NewItem(string labelIn, element objectref, item itemIn)
        {
            return NewItem(labelIn, null, objectref, new item[] { itemIn });
        }
        public static item NewItem(string labelIn, element objectref, item[] itemsIn)
        {
            return NewItem(labelIn, null, objectref, itemsIn);
        }
        public static item NewItem(string labelIn, string langIn, view objectref)
        {
            item item = new item()
            {
                item_Id = Util.MakeIdInt32(),
                identifier = Util.MakeIdentifierTimestamped(ModelConst.ITEM_PREFIX, DateTime.Now),
                //identifier = Util.MakeIdentifierFromParentIdentifier(
                //    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                //    typeof(item).Name, Util.TrimWhitespace(labelIn)),
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(labelIn))
            {
                item.label.Add(NewLabel(labelIn, langIn));

                item.identifier = Util.MakeIdentifierFromParentIdentifier(
                                        Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                                        typeof(item).Name, Util.TrimWhitespace(labelIn));
            }

            if (objectref != null && !String.IsNullOrEmpty(objectref.identifier))
            {
                item.identifierref = objectref.identifier;
            }

            return item;
        }
        public static item NewItem(string labelIn, string langIn, relationship objectref)
        {
            item item = new item()
            {
                item_Id = Util.MakeIdInt32(),
                identifier = Util.MakeIdentifierTimestamped(ModelConst.ITEM_PREFIX, DateTime.Now),
                //identifier = Util.MakeIdentifierFromParentIdentifier(
                //    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                //    typeof(item).Name, Util.TrimWhitespace(labelIn)),
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(labelIn))
            {
                item.label.Add(NewLabel(labelIn, langIn));

                item.identifier = Util.MakeIdentifierFromParentIdentifier(
                                        Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                                        typeof(item).Name, Util.TrimWhitespace(labelIn));
            }

            if (objectref != null && !String.IsNullOrEmpty(objectref.identifier))
            {
                item.identifierref = objectref.identifier;
            }

            return item;
        }
        public static item NewItem(string labelIn, string langIn, element objectref, item[] itemsIn)
        {
            item item = new item()
            {
                item_Id = Util.MakeIdInt32(),
                identifier = Util.MakeIdentifierTimestamped(ModelConst.ITEM_PREFIX, DateTime.Now),
                //identifier = Util.MakeIdentifierFromParentIdentifier(
                //    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                //    typeof(item).Name, labelIn),
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(labelIn))
            {
                item.label.Add(NewLabel(labelIn, langIn));

                item.identifier = Util.MakeIdentifierFromParentIdentifier(
                                        Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                                        typeof(item).Name, Util.TrimWhitespace(labelIn));
            }

            if (objectref != null && !String.IsNullOrEmpty(objectref.identifier))
            {
                item.identifierref = objectref.identifier;
            }

            if (itemsIn != null)
            {
                foreach (item i in itemsIn)
                {
                    item.item1.Add(i);
                }
            }

            return item;
        }

        public static organization NewOrganization()
        {
            return NewOrganization((item[])null);
        }
        public static organization NewOrganization(item itemIn)
        {
            if (itemIn != null)
            {
                return NewOrganization(new item[] { itemIn });
            }
            else
            {
                return NewOrganization((item[])null);
            }
        }
        public static organization NewOrganization(item[] itemsIn)
        {
            organization organization = new organization()
            {
                organization_Id = Util.MakeIdInt32(),
            };

            if (itemsIn != null)
            {
                foreach (item i in itemsIn)
                {
                    organization.item.Add(i);
                }
            }

            return organization;
        }

        public static propertydef NewPropertyDef(string nameIn)
        {
            return NewPropertyDef(nameIn, ModelConst.PropertyDataType.stringType);
        }
        public static propertydef NewPropertyDef(string nameIn, ModelConst.PropertyDataType typeIn)
        {
            propertydef propertydef = new propertydef()
            {
                propertydef_Id = Util.MakeIdInt32(),
                //identifier = Util.MakeIdentifierTimestamped(ModelConst.PROPERTYDEF_PREFIX, DateTime.Now),
                identifier = Util.MakeIdentifierFromParentIdentifier(
                    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                    typeof(propertydef).Name, typeIn.ToString().Replace("Type", ""), Util.TrimWhitespace(nameIn)),
                name = Util.TrimWhitespace(nameIn), // attribute - not an element - not a table
                type = typeIn.ToString().Replace("Type", ""),
                operation = PCOOperation.atrest.ToString(),
            };

            return propertydef;
        }
        public static propertydefs NewPropertyDefs(propertydef propertydefIn)
        {
            if (propertydefIn != null)
            {
                return NewPropertyDefs(new propertydef[] { propertydefIn });
            }
            else
            {
                return NewPropertyDefs((propertydef[])null);
            }
        }
        public static propertydefs NewPropertyDefs(propertydef[] propertydefsIn)
        {
            propertydefs propertydefs = new propertydefs()
            {
                propertydefs_Id = Util.MakeIdInt32(),
                // TODO
            };

            if (propertydefsIn != null)
            {
                foreach (propertydef p in propertydefsIn)
                {
                    propertydefs.propertydef.Add(p);
                }
            }

            return propertydefs;
        }

        public static property NewProperty(value valueIn, propertydef propertydefrefIn)
        {
            if (valueIn == null) throw new ArgumentNullException("valueIn");
            if (propertydefrefIn == null) throw new ArgumentNullException("propertydefrefIn");

            property property = new property()
            {
                property_Id = Util.MakeIdInt32(),
                operation = PCOOperation.atrest.ToString(),
            };

            if (propertydefrefIn != null && !String.IsNullOrEmpty(propertydefrefIn.identifier))
            {
                property.identifierref = propertydefrefIn.identifier;
            }

            if (valueIn != null)
            {
                property.value.Add(valueIn);
            }

            return property;
        }

        public static properties NewProperties()
        {
            return NewProperties((property[])null);
        }
        public static properties NewProperties(property propertyIn)
        {
            if (propertyIn != null)
            {
                return NewProperties(new property[] { propertyIn });
            }
            else
            {
                return NewProperties((property[])null);
            }
        }
        public static properties NewProperties(property[] propertiesIn)
        {
            properties properties = new properties()
            {
                properties_Id = Util.MakeIdInt32(),
                // TODO
            };

            if (propertiesIn != null)
            {
                foreach (property p in propertiesIn)
                {
                    properties.property.Add(p);
                }
            }

            return properties;
        }

        //public static element NewElement(Const.ElementType typeIn)
        //{
        //    return NewElement(null, (string)null, typeIn);
        //}
        public static element NewElement(string labelIn, ModelConst.ElementType typeIn)
        {
            return NewElement(labelIn, (string)null, typeIn);
        }
        public static element NewElement(string labelIn, string langIn, ModelConst.ElementType typeIn)
        {
            return NewElement(labelIn, langIn, typeIn, (properties)null);
        }
        public static element NewElement(string labelIn, ModelConst.ElementType typeIn, property propertyIn)
        {
            return NewElement(labelIn, (string)null, typeIn, NewProperties(propertyIn));
        }
        public static element NewElement(string labelIn, string langIn, ModelConst.ElementType typeIn, property propertyIn)
        {
            return NewElement(labelIn, langIn, typeIn, NewProperties(propertyIn));
        }
        public static element NewElement(string labelIn, ModelConst.ElementType typeIn, properties propertiesIn)
        {
            return NewElement(labelIn, null, typeIn, propertiesIn);
        }
        public static element NewElement(string labelIn, string langIn, ModelConst.ElementType typeIn, properties propertiesIn)
        {
            element element = new element()
            {
                element_Id = Util.MakeIdInt32(),
                identifier = Util.MakeIdentifierTimestamped(ModelConst.ELEMENT_PREFIX, DateTime.Now),
                //identifier = Util.MakeIdentifierFromParentIdentifier(
                //    Util.MakeIdentifierRootIdentifier(typeof(model).Name, modelName),
                //    typeof(element).Name, typeIn.ToString(), Util.TrimWhitespace(labelIn)),
                type = typeIn.ToString(),
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(labelIn))
            {
                element.label.Add(NewLabel(labelIn, langIn));

                element.identifier = Util.MakeModelElementIdentifier(modelName, labelIn, typeIn);
            }

            if (propertiesIn != null)
            {
                element.properties.Add(propertiesIn);
            }

            return element;
        }
        public static elements NewElements()
        {
            return NewElements((element[])null);
        }
        public static elements NewElements(element elementIn)
        {
            if (elementIn != null)
            {
                return NewElements(new element[] { elementIn });
            }
            else
            {
                return NewElements((element[])null);
            }
        }
        public static elements NewElements(element[] elementsIn)
        {
            elements elements = new elements()
            {
                elements_Id = Util.MakeIdInt32(),
            };

            if (elementsIn != null)
            {
                foreach (element e in elementsIn)
                {
                    elements.element.Add(e);
                }
            }

            return elements;
        }

        public static model NewModel(string nameTextIn)
        {
            return NewModel(nameTextIn, (string)null);
        }
        public static model NewModel(string nameTextIn, string langIn)
        {
            return NewModel(nameTextIn, langIn, (elements)null, (propertydefs)null);
        }
        public static model NewModel(string nameTextIn, element elementIn)
        {
            return NewModel(nameTextIn, (string)null, NewElements(elementIn), (propertydefs)null);
        }
        public static model NewModel(string nameTextIn, string langIn, element elementIn)
        {
            return NewModel(nameTextIn, langIn, NewElements(elementIn), (propertydefs)null);
        }
        public static model NewModel(string nameTextIn, elements elementsIn)
        {
            return NewModel(nameTextIn, (string)null, elementsIn, (propertydefs)null);
        }
        public static model NewModel(string nameTextIn, elements elementsIn, propertydef propertydefIn)
        {
            return NewModel(nameTextIn, null, elementsIn, NewPropertyDefs(propertydefIn));
        }
        public static model NewModel(string nameTextIn, string langIn, elements elementsIn, propertydef propertydefIn)
        {
            return NewModel(nameTextIn, langIn, elementsIn, NewPropertyDefs(propertydefIn));
        }
        public static model NewModel(string nameTextIn, elements elementsIn, propertydefs propertydefsIn)
        {
            return NewModel(nameTextIn, null, elementsIn, propertydefsIn);
        }
        public static model NewModel(string nameTextIn, string langIn, elements elementsIn, propertydefs propertydefsIn)
        {
            if (String.IsNullOrEmpty(nameTextIn)) throw new ArgumentNullException("nameTextIn");

            model model = new model()
            {
                model_Id = Util.MakeIdInt32(),
                //identifier = Util.MakeIdentifierTimestamped(ModelConst.MODEL_PREFIX, DateTime.Now),
                identifier = Util.MakeIdentifierFromParentIdentifier(
                    Util.MakeIdentifierRootIdentifier(typeof(tenant).Name, TenantName),
                    typeof(model).Name, Util.TrimWhitespace(nameTextIn)),
                version = ModelConst.MODEL_VERSION,
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(nameTextIn))
            {
                model.name.Add(NewName(nameTextIn, langIn));
            }

            if (elementsIn != null)
            {
                model.elements.Add(elementsIn);
            }

            if (propertydefsIn != null)
            {
                model.propertydefs.Add(propertydefsIn);
            }

            return model;
        }
        public static models NewModels(model modelIn)
        {
            if (modelIn != null)
            {
                return NewModels(new model[] { modelIn });
            }
            else
            {
                return NewModels((model[])null);
            }
        }
        public static models NewModels(model[] modelsIn)
        {
            models models = new models()
            {
                models_Id = Util.MakeIdInt32(),
            };

            if (modelsIn != null)
            {
                foreach (model m in modelsIn)
                {
                    models.model.Add(m);
                }
            }

            return models;
        }

        public static folder NewFolder(string nameTextIn)
        {
            return NewFolder(nameTextIn, (string)null, (models)null);
        }
        public static folder NewFolder(string nameTextIn, string langIn)
        {
            return NewFolder(nameTextIn, langIn, (models)null);
        }
        public static folder NewFolder(string nameTextIn, model modelIn)
        {
            return NewFolder(nameTextIn, null, NewModels(modelIn));
        }
        public static folder NewFolder(string nameTextIn, string langIn, model modelIn)
        {
            return NewFolder(nameTextIn, langIn, NewModels(modelIn));
        }
        public static folder NewFolder(string nameTextIn, models modelsIn)
        {
            return NewFolder(nameTextIn, null, modelsIn);
        }
        public static folder NewFolder(string nameTextIn, string langIn, models modelsIn)
        {
            folder folder = new folder()
            {
                folder_Id = Util.MakeIdInt32(),
                //identifier = Util.MakeIdentifierTimestamped(ModelConst.FOLDER_PREFIX, DateTime.Now),
                identifier = Util.MakeIdentifierFromParentIdentifier(
                    Util.MakeIdentifierRootIdentifier(typeof(tenant).Name, TenantName),
                    typeof(folder).Name, (nameTextIn == "/" ? "Root" : Util.TrimWhitespace(nameTextIn))),
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(nameTextIn))
            {
                folder.name.Add(NewName(nameTextIn, langIn));
            }

            if (modelsIn != null)
            {
                folder.models.Add(modelsIn);
            }

            return folder;
        }
        public static folder NewFolder(string nameTextIn, folder folderIn)
        {
            return NewFolder(nameTextIn, null, new folder[] { folderIn });
        }
        public static folder NewFolder(string nameTextIn, string langIn, folder folderIn)
        {
            return NewFolder(nameTextIn, langIn, new folder[] { folderIn });
        }
        public static folder NewFolder(string nameTextIn, folder[] foldersIn)
        {
            return NewFolder(nameTextIn, null, foldersIn);
        }
        public static folder NewFolder(string nameTextIn, string langIn, folder[] foldersIn)
        {
            folder folder = new folder()
            {
                folder_Id = Util.MakeIdInt32(),
                //identifier = Util.MakeIdentifierTimestamped(ModelConst.FOLDER_PREFIX, DateTime.Now),
                identifier = Util.MakeIdentifierFromParentIdentifier(
                    Util.MakeIdentifierRootIdentifier(typeof(tenant).Name, TenantName),
                    typeof(folder).Name, (nameTextIn == "/" ? "Root" : Util.TrimWhitespace(nameTextIn))),
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(nameTextIn))
            {
                folder.name.Add(NewName(nameTextIn, langIn));
            }

            if (foldersIn != null)
            {
                foreach (folder f in foldersIn)
                {
                    folder.folder1.Add(f);
                }
            }

            return folder;
        }
        public static folders NewFolders(folder folderIn)
        {
            if (folderIn != null)
            {
                return NewFolders(new folder[] { folderIn });
            }
            else
            {
                return NewFolders((folder[])null);
            }
        }
        public static folders NewFolders(folder[] foldersIn)
        {
            folders folders = new folders()
            {
                folders_Id = Util.MakeIdInt32(),
            };

            if (foldersIn != null)
            {
                foreach (folder f in foldersIn)
                {
                    folders.folder.Add(f);
                }
            }

            return folders;
        }

        public static tenant NewTenant(string nameTextIn)
        {
            return NewTenant(nameTextIn, (string)null, (folders)null);
        }
        public static tenant NewTenant(string nameTextIn, string langIn)
        {
            return NewTenant(nameTextIn, langIn, (folders)null);
        }
        public static tenant NewTenantWithRootFolder(string nameTextIn)
        {
            return NewTenantWithRootFolder(nameTextIn, null);
        }
        public static tenant NewTenantWithRootFolder(string nameTextIn, string langIn)
        {
            folder folder = NewFolder(ModelConst.ROOTFOLDERNAME);
            return NewTenant(nameTextIn, langIn, NewFolders(folder));
        }
        public static tenant NewTenant(string nameTextIn, folder folderIn)
        {
            return NewTenant(nameTextIn, null, NewFolders(folderIn));
        }
        public static tenant NewTenant(string nameTextIn, string langIn, folder folderIn)
        {
            return NewTenant(nameTextIn, langIn, NewFolders(folderIn));
        }
        public static tenant NewTenant(string nameTextIn, folders foldersIn)
        {
            return NewTenant(nameTextIn, null, foldersIn);
        }
        public static tenant NewTenant(string nameTextIn, string langIn, folders foldersIn)
        {
            tenant tenant = new tenant()
            {
                tenant_Id = Util.MakeIdInt32(),
                //identifier = Util.MakeIdentifierTimestamped(ModelConst.TENANT_PREFIX, DateTime.Now),
                identifier = Util.MakeIdentifierRootIdentifier(typeof(tenant).Name, Util.TrimWhitespace(nameTextIn)),
                version = ModelConst.TENANT_VERSION,
                operation = PCOOperation.atrest.ToString(),
            };

            if (!String.IsNullOrEmpty(nameTextIn))
            {
                tenant.name.Add(NewName(nameTextIn, langIn));
            }

            if (foldersIn != null)
            {
                tenant.folders.Add(foldersIn);
            }

            return tenant;
        }

        public static label NewLabel()
        {
            return NewLabel(null, null);
        }
        public static label NewLabel(string labelTextIn)
        {
            return NewLabel(labelTextIn, null);
        }
        public static label NewLabel(string labelTextIn, string langIn)
        {
            if (String.IsNullOrEmpty(labelTextIn)) labelTextIn = Util.MakeIdentifierTimestamped(ModelConst.LABEL_PREFIX);
            if (String.IsNullOrEmpty(langIn)) langIn = ModelConst.LANG_EN;

            label label = new label()
            {
                label_Id = Util.MakeIdInt32(),
                lang = Util.TrimWhitespace(langIn),
                label_text = Util.TrimWhitespace(labelTextIn),
                operation = PCOOperation.atrest.ToString(),
            };

            return label;
        }

        public static name NewName()
        {
            return NewName(null, null);
        }
        public static name NewName(string nameTextIn)
        {
            return NewName(nameTextIn, null);
        }
        public static name NewName(string nameTextIn, string langIn)
        {
            if (String.IsNullOrEmpty(nameTextIn)) nameTextIn = Util.MakeIdentifierTimestamped(ModelConst.NAME_PREFIX);
            if (String.IsNullOrEmpty(langIn)) langIn = ModelConst.LANG_EN;

            name name = new name()
            {
                name_Id = Util.MakeIdInt32(),
                lang = Util.TrimWhitespace(langIn),
                name_text = Util.TrimWhitespace(nameTextIn),
                operation = PCOOperation.atrest.ToString(),
            };
            
            return name;
        }

        public static value NewValue(string valueIn)
        {
            return NewValue(valueIn, null);
        }
        public static value NewValue(string valueIn, string langIn) // TODO: Need to support null values? ...non-string values?
        {
            if (valueIn == null) throw new ArgumentNullException("valueIn");

            if (String.IsNullOrEmpty(langIn)) langIn = ModelConst.LANG_EN;

            value value = new value()
            {
                value_Id = Util.MakeIdInt32(),
                lang = Util.TrimWhitespace(langIn),
                operation = PCOOperation.atrest.ToString(),
            };

            value.value_text = Util.TrimWhitespace(valueIn);

            return value;
        }
        public static documentation NewDocumentation()
        {
            return NewDocumentation(null, null);
        }
        public static documentation NewDocumentation(string documentationTextIn)
        {
            return NewDocumentation(documentationTextIn, null);
        }
        public static documentation NewDocumentation(string documentationTextIn, string langIn)
        {
            if (documentationTextIn == null) throw new ArgumentNullException("documentationTextIn");

            if (String.IsNullOrEmpty(documentationTextIn)) documentationTextIn = Util.MakeIdentifierTimestamped(ModelConst.DOCUMENTATION_PREFIX);
            if (String.IsNullOrEmpty(langIn)) langIn = ModelConst.LANG_EN;

            documentation documentation = new documentation()
            {
                documentation_Id = Util.MakeIdInt32(),
                lang = Util.TrimWhitespace(langIn),
                documentation_text = Util.TrimWhitespace(documentationTextIn),
                operation = PCOOperation.atrest.ToString(),
            };

            return documentation;
        }
    }
}
