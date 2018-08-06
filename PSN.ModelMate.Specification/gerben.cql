MATCH p1 = (s:ElementProtoype_ArchiMate30_TheOpenGroup) -[r]-> (t:ElementProtoype_ArchiMate30_TheOpenGroup) 
SET		r._DatabaseVersion = "January 2017 PSN 1.0",
        r._IsCore_ArchiMate30_MasteringArchiMateIII = 0,
		r._IsArchi_ArchiMate30_Archi = 1,
		r._DatabaseSource =	"https://raw.githubusercontent.com/archimatetool/archi/master/com.archimatetool.model/model/relationships.xml"
RETURN COUNT(*)

LOAD CSV FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN row
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN row
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN DISTINCT row.QualifiedSourceConcept

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as row
FIELDTERMINATOR ','
WITH row 
RETURN DISTINCT row.QualifiedTargetConcept

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as csvdata
FIELDTERMINATOR ','
MATCH	(s:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedSourceConcept }), 
		(t:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedTargetConcept })
WITH s, t, split(csvdata.RelationCodes4, ",") as relations
RETURN s, t, relations
LIMIT 100

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as csvdata
FIELDTERMINATOR ','
WITH split(csvdata.RelationCodes4, ",") as relations,csvdata
UNWIND relations AS relation
MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedSourceConcept })-[r]->
		  (t:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedTargetConcept })
WHERE type(r) = relation
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as csvdata
FIELDTERMINATOR ','
WITH split(csvdata.RelationCodes4, ",") as relations,csvdata
UNWIND relations AS relation
MATCH WHERE NOT (s:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedSourceConcept })-[r]->
		  (t:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedTargetConcept })
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as csvdata
FIELDTERMINATOR ','
WITH split(csvdata.RelationCodes4, ",") as relations,csvdata
UNWIND relations AS relation
RETURN DISTINCT relation

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as csvdata
FIELDTERMINATOR ','
WITH split(csvdata.RelationCodes4, ",") as relations,csvdata
UNWIND relations AS relation
RETURN DISTINCT csvdata.QualifiedSourceConcept, csvdata.QualifiedTargetConcept, relation

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as csvdata
FIELDTERMINATOR ','
WITH split(csvdata.RelationCodes4, ",") as relations,csvdata
UNWIND relations AS relation
MATCH p = (s:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedSourceConcept })-[r]->
		  (t:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedTargetConcept })
WHERE type(r) = relation
RETURN p

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as csvdata
FIELDTERMINATOR ','
WITH split(csvdata.RelationCodes4, ",") as relations,csvdata
UNWIND relations AS relation
MATCH	(s:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedSourceConcept })-[r]->
		(t:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedTargetConcept })
WHERE type(r) = relation
SET     r._IsCore_ArchiMate30_MasteringArchiMateIII = 1,
		r._DatabaseVersion = "January 2017 PSN 1.0",
		r._DatabaseSource =	 "Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv"
RETURN COUNT(*)

LOAD CSV WITH HEADERS FROM "file:///Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv" as csvdata
FIELDTERMINATOR ','
WITH split(csvdata.RelationCodes4, ",") as relations,csvdata
UNWIND relations AS relation
MERGE	(s:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedSourceConcept })-[r]->
		(t:ElementProtoype_ArchiMate30_TheOpenGroup { QualifiedName: csvdata.QualifiedTargetConcept })
SET     r._IsCore_ArchiMate30_MasteringArchiMateIII = 1,
		r._IsCoreRelation = relation,
		r._IsCoreDatabaseVersion = "January 2017 PSN 1.0",
		r._IsCoreDatabaseSource =	 "Gerben-AllowedRelationsArchiMate30-mwh4-subset.csv"
RETURN COUNT(*)

