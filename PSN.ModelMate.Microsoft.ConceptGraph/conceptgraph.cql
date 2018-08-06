names=['concept','instance','count']
http://www.orbifold.net/default/2016/11/16/microsoft-concept-graph-in-neo4j/

MATCH (a)-[r]->(b) 
WHERE labels(a) <> [] AND labels(b) <> [] 
RETURN DISTINCT head(labels(a)) AS FromThis, type(r) as Using, head(labels(b)) AS ToThat 
LIMIT 25

http://iansrobinson.com/2013/06/24/cypher-calculating-shortest-weighted-path/

// https://neo4j.com/blog/neo4j-community-november-2016/
// http://www.orbifold.net/default/2016/11/16/microsoft-concept-graph-in-neo4j/
// https://concept.research.microsoft.com/

MATCH (c:Concept_ConceptGraph_ModelMate) DETACH DELETE c RETURN COUNT(*);

USING PERIODIC COMMIT 1000
MATCH (i:Instance_ConceptGraph_ModelMate) DETACH DELETE i RETURN COUNT(*);

CREATE CONSTRAINT ON (c:Concept_ConceptGraph_ModelMate)  ASSERT c.Name IS UNIQUE;
CREATE CONSTRAINT ON (i:Instance_ConceptGraph_ModelMate) ASSERT i.Name IS UNIQUE;

LOAD CSV FROM "file:///data-concept-instance-relations.txt" as row
FIELDTERMINATOR '\t'
WITH row LIMIT 100
MERGE (c:Concept {name: row[0]}) MERGE (i:Instance {name:row[1]}) MERGE (i)-[:isa]->(c)

LOAD CSV FROM "file:///data-concept-instance-relations.txt" as row
FIELDTERMINATOR '\t'
WITH row 
RETURN row
LIMIT 10

USING PERIODIC COMMIT 1000
LOAD CSV FROM "file:///data-concept-instance-relations.txt" as row
FIELDTERMINATOR '\t'
WITH row
MERGE (c:Concept_ConceptGraph_ModelMate {Name:row[0]}) 
MERGE (i:Instance_ConceptGraph_ModelMate {Name:row[1]}) 
MERGE (i)-[:IsA_ConceptGraph_ModelMate {Count:toInt(row[2])}]->(c)
RETURN COUNT(*)

USING PERIODIC COMMIT 1000
LOAD CSV FROM "file:///data-concept-instance-relations.txt" as row
FIELDTERMINATOR '\t'
WITH row
MERGE (c:Concept_ConceptGraph_ModelMate {Name:row[0]}) 
RETURN COUNT(*)

LOAD CSV FROM "file:///data-concept-instance-relations.txt" as row
FIELDTERMINATOR '\t'
WITH row
RETURN COUNT(DISTINCT row[0])

match (n:Concept_ConceptGraph_ModelMate) RETURN DISTINCT COUNT(n);

MATCH (c:Concept_ConceptGraph_ModelMate) WHERE c.Name =~ '.*enhanced enforcement initiative.*'  
RETURN c, id(c);

USING PERIODIC COMMIT 1000
LOAD CSV FROM "file:///data-concept-instance-relations.txt" as row
FIELDTERMINATOR '\t'
WITH row
MERGE (i:Instance_ConceptGraph_ModelMate {Name:row[1]}) 
RETURN COUNT(*)

LOAD CSV FROM "file:///data-concept-instance-relations.txt" as row
FIELDTERMINATOR '\t'
WITH row
RETURN COUNT(DISTINCT row[1])

match (n:Instance_ConceptGraph_ModelMate) RETURN DISTINCT COUNT(n);

MATCH (c:Instance_ConceptGraph_ModelMate) WHERE c.Name =~ '.*pattern of violation notification.*'  
RETURN c, id(c);

USING PERIODIC COMMIT 1000
LOAD CSV FROM "file:///data-concept-instance-relations.txt" as row
FIELDTERMINATOR '\t'
WITH row
MATCH (i:Instance_ConceptGraph_ModelMate {Name:row[1]}), (c:Concept_ConceptGraph_ModelMate {Name:row[0]})
CREATE (i)-[:IsA_ConceptGraph_ModelMate {Weight:toInt(row[2])}]->(c)
RETURN COUNT(*)

LOAD CSV FROM "file:///data-concept-instance-relations.txt" as row
FIELDTERMINATOR '\t'
WITH row
RETURN COUNT(*)

MATCH (i:Instance_ConceptGraph_ModelMate)-[r:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate) 
RETURN COUNT(r)

CALL apoc.meta.stats yield labelCount, relTypeCount, propertyKeyCount, nodeCount, relCount, labels, relTypes, stats

MATCH (i:Instance_ConceptGraph_ModelMate)-[r:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate) WHERE i.Name = 'rembrandt'  
RETURN r

MATCH (i:Instance_ConceptGraph_ModelMate)-[r:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate) WHERE i.Name =~ '(?i).*Rembrandt.*'  
RETURN r

MATCH p = (i:Instance_ConceptGraph_ModelMate)-[r:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate) WHERE c.Name = 'cheese'  
RETURN count(*)

MATCH p = (i:Instance_ConceptGraph_ModelMate)-[r:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate) WHERE c.Name = 'red wine'
RETURN p LIMIT 200

MATCH p = (i:Instance_ConceptGraph_ModelMate)-[r:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate) WHERE c.Name = 'white wine'
RETURN p LIMIT 200

MATCH p = (i:Instance_ConceptGraph_ModelMate)-[r:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate) WHERE c.Name = 'type of wine'
RETURN p

MATCH p = (i:Instance_ConceptGraph_ModelMate)-[r:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate) WHERE c.Name = 'type of cheese'
RETURN p

MATCH (c:Concept_ConceptGraph_ModelMate) WHERE c.Name =~ '.*wine.*'
RETURN c

MATCH (c:Concept_ConceptGraph_ModelMate) WHERE c.Name =~ '.*wine'
RETURN c

MATCH (i:Instance_ConceptGraph_ModelMate) WHERE i.Name = 'red flower'  
RETURN i

MATCH (i:Concept_ConceptGraph_ModelMate) WHERE i.Name = 'algebra'  
RETURN i

MATCH p = shortestPath((c:Concept_ConceptGraph_ModelMate {Name:"algebra"})-[*]-(i:Instance_ConceptGraph_ModelMate {Name:"red flower"})) 
RETURN p

MATCH p = shortestPath((c:Concept_ConceptGraph_ModelMate)-[*]-(i:Instance_ConceptGraph_ModelMate {mame:"microsoft"}))
LIMIT 25 
RETURN p

MATCH p = shortestPath((iany:Instance_ConceptGraph_ModelMate)-[*1..2]-(i:Instance_ConceptGraph_ModelMate {mame:"microsoft"}))
LIMIT 25 
RETURN p


MATCH p = shortestPath((i1:Instance_ConceptGraph_ModelMate {Name:"quantum mechanics"})-[*]-(i2:Instance_ConceptGraph_ModelMate {Name:"dog"}))
RETURN p LIMIT 25 

MATCH p = (i1:Instance_ConceptGraph_ModelMate {Name:"quantum mechanics"})-[*1..3]-(i2:Instance_ConceptGraph_ModelMate {Name:"dog"})
RETURN p LIMIT 25

MATCH (c:Concept_ConceptGraph_ModelMate) WHERE c.Name =~ '.*enhanced enforcement initiative.*'  
RETURN c

MATCH (c:Instance_ConceptGraph_ModelMate) WHERE c.Name =~ '.*fuck.*'  
RETURN c

MATCH (c:Concept_ConceptGraph_ModelMate) WHERE c.Name =~ '.*fuck.*'  
RETURN c

//
// https://gist.github.com/jexp/d3110f05b25914ffb698775f02d04696
//
MATCH (i:Instance_ConceptGraph_ModelMate {Name:"apple"})-[r:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate)
RETURN i.Name, r.Weight, c.Name ORDER BY r.Weight DESC LIMIT 10


MATCH (leftInstance:Instance_ConceptGraph_ModelMate {Name:"apple"})-[:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate)<-[:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate {Name:"pie"})
USING INDEX leftInstance:Instance_ConceptGraph_ModelMate(Name)
USING INDEX rightInstance:Instance_ConceptGraph_ModelMate(Name)
MATCH (c)<-[:IsA_ConceptGraph_ModelMate]-(o) WHERE o <> leftInstance and o <> rightInstance
WITH o, count(*) as freq order by freq desc limit 10
RETURN o.Name, freq;

MATCH (leftInstance:Instance_ConceptGraph_ModelMate {Name:"apple"})-[:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate)<-[:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate {Name:"ipad"})
USING INDEX leftInstance:Instance_ConceptGraph_ModelMate(Name)
USING INDEX rightInstance:Instance_ConceptGraph_ModelMate(Name)
MATCH (c)<-[:IsA_ConceptGraph_ModelMate]-(o) WHERE o <> leftInstance and o <> rightInstance
WITH o, count(*) as freq order by freq desc limit 10
RETURN o.Name, freq;


MATCH (leftInstance:Instance_ConceptGraph_ModelMate {Name:"apple"})-[r1:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate {Name:"pie"})
USING INDEX leftInstance:Instance_ConceptGraph_ModelMate(Name)
USING INDEX rightInstance:Instance_ConceptGraph_ModelMate(Name)
MATCH (c)<-[s:IsA_ConceptGraph_ModelMate]-(o)
WITH o, count(*) as freq order by freq desc limit 10
RETURN o.Name, freq;

MATCH (leftInstance:Instance_ConceptGraph_ModelMate {Name:"apple"})-[r1:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate {Name:"ipad"})
USING INDEX leftInstance:Instance_ConceptGraph_ModelMate(Name)
USING INDEX rightInstance:Instance_ConceptGraph_ModelMate(Name)
MATCH (c)<-[s:IsA_ConceptGraph_ModelMate]-(o)
WITH o, count(*) as freq order by freq desc limit 10
RETURN o.Name, freq;


MATCH (leftInstance:Instance_ConceptGraph_ModelMate {Name:"apple"})-[r1:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate {Name:"pie"})
USING INDEX leftInstance:Instance_ConceptGraph_ModelMate(Name)
USING INDEX rightInstance:Instance_ConceptGraph_ModelMate(Name)
RETURN c.Name, r1.Weight+r2.Weight as freq order by freq desc limit 10;

MATCH (leftInstance:Instance_ConceptGraph_ModelMate {Name:"apple"})-[r1:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate {Name:"ipad"})
USING INDEX leftInstance:Instance_ConceptGraph_ModelMate(Name)
USING INDEX rightInstance:Instance_ConceptGraph_ModelMate(Name)
RETURN c.Name, r1.Weight+r2.Weight as freq order by freq desc limit 10;


MATCH (leftInstance:Instance_ConceptGraph_ModelMate {Name:"apple"})-[:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate)<-[:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate {Name:"pie"})
USING INDEX leftInstance:Instance_ConceptGraph_ModelMate(Name)
USING INDEX rightInstance:Instance_ConceptGraph_ModelMate(Name)
RETURN count(*); // 146

MATCH (leftInstance:Instance_ConceptGraph_ModelMate {Name:"apple"})-[:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate)<-[:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate {Name:"ipad"})
USING INDEX leftInstance:Instance_ConceptGraph_ModelMate(Name)
USING INDEX rightInstance:Instance_ConceptGraph_ModelMate(Name)
RETURN count(*); // 106


with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
match (i:Instance_ConceptGraph_ModelMate {Name:word})
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance)
MATCH (concept)<-[:IsA_ConceptGraph_ModelMate]-(conceptInstance) WHERE conceptInstance <> leftInstance AND conceptInstance <> rightInstance
WITH idx, leftInstance, rightInstance, conceptInstance, count(*) as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect(conceptInstance.Name)[0..5] as conceptInstances order by idx;

with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
match (i:Instance_ConceptGraph_ModelMate {Name:word})
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance)
MATCH (concept)<-[r3:IsA_ConceptGraph_ModelMate]-(conceptInstance) WHERE conceptInstance <> leftInstance AND conceptInstance <> rightInstance
WITH idx, leftInstance, rightInstance, conceptInstance, count(*) as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect(conceptInstance.Name)[0..5] as conceptInstances order by idx;

with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
match (i:Instance_ConceptGraph_ModelMate {Name:word})
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance)
MATCH (concept)<-[r3:IsA_ConceptGraph_ModelMate]-(conceptInstance) WHERE conceptInstance <> leftInstance AND conceptInstance <> rightInstance
WITH idx, leftInstance, rightInstance, concept, conceptInstance, count(*) as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect({conceptName: concept.Name, conceptInstanceName: conceptInstance.Name})[0..5] as conceptInstances order by idx;

with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
match (i:Instance_ConceptGraph_ModelMate {Name:word})
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance)
MATCH (concept)<-[r3:IsA_ConceptGraph_ModelMate]-(conceptInstance) WHERE conceptInstance <> leftInstance AND conceptInstance <> rightInstance
WITH idx, leftInstance, rightInstance, concept, conceptInstance, (r1.Weight+r2.Weight)*r3.Weight as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect({conceptName: concept.Name, conceptInstanceName: conceptInstance.Name, weight:freq})[0..5] as conceptInstances order by idx;

cypher runtime=compiled profile
with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
match (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT (i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance)
MATCH (concept)<-[r3:IsA_ConceptGraph_ModelMate]-(conceptInstance) 
WHERE conceptInstance <> leftInstance AND conceptInstance <> rightInstance
WITH idx, leftInstance, rightInstance, concept, conceptInstance, (r1.Weight+r2.Weight)*r3.Weight as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect({conceptName: concept.Name, conceptInstanceName: conceptInstance.Name, weight:freq})[0..5] as conceptInstances order by idx;

cypher runtime=compiled profile
with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
match (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT ((i.Name = "is") OR ((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
return idx, leftInstance, rightInstance;


cypher runtime=compiled profile
with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT (((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance:Instance_ConceptGraph_ModelMate)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate)
MATCH (concept:Concept_ConceptGraph_ModelMate)<-[r3:IsA_ConceptGraph_ModelMate]-(conceptInstance:Instance_ConceptGraph_ModelMate) 
WHERE conceptInstance <> leftInstance AND conceptInstance <> rightInstance
WITH idx, leftInstance, rightInstance, concept, conceptInstance, (r1.Weight+r2.Weight)*r3.Weight as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect({conceptName: concept.Name, conceptInstanceName: conceptInstance.Name, weight:freq})[0..2] as weightedConceptInstances order by idx;

cypher runtime=compiled profile
with "apple engineer eating apple" as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT (((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance:Instance_ConceptGraph_ModelMate)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate)
MATCH (concept:Concept_ConceptGraph_ModelMate)<-[r3:IsA_ConceptGraph_ModelMate]-(conceptInstance:Instance_ConceptGraph_ModelMate) 
WHERE conceptInstance <> leftInstance AND conceptInstance <> rightInstance
WITH idx, leftInstance, rightInstance, concept, conceptInstance, (r1.Weight+r2.Weight)*r3.Weight as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect({conceptName: concept.Name, conceptInstanceName: conceptInstance.Name, weight:freq})[0..2] as weightedConceptInstances order by idx;

cypher runtime=compiled profile
with "apple engineer " as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT (((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance:Instance_ConceptGraph_ModelMate)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate)
MATCH (concept:Concept_ConceptGraph_ModelMate)<-[r3:IsA_ConceptGraph_ModelMate]-(conceptInstance:Instance_ConceptGraph_ModelMate) 
WHERE conceptInstance <> leftInstance AND conceptInstance <> rightInstance
WITH idx, leftInstance, rightInstance, concept, conceptInstance, (r1.Weight+r2.Weight)*r3.Weight as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect({conceptName: concept.Name, conceptInstanceName: conceptInstance.Name, weight:freq})[0..2] as weightedConceptInstances order by idx;

cypher runtime=compiled profile
with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT (((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance:Instance_ConceptGraph_ModelMate)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate)
WHERE NOT (concept.Name IN [ "concept", "instance", "word"])
MATCH (concept:Concept_ConceptGraph_ModelMate)<-[r3:IsA_ConceptGraph_ModelMate]-(conceptInstance:Instance_ConceptGraph_ModelMate) 
WHERE id(conceptInstance) <> id(leftInstance) AND id(conceptInstance) <> id(rightInstance) AND NOT (conceptInstance.Name IN [ "concept", "instance", "word"])
WITH idx, leftInstance, rightInstance, concept, conceptInstance, (r1.Weight+r2.Weight)*r3.Weight as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect({conceptName: concept.Name, conceptInstanceName: conceptInstance.Name, weight:freq})[0..5] as weightedConceptInstances order by idx; // GOOD only 2 results: 	apple	engineer  company and  eating apple fruit

cypher runtime=compiled profile
with "apple engineer eating apple" as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT (((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance:Instance_ConceptGraph_ModelMate)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate)
WHERE NOT (concept.Name IN [ "concept", "instance", "word"])
RETURN idx, leftInstance.Name, rightInstance.Name, collect({conceptName: concept.Name, conceptWeight: r1.Weight+r2.Weight})[0..5] as weightedConcepts, order by idx, conceptWeight; // need to sort by conceptWeight

cypher runtime=compiled profile
with "apple engineer eating apple" as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT (((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
return idx, leftInstance, rightInstance;

cypher runtime=compiled profile
with "apple engineer eating apple" as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT (((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance:Instance_ConceptGraph_ModelMate)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate)
WHERE NOT (concept.Name IN [ "concept", "instance", "word", "item", "thing"])
WITH idx, leftInstance, rightInstance, concept, r1.Weight*2+r2.Weight AS conceptWeight
ORDER BY  idx ASC, conceptWeight DESC
RETURN idx, leftInstance.Name, rightInstance.Name, collect({conceptName: concept.Name, conceptWeights: conceptWeight})[0..5] as weightedConcepts
ORDER BY idx ASC; // BEST

profile MATCH 
p = (instance1:Instance_ConceptGraph_ModelMate {Name: "banana"}   )-[r1]->(concept1:Concept_ConceptGraph_ModelMate), 
q = (instance2:Instance_ConceptGraph_ModelMate {Name: concept1.Name})-[r2]->(concept2:Concept_ConceptGraph_ModelMate),
s = (instance3:Instance_ConceptGraph_ModelMate {Name: concept2.Name})-[r3]->(concept3:Concept_ConceptGraph_ModelMate)
WHERE r1.Weight > 100 AND r2.Weight > 100 AND r3.Weight > 100
AND NOT (concept1.Name in ["option", "consideration", "case", "attribute", "aspect", "aspect of the property", "adding new input", "element", "issue", "basic information", "issue", "symbol", "word", "term", "information", "entity", "factor", "topic", "criterion", "characteristic", "variable", "feature", "concept", "instance", "datum", "basic datum", "rich datum", "parameter", "item"])
AND NOT (concept2.Name in ["option", "consideration", "case", "attribute", "aspect", "aspect of the property", "adding new input", "element", "issue", "basic information", "issue", "symbol", "word", "term", "information", "entity", "factor", "topic", "criterion", "characteristic", "variable", "feature", "concept", "instance", "datum", "basic datum", "rich datum", "parameter", "item"])
AND NOT (concept3.Name in ["option", "consideration", "case", "attribute", "aspect", "aspect of the property", "adding new input", "element", "issue", "basic information", "issue", "symbol", "word", "term", "information", "entity", "factor", "topic", "criterion", "characteristic", "variable", "feature", "concept", "instance", "datum", "basic datum", "rich datum", "parameter", "item"])
return p, q, s, r1.Weight + r2.Weight + r3.Weight as weight
order by weight DESC
limit 100; // GOOD

profile MATCH 
p = (instance1:Instance_ConceptGraph_ModelMate {Name: "new york"}   )-[r1]->(concept1:Concept_ConceptGraph_ModelMate), 
q = (instance2:Instance_ConceptGraph_ModelMate {Name: concept1.Name})-[r2]->(concept2:Concept_ConceptGraph_ModelMate),
s = (instance3:Instance_ConceptGraph_ModelMate {Name: concept2.Name})-[r3]->(concept3:Concept_ConceptGraph_ModelMate)
WHERE r1.Weight >= 30 AND r2.Weight >= 1.5*r1.Weight AND r3.Weight >= 2.0*r1.Weight
AND NOT (concept1.Name in ["value", "area", "category", "subject", "option", "consideration", "case", "attribute", "aspect", "aspect of the property", "adding new input", "element", "issue", "basic information", "issue", "symbol", "word", "term", "information", "entity", "factor", "topic", "criterion", "characteristic", "variable", "feature", "concept", "instance", "datum", "basic datum", "rich datum", "parameter", "item"])
AND NOT (concept2.Name in ["value", "area", "category", "subject", "option", "consideration", "case", "attribute", "aspect", "aspect of the property", "adding new input", "element", "issue", "basic information", "issue", "symbol", "word", "term", "information", "entity", "factor", "topic", "criterion", "characteristic", "variable", "feature", "concept", "instance", "datum", "basic datum", "rich datum", "parameter", "item"])
AND NOT (concept3.Name in ["value", "area", "category", "subject", "option", "consideration", "case", "attribute", "aspect", "aspect of the property", "adding new input", "element", "issue", "basic information", "issue", "symbol", "word", "term", "information", "entity", "factor", "topic", "criterion", "characteristic", "variable", "feature", "concept", "instance", "datum", "basic datum", "rich datum", "parameter", "item"])
WITH instance1, instance2, instance3, r1, r2, r3, concept1, concept2, concept3, 1*r1.Weight+1*r2.Weight+1*r3.Weight AS weightSum
order by weightSum DESC
return collect(DISTINCT {instances:{i1:instance1.Name, i2:instance2.Name, i3:instance3.Name}, 
concepts:{c1:concept1.Name, c2:concept2.Name, c3:concept3.Name}, 
weights:{w1:r1.Weight, w2:r2.Weight, w3:r3.Weight, weight:weightSum}}) AS instances
limit 100; // GOOD

cypher runtime=compiled profile
with "apple engineer eating apple" as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT (((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance:Instance_ConceptGraph_ModelMate)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate)
WHERE NOT (concept.Name IN [ "concept", "instance", "word"])
MATCH (concept:Concept_ConceptGraph_ModelMate)<-[r3:IsA_ConceptGraph_ModelMate]-(conceptInstance:Instance_ConceptGraph_ModelMate) 
WHERE id(conceptInstance) <> id(leftInstance) AND id(conceptInstance) <> id(rightInstance) AND NOT (conceptInstance.Name IN [ "concept", "instance", "word"])
WITH idx, leftInstance, rightInstance, concept, conceptInstance, (r1.Weight+r2.Weight)*r3.Weight as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect({conceptName: concept.Name, conceptInstanceName: conceptInstance.Name, weight:freq})[0..5] as weightedConceptInstances order by idx;

cypher runtime=compiled profile
with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT ((i.Name = "is") OR ((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance:Instance_ConceptGraph_ModelMate)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate)
RETURN idx, leftInstance.Name, rightInstance.Name, concept.Name, (r1.Weight+r2.Weight) as conceptWeight
ORDER BY idx, leftInstance.Name, rightInstance.Name, conceptWeight DESC; // 178

cypher runtime=compiled profile
with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT ((i.Name = "is") OR ((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance:Instance_ConceptGraph_ModelMate)-[r1:IsA_ConceptGraph_ModelMate]->
            (concept:Concept_ConceptGraph_ModelMate)
       <-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate)
MATCH (concept:Concept_ConceptGraph_ModelMate)<-[r3:IsA_ConceptGraph_ModelMate]-(conceptInstance:Instance_ConceptGraph_ModelMate) 
WHERE id(conceptInstance) <> id(leftInstance) AND id(conceptInstance) <> id(rightInstance)
RETURN idx, leftInstance.Name, rightInstance.Name, concept.Name, (r1.Weight+r2.Weight)*r3.Weight as conceptWeight
ORDER BY idx, leftInstance.Name, rightInstance.Name, conceptWeight DESC;

cypher runtime=compiled profile
with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT ((i.Name = "is") OR ((i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})))
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance:Instance_ConceptGraph_ModelMate)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance:Instance_ConceptGraph_ModelMate)
MATCH (concept:Concept_ConceptGraph_ModelMate)<-[r3:IsA_ConceptGraph_ModelMate]-(conceptInstance:Instance_ConceptGraph_ModelMate) 
WHERE conceptInstance <> leftInstance AND conceptInstance <> rightInstance
WITH idx, leftInstance, rightInstance, concept, conceptInstance, (r1.Weight+r2.Weight)*r3.Weight as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect({conceptName: concept.Name, conceptInstanceName: conceptInstance.Name, weight:freq})[0..2] as weightedConceptInstances order by idx;


with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
match (i:Instance_ConceptGraph_ModelMate {Name:word})
WHERE NOT (i)-[:IsA_ConceptGraph_ModelMate]->(:Concept_ConceptGraph_ModelMate {Name:"stop words"})
with collect(i) as instances
unwind range(0,length(instances)-2) as idx
with idx, instances[idx] as leftInstance, instances[idx+1] as rightInstance
MATCH (leftInstance)-[r1:IsA_ConceptGraph_ModelMate]->(concept:Concept_ConceptGraph_ModelMate)<-[r2:IsA_ConceptGraph_ModelMate]-(rightInstance)
WITH idx, leftInstance, rightInstance, concept, r1.Weight+r2.Weight as freq order by freq desc
RETURN idx, leftInstance.Name, rightInstance.Name, collect({concept:concept.Name, weight:freq})[0..5] as weightedConcepts order by idx;


with "the apple engineer is eating the apple" as sentence
unwind split(sentence," ") as word
MATCH (i:Instance_ConceptGraph_ModelMate {Name:word})
MATCH (i)-[r1:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate {Name:"stop words"})
RETURN i, c limit 10;


MATCH p= (i)-[r1:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate {Name:"stop words"}) return p

MATCH p = (i)-[r1:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate {Name:"concept"}) return count(*) // 48,435

MATCH p = (i)-[r1:IsA_ConceptGraph_ModelMate]->(c:Concept_ConceptGraph_ModelMate {Name:"instance"}) return count(*) // 13,153


//
// OLD
//

// http://iansrobinson.com/2013/06/24/cypher-calculating-shortest-weighted-path/

START  startNode=node:node_auto_index(name={startNode}),
       endNode=node:node_auto_index(name={endNode})IsA_ConceptGraph_ModelMate
MATCH  p=(startNode)-[:CONNECTED_TO*1..4]->(endNode)
RETURN p AS shortestPath, 
       reduce(weight=0, r in relationships(p) : weight+r.weight) AS totalWeight
       ORDER BY totalWeight ASC
       LIMIT 1


df = pd.read_csv("/data-concept-instance-relations.txt", sep="\t", header=None, names=['concept','instance','count'])

import time
import re
from neo4j.v1 import GraphDatabase, basic_auth
start = time.time()
driver = GraphDatabase.driver("bolt://localhost", auth=basic_auth("neo4j", "goforit"))
session = driver.session()
for index, row in df.head(10000).iterrows():
    create = "MERGE (c:Concept {name:\"" + row['concept'] + "\"}) MERGE (i:Instance {name:\"" + row['instance'] + "\"}) MERGE (i)-[:isa {order:" +  str(row['count']) + "}]->(c)"
    session.run(create)
session.close()
print("> Loading done in %.2f seconds." % (time.time() - start))