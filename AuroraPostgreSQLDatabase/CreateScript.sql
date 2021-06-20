--step 1 - drop tables
drop table bucket.bucketlistuser;
drop table bucket.buildstatistics;
drop table bucket.log;
drop table bucket.systemstatistics;
drop table bucket.bucketlistitem;
drop table bucket.bucketuser;

--step 2 - drop schema
drop schema if exists bucket cascade;

--step 3 - recreate schema
create schema bucket;

--step 4 - recreate tables
create table bucket.bucketlistitem ( 
	bucketListitemid bigint GENERATED ALWAYS AS IDENTITY,
	listitemname varchar NULL,
	created timestamp with time zone NULL,
	category varchar(255) NULL,
	achieved boolean NULL,
	categorysortorder int NULL,
	latitude decimal(18, 10) NULL,
	longitude decimal(18, 10) NULL,
	PRIMARY KEY(bucketListitemid)
);

create table bucket.bucketuser(
	userId bigint GENERATED ALWAYS AS IDENTITY,
	userName varchar(255) NULL,
	salt varchar NULL,
	password varchar NULL,
	email varchar(255) NULL,
	token varchar(1000) NULL,
	created timestamp with time zone NULL,
	createdby varchar(255) NULL,
	modified timestamp with time zone NULL,
	modifiedby varchar(255) NULL,
	PRIMARY KEY(userId)
);

create table bucket.bucketlistuser(
	bucketlistuserid bigint GENERATED ALWAYS AS IDENTITY,
	bucketListitemid bigint NULL references bucket.bucketlistitem(bucketListitemid),
	userid bigint NULL references bucket.bucketuser(userid),
	PRIMARY KEY(bucketlistuserid)
);

create table bucket.buildstatistics(
	id bigint GENERATED ALWAYS AS IDENTITY,
	starttime timestamp with time zone NULL,
	endtime timestamp with time zone NULL,
	buildnumber varchar(500) NULL,
	status varchar(500) NULL,
	type varchar(500) NULL,
	PRIMARY KEY(id)
);

create table bucket.log(
	logid bigint GENERATED ALWAYS AS IDENTITY,
	messate varchar NULL,
	created timestamp with time zone NULL,
	PRIMARY KEY(logid)
);

create table bucket.systemstatistics(
	id bigint GENERATED ALWAYS AS IDENTITY,
	websiteisup boolean NULL,
	databaseisup boolean NULL,
	azurefunctionisup boolean NULL,
	created timestamp with time zone NULL,
	PRIMARY KEY(id)
);

