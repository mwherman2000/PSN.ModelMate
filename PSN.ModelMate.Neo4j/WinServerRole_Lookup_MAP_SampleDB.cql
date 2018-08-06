MATCH  (n:WinServerRole_Lookup_MAP_SampleDB) RETURN count(*);

MATCH  MATCH  (n:WinServerRole_Lookup_MAP_SampleDB) DELETE n RETURN count(*);

DROP   CONSTRAINT ON (n:WinServerRole_Lookup_MAP_SampleDB) ASSERT n.RoleId IS UNIQUE;
CREATE CONSTRAINT ON (n:WinServerRole_Lookup_MAP_SampleDB) ASSERT n.RoleId IS UNIQUE;
CREATE INDEX ON:WinServerRole_Lookup_MAP_SampleDB(RoleId);

CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3000, RoleText: "Active Directory" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3001, RoleText: "Active Directory Certificate Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3002, RoleText: "Active Directory Rights Management Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3003, RoleText: "DHCP Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3004, RoleText: "DNS Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3005, RoleText: "Message Queuing" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3006, RoleText: "Remote Access\VPN Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3007, RoleText: "Streaming Media Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3008, RoleText: "Terminal Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3009, RoleText: "Virtual Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3010, RoleText: "Web Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3011, RoleText: "WINS Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3012, RoleText: "Windows Deployment Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3013, RoleText: "Windows SharePoint Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3014, RoleText: "Windows Server Update Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3015, RoleText: "Terminal Services Licensing Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3016, RoleText: "Application Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3017, RoleText: "Microsoft SQL Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3200, RoleText: "Active Directory Domain Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3201, RoleText: "Active Directory Certificate Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3202, RoleText: "Active Directory Rights Management Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3203, RoleText: "DHCP Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3204, RoleText: "DNS Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3205, RoleText: "Application Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3206, RoleText: "Network Policy and Access Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3207, RoleText: "Streaming Media Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3208, RoleText: "Terminal Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3209, RoleText: "Hyper-V" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3210, RoleText: "Web Server (IIS)" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3211, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3212, RoleText: "Windows Deployment Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3213, RoleText: "Windows SharePoint Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3214, RoleText: "Windows Server Update Services" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3215, RoleText: "Terminal Services Licensing Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3216, RoleText: "Application Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3217, RoleText: "Microsoft SQL Server" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3300, RoleText: "This role is supported in Windows Server 2008 R2. The machine must first be upgraded to Windows Server 2003.  See Recommendation for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3301, RoleText: "This role is supported in Windows Server 2008 R2. The machine must first be upgraded to Windows Server 2003.  See Recommendation for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3302, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3303, RoleText: "This role is supported in Windows Server 2008 R2.  The machine must be upgraded to Windows Server 2003 first." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3304, RoleText: "This role is supported in Windows Server 2008 R2.  The machine must be upgraded to Windows Server 2003 first." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3305, RoleText: "This feature is supported in Windows Server 2008 R2.  You can install  by using the Add Features Wizard available in Server Manager." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3306, RoleText: "This role is supported in Windows Server 2008 R2.  The machine must be upgraded to Windows Server 2003 first." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3307, RoleText: "This role is supported in Windows Server 2008 R2.  The machine must first be upgraded to Windows Server 2003.  See Recommendation for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3308, RoleText: "This role is supported in Windows Server 2008 R2.  The machine must first be upgraded to Windows Server 2003.  See Recommendation for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3309, RoleText: "NULL"  };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3310, RoleText: "This role is supported in Windows Server 2008 R2.  The machine must be upgraded to Windows Server 2003 first." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3311, RoleText: "This feature is supported in Windows Server 2008 R2.  You can install by using the Add Features Wizard available in Server Manager." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3312, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3313, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3314, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3315, RoleText: "This role is supported in Windows Server 2008 R2.  The machine must first be upgraded to Windows Server 2003.  See Recommendation for additional information."  };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3316, RoleText: "This feature is supported in Windows Server 2008 R2.  You can install  by using the Add Features Wizard available in Server Manager." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3317, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3400, RoleText: "This role can be migrated to Windows Server 2008 R2.  See Recommendation for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3401, RoleText: "This role is supported in Windows Server 2008 R2.  The upgrade will require the user to make changes.  See Recommendation for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3402, RoleText: "This role can be migrated to Windows Server 2008 R2.  See Recommendation for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3403, RoleText: "This role can be migrated to Windows Server 2008 R2." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3404, RoleText: "This role can be migrated to Windows Server 2008 R2." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3405, RoleText: "The role of Application Server is supported in Windows Server 2008 R2. There is no supported upgrade path to Windows Server 2008 R2. A rebuild of the machine is required." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3406, RoleText: "This role can be migrated to Windows Server 2008 R2." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3407, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3408, RoleText: "This role can be migrated to Windows Server 2008 R2.  See Recommendation for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3409, RoleText: "The Hyper-V role in Windows Server 2008 R2 supports this role." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3410, RoleText: "This role is supported in Windows Server 2008 R2." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3411, RoleText: "This feature is supported in Windows Server 2008 R2.  You can install by using the Add Features Wizard available in Server Manager." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3412, RoleText: "Role can be migrated to Windows Server 2008 R2.  See Recommendations for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3413, RoleText: "This role can be migrated to Windows Server 2008 R2.  See Recommendation for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3414, RoleText: "Role can be migrated to Windows Server 2008 R2.  See Recommendations for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3415, RoleText: "This role can be migrated to Windows Server 2008 R2.  See Recommendation for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3416, RoleText: "The role of Application Server is supported in Windows Server 2008 R2. There is no supported upgrade path to Windows Server 2008 R2. A rebuild of the machine is required." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3417, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3500, RoleText: "Installation Scenarios for AD DS - Windows Server 2008 R2 Technical Library > Active Directory Domain Services > Getting Started > Step-by-Step Guide for Windows Server 2008 R2 Active Directory Domain Services Installation and Removal" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3501, RoleText: "May need to export and restore root CA via ????.  See http://support.microsoft.com/default.aspx/kb/298138 for additional information." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3502, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3503, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3504, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3505, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3506, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3507, RoleText: "Streaming Media Services is not included on the Windows Server 2008 R2 distribution and must be downloaded separately http://support.microsoft.com/kb/934518  Once installed, the settings from Windows Server 2003 will automatically migrate."  };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3508, RoleText: "May need to re-run Application Wizard to publish programs to Terminal Services Web Access after migration to Windows Server 2008 R2." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3509, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3510, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3511, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3512, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3513, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3514, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3515, RoleText: "May need to re-run Application Wizard to publish programs to Terminal Services Web Access after migration to Windows Server 2008 R2."  };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3516, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3517, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3600, RoleText: "Installation Scenarios for AD DS - Windows Server 2008 R2 Technical Library > Active Directory Domain Services > Getting Started > Step-by-Step Guide for Windows Server 2008 R2 Active Directory Domain Services Installation and Removal"  };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3601, RoleText: "May need to export and restore root CA via ????.  See http://support.microsoft.com/default.aspx/kb/298138 for additional information."  };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3602, RoleText: "After upgrade from W2K3, user needs to go to RMT and click link to complete RMS upgrade" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3603, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3604, RoleText: "In some instances, domain names that were not blocked in previous versions of Windows Server are blocked in Windows Server 2008 R2. You may experience issues if you subsequently implement or remove the Intra-site Automatic Tunnel Addressing Protocol (ISATAP) or the Web Proxy Auto-Discovery Protocol (WSUAD).

For more information, see DNS Server Global Query Block List

(http://go.microsoft.com/fwlink/?LinkId=110937)."  };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3605, RoleText: "The Application Role is supported in Windows Server 2008 R2, however the role cannot be migrated. To upgrade, we recommend that the machine be rebuilt." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3606, RoleText: "To enable NAP, you must upgrade the following Windows Server 2003 server roles and role services to the specified Windows Server 2008 R2 server role or role service:

Upgrade the Internet Authentication Service (IAS) to Network Policy Server (applies to all NAP enforcement methods).


Upgrade VPN to VPN (RRAS) (applies only to the VPN enforcement method).


Upgrade DHCP to DHCP (applies only to the DHCP enforcement method).


Upgrade TS Gateway to TS Gateway (applies only to the TS Gateway enforcement method).


For more information, see What is New in Routing and Remote Access 

(http://go.microsoft.com/fwlink/?LinkID=111013)." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3607, RoleText: "Streaming Media Services is not included on the Windows Server 2008 R2 distribution and must be downloaded separately http://support.microsoft.com/kb/934518  Once installed, the settings from Windows Server 2003 will automatically migrate." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3608, RoleText: "There are no known issues with performing an upgrade of a Windows Server 2003 terminal server to the Windows Server 2008 R2 operating system. All settings and configurations are maintained during the upgrade. 

However, a terminal server running Windows Server 2008 R2 can only communicate with a Terminal Services license server running Windows Server 2008 R2, and the license server must have Windows Server 2008 R2 Terminal Services client access licenses (TS CALs) installed." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3609, RoleText: "Users running "RIS" (Remote Installation Services) must upgrade to Windows 2003 SP2 to convert to WDS before installing Windows Server 2008 R2" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3610, RoleText: "For more information, see:

IIS 7.0: Deploying an IIS 7.0 Web Server (http://go.microsoft.com/fwlink/?LinkId=111690)


IIS 7.0: Upgrading to IIS 7.0 on Windows Server 2008 R2 (http://go.microsoft.com/fwlink/?LinkId=111691)" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3611, RoleText: "NULL" };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3612, RoleText: "For more information, see the Windows Deployment Role Step-by-Step Guide

(http://go.microsoft.com/fwlink/?LinkId=84628)." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3613, RoleText: "This role must be downloaded separately."  };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3614, RoleText: "You must upgrade to WSUS 3.0 SP1 or later, before upgrading to Windows Server 2008 R2. In Windows Server 2008 R2, you can install Server Manager Update to get WSUS role through Server Manager." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) {  
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3615, RoleText: "There are no known issues with performing an upgrade of a Windows Server 2003 terminal server to the Windows Server 2008 R2 operating system. All settings and configurations are maintained during the upgrade." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) { 
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3616, RoleText: "You cannot migrate the Application Server role from Windows Server 2003 to Windows Server 2008 R2. It was rebuilt for Windows Server 2008 R2, and it includes default support for applications that are built with .NET Framework 3.0 components, such as Windows Communication Foundation (WCF), Windows Workflow Foundation (WF), and Windows Presentation Foundation (WPF). Application Server offers optional role services that activate features in Windows Server 2008 R2 (such as Windows Process Activation Service (WAS) support for non-HTTP protocols, TCP Port Sharing, and support for IIS 7.0). If you upgrade your server to Windows Server 2008 R2 from Windows Server 2003 or an earlier operating system, and you want to use the capabilities of the Application Server role, you must reinstall the Application Server role by using the Add Roles Wizard in Server Manager. If you configure Windows Server 2008 R2 with the correct application services by using the Add Roles Wizard, your applications will work correctly after you move them from Windows Server 2003 to Windows Server 2008 R2.

For more information, see Frequently Asked Questions for Application Server

(http://go.microsoft.com/fwlink/?LinkId=110936)." };
CREATE (n:WinServerRole_Lookup_MAP_SampleDB) {  
        _DatabaseVersion: "0.94",
		_DatabaseSource:  "[MAP_SampleDB].[WinServer_Reporting].[GetServerRoleStr]",
		_ArchiMate2Type:   "Artifact_ArchiMate2", 
		RoleId: 3617, RoleText: "NULL"  };

