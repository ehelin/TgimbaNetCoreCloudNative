INSERT INTO bucket.bucketuser
(username, salt, "password", email, token, created, createdby, modified, modifiedby) 
VALUES 
( 
'demouser', 
'',
'', 
'demo@test.com', 
'', 
'2016-05-13 07:25:54.347', 
'Website', 
'2016-05-13 07:25:54.347', 
'Website');

INSERT INTO bucket.systemstatistics
(websiteisup, databaseisup, azurefunctionisup, created) 
VALUES ( true, true, true, '2019-06-07 09:30:02.107');

INSERT INTO bucket.buildstatistics
(starttime, endtime, buildnumber, status, "type") 
VALUES 
(
'2019-05-10 03:27:36.000', 
'2019-05-10 03:27:56.000', 
'20190510.6', 
'Succeeded', 
'CICD Pipeline-Website');
				
