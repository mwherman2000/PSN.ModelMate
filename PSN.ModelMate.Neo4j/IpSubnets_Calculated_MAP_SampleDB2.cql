MATCH (na:NetworkAdapters_Linux_Inventory_MAP_SampleDB)
WITH split(na.Ipv4Addr, ";") AS IPAddresses
WITH IPAddresses[0] AS IP4
WITH split(IP4, ".") AS Octets
WITH Octets[0] + "." + Octets[1] + "." + Octets[2] AS Ipv4Subnet
RETURN Ipv4Subnet, count(*) ORDER BY Ipv4Subnet;

MATCH path = (source)-[r:NetworkAdapters_Linux_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(target) RETURN count(*);

MATCH path = (source)-[r:NetworkAdapters_Linux_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(target) DELETE r RETURN count(*);

MATCH (ips:IpSubnets_Calculated_MAP_SampleDB) RETURN count(*);

//MATCH (ips:IpSubnets_Calculated_MAP_SampleDB) DELETE ips RETURN count(*);

//DROP CONSTRAINT ON (ips:IpSubnets_Calculated_MAP_SampleDB) ASSERT ips.Ipv4SubnetAddress IS UNIQUE;
//CREATE CONSTRAINT ON (ips:IpSubnets_Calculated_MAP_SampleDB) ASSERT ips.Ipv4SubnetAddress IS UNIQUE;

MATCH (na:NetworkAdapters_Linux_Inventory_MAP_SampleDB)
WITH split(na.Ipv4Addr, ";") AS IPAddresses
WITH IPAddresses[0] AS IP4
WITH split(IP4, ".") AS Octets
WITH coalesce(Octets[0] + "." + Octets[1] + "." + Octets[2], "NULL") AS Ipv4Subnet
MERGE (ips:IpSubnets_Calculated_MAP_SampleDB { Ipv4SubnetAddress: Ipv4Subnet } )
ON CREATE SET 
		ips._DatabaseVersion = "0.94",
		ips._DatabaseSource = "[MAP_SampleDB].[Linux_Inventory].[NetworkAdapters]",
	    ips._ArchiMate2Type = "Network_ArchiMate2",
		ips._Created = timestamp(), 
		ips._Modified = timestamp(), 
		ips._Removed = 0
ON MATCH SET
		ips._Modified = timestamp(), 
		ips._Removed = 0
RETURN ips.Ipv4SubnetAddress, count(*) ORDER BY count(*) DESC;

MATCH (ips:IpSubnets_Calculated_MAP_SampleDB)
CALL apoc.create.addLabels([ id(ips) ], [ ips._ArchiMate2Type ]) YIELD node
RETURN count(*);

MATCH (na:NetworkAdapters_Linux_Inventory_MAP_SampleDB), (ips:IpSubnets_Calculated_MAP_SampleDB)
WITH split(na.Ipv4Addr, ";") AS IPAddresses, ips, na
WITH IPAddresses[0] AS IP4, ips, na
WITH split(IP4, ".") AS Octets, ips, na
WITH coalesce(Octets[0] + "." + Octets[1] + "." + Octets[2], "NULL") AS Ipv4Subnet, ips, na
WHERE Ipv4Subnet = ips.Ipv4SubnetAddress
MERGE p = (na)-[r:NetworkAdapters_Linux_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips)
RETURN count(*);

MATCH p = (na)-[r:NetworkAdapters_Linux_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips)
WHERE na.Ipv4Addr <> "NULL"
RETURN count(*);

MATCH p = (nac)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips1), 
      q = (nac)-[s:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpGateways_Calculated_MAP_SampleDB]->(ipg),
	  o = (na)-[t:NetworkAdapters_Linux_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips2)
WHERE ips1.Ipv4SubnetAddress <> "NULL" AND ips2.Ipv4SubnetAddress <> "NULL"
RETURN o, p, q, count(*);

MATCH p = (nac)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips1)
WHERE ips1.Ipv4SubnetAddress <> "NULL"
RETURN p;

MATCH o = (na)-[t:NetworkAdapters_Linux_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips2)
WHERE ips2.Ipv4SubnetAddress <> "NULL"
RETURN o;

MATCH p = (nac:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips1:IpSubnets_Calculated_MAP_SampleDB), 
	  o = (na:NetworkAdapters_Linux_Inventory_MAP_SampleDB)-[t:NetworkAdapters_Linux_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips2:IpSubnets_Calculated_MAP_SampleDB)
WHERE ips1.Ipv4SubnetAddress <> "NULL" AND ips2.Ipv4SubnetAddress <> "NULL"
RETURN collect(r) + collect(t);

MATCH p = (nac:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips1:IpSubnets_Calculated_MAP_SampleDB), 
	  o = (na:NetworkAdapters_Linux_Inventory_MAP_SampleDB)-[t:NetworkAdapters_Linux_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips2:IpSubnets_Calculated_MAP_SampleDB)
WHERE ips1.Ipv4SubnetAddress <> "NULL" AND ips2.Ipv4SubnetAddress <> "NULL"
RETURN collect(r) + collect(t);

MATCH p = (d1:Devices_Core_Inventory_MAP_SampleDB) --> (nac:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB) return d1, nac;
MATCH p = (source)-[r:Devices_Core_Inventory_MAP_SampleDB__NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB]->(target) RETURN count(*);

MATCH p = (d1:Devices_Core_Inventory_MAP_SampleDB) -[a:Devices_Core_Inventory_MAP_SampleDB__NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB]-> (nac:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)-[r:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips1:IpSubnets_Calculated_MAP_SampleDB), 
	  o = (d2:Devices_Core_Inventory_MAP_SampleDB) -[b:Devices_Core_Inventory_MAP_SampleDB__NetworkAdapters_Linux_Inventory_MAP_SampleDB]-> (na:NetworkAdapters_Linux_Inventory_MAP_SampleDB)-[t:NetworkAdapters_Linux_Inventory_MAP_SampleDB__IpSubnets_Calculated_MAP_SampleDB]->(ips2:IpSubnets_Calculated_MAP_SampleDB)  
WHERE ips1.Ipv4SubnetAddress <> "NULL" AND ips2.Ipv4SubnetAddress <> "NULL"
RETURN collect(a) + collect(b) + collect(r) + collect(t);


