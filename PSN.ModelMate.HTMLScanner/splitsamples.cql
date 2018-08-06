MATCH (n:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
RETURN n.IpAddress, split(n.IpAddress, ",") LIMIT 25

MATCH (n:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
WITH split(n.IpAddress, ",") AS IP4
RETURN IP4[0] LIMIT 25

MATCH (n:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
WITH split(n.IpAddress, ";") AS IPAddresses
WITH IPAddresses[0] AS IP4
RETURN DISTINCT IP4 ORDER BY IP4

MATCH (n:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
WITH split(n.IpAddress, ";") AS IPAddresses
WITH DISTINCT IPAddresses[0] AS IP4
WITH split(IP4, ".") AS Octet
RETURN Octet[0], Octet[1], Octet[2], Octet[3]

MATCH (n:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
WITH split(n.IpAddress, ";") AS IPAddresses
WITH DISTINCT IPAddresses[0] AS IP4
WITH split(IP4, ".") AS Octet
RETURN Octet[0], Octet[1], Octet[2], Octet[3], Octet[0] + "." + Octet[1] + "." + Octet[2]

MATCH (n:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
WITH split(n.IpAddress, ";") AS IPAddresses
WITH DISTINCT IPAddresses[0] AS IP4
WITH split(IP4, ".") AS Octet
WITH DISTINCT Octet[0] + "." + Octet[1] + "." + Octet[2] AS Ipv4Subnet
RETURN Ipv4Subnet, count(*)

MATCH (n:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
WITH split(n.IpAddress, ";") AS IPAddresses
WITH IPAddresses[0] AS IP4
WITH split(IP4, ".") AS Octet
WITH Octet[0] + "." + Octet[1] + "." + Octet[2] AS Ipv4Subnet
RETURN Ipv4Subnet, count(*) ORDER BY count(*) DESC

MATCH (n:NetworkAdapterConfigurations_Win_Inventory_MAP_SampleDB)
WITH split(n.IpAddress, ";") AS IPAddresses
WITH IPAddresses[0] AS IP4
WITH split(IP4, ".") AS Octet
WITH Octet[0] + "." + Octet[1] + "." + Octet[2] AS Ipv4Subnet
RETURN Ipv4Subnet, count(*) ORDER BY Ipv4Subnet

