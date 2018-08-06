# ModelIdentifier,ModelName,RelationshipName,RelationshipId,Source,SourceType,SourceIdentifier,SourcePropertyName,SourcePropertyValue,Target,TargetType,TargetIdentifier,TargetPropertyName,TargetPropertyValue

# file:/C:/Users/Michael/Documents/Neo4j/default.graphdb/import/VetContextGraphHdrs.csv

MATCH (n) DETACH DELETE n;

CREATE CONSTRAINT ON (e:Relationship) ASSERT e.identifier IS UNIQUE;

CREATE CONSTRAINT ON (e:Element) ASSERT e.identifier IS UNIQUE;

:config initialNodeDisplay: 10000

LOAD CSV WITH HEADERS FROM 'file:///VetContextGraphHdrs.csv' AS rel FIELDTERMINATOR "," 
MERGE  (source:Element {name: rel.Source, type: rel.SourceType, identifier: rel.SourceIdentifier, eadomain: rel.SourcePropertyValue})
MERGE  (target:Element {name: rel.Target, type: rel.TargetType, identifier: rel.TargetIdentifier, eadomain: rel.TargetPropertyValue})
MERGE (source)-[:RELATED_TO]->(target)
RETURN *;

match(s:Element) -[*1..3]-> (t:Element) where s.type in ["Artifact"] and (not t.type in ["SystemSoftware", "InfrastructureService", "InfrastructureInterface"]) return s, t;

https://neo4j.com/developer/guide-import-csv/

LOAD CSV FROM 'file:///636167028965429021.csv' AS line
RETURN COUNT(*);

LOAD CSV FROM 'file:///636167028965429021.csv' AS line WITH line
RETURN line
LIMIT 5;

LOAD CSV WITH HEADERS FROM 'file:///636167028965429021.csv' AS line WITH line
RETURN line
LIMIT 5;



MATCH (e:Element_ArchiMate) DETACH DELETE e RETURN COUNT(*);

CREATE CONSTRAINT ON (e:Element_ArchiMate) ASSERT e.Guid IS UNIQUE;

USING PERIODIC COMMIT 1000
LOAD CSV WITH HEADERS FROM 'file:///636167028965429021.csv' AS csvdata FIELDTERMINATOR "," 
MERGE  (parent:Element_ArchiMate {Guid: csvdata.ParentId})
ON CREATE SET parent._Created = timestamp(), parent._Modified = timestamp(), parent._Removed = 0,
		parent._DatabaseVersion =	"2.2.0.0 PSN 1.0",
		parent._DatabaseSource =   "SharePoint Online Client Browser PSN",
		parent._ArchiMate2Type =   "Artifact_ArchiMate2",
		parent.Id = csvdata.ID,
		parent.Index = csvdata.ParentIndex,
		parent.Level = csvdata.ParentLevel,
		parent.Name = csvdata.ParentName,
		parent.TagType = csvdata.ParentTagType,
		parent.TagLabel = csvdata.ParentTagLabel,
		parent.Text = csvdata.ParentText,
		parent.TextName = csvdata.ParentTextName,
		parent.FullPath = csvdata.ParentFullPath,
		parent.Template = csvdata.ChildTemplate,
		parent.NameField = csvdata.ChildNameField,
		parent.SchemeLabel = csvdata.ParentSchemeLabel,
		parent.SchemeDomainLabel = csvdata.SchemeDomainLabel
RETURN COUNT(*);

USING PERIODIC COMMIT 1000
LOAD CSV WITH HEADERS FROM 'file:///636167028965429021.csv' AS csvdata FIELDTERMINATOR "," 
MERGE  (child:Element_ArchiMate { Guid: csvdata.ChildId })
ON CREATE SET child._Created = timestamp(), child._Modified = timestamp(), child._Removed = 0,
		child._DatabaseVersion =	"2.2.0.0 PSN 1.0",
		child._DatabaseSource =		"SharePoint Online Client Browser PSN",
		child._ArchiMate2Type =		"Artifact_ArchiMate2",
		child.Id = csvdata.ID,
		child.Index = csvdata.ChildIndex,
		child.Level = csvdata.ChildLevel,
		child.Name = csvdata.ChildName,
		child.TagType = csvdata.ChildTagType,
		child.TagLabel = csvdata.ChildTagLabel,
		child.Text = csvdata.ChildText,
		child.TextName = csvdata.ChildTextName,
		child.FullPath = csvdata.ChildFullPath,
		child.Template = csvdata.ChildTemplate,
		child.NameField = csvdata.ChildNameField,
		child.SchemeLabel = csvdata.ChildSchemeLabel,
		child.SchemeDomainLabel = csvdata.SchemeDomainLabel
RETURN COUNT(*);

LOAD CSV WITH HEADERS FROM 'file:///VetContextGraphHdrs.csv' AS rel FIELDTERMINATOR "," 
MERGE  (source:Element {name: rel.Source, type: rel.SourceType, identifier: rel.SourceIdentifier, eadomain: rel.SourcePropertyValue})
MERGE  (target:Element {name: rel.Target, type: rel.TargetType, identifier: rel.TargetIdentifier, eadomain: rel.TargetPropertyValue})
RETURN *;

LOAD CSV WITH HEADERS FROM 'file:///VetContextGraphHdrs.csv' AS rel FIELDTERMINATOR "," 
MERGE  (source:Element {name: rel.Source, type: rel.SourceType, identifier: rel.SourceIdentifier, eadomain: rel.SourcePropertyValue})
RETURN *;

LOAD CSV WITH HEADERS FROM 'file:///VetContextGraphHdrs.csv' AS rel FIELDTERMINATOR "," 
MERGE  (target:Element {name: rel.Target, type: rel.TargetType, identifier: rel.TargetIdentifier, eadomain: rel.TargetPropertyValue})
RETURN *;

LOAD CSV WITH HEADERS FROM 'file:///VetContextGraphHdrs.csv' AS rel FIELDTERMINATOR "," 
MERGE (source)-[:RELATED_TO]->(target)
RETURN *;

LOAD CSV WITH HEADERS FROM 'file:///VetContextGraphHdrs.csv' AS rel 
FIELDTERMINATOR "," 
with rel
MERGE  (source:Element {name: rel.Source, type: rel.SourceType, identifier: rel.SourceIdentifier, eadomain: rel.SourcePropertyValue}) as s
MERGE  (target:Element {name: rel.Target, type: rel.TargetType, identifier: rel.TargetIdentifier, eadomain: rel.TargetPropertyValue}) as t
with s, t, rel
call apoc.create.relationship(s, rel.RelationshpName, {}, t)
RETURN s, t;

LOAD CSV WITH HEADERS FROM 'file:///VetContextGraphHdrs.csv' AS relations 
FIELDTERMINATOR "," 
with relations
MERGE  (source:Element {name: relations.Source, type: relations.SourceType, identifier: relations.SourceIdentifier, eadomain: relations.SourcePropertyValue})
MERGE  (target:Element {name: relations.Target, type: relations.TargetType, identifier: relations.TargetIdentifier, eadomain: relations.TargetPropertyValue})
with source, target, relations
call apoc.create.relationship(source, relations.RelationshpName, {}, target) yield rel
RETURN source, target;

MATCH (e:Element)
call apoc.create.addLabels([ id(e) ], [ e.type ]) yield node
return e;

