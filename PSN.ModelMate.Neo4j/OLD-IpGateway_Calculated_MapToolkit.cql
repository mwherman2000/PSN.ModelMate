//MATCH  (ipg:IpGateway_Calculated_MapToolkit) DELETE ipg RETURN count(*);
//DROP   CONSTRAINT ON (ipg:IpGateway_Calculated_MapToolkit) ASSERT ipg.Ipv4Address IS UNIQUE;
//CREATE CONSTRAINT ON (ipg:IpGateway_Calculated_MapToolkit) ASSERT ipg.Ipv4Address IS UNIQUE;

MERGE (ipg:IpGateway_Calculated_MAP_SampleDB) DELETE ipg RETURN count(*); // singular
DROP CONSTRAINT ON (ipg:IpGateway_Calculated_MAP_SampleDB) ASSERT ipg.Ipv4Address IS UNIQUE; // singular

MERGE (ipg:IpGateways_Calculated_MAP_SampleDB) DELETE ipg RETURN count(*);

DROP CONSTRAINT ON (ipg:IpGateways_Calculated_MAP_SampleDB) ASSERT ipg.Ipv4Address IS UNIQUE;
CREATE CONSTRAINT ON (ipg:IpGateways_Calculated_MAP_SampleDB) ASSERT ipg.Ipv4Address IS UNIQUE;

MATCH (nac:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB) RETURN count(*);
MATCH (ipg:IpGateways_Calculated_MAP_SampleDB) RETURN count(*);
MATCH (ipg:IpGateways_Calculated_MAP_SampleDB) RETURN ipg;

MATCH (nac:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
MERGE (ipg:IpGateways_Calculated_MAP_SampleDB {
		_DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[Win_Inventory].[NetworkAdapterConfigurations]",
	    _ArchiMate2Type:  "Device_ArchiMate2",
		Ipv4Address: nac.DefaultIpGateway,
		_Removed: 0
}) 
ON CREATE SET ipg._Created = timestamp(), ipg._Modified = timestamp(), ipg._Removed = 0
RETURN count(*), ipg.Ipv4Address;
MATCH (n:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
CALL apoc.create.addLabels([ id(n) ], [ n._ArchiMate2Type ]) YIELD node
RETURN count(*);

MATCH ()-[]->() RETURN COUNT(*)

CALL db.relationshipTypes();

CALL db.labels() 
YIELD label
RETURN label 
ORDER BY label;

