LOAD CSV FROM "file:///Parallelspace_ModelMate_MasterDatasets_Complete10.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN row
LIMIT 100

LOAD CSV FROM "file:///Parallelspace_ModelMate_MasterDatasets_Complete10.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN row
LIMIT 1

//[RelativeStrength, IsInputRelation, StandardVersion, ScriptVersion, RelationName, RelationLabel, RelationQualifiedLabel, RelationForwardVerbLabel, RelationReverseVerbLabel, RelationQualifiedForwardVerbLabel, RelationQualifiedReverseVerbLabel, SourceName, SourceLabel, SourceQualifiedLabel, TargetName, TargetLabel, TargetQualifiedLabel, SourceDomainName, SourceDomainLabel, SourceDomainQualifiedLabel, SourceAspectName, SourceAspectLabel, SourceAspectQualifiedLabel, TargetDomainName, TargetDomainLabel, TargetDomainQualifiedLabel, TargetAspectName, TargetAspectLabel, TargetAspectQualifiedLabel]

LOAD CSV WITH HEADERS FROM "file:///Parallelspace_ModelMate_MasterDatasets_Complete10.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN row
LIMIT 1

//[RelativeStrength, IsInputRelation, StandardVersion, ScriptVersion, RelationName, RelationLabel, RelationQualifiedLabel, RelationForwardVerbLabel, RelationReverseVerbLabel, RelationQualifiedForwardVerbLabel, RelationQualifiedReverseVerbLabel, SourceName, SourceLabel, SourceQualifiedLabel, TargetName, TargetLabel, TargetQualifiedLabel, SourceDomainName, SourceDomainLabel, SourceDomainQualifiedLabel, SourceAspectName, SourceAspectLabel, SourceAspectQualifiedLabel, TargetDomainName, TargetDomainLabel, TargetDomainQualifiedLabel, TargetAspectName, TargetAspectLabel, TargetAspectQualifiedLabel]

LOAD CSV WITH HEADERS FROM "file:///Parallelspace_ModelMate_MasterDatasets_Complete10.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN row
LIMIT 100

CREATE CONSTRAINT ON (c:ElementProtoype_ArchiMate30_TheOpenGroup) ASSERT c.QualifiedName IS UNIQUE;

MATCH (c:ElementProtoype_ArchiMate30_TheOpenGroup) DETACH DELETE c RETURN COUNT(*);

USING PERIODIC COMMIT 1000
LOAD CSV WITH HEADERS FROM "file:///Parallelspace_ModelMate_MasterDatasets_Complete10.csv" as csvdata
FIELDTERMINATOR ','
MERGE  (c:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.SourceQualifiedLabel })
ON CREATE SET c._Created = timestamp(), c._Modified = timestamp(), c._Removed = 0,
		c.Name = csvdata.SourceName,
		c._DatabaseVersion =	   "February 2017 PSN 1.0",
		c._DatabaseSource =		   "Parallelspace_ModelMate_MasterDatasets_Complete10.csv/Source",
		c._DatabaseSourceVersion = csvdata.StandardVersion,
		c._DatabaseScriptVersion = csvdata.ScriptVersion,
		c.Label = csvdata.SourceLabel,
		c.QualifiedLabel = csvdata.SourceQualifiedLabel,
		c.DomainName = csvdata.SourceDomainName,
		c.DomainLabel = csvdata.SourceDomainLabel,
		c.DomainQualifiedLabel = csvdata.SourceDomainQualifiedLabel,
	    c.AspectName = csvdata.SourceAspectName,
		c.AspectLabel = csvdata.SourceAspectLabel,
		c.AspectQualifiedLabel = csvdata.SourceAspectQualifiedLabel
RETURN COUNT(*);

USING PERIODIC COMMIT 1000
LOAD CSV WITH HEADERS FROM "file:///Parallelspace_ModelMate_MasterDatasets_Complete10.csv" as csvdata
FIELDTERMINATOR ','
MERGE  (c:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.TargetQualifiedLabel })
ON CREATE SET c._Created = timestamp(), c._Modified = timestamp(), c._Removed = 0,
		c.Name = csvdata.TargetName,
		c._DatabaseVersion =	   "February 2017 PSN 1.0",
		c._DatabaseSource =		   "Parallelspace_ModelMate_MasterDatasets_Complete10.csv/Target",
		c._DatabaseSourceVersion = csvdata.StandardVersion,
		c._DatabaseScriptVersion = csvdata.ScriptVersion,
		c.Label = csvdata.TargetLabel,
		c.QualifiedLabel = csvdata.TargetQualifiedLabel,
		c.DomainName = csvdata.TargetDomainName,
		c.DomainLabel = csvdata.TargetDomainLabel,
		c.DomainQualifiedLabel = csvdata.TargetDomainQualifiedLabel,
	    c.AspectName = csvdata.TargetAspectName,
		c.AspectLabel = csvdata.TargetAspectLabel,
		c.AspectQualifiedLabel = csvdata.TargetAspectQualifiedLabel
RETURN COUNT(*);

MATCH (e:ElementProtoype_ArchiMate30_TheOpenGroup)
RETURN COUNT(*);

MATCH (e:ElementProtoype_ArchiMate30_TheOpenGroup)
call apoc.create.addLabels([ id(e) ], [ e.QualifiedLabel, e.DomainQualifiedLabel, e.AspectQualifiedLabel ]) yield node
RETURN COUNT(*);

LOAD CSV WITH HEADERS FROM "file:///Parallelspace_ModelMate_MasterDatasets_Complete10.csv" as row
FIELDTERMINATOR ','
WITH row
WHERE row.IsInputRelation = "TRUE"
RETURN row
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Parallelspace_ModelMate_MasterDatasets_Complete10.csv" as row
FIELDTERMINATOR ','
WITH row
WHERE row.IsInputRelation = "TRUE"
RETURN COUNT(*)

//[RelativeStrength, IsInputRelation, StandardVersion, ScriptVersion, RelationName, RelationLabel, RelationQualifiedLabel, RelationForwardVerbLabel, RelationReverseVerbLabel, RelationQualifiedForwardVerbLabel, RelationQualifiedReverseVerbLabel, SourceName, SourceLabel, SourceQualifiedLabel, TargetName, TargetLabel, TargetQualifiedLabel, SourceDomainName, SourceDomainLabel, SourceDomainQualifiedLabel, SourceAspectName, SourceAspectLabel, SourceAspectQualifiedLabel, TargetDomainName, TargetDomainLabel, TargetDomainQualifiedLabel, TargetAspectName, TargetAspectLabel, TargetAspectQualifiedLabel]

USING PERIODIC COMMIT 1000
LOAD CSV WITH HEADERS FROM "file:///Parallelspace_ModelMate_MasterDatasets_Complete10.csv" as csvdata
FIELDTERMINATOR ','
MATCH	(s:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.SourceQualifiedLabel }), 
		(t:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.TargetQualifiedLabel })
WHERE csvdata.IsInputRelation = "FALSE"
call apoc.create.relationship(s, "DerivedRelation_ModelMate30_Parallelspace", {}, t) yield rel
SET rel._Created = timestamp(), rel._Modified = timestamp(), rel._Removed = 0,
    rel.Name = csvdata.RelationName,
    rel.Verb = csvdata.RelationForwardVerbLabel,
	rel._DatabaseVersion =	     "February 2017 PSN 1.0",
	rel._DatabaseSource =		 "Parallelspace_ModelMate_MasterDatasets_Complete10.csv/DerivedRelation",
	rel._DatabaseSourceVersion = csvdata.StandardVersion,
	rel._DatabaseScriptVersion = csvdata.ScriptVersion,

	rel.RelativeStrength = csvdata.RelativeStrength,
	rel.IsInputRelation = csvdata.IsInputRelation,
    rel.Label = csvdata.RelationLabel,
	rel.QualifiedLabel = csvdata.RelationQualifiedLabel,
	rel.ForwardVerbLabel = csvdata.RelationForwardVerbLabel,
	rel.BackwardVerbLabel = csvdata.RelationReverseVerbLabel,
	rel.QualifiedForwardVerbLabel = csvdata.RelationQualifiedForwardVerbLabel,
	rel.QualifiedBackwardVerbLabel = csvdata.RelationQualifiedReverseVerbLabel
RETURN COUNT(*)

USING PERIODIC COMMIT 1000
LOAD CSV WITH HEADERS FROM "file:///Parallelspace_ModelMate_MasterDatasets_Complete10.csv" as csvdata
FIELDTERMINATOR ','
MATCH	(s:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.SourceQualifiedLabel }), 
		(t:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.TargetQualifiedLabel })
WHERE csvdata.IsInputRelation = "TRUE"
call apoc.create.relationship(s, "CoreRelation_ModelMate30_Parallelspace", {}, t) yield rel
SET rel._Created = timestamp(), rel._Modified = timestamp(), rel._Removed = 0,
    rel.Name = csvdata.RelationName,
    rel.Verb = csvdata.RelationForwardVerbLabel,
	rel._DatabaseVersion =	     "February 2017 PSN 1.0",
	rel._DatabaseSource =		 "Parallelspace_ModelMate_MasterDatasets_Complete10.csv/CoreRelation",
	rel._DatabaseSourceVersion = csvdata.StandardVersion,
	rel._DatabaseScriptVersion = csvdata.ScriptVersion,

	rel.RelativeStrength = csvdata.RelativeStrength,
	rel.IsInputRelation = csvdata.IsInputRelation,
    rel.Name2 = csvdata.RelationName,
    rel.Label = csvdata.RelationLabel,
	rel.QualifiedLabel = csvdata.RelationQualifiedLabel,
	rel.ForwardVerbLabel = csvdata.RelationForwardVerbLabel,
	rel.BackwardVerbLabel = csvdata.RelationReverseVerbLabel,
	rel.QualifiedForwardVerbLabel = csvdata.RelationQualifiedForwardVerbLabel,
	rel.QualifiedBackwardVerbLabel = csvdata.RelationQualifiedReverseVerbLabel
RETURN COUNT(*)

MATCH (source:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["BusinessDomain_ArchiMate30_TheOpenGroup"]
RETURN source;

MATCH (source:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["ApplicationDomain_ArchiMate30_TheOpenGroup"]
RETURN source;

MATCH (source:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["TechnologyDomain_ArchiMate30_TheOpenGroup"]
RETURN source;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["BusinessDomain_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace"]
RETURN path;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["ApplicationDomain_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace"]
RETURN path;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["TechnologyDomain_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace"]
RETURN path;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["BusinessDomain_ArchiMate30_TheOpenGroup"]
AND   target.DomainQualifiedLabel IN ["BusinessDomain_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace"]
RETURN path;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["ApplicationDomain_ArchiMate30_TheOpenGroup"]
AND   target.DomainQualifiedLabel IN ["ApplicationDomain_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace"]
RETURN path;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["TechnologyDomain_ArchiMate30_TheOpenGroup"]
AND   target.DomainQualifiedLabel IN ["TechnologyDomain_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace"]
RETURN path;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["TechnologyDomain_ArchiMate30_TheOpenGroup"]
AND   target.DomainQualifiedLabel IN ["TechnologyDomain_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace"]
AND   source.QualifiedName = target.QualifiedName
RETURN path;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["TechnologyDomain_ArchiMate30_TheOpenGroup"]
AND   target.DomainQualifiedLabel IN ["TechnologyDomain_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace", "DerivedRelation_ModelMate30_Parallelspace"]
AND   source.QualifiedName <> target.QualifiedName
RETURN path;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.DomainQualifiedLabel IN ["TechnologyDomain_ArchiMate30_TheOpenGroup"]
AND   target.DomainQualifiedLabel IN ["TechnologyDomain_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace", "DerivedRelation_ModelMate30_Parallelspace"]
AND   source.QualifiedName <> target.QualifiedName
AND   source.QualifiedName = "Node_ArchiMate30_TheOpenGroup"
RETURN path;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.AspectQualifiedLabel IN ["PassiveStructureAspect_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace"]
AND   source.QualifiedName <> target.QualifiedName
RETURN path;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.AspectQualifiedLabel IN ["ActiveStructureAspect_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace"]
AND   source.QualifiedName <> target.QualifiedName
RETURN path;

MATCH path = (source:ElementProtoype_ArchiMate30_TheOpenGroup) -[relation]-> (target:ElementProtoype_ArchiMate30_TheOpenGroup)
WHERE source.AspectQualifiedLabel IN ["BehaviorAspect_ArchiMate30_TheOpenGroup"]
AND   type(relation) IN ["CoreRelation_ModelMate30_Parallelspace"]
AND   source.QualifiedName <> target.QualifiedName
RETURN path;

