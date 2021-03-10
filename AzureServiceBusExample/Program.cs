using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace AzureServiceBusExample
{
    class Program
    {
        async static Task Main(string[] args)
        {
            await Test01();
            Console.ReadLine();
        }

        private async static Task Test01()
        {
            string connectionString = "<This is your endpoint connectionString>";
            string queueName = "<your queue name>";

            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using var client = new ServiceBusClient(connectionString);

            // create the sender
            ServiceBusSender sender = client.CreateSender(queueName);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage message = new ServiceBusMessage("Hello world!");

            // send the message
            await sender.SendMessageAsync(message);

            // create a receiver that we can use to receive the message
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);

            // the received message is a different type as it contains some service set properties
            ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

            // get the message body as a string
            string body = receivedMessage.Body.ToString();
            Console.WriteLine(body);
        }
    }
}
