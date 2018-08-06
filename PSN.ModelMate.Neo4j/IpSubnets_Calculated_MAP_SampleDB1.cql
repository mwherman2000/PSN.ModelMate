MATCH (nac:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
WITH split(nac.IpAddress, ";") AS IPAddresses
WITH IPAddresses[0] AS IP4
WITH split(IP4, ".") AS Octets
WITH Octets[0] + "." + Octets[1] + "." + Octets[2] AS Ipv4Subnet
RETURN Ipv4Subnet, count(*) ORDER BY Ipv4Subnet

MATCH path = (source)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(target) RETURN count(*);

MATCH path = (source)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(target) DELETE r RETURN count(*);

MATCH (ips:IpSubnets_Calculated_MAP_SampleDB) RETURN count(*);

MATCH (ips:IpSubnets_Calculated_MAP_SampleDB) DELETE ips RETURN count(*);

DROP CONSTRAINT ON (ips:IpSubnets_Calculated_MAP_SampleDB) ASSERT ips.Ipv4SubnetAddress IS UNIQUE;
CREATE CONSTRAINT ON (ips:IpSubnets_Calculated_MAP_SampleDB) ASSERT ips.Ipv4SubnetAddress IS UNIQUE;

MATCH (nac:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
WITH split(nac.IpAddress, ";") AS IPAddresses
WITH IPAddresses[0] AS IP4
WITH split(IP4, ".") AS Octets
WITH Octets[0] + "." + Octets[1] + "." + Octets[2] AS Ipv4Subnet
MERGE (ips:IpSubnets_Calculated_MAP_SampleDB {
		_DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[Win_Inventory].[NetworkAdapterConfigurations]",
	    _ArchiMate2Type:  "Network_ArchiMate2",
		Ipv4SubnetAddress:  Ipv4Subnet,
		_Removed: 0
}) 
ON CREATE SET ips._Created = timestamp(), ips._Modified = timestamp(), ips._Removed = 0
RETURN ips.Ipv4SubnetAddress, count(*) ORDER BY count(*) DESC;

MATCH (ips:IpSubnets_Calculated_MAP_SampleDB)
CALL apoc.create.addLabels([ id(ips) ], [ ips._ArchiMate2Type ]) YIELD node
RETURN count(*);

MATCH (nac:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB), (ips:IpSubnets_Calculated_MAP_SampleDB)
WITH split(nac.IpAddress, ";") AS IPAddresses, ips, nac
WITH IPAddresses[0] AS IP4, ips, nac
WITH split(IP4, ".") AS Octets, ips, nac
WITH Octets[0] + "." + Octets[1] + "." + Octets[2] AS Ipv4Subnet, ips, nac
WHERE Ipv4Subnet = ips.Ipv4SubnetAddress
MERGE p = (nac)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips)
RETURN count(*);

MATCH p = (nac)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips), 
      q = (nac)-[s:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpGateways_Calculated_MAP_SampleDB]->(ipg)
WHERE ipg.Ipv4Address <> "NULL"
RETURN count(*);

MATCH p = (nac)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips), 
      q = (nac)-[s:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpGateways_Calculated_MAP_SampleDB]->(ipg)
WHERE ips.Ipv4SubnetAddress <> "NULL"
RETURN p, q;


