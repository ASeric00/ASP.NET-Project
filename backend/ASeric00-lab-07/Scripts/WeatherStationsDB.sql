BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "WeatherStations" (
	"ID"	INTEGER NOT NULL,
	"Timestamp"	TEXT,
	"StationName"	TEXT,
	"Conditions"	TEXT,
	"Temperature"	TEXT,
	"Pressure"	TEXT,
	"WindSpeed"	TEXT,
	"WindDirection"	TEXT,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
INSERT INTO "WeatherStations" ("ID","Timestamp","StationName","Conditions","Temperature","Pressure","WindSpeed","WindDirection") VALUES (10,'2025-01-18T17:56:05.337','Zagreb','SNOW','-3','997','24','E'),
 (11,'2025-01-14T23:19:25.704','Split','CLOUDY','15','1012','23','NW'),
 (12,'2025-01-14T23:19:50.325','Zagreb','RAIN','8','1008','12','NW'),
 (16,'2025-01-18T17:47:09.982','Rijeka','CLOUDY','12','1005','12','NE'),
 (18,'2025-01-18T11:58:00.631','Dubrovnik','RAIN','12','1015','16','NE'),
 (23,'2025-01-18T13:32:46.124','Rijeka','SUNNY','23','1012','8','CALM'),
 (25,'2025-01-18T17:47:09.982','Osijek','CLOUDY','12','1005','12','NE'),
 (26,'2025-01-18T15:52:00.282','Rijeka','SUNNY','5','1015','13','SW'),
 (30,'2025-01-18T17:56:35.282','Osijek','SUNNY','18','1015','12','W'),
 (31,'2025-01-18T18:19:46.375','Zagreb','SNOW','-8','990','34','N'),
 (32,'2025-01-18T19:30:38.026','Split','SUNNY','23','1020','10','E'),
 (33,'2025-01-18T20:33:04.393','Zagreb','SUNNY','23','1015','5','S'),
 (34,'2025-01-18T21:29:14.593','Dubrovnik','RAIN','12','990','50','SE');
COMMIT;
