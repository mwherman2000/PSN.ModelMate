MATCH path = (source)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpGateways_Calculated_MAP_SampleDB]->(target) RETURN count(*);

MATCH path = (source)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpGateways_Calculated_MAP_SampleDB]->(target) DELETE r RETURN count(*);

MATCH (ipg:IpGateways_Calculated_MAP_SampleDB) RETURN count(*);

MATCH (ipg:IpGateways_Calculated_MAP_SampleDB) DELETE ipg RETURN count(*);

DROP CONSTRAINT ON (ipg:IpGateways_Calculated_MAP_SampleDB) ASSERT ipg.Ipv4Address IS UNIQUE;
CREATE CONSTRAINT ON (ipg:IpGateways_Calculated_MAP_SampleDB) ASSERT ipg.Ipv4Address IS UNIQUE;

MATCH (nac:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
WITH split(nac.DefaultIpGateway,";") AS IPAddresses
MERGE (ipg:IpGateways_Calculated_MAP_SampleDB {
		_DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[Win_Inventory].[NetworkAdapterConfigurations]",
	    _ArchiMate2Type:  "Device_ArchiMate2",
		Ipv4Address: IPAddresses[0],
		_Removed: 0
}) 
ON CREATE SET ipg._Created = timestamp(), ipg._Modified = timestamp(), ipg._Removed = 0
RETURN ipg.Ipv4Address, count(*) ORDER BY count(*);

MATCH (ipg:IpGateways_Calculated_MAP_SampleDB)
CALL apoc.create.addLabels([ id(ipg) ], [ ipg._ArchiMate2Type ]) YIELD node
RETURN count(*);

MATCH (nac:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB), (ipg:IpGateways_Calculated_MAP_SampleDB)
WHERE nac.DefaultIpGateway = ipg.Ipv4Address
MERGE p = (nac)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpGateways_Calculated_MAP_SampleDB]->(ipg)
RETURN count(*);

MATCH p = (nac)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpGateways_Calculated_MAP_SampleDB]->(ipg)
WHERE ipg.Ipv4Address <> "NULL"
RETURN count(*);

MATCH p = (nac)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpGateways_Calculated_MAP_SampleDB]->(ipg)
WHERE ipg.Ipv4Address <> "NULL"
RETURN p;