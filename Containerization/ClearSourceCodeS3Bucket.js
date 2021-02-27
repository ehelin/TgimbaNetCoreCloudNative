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

		return 0; //codepipeline.putJobSuccessResult(params).promise();
	} 
	catch(err)
	{
		console.log('exports.handler() error: ' + err);
			
		return 0; //codepipeline.putJobFailureResult(params).promise();
	}
}

exports.handler = async (event) => {
	console.log('exports.handler() starting...');
	
	//var jobId = event["CodePipeline.job"].id
	//var awsHost = '';
	//AWS.config.update({region: 'us-east-2'});
	
	var jobId = 1;
	var params = { jobId: jobId	};
	var bucket = 'codepipeline-us-east-2-778197328464';
	var dir = 'NetCore3_1SampleApp2/SourceArti';

	return DeleteAllDataFromDir(bucket, dir, params);
}
