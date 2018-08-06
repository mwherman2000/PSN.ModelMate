LOAD CSV FROM "file:///Archi-relationships-xml-2017-01-13d-entities.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN row
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Archi-relationships-xml-2017-01-13d-entities.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN row
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Archi-relationships-xml-2017-01-13d-relations.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN row
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Archi-relationships-xml-2017-01-13d-relations.csv" as csvdata
FIELDTERMINATOR ','
RETURN csvdata.QualifiedSourceConcept, csvdata.QualifiedTargetConcept
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Archi-relationships-xml-2017-01-13d-relations.csv" as csvdata
FIELDTERMINATOR ','
MATCH	(s:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedSourceConcept }), 
		(t:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedTargetConcept })
WITH s, t, split(csvdata.QualifiedRelationsFinal, ",") as relations
RETURN s, t, relations
LIMIT 100

CREATE CONSTRAINT ON (c:ElementProtoype_ArchiMate30_TheOpenGroup) ASSERT c.QualifiedName IS UNIQUE;

MATCH (c:ElementProtoype_ArchiMate30_TheOpenGroup) DETACH DELETE c RETURN COUNT(*);

LOAD CSV WITH HEADERS FROM "file:///Archi-relationships-xml-2017-01-13d-entities.csv" as csvdata
FIELDTERMINATOR ','
MERGE  (c:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedConcept })
ON CREATE SET c._Created = timestamp(), c._Modified = timestamp(), c._Removed = 0,
		c.Name = csvdata.Concept,
		c._DatabaseVersion =	"January 2017 PSN 1.0",
		c._DatabaseSource =		"https://raw.githubusercontent.com/archimatetool/archi/master/com.archimatetool.model/model/relationships.xml",

		c.Type = csvdata.QualifiedConcept,
		c.Domain = csvdata.Domain,
		c.QualifiedDomain = csvdata.QualifiedDomain
RETURN COUNT(*);

MATCH (e:ElementProtoype_ArchiMate30_TheOpenGroup)
call apoc.create.addLabels([ id(e) ], [ e.Type ]) yield node
RETURN COUNT(*);

LOAD CSV WITH HEADERS FROM "file:///Archi-relationships-xml-2017-01-13d-relations.csv" as csvdata
FIELDTERMINATOR ','
MATCH	(s:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedSourceConcept }), 
		(t:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedTargetConcept })
WITH s, t, split(csvdata.QualifiedRelationsFinal, ",") as relations, csvdata
UNWIND relations AS relation
call apoc.create.relationship(s, relation, {}, t) yield rel
WITH rel, split(type(rel), "_") as relparts
SET rel.Name = relparts[0]
RETURN COUNT(*)

MATCH p = (s) -[r:IsComposedOf_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 70, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:Aggregates_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 60, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:IsAssignedTo_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 50, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:Realizes_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 40, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:Serves_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 30, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:Accesses_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 20, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:Influences_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 10, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r]-> (t)
SET r.Strength = coalesce(r.Strength,5), r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ApplicationComponent_ArchiMate30_TheOpenGroup) -[r]-> (t) 
WHERE (NOT t.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture", "ApplicationArchitecture"])
AND   (NONE (r IN rels(p) WHERE type(r) IN ["IsAssociatedWith_ArchiMate30_ModelMate_Parallelspace"]))
RETURN p

MATCH p = (s:ApplicationService_ArchiMate30_TheOpenGroup) -[r]-> (t) 
WHERE (NOT t.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture", "ApplicationArchitecture"])
AND   (ANY (r IN rels(p) WHERE type(r) IN ["FlowsTo_ArchiMate30_ModelMate_Parallelspace", "Accesses_ArchiMate30_ModelMate_Parallelspace", "Triggers_ArchiMate30_ModelMate_Parallelspace"]))
RETURN p

MATCH p = (s) -[r]-> (t) 
WHERE (NOT s.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture"])
AND   (NOT t.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture"])
AND   (ANY (r IN rels(p) WHERE type(r) IN ["FlowsTo_ArchiMate30_ModelMate_Parallelspace", "Accesses_ArchiMate30_ModelMate_Parallelspace", "Triggers_ArchiMate30_ModelMate_Parallelspace"]))
RETURN p

MATCH p = (s) -[r]-> (t) 
WHERE (NOT s.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture"])
AND   (NOT t.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture"])
AND   (ANY (r IN rels(p) WHERE type(r) IN ["FlowsTo_ArchiMate30_ModelMate_Parallelspace", "Triggers_ArchiMate30_ModelMate_Parallelspace"]))
RETURN p

MATCH p = (s) -[r]-> (t) 
WHERE (NOT s.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture"])
AND   (NOT t.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture"])
AND   (ANY (r IN rels(p) WHERE type(r) IN ["Realizes_ArchiMate30_ModelMate_Parallelspace"]))
RETURN p

MATCH p = (s) -[r]-> (t) 
WHERE (NOT s.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
AND   (NOT t.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
AND   (ANY (r IN rels(p) WHERE type(r) IN ["Realizes_ArchiMate30_ModelMate_Parallelspace"]))
RETURN p

MATCH p = (s) -[r]-> (t) 
WHERE (NOT s.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
AND   (NOT t.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
AND   (ANY (r IN rels(p) WHERE type(r) IN ["Serves_ArchiMate30_ModelMate_Parallelspace"]))
RETURN p

MATCH p = (s) -[r:IsComposedOf_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 70, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:Aggregates_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 60, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:IsAssignedTo_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 50, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:Realizes_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 40, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:Serves_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 30, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:Accesses_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 20, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r:Influences_ArchiMate30_ModelMate_Parallelspace]-> (t)
SET r.Strength = 10, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s) -[r]-> (t)
WHERE NOT (type(r) in ["Influences_ArchiMate30_ModelMate_Parallelspace", "Accesses_ArchiMate30_ModelMate_Parallelspace", "Serves_ArchiMate30_ModelMate_Parallelspace", "Serves_ArchiMate30_ModelMate_Parallelspace", "IsAssignedTo_ArchiMate30_ModelMate_Parallelspace", "Aggregates_ArchiMate30_ModelMate_Parallelspace", "IsComposedOf_ArchiMate30_ModelMate_Parallelspace"])
SET r.Strength = coalesce(r.Strength,5), r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)


MATCH p = (s) -[r]-> (t) 
WHERE (NOT s.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
AND   (NOT t.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
RETURN  p AS shortestPath, reduce(Weight=0, r in rels(p) | Weight+r.Strength) AS totalWeight
ORDER BY totalWeight DESC
LIMIT 100

MATCH p = (s) -[r]-> (t) 
WHERE (NOT s.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
AND   (NOT t.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
WITH  p AS shortestPath, reduce(Weight=0, r in rels(p) | Weight+r.Strength) AS totalWeight
WHERE totalWeight >= 10
RETURN shortestPath, totalWeight
ORDER BY totalWeight DESC
LIMIT 1000

MATCH p = (s) -[r]-> (t) 
WHERE (NOT s.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
AND   (NOT t.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
WITH  p AS shortestPath, reduce(Weight=0, r in rels(p) | Weight+r.Strength) AS totalWeight, s, t
WHERE totalWeight >= 10
RETURN DISTINCT s, t
ORDER BY totalWeight DESC
LIMIT 1000

MATCH p1 = (s) -[r]-> (t) 
WITH DISTINCT s, t
MATCH p2 = (s) -[r]-> (t) 
WITH s, t, reduce(Weight = {max: 0}, x in rels(p2) |
	CASE WHEN x.Strength > Weight.max
		 THEN  {max: x.Strength, id: x.Name}
		 ELSE Weight
    END
) AS Current
RETURN s.Name, t.Name, Current
LIMIT 100

MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH DISTINCT s, t, COLLECT({left: s, right: t}) as Distincts
RETURN COUNT(*) // 3718

MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH DISTINCT s, t, COLLECT({leftName: s.QualifiedName, rightName: t.QualifiedName}) as Distincts
RETURN COUNT(*) // 3718

MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH DISTINCT s, t, COLLECT({leftName: s.QualifiedName, rightName: t.QualifiedName}) as Distincts
RETURN Distincts
LIMIT 100

MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH DISTINCT s, t, COLLECT({leftName: s.QualifiedName, rightName: t.QualifiedName}) as Distincts
RETURN Distincts[0] as First

MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH DISTINCT s, t, COLLECT({leftName: s.QualifiedName, rightName: t.QualifiedName}) as Distincts
WITH Distincts[0] AS First
MATCH p2 = (s2:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: First.leftName})-[r2]->(t2:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: First.rightName}) 
RETURN COUNT(*)

MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH DISTINCT {leftName: s.QualifiedName, rightName: t.QualifiedName} as Distincts
RETURN COUNT(*)

MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH DISTINCT {leftName: s.QualifiedName, rightName: t.QualifiedName} as Distincts
RETURN Distincts

MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH DISTINCT {left: s, right: t} as Distincts
RETURN Distincts

MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH DISTINCT {left: s.QualifiedName, right: t.QualifiedName} as Distincts
MATCH p2 = (s2:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: Distincts[0].left})-[r2]->(t2:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: Distincts[0].right})
WITH s2, t2, reduce(Weight = {max: 0}, x in rels(p2) |
	CASE WHEN x.Strength > Weight.max
		 THEN  {max: x.Strength, id: x.Name}
		 ELSE Weight
    END
) AS Current
RETURN Current


MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH DISTINCT s, t, COLLECT({leftName: s.QualifiedName, rightName: t.QualifiedName}) as Distincts
WITH Distincts[0] AS First
MATCH p2 = (s2:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: First.leftName})-[r2]->(t2:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: First.rightName})
WITH s2, t2, reduce(Weight = {max: 0}, x in rels(p2) |
	CASE WHEN x.Strength > Weight.max
		 THEN  {max: x.Strength, id: x.Name}
		 ELSE Weight
    END
) AS Current
RETURN Current


MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH DISTINCT s, t, COLLECT({leftName: s.QualifiedName, rightName: t.QualifiedName}) as Distincts
WITH Distincts[0] AS First
MATCH p2 = (s2:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: First.leftName})-[r2]->(t2:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: First.rightName})
WITH s2, t2, reduce(Weight = {max: 0}, x in rels(p2) |
	CASE WHEN x.Strength > Weight.max
		 THEN  {max: x.Strength, id: x.Name}
		 ELSE Weight
    END
) AS Current
RETURN s2.Name, t2.Name, Current
LIMIT 100

MATCH path1 = (s1:ElementProtoype_ArchiMate30_TheOpenGroup) -[r1]-> (t1:ElementProtoype_ArchiMate30_TheOpenGroup) 
WHERE (NOT s1.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
AND   (NOT t1.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture", "TechnologyArchitecture"])
AND   (s1.QualifiedName <> t1.QualifiedName)
AND   (NOT s1.QualifiedName =~ ".*Collaboration.*")
AND   (NOT s1.QualifiedName =~ ".*Interaction.*")
AND   (NOT t1.QualifiedName =~ ".*Collaboration.*")
AND   (NOT t1.QualifiedName =~ ".*Interaction.*")
WITH DISTINCT s1, t1, max(r1.Strength) AS MaxStrength
MATCH path2 = (s2)-[r2]->(t2) 
WHERE s2.QualifiedName = s1.QualifiedName AND t2.QualifiedName = t1.QualifiedName AND r2.Strength = MaxStrength AND r2.Strength >= 40
RETURN path2

MATCH p=(n:FooNode)-[r]->(m:FooNode)
WITH DISTINCT n, m, count(*) as Counts
//RETURN Counts, n.name. m.name
UNWIND range(0,Counts-1) AS idx
RETURN idx

MATCH p=(n)-[r]->(m)
WITH COLLECT({paths:p, weight: r.weight}) AS pathinfo
RETURN FILTER(pi IN pathinfo WHERE pi.weight = max(pi.weight)) AS longestPath

MATCH p=(n)-[r]->(m)
WITH COLLECT({paths:p, weight: r.weight}) AS pathinfo, MAX(r.weight) AS maxw
RETURN FILTER(pi IN pathinfo WHERE pi.weight = maxw) AS longestPath

MATCH p=(n:FooNode)-[r]->(m:FooNode)
WITH DISTINCT n, m, max(r.weight) AS maxw
RETURN n.name, m.name, maxw

MATCH p=(n:FooNode)-[r]->(m:FooNode)
WITH DISTINCT n, m, max(r.weight) AS maxw
MATCH p2=(n2)-[r2]->(m2) WHERE n2.name = n.name AND m2.name = m.name AND r2.weight = maxw
RETURN n2.name, type(r2), m2.name, maxw

MATCH p=(n:FooNode)-[r]->(m:FooNode)
WITH DISTINCT n, m, max(r.weight) AS maxw
MATCH p2=(n2)-[r2]->(m2) WHERE n2.name = n.name AND m2.name = m.name AND r2.weight = maxw
RETURN n2.name, type(r2), m2.name, maxw

MATCH p=(n:FooNode)-[r]->(m:FooNode)
WITH DISTINCT n, m, max(r.weight) AS maxw
MATCH p2=(n2)-[r2]->(m2) WHERE n2.name = n.name AND m2.name = m.name AND r2.weight = maxw
RETURN p2

MATCH p=(n:FooNode)-[r]->(m:FooNode)
WITH DISTINCT n, m, max(r.weight) AS maxw
MATCH p2=(n2)-[r2]->(m2) 
WHERE n2.name = n.name AND m2.name = m.name AND r2.weight = maxw
RETURN p2, type(r2)