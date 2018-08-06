Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv

LOAD CSV FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN row
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN row
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as csvdata
FIELDTERMINATOR ','
WITH csvdata, split(csvdata.Relations, ",") as relations
RETURN csvdata.QualifiedSourceConcept, csvdata.QualifiedTargetConcept, relations
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as csvdata
FIELDTERMINATOR ','
WITH csvdata, split(csvdata.Relations, ",") as relations
UNWIND relations AS relation
RETURN csvdata.QualifiedSourceConcept, csvdata.QualifiedTargetConcept, relation
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as csvdata
FIELDTERMINATOR ','
WITH csvdata, split(csvdata.Relations, ",") as relations
UNWIND relations AS relation
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as csvdata
FIELDTERMINATOR ','
WITH csvdata, split(csvdata.Relations, ",") as relations
UNWIND relations AS relation
WITH DISTINCT {sname: csvdata.QualifiedSourceConcept, tname: csvdata.QualifiedTargetConcept, rname: relation} AS tuple
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as csvdata
FIELDTERMINATOR ','
WITH csvdata, split(csvdata.Relations, ",") as relations
UNWIND relations AS relation
WITH DISTINCT {sname: csvdata.QualifiedSourceConcept, tname: csvdata.QualifiedTargetConcept, r: relation} AS tuple
MATCH	(s:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: tuple.sname}), (t:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: tuple.tname}) 
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as csvdata
FIELDTERMINATOR ','
WITH csvdata, split(csvdata.Relations, ",") as relations
UNWIND relations AS relation
WITH DISTINCT {sname: csvdata.QualifiedSourceConcept, tname: csvdata.QualifiedTargetConcept, r: relation} AS tuple
MATCH	(s:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: tuple.sname}) 
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as csvdata
FIELDTERMINATOR ','
WITH csvdata, split(csvdata.Relations, ",") as relations
UNWIND relations AS relation
WITH DISTINCT {sname: csvdata.QualifiedSourceConcept, tname: csvdata.QualifiedTargetConcept, r: relation} AS tuple
MATCH	(t:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: tuple.tname}) 
RETURN COUNT(*)

MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
RETURN COUNT(*)

MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
SET		r._DatabaseVersion = "January 2017 PSN 1.0",
        r._IsCore_ArchiMate30_MasteringArchiMateIII = 0,
		r._IsArchi_ArchiMate30_Archi = 1,
		r._DatabaseSource =	"https://raw.githubusercontent.com/archimatetool/archi/master/com.archimatetool.model/model/relationships.xml"
RETURN COUNT(*)

MATCH (n:FooNode) return n;
MATCH (s:FooNode {name: "a"}), (t:FooNode {name: "b"})
call apoc.create.relationship(s, "FooRel", {}, t) yield rel
SET rel.Source = "Hi there"
return rel

MATCH (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
DELETE r
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as csvdata
FIELDTERMINATOR ','
WITH csvdata, split(csvdata.Relations, ",") as relations
UNWIND relations AS relation
WITH DISTINCT {sname: csvdata.QualifiedSourceConcept, tname: csvdata.QualifiedTargetConcept, r: relation} AS tuple
MATCH	(s:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: tuple.sname}), (t:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: tuple.tname}) 
call apoc.create.relationship(s, tuple.r, {}, t) yield rel
SET		rel._DatabaseVersion = "January 2017 PSN 1.0",
        rel._IsCore_ArchiMate30_MasteringArchiMateIII = 0,
		rel._IsArchi_ArchiMate30_Archi40 = 0,
		rel._DatabaseSource = "Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv"
WITH rel, split(type(rel), "_") as relparts
SET     rel.Name = relparts[0]
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WITH r, split(type(r), "_") as relparts
SET  r.Name = relparts[0] + " (" + r.Strength + ")"
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as csvdata
FIELDTERMINATOR ','
WITH csvdata, split(csvdata.Relations, ",") as relations
WHERE toInt(csvdata.IsCore_ArchiMate30_MasteringArchiMateIII) = 1
UNWIND relations AS relation
WITH {sname: csvdata.QualifiedSourceConcept, tname: csvdata.QualifiedTargetConcept, rname: relation, iscore: csvdata.IsCore_ArchiMate30_MasteringArchiMateIII} AS tuple
MATCH	(s:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: tuple.sname}) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: tuple.tname}) 
WHERE type(r) = tuple.rname
SET r._IsCore_ArchiMate30_MasteringArchiMateIII = 1
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh6-combined.csv" as csvdata
FIELDTERMINATOR ','
WITH csvdata, split(csvdata.Relations, ",") as relations
WHERE toInt(csvdata.IsArchi_ArchiMate30_Archi40) = 1
UNWIND relations AS relation
WITH {sname: csvdata.QualifiedSourceConcept, tname: csvdata.QualifiedTargetConcept, rname: relation, iscore: csvdata.IsArchi_ArchiMate30_Archi40} AS tuple
MATCH	(s:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: tuple.sname}) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup {QualifiedName: tuple.tname}) 
WHERE type(r) = tuple.rname
SET r._IsArchi_ArchiMate30_Archi40 = 1
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
WHERE r._IsArchi_ArchiMate30_Archi40 = 0
AND   r._IsCore_ArchiMate30_MasteringArchiMateIII = 1
RETURN s.QualifiedName, type(r), t.QualifiedName
ORDER BY type(r), s.QualifiedName, t.QualifiedName

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:IsComposedOf_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 240, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:Aggregates_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 230, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:IsAssignedTo_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 220, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:Realizes_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 210, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:Serves_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 130, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:Accesses_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 120, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:Influences_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 110, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:Triggers_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 60, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:FlowsTo_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 50, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:IsDerivedFrom_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 40, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:IsAssociatedWith_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 30, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:Joins_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 20, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r:IsDerivedFromMeta_ArchiMate30_ModelMate_Parallelspace]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup)
SET r.Strength = 10, r.Name = r.Name + " (" + r.Strength + ")"
RETURN COUNT(*)

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

MATCH path1 = (s1:ElementProtoype_ArchiMate30_TheOpenGroup) -[r1]-> (t1:ElementProtoype_ArchiMate30_TheOpenGroup) 
WHERE (r1._IsCore_ArchiMate30_MasteringArchiMateIII = 1)
  AND (NOT s1.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture"])
  AND (NOT t1.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic", "PhysicalArchitecture"])
  AND (s1.QualifiedName <> t1.QualifiedName)
  AND (NOT s1.QualifiedName =~ ".*Collaboration.*")
  AND (NOT s1.QualifiedName =~ ".*Interaction.*")
  AND (NOT t1.QualifiedName =~ ".*Collaboration.*")
  AND (NOT t1.QualifiedName =~ ".*Interaction.*")
WITH DISTINCT s1, t1, max(r1.Strength) AS MaxStrength
MATCH path2 = (s2)-[r2]->(t2) 
WHERE s2.QualifiedName = s1.QualifiedName 
  AND t2.QualifiedName = t1.QualifiedName 
  AND r2.Strength = MaxStrength AND r2.Strength >= 0
RETURN path2

MATCH 
  path1 = (s1:ElementProtoype_ArchiMate30_TheOpenGroup) -[r1]-> (t1:ElementProtoype_ArchiMate30_TheOpenGroup) 
WHERE (r1._IsCore_ArchiMate30_MasteringArchiMateIII = 1)
  AND (NOT s1.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic"])
  AND (NOT t1.Domain in ["Motivation", "Strategy", "ImplementationAndMigration", "Generic"])
  AND (s1.QualifiedName <> t1.QualifiedName)
  AND (NOT s1.QualifiedName =~ ".*Collaboration.*")
  AND (NOT s1.QualifiedName =~ ".*Interaction.*")
  AND (NOT t1.QualifiedName =~ ".*Collaboration.*")
  AND (NOT t1.QualifiedName =~ ".*Interaction.*")
WITH DISTINCT s1, t1, max(r1.Strength) AS MaxStrength
MATCH path2 = (s2)-[r2]->(t2) 
WHERE s2.QualifiedName = s1.QualifiedName 
  AND t2.QualifiedName = t1.QualifiedName 
  AND r2.Strength = MaxStrength AND r2.Strength >= 0
RETURN path2

MATCH p = (s:Node_ArchiMate30_TheOpenGroup) -[r]- (t:Product_ArchiMate30_TheOpenGroup)
RETURN COUNT(*) 

MATCH (s1:ElementProtoype_ArchiMate30_TheOpenGroup)
RETURN s1
LIMIT 100

MATCH path1 = (s1:ElementProtoype_ArchiMate30_TheOpenGroup) -[r1]-> (t1:ElementProtoype_ArchiMate30_TheOpenGroup)
RETURN Count(*) 


