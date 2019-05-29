using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQS.NetCore
{
    class SQSService
    {
        private const string accessKey = "PLACEHOLDER";
        private const string secretKey = "PLACEHOLDER";
        private const string queueUrl = "https://sqs.us-east-2.amazonaws.com/PLACEHOLDER/PLACEHOLDER";
        private readonly AmazonSQSClient sqsClient;

        public SQSService()
        {
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast2);
        }

        public async Task SendMessages()
        {
            for (var i = 0; i < 10; i++)
            {
                var request = new SendMessageRequest
                {
                    QueueUrl = queueUrl,
                    MessageBody = "{ 'message': 'Test message' }"
                };

                Console.WriteLine("Sending message...");

                await sqsClient.SendMessageAsync(request);
            }
        }

        public async Task ReceiveMessages()
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl
            };

            while (true)
            {
                var response = await sqsClient.ReceiveMessageAsync(request);

                if (response.Messages.Count > 0)
                {
                    foreach (var message in response.Messages)
                    {
                        Console.WriteLine(message.Body);

                        var deleteRequest = new DeleteMessageRequest
                        {
                            QueueUrl = queueUrl,
                            ReceiptHandle = message.ReceiptHandle
                        };
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}