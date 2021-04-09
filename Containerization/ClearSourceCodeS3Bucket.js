var AWS = require('aws-sdk')
var codepipeline = new AWS.CodePipeline();

async function DeleteAllDataFromDir(bucket, dir, params) {
	console.log('Startig DeleteAllDataFromDir(args)');
	console.log('bucket: ' + bucket);
	console.log('dir: ' + dir);
	console.log('params: ' + params);
	
	try 
	{		
		const listParams = {
			Bucket: bucket,
			Prefix: dir
		};
		var s3 = new AWS.S3();

		const listedObjects = await s3.listObjects(listParams).promise();
		console.log('reponse: ');
    		console.log(JSON.stringify(listedObjects, null, 2))
    
		if (listedObjects.Contents.length === 0) {
			console.log('There are no files to delete');
			return 0;
		}
		console.log('There are ' + listedObjects.Contents.length + ' records to delete');

		const deleteParams = {
			Bucket: bucket,
			Delete: { Objects: [] }
		};

		for(var i=0; i<listedObjects.Contents.length; i++)
		{
			var currentObject = listedObjects.Contents[i];
			console.log('currentObject: ');
			console.log(JSON.stringify(currentObject, null, 2))
			var Key = currentObject.Key;
			console.log('Adding key to delete - ' + Key);
			deleteParams.Delete.Objects.push({ Key });
		}

		console.log('Deleting all files');
		await s3.deleteObjects(deleteParams).promise();

		if (listedObjects.IsTruncated) await DeleteAllDataFromDir(bucket, dir);
		
		console.log('Done deleting all files');

		return codepipeline.putJobSuccessResult(params).promise();
	} 
	catch(err)
	{
		console.log('exports.handler() error: ' + err);
			
		return codepipeline.putJobFailureResult(params).promise();
	}
}

async function timeout(ms) {
    	return new Promise(resolve => setTimeout(resolve, ms));
}

exports.handler = async (event) => {
	console.log('exports.handler() starting...');

	// give ec2 instance time to spin up...w/o this delay, the source artifact is removed too soon
	await timeout(120000);

	var jobId = event["CodePipeline.job"].id;
	AWS.config.update({region: 'us-east-2'});

	var params = { jobId: jobId	};
	var bucket = 'your bucket name here (no slashes or s3://)';
	var dir = 'your bucket sub path (i.e. /path/path2/)';

	return DeleteAllDataFromDir(bucket, dir, params);
}
