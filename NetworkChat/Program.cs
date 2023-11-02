namespace NetworkChat;

public class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length == 1)
        {
            var serverPort = 0;
            var serverParse = int.TryParse(args[0], out serverPort);
            /// <summary>
            /// If the conversion failed, an error message is displayed and the program terminates execution.
            /// </summary>
            if (!serverParse)
            {
                Console.WriteLine("Incorrect port");
                return;
            }
            /// <summary>
            /// the server that listens on the specified port will be started
            /// </summary>
            await Server.Run(serverPort, Console.In, Console.Out);
            break;
        }
        else if (args.Length == 2)
        {
            var clientPort = 0;
            var clientParse = int.TryParse(args[1], out clientPort);
            if (!clientParse)
            {
                Console.WriteLine("Incorrect port");
                return;
            }
            /// <summary>
            /// The client will be launched, which will connect to the specified IP address and port
            /// </summary>
            await Client.Run(clientPort, args[0], Console.In, Console.Out);
            break;
        }
        else
        {
            Console.WriteLine("Incorrect input");
            break;
        }
    }
}