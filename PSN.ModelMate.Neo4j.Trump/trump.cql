# https://gist.github.com/jexp/0706b1668a29451a917b08ffd7aa9b97

org-org-connections.csv
person-org-connections.csv
person-person-connections.csv
Quartz_ Donald Trump's May 2016 financial disclosure-Part 1a Positions v1.1.csv
Quartz_ Donald Trump's May 2016 financial disclosure-Part 1b Ownership v1.1.csv
Quartz_ Donald Trump's May 2016 financial disclosure-Part 2 Assets & Income v1.1.csv
Quartz_ Donald Trump's May 2016 financial disclosure-Part 3 Employment Agreements v1.1.csv
Quartz_ Donald Trump's May 2016 financial disclosure-Part 4 Compensation v1.1.csv
Quartz_ Donald Trump's May 2016 financial disclosure-Part 5 Spouse's Assets & Income v1.1.csv


# Cleaning Relationship-Types

WITH
'https://docs.google.com/spreadsheets/u/1/d/1Z5Vo5pbvxKJ5XpfALZXvCzW26Cl4we3OaN73K9Ae5Ss/export?format=csv&gid=634968401' AS url
LOAD CSV WITH HEADERS FROM url AS row
RETURN row.`Organization A`,row.`Organization B`,row.Connection, row.`Source(s)`
LIMIT 5

WITH
'https://docs.google.com/spreadsheets/u/1/d/1Z5Vo5pbvxKJ5XpfALZXvCzW26Cl4we3OaN73K9Ae5Ss/export?format=csv&gid=634968401' AS url
load csv from url AS row
with row
UNWIND  row[0..1] AS org
RETURN org, count(*)
ORDER BY count(*) DESC LIMIT 10

WITH
'https://docs.google.com/spreadsheets/u/1/d/1Z5Vo5pbvxKJ5XpfALZXvCzW26Cl4we3OaN73K9Ae5Ss/export?format=csv&gid=634968401' AS url
LOAD CSV WITH HEADERS FROM url AS row
RETURN row.Connection AS type, count(*)
ORDER BY count(*) DESC

WITH
'https://docs.google.com/spreadsheets/u/1/d/1Z5Vo5pbvxKJ5XpfALZXvCzW26Cl4we3OaN73K9Ae5Ss/export?format=csv&gid=634968401' AS url,
['LOAN','LOBBIED','SALE','SUPPLIER','SHAREHOLDER','LICENSES','AFFILIATED','TIES','NEGOTIATION','INVOLVED','PARTNER'] AS terms
LOAD CSV WITH HEADERS FROM url AS row
WITH apoc.text.regreplace(toUpper(row.Connection),'\\W+','_') AS type, row, terms
WITH head(filter(term IN terms WHERE type CONTAINS term)) AS found, type, row
RETURN coalesce(found,type) AS type, count(*), collect(distinct row.Connection) AS connections
ORDER BY count(*) DESC

# Import Organization Relationships

CREATE CONSTRAINT ON (o:Organization) ASSERT o.name IS UNIQUE;

WITH
'https://docs.google.com/spreadsheets/u/1/d/1Z5Vo5pbvxKJ5XpfALZXvCzW26Cl4we3OaN73K9Ae5Ss/export?format=csv&gid=634968401' AS url,
['LOAN','LOBBIED','SALE','SUPPLIER','SHAREHOLDER','LICENSES','AFFILIATED','TIES','NEGOTIATION','INVOLVED','PARTNER'] AS terms
LOAD CSV WITH HEADERS FROM url AS row

WITH apoc.text.regreplace(toUpper(row.Connection),'\\W+','_') AS type, row, terms
WITH coalesce(head(filter(term IN terms WHERE type CONTAINS term)), type) AS type, row

MERGE (o1:Organization {name:row.`Organization A`})
MERGE (o2:Organization {name:row.`Organization B`})
WITH o1,o2,type,row
CALL apoc.create.relationship(o1,type, {source:row.`Source(s)`, connection:row.Connection},o2) YIELD rel
RETURN type(rel), count(*)
ORDER BY count(*) desc

MATCH (m)-[r]->(n) RETURN *

MATCH (o:Organization)
WHERE o.name CONTAINS "BANK" SET o:Bank


MATCH (o:Organization)
WHERE o.name CONTAINS "HOTEL" SET o:Hotel

MATCH (o:Organization)
WHERE any(term in ["TRUMP","DT","DJT"] WHERE o.name CONTAINS (term + " "))
SET o:Trump

# Import Person Organization Relationships

WITH
'https://docs.google.com/spreadsheets/u/1/d/1Z5Vo5pbvxKJ5XpfALZXvCzW26Cl4we3OaN73K9Ae5Ss/export?format=csv&gid=1368567920' AS url
LOAD CSV WITH HEADERS FROM url AS row
RETURN row.Organization,row.Person,row.Connection, row.`Source(s)`
LIMIT 5

WITH
'https://docs.google.com/spreadsheets/u/1/d/1Z5Vo5pbvxKJ5XpfALZXvCzW26Cl4we3OaN73K9Ae5Ss/export?format=csv&gid=1368567920' AS url
LOAD CSV WITH HEADERS FROM url AS row
RETURN row.Connection AS type, count(*)
ORDER BY count(*) DESC LIMIT 5

WITH
'https://docs.google.com/spreadsheets/u/1/d/1Z5Vo5pbvxKJ5XpfALZXvCzW26Cl4we3OaN73K9Ae5Ss/export?format=csv&gid=1368567920' AS url,
['BOARD','DIRECTOR','INCOME','PRESIDENT','CHAIR','CEO','PARTNER','OWNER','INVESTOR','FOUNDER','STAFF','DEVELOPER','EXECUTIVE_COMITTEE','EXECUTIVE','FELLOW','BANKER','COUNSEL','ADVISOR','SHAREHOLDER','LIASON','SPEECH','CONNECTED','HIRED','CONSULTED','INVOLVED','APPOINTEE','MANAGER','TRUSTEE','AMBASSADOR','PUBLISHER','LAWYER'] AS terms
LOAD CSV WITH HEADERS FROM url AS row
WITH apoc.text.regreplace(toUpper(row.Connection),'\\W+','_') AS type, row, terms
WITH head(filter(term IN terms WHERE type CONTAINS term)) AS found, type, row
RETURN coalesce(found,type) AS type, count(*), collect(distinct row.Connection) AS connections
ORDER BY count(*) DESC

CREATE CONSTRAINT ON (p:Person) ASSERT p.name IS UNIQUE;

WITH
'https://docs.google.com/spreadsheets/u/1/d/1Z5Vo5pbvxKJ5XpfALZXvCzW26Cl4we3OaN73K9Ae5Ss/export?format=csv&gid=1368567920' AS url,
['BOARD','DIRECTOR','INCOME','PRESIDENT','CHAIR','CEO','PARTNER','OWNER','INVESTOR','FOUNDER','STAFF','DEVELOPER','EXECUTIVE_COMITTEE','EXECUTIVE','FELLOW','BANKER','COUNSEL','ADVISOR','SHAREHOLDER','LIASON','SPEECH','CONNECTED','HIRED','CONSULTED','INVOLVED','APPOINTEE','MANAGER','TRUSTEE','AMBASSADOR','PUBLISHER','LAWYER'] AS terms
LOAD CSV WITH HEADERS FROM url AS row

WITH apoc.text.regreplace(toUpper(row.Connection),'\\W+','_') AS type, row, terms
WITH coalesce(head(filter(term IN terms WHERE type CONTAINS term)), 'INVOLVED_WITH') AS type, row

MERGE (o:Organization {name:row.Organization})
MERGE (p:Person {name:row.Person})
WITH o,p,type,row
CALL apoc.create.relationship(p,type, {source:row.`Source(s)`, connection:row.Connection},o) YIELD rel
RETURN type(rel), count(*)
ORDER BY count(*) desc


# Import Person-Person Relationships

WITH
'https://docs.google.com/spreadsheets/u/1/d/1Z5Vo5pbvxKJ5XpfALZXvCzW26Cl4we3OaN73K9Ae5Ss/export?format=csv&gid=905294723' AS url,
['WHITE_HOUSE','REPRESENTATIVE','FRIEND','DIRECTOR','ADVISOR','WORKED','MET','LUNCHED','NOMINEE','COUNSELOR','AIDED','CAMPAIGN','PARTNER','MARRIED','CLOSE','APPEARANCE','BOUGHT','SAT_IN','CONSULTED','CO_CHAIR','GAVE'] AS terms
LOAD CSV WITH HEADERS FROM url AS row
WITH apoc.text.regreplace(toUpper(row.Connection),'\\W+','_') AS type, row, terms
WITH head(filter(term IN terms WHERE type CONTAINS term)) AS found, type, row
RETURN coalesce(found,type) AS type, count(*), collect(distinct row.Connection) AS connections
ORDER BY count(*) DESC


