using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using ShopAPI.Application.Abstraction.Storage.AWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Infrastructure.Services.Storage.Storage.AWS
{
    public class AwsStorage : Storage, IAwsStorage
    {
        readonly IAmazonS3 _amazonS3;

        public AwsStorage(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }


        private async Task CreateBucketAsync(string bucketName)
        {
            try
            {
                var bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
                if (!bucketExists)
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        BucketRegion = S3Region.EUNorth1
                    };

                    var response = await _amazonS3.PutBucketAsync(putBucketRequest);
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine("Bucket created successfully.");
                    }
                }
                else
                {
                    Console.WriteLine("Bucket already exists.");
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }



           







        public async Task DeleteAsync(string pathOrbucketName, string fileName)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = pathOrbucketName,
                Key = fileName
            };

            await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
        }





        public async Task<List<string>> GetFiles(string pathOrbucketName)
        {
            var listObjectsRequest = new ListObjectsV2Request
            {
                BucketName = pathOrbucketName
            };

            var listObjectsResponse = await _amazonS3.ListObjectsV2Async(listObjectsRequest);

            return listObjectsResponse.S3Objects.Select(o => o.Key).ToList();
        }



        public async Task<bool> HasFile(string pathOrbucketName, string fileName)
        {
            try
            {
                var getObjectMetadataRequest = new GetObjectMetadataRequest
                {
                    BucketName = pathOrbucketName,
                    Key = fileName
                };

                var response = await _amazonS3.GetObjectMetadataAsync(getObjectMetadataRequest);

                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }



        public async Task<List<(string fileName, string pathOrbucketName)>> UploadAsync(string pathOrbucketName, IFormFileCollection files)
        {

            var uploadedFiles = new List<(string fileName, string bucketName)>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var newFileName = await FileRenameAsync(file.FileName);

                    var key = newFileName;
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadRequest = new PutObjectRequest
                        {
                            BucketName = pathOrbucketName,
                            Key = key,
                            InputStream = stream,
                            ContentType = file.ContentType
                        };

                        var response = await _amazonS3.PutObjectAsync(uploadRequest);

                        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        {
                            uploadedFiles.Add((key, $"{pathOrbucketName}/{key}"));
                        }
                    }
                }
            }

            return uploadedFiles;
        }


     
    }
}
