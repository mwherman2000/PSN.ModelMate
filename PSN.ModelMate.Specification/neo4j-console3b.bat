set PATH="C:\Program Files\Neo4j CE 3.0.6\bin";%PATH%
rem "C:\Program Files\Java\jre1.8.0_91\bin\java.exe" -classpath "C:\Program Files\Neo4j CE 3.0.6\bin\neo4j-desktop-3.0.6.jar" org.neo4j.shell.StartClient -file MAP_SampleDB.ModelMate2Neo4j.cql > MAP_SampleDB.ModelMate2Neo4j.txt
pause
REM "C:\Program Files\Java\jre1.8.0_91\bin\java.exe" -classpath "C:\Program Files\Neo4j CE 3.0.6\bin\neo4j-desktop-3.0.6.jar" org.neo4j.shell.StartClient -file IpGateways_Calculated_MAP_SampleDB.cql > IpGateways_Calculated_MAP_SampleDB.txt
pause
"C:\Program Files\Java\jre1.8.0_91\bin\java.exe" -classpath "C:\Program Files\Neo4j CE 3.0.6\bin\neo4j-desktop-3.0.6.jar" org.neo4j.shell.StartClient 
pause
