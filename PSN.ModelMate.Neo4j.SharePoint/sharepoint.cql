https://neo4j.com/developer/guide-import-csv/

LOAD CSV FROM 'file:///636167898857890007.csv' AS line
RETURN COUNT(*);

LOAD CSV FROM 'file:///636167898857890007.csv' AS line WITH line
RETURN line
LIMIT 5;

LOAD CSV WITH HEADERS FROM 'file:///636167898857890007.csv' AS line WITH line
RETURN line
LIMIT 5;



MATCH (e:Element_ArchiMate) DETACH DELETE e RETURN COUNT(*);

MATCH (e) DETACH DELETE e RETURN COUNT(*);

CREATE CONSTRAINT ON (e:Element_ArchiMate) ASSERT e.Guid IS UNIQUE;

USING PERIODIC COMMIT 1000
LOAD CSV WITH HEADERS FROM 'file:///636167898857890007.csv' AS csvdata FIELDTERMINATOR "," 
MERGE  (child:Element_ArchiMate { Guid: csvdata.ChildId })
ON CREATE SET child._Created = timestamp(), child._Modified = timestamp(), child._Removed = 0,
		child._DatabaseVersion =	"2.2.0.0 PSN 1.0",
		child._DatabaseSource =		"SharePoint Online Client Browser PSN",
		child._ArchiMate2Type =		csvdata.ChildSchemeLabel,
		child.Index = csvdata.ChildIndex,
		child.Level = csvdata.ChildLevel,
		child.Name = csvdata.ChildText,
		child.Name2 = csvdata.ChildName,
		child.TagType = csvdata.ChildTagType,
		child.TagLabel = csvdata.ChildTagLabel,
		child.Text = csvdata.ChildText,
		child.Text2 = csvdata.ChildText2,
		child.TextName = csvdata.ChildTextName,
		child.FullPath = csvdata.ChildFullPath,
		child.Template = csvdata.ChildTemplates,
		child.NameField = csvdata.ChildNameField,
		child.SchemeLabel = csvdata.ChildSchemeLabel,
		child.SchemeDomainLabel = csvdata.SchemeDomainLabel
RETURN COUNT(*);

USING PERIODIC COMMIT 1000
LOAD CSV WITH HEADERS FROM 'file:///636167898857890007.csv' AS csvdata FIELDTERMINATOR "," 
MERGE  (parent:Element_ArchiMate {Guid: csvdata.ParentId})
ON CREATE SET parent._Created = timestamp(), parent._Modified = timestamp(), parent._Removed = 0,
		parent._DatabaseVersion =	"2.2.0.0 PSN 1.0",
		parent._DatabaseSource =   "SharePoint Online Client Browser PSN",
		parent._ArchiMate2Type =   csvdata.ParentSchemeLabel,
		parent.Id = csvdata.ID,
		parent.Index = csvdata.ParentIndex,
		parent.Level = csvdata.ParentLevel,
		parent.Name = csvdata.ParentText,
		parent.Name2 = csvdata.ParentName,
		parent.TagType = csvdata.ParentTagType,
		parent.TagLabel = csvdata.ParentTagLabel,
		parent.Text = csvdata.ParentText,
		parent.Text2 = csvdata.ParentText2,
		parent.TextName = csvdata.ParentTextName,
		parent.FullPath = csvdata.ParentFullPath,
		parent.Template = csvdata.ParentTemplates,
		parent.NameField = csvdata.ParentNameField,
		parent.SchemeLabel = csvdata.ParentSchemeLabel,
		parent.SchemeDomainLabel = csvdata.SchemeDomainLabel
RETURN COUNT(*);


//MATCH p=(parent:Web_Client_SharePoint_Microsoft) -[*1..1]-> (child)
//RETURN p;

//LOAD CSV WITH HEADERS FROM 'file:///636167898857890007.csv' AS csvdata FIELDTERMINATOR "," 
//MATCH  (parent:Element_ArchiMate {Guid: csvdata.ParentId}), (child:Element_ArchiMate { Guid: csvdata.ChildId })
//RETURN parent, child LIMIT 5;

//USING PERIODIC COMMIT 1000
//LOAD CSV WITH HEADERS FROM 'file:///636167898857890007.csv' AS csvdata FIELDTERMINATOR "," 
//MATCH  (parent:Element_ArchiMate {Guid: csvdata.ParentId}), (child:Element_ArchiMate { Guid: csvdata.ChildId })
//MERGE path = (parent) -[r:PARENT_CHILD]-> (child)
//RETURN COUNT(*);

//MERGE path = (parent) -[r:PARENT_CHILD]-> (child) RETURN COUNT(*)
//MATCH  path = (parent:Element_ArchiMate) -[r:PARENT_CHILD]-> (child:Element_ArchiMate)
//DELETE r RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM 'file:///636167898857890007.csv' AS csvdata FIELDTERMINATOR "," 
MATCH  (parent:Element_ArchiMate {Guid: csvdata.ParentId}), (child:Element_ArchiMate { Guid: csvdata.ChildId })
WITH parent, child, csvdata
call apoc.create.relationship(parent, csvdata.Relation, {}, child) yield rel
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM 'file:///636167898857890007.csv' AS csvdata FIELDTERMINATOR "," 
MATCH  (parent:Element_ArchiMate {Guid: csvdata.ParentId}), (child:Element_ArchiMate { Guid: csvdata.ChildId })
WITH parent, child, csvdata
call apoc.create.relationship(parent, csvdata.ArchiMateRelation, {}, child) yield rel
RETURN COUNT(*)

MATCH (e:Element_ArchiMate)
call apoc.create.addLabels([ id(e) ], [ e.SchemeLabel ]) yield node
RETURN COUNT(*);

MATCH (e:Element_ArchiMate)
call apoc.create.addLabels([ id(e) ], [ e.TagLabel ]) yield node
RETURN COUNT(*);

MATCH p=(parent:Web_Client_SharePoint_Microsoft) -[*1..1]-> (child)
RETURN p;

MATCH (e:Element_ArchiMate)
call apoc.create.removeLabels([ id(e) ], [ "Element_ArchiMate" ]) yield node
RETURN COUNT(*);

MATCH 
q=()-[r1:ROOT_Calculated_SharePoint_ModelMate__Site_Client_SharePoint_Microsoft]->(),
s=()-[r2:Site_Client_SharePoint_Microsoft__Web_Client_SharePoint_Microsoft]->(),
p=()-[r3:Web_Client_SharePoint_Microsoft__WebSubWebs_Calculated_SharePoint_ModelMate]->(), 
t=()-[r4:Web_Client_SharePoint_Microsoft__WebLists_Calculated_SharePoint_ModelMate]->(),
u=()-[r5:WebLists_Calculated_SharePoint_ModelMate__List_Client_SharePoint_Microsoft]->()
return collect(p), collect(q), collect(s), collect(t), collect(u);

MATCH 
q=(:ROOT_Calculated_SharePoint_ModelMate)-[r1:ROOT_Calculated_SharePoint_ModelMate__Site_Client_SharePoint_Microsoft]->(:Site_Client_SharePoint_Microsoft),
s=(:Site_Client_SharePoint_Microsoft)-[r2:Site_Client_SharePoint_Microsoft__Web_Client_SharePoint_Microsoft]->(:Web_Client_SharePoint_Microsoft),
p1=(:Web_Client_SharePoint_Microsoft)-[r31:Web_Client_SharePoint_Microsoft__WebSubWebs_Calculated_SharePoint_ModelMate]->(:WebSubWebs_Calculated_SharePoint_ModelMate),
p2=(:WebSubWebs_Calculated_SharePoint_ModelMate)-[r32:WebSubWebs_Calculated_SharePoint_ModelMate__Web_Client_SharePoint_Microsoft]->(:Web_Client_SharePoint_Microsoft),  
t=(:Web_Client_SharePoint_Microsoft)-[r4:Web_Client_SharePoint_Microsoft__WebLists_Calculated_SharePoint_ModelMate]->(:WebLists_Calculated_SharePoint_ModelMate),
u=(:WebLists_Calculated_SharePoint_ModelMate)-[r5:WebLists_Calculated_SharePoint_ModelMate__List_Client_SharePoint_Microsoft]->(:List_Client_SharePoint_Microsoft)
return collect(p1), collect(p2), collect(q), collect(s), collect(t), collect(u);

MATCH 
q=(:ROOT_Calculated_SharePoint_ModelMate)-[r1:ROOT_Calculated_SharePoint_ModelMate__Site_Client_SharePoint_Microsoft]->(:Site_Client_SharePoint_Microsoft),
s=(:Site_Client_SharePoint_Microsoft)-[r2:Site_Client_SharePoint_Microsoft__Web_Client_SharePoint_Microsoft]->(:Web_Client_SharePoint_Microsoft)
return collect(q), collect(s);

MATCH  path = (parent:Element_ArchiMate) -[r]-> (child:Element_ArchiMate)
RETURN r LIMIT 5;

MATCH  path = (parent:Element_ArchiMate) -[r]-> (child:Element_ArchiMate)
DELETE r RETURN COUNT(*);

MATCH  path = (parent:Element_ArchiMate) -[r]-> (child:Element_ArchiMate)
RETURN COUNT(*);

MATCH 
q=(:ROOT_Calculated_SharePoint_ModelMate)-[r1:ROOT_Calculated_SharePoint_ModelMate__Site_Client_SharePoint_Microsoft]->(:Site_Client_SharePoint_Microsoft),
s=(:Site_Client_SharePoint_Microsoft)-[r2:Site_Client_SharePoint_Microsoft__Web_Client_SharePoint_Microsoft]->(:Web_Client_SharePoint_Microsoft)
return collect(q), collect(s);

MATCH (e:Element_ArchiMate)
call apoc.create.removeLabels([ id(e) ], [ e.SchemeLabel ]) yield node
RETURN COUNT(*);

MATCH p=(parent:ROOT_Calculated_SharePoint_ModelMate) -[*1..2]-> (child)
RETURN p;

MATCH p=(parent:ROOT_Calculated_SharePoint_ModelMate) -[*1..3]-> (child)
RETURN p;

XMATCH p=(parent) -[*1..2]-> (child)
WHERE any(l IN labels(parent) WHERE <> "WebContentTypes_Calculated_SharePoint_ModelMate")
AND any(l IN labels(child) WHERE <> "ContentType_Client_SharePoint_Microsoft")
RETURN p LIMIT 5;

XMATCH p=(parent) -[*1..2]-> (child)
WHERE p <> (parent:WebContentTypes_Calculated_SharePoint_ModelMate) -[]-> (child:ContentType_Client_SharePoint_Microsoft)
RETURN p LIMIT 5;

MATCH p=(parent:Site_Client_SharePoint_Microsoft) -[*1..3]-> (child)
WHERE (NONE (r IN rels(p) WHERE type(r) =  "ListContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft"))
AND   (NONE (r IN rels(p) WHERE type(r) =  "SiteContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft"))
AND   (NONE (r IN rels(p) WHERE type(r) =  "WebContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ListFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "WebFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "SiteFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ContentTypeFields_Calculated_SharePoint_ModelMate__Field.*"))
RETURN p;

ContentTypeFields_Calculated_SharePoint_ModelMate__SiteColumnGroup_Calculated_SharePoint_ModelMate

ContentTypeGroup_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft
ListContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft

ContentTypeFields_Calculated_SharePoint_ModelMate__FieldComputed_Client_SharePoint_Microsoft

MATCH p=(parent:Web_Client_SharePoint_Microsoft) -[*1..2]-> (child)
WHERE (NONE (r IN rels(p) WHERE type(r) =  "ListContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft"))
AND   (NONE (r IN rels(p) WHERE type(r) =  "SiteContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft"))
AND   (NONE (r IN rels(p) WHERE type(r) =  "WebContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ListFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "WebFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "SiteFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ContentTypeFields_Calculated_SharePoint_ModelMate__Field.*"))
RETURN p;

// IMPORTANT
MATCH p=(parent:Web_Client_SharePoint_Microsoft) -[*1..3]-> (child)
WHERE (NONE (r IN rels(p) WHERE type(r) =  "ListContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft"))
AND   (NONE (r IN rels(p) WHERE type(r) =  "SiteContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft"))
AND   (NONE (r IN rels(p) WHERE type(r) =  "WebContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ListFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "WebFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "SiteFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ContentTypeFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (parent.Name = "Team Site (/)")
RETURN p;

MATCH p=(parent:Web_Client_SharePoint_Microsoft) -[*1..3]-> (child)
WHERE (NONE (r IN rels(p) WHERE type(r) IN [ "ListContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft",
											 "SiteContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft",
											 "WebContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft" ] ))
AND   (NONE (l IN labels(child) WHERE l IN [ "WebProperties_Calculated_SharePoint_ModelMate",
											 "UserProfileProrties_Calculated_SharePoint_ModelMate" ] ))
AND   (NONE (l IN labels(child) WHERE l =~ "Add.*"))
AND   (NONE (l IN labels(child) WHERE l =~ "vti.*"))
AND   (NONE (l IN labels(child) WHERE l =~ "Anon.*"))
AND   (NONE (l IN labels(child) WHERE l =~ "App.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ListFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "WebFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "SiteFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ContentTypeFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (parent.Name = "Team Site (/)")
RETURN p;

MATCH (parent:Site_Client_SharePoint_Microsoft) return parent;

MATCH p=(parent:Web_Client_SharePoint_Microsoft) -[*1..3]-> (child)
WHERE (NONE (r IN rels(p) WHERE type(r) IN [ "ListContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft",
											 "SiteContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft",
											 "WebContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft" ] ))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel IN [ "WebProperties_Calculated_SharePoint_ModelMate",
											     "UserProfileProperties_Calculated_SharePoint_ModelMate" ] ))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Boolean.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "DateTime.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Empty.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "ProjectPolicies.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "RegionalSettings.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Taxonomy.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Theme.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Add.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Cancel.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Browse.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Create.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Delete.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Edit.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Enumerate.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "View.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Manage.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Navigation.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Open.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Workflow.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "ListWorkflow.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ ".*CustomActions.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "WebWorkflow.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "vti.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Anon.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "App.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ListFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "WebFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "SiteFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ContentTypeFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (parent.Name = "Team Site (/)")
RETURN p

MATCH p=(parent:Site_Client_SharePoint_Microsoft) -[*1..3]-> (child)
WHERE (NONE (r IN rels(p) WHERE type(r) IN [ "ListContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft",
											 "SiteContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft",
											 "WebContentTypes_Calculated_SharePoint_ModelMate__ContentType_Client_SharePoint_Microsoft" ] ))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel IN [ "WebProperties_Calculated_SharePoint_ModelMate",
											     "UserProfileProrties_Calculated_SharePoint_ModelMate" ] ))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Boolean.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "DateTime.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Empty.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "ProjectPolicies.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "RegionalSettings.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Taxonomy.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Theme.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Add.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Cancel.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Browse.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Create.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Delete.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Edit.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Enumerate.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "View.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Manage.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Navigation.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Open.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Workflow.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "ListWorkflow.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ ".*CustomActions.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "WebWorkflow.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "vti.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "Anon.*"))
AND   (NONE (n IN nodes(p) WHERE n.TagLabel =~ "App.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ListFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "WebFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "SiteFields_Calculated_SharePoint_ModelMate__Field.*"))
AND   (NONE (r IN rels(p) WHERE type(r) =~ "ContentTypeFields_Calculated_SharePoint_ModelMate__Field.*"))
RETURN p

// IMPORTANT

match (n) where (n.Template =~ "STS.*" OR n.NameField =~ "STS.*") return n.Name, n.Template, n.TagLabel, n.FullPath, n.NameField ORDER BY n.Template, n.Name DESC;

match (n) where (n.Template =~ "STS.*" OR n.NameField =~ "STS.*") return n.Name, n.Template, n.TagLabel, n.FullPath, n.NameField ORDER BY n.Template, n.Name DESC;

match (n) where (n.Template = "101" OR n.NameField = "101") return n.Name, n.Template, n.TagLabel, n.FullPath, n.NameField ORDER BY n.Template, n.Name DESC;

match (n:WebTemplate_Client_SharePoint_Microsoft) where (n.Template =~ "STS.*" OR n.NameField =~ "STS.*") 
set n.Template = n.NameField
return n.Name, n.Template, n.TagLabel, n.FullPath, n.NameField ORDER BY n.Template, n.Name DESC;

match (n) where n.Template <> "NULL" return n.Name, n.Template, n.TagLabel, n.FullPath, n.NameField ORDER BY n.Template, n.TagLabel DESC;

