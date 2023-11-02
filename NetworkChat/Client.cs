using System.Net.Sockets;

namespace NetworkChat;

public static class Client
{
    public static async Task Run(int port, string IPaddress, TextReader userRead, TextWriter userWrite)
    {
        using (var tcpClient = new TcpClient(IPaddress, port))
        {
            /// <summary>
            /// A TcpClient object is created using the specified IP address and port.
            /// The TcpClient object provides functionality for establishing a network connection to a TCP server.
            /// </summary>
            using var serverWrite = new StreamWriter(stream);
            using var serverRead = new StreamReader(stream);
            using var stream = tcpClient.GetStream();

            while (true)
            {
                /// <summary>
                /// The string is sent to the server using the WriteLine Async method of the serverWrite object.
                /// The FlushAsync method is used to reset the buffer and send data to the server.
                /// </summary>
                var message = userRead.ReadLine();
                await serverWrite.WriteLineAsync(message);
                await serverWrite.FlushAsync();

                if (message == "exit")
                {
                    break;
                }
                /// <summary>
                /// Writing information about the sent message using the WriteLine Async method for the user Write object,
                /// reading a line from the server using ReadLineAsync for the serverReader object,
                /// writing information using WriteLine Async for the user Write object.
                /// </summary>
                await userWrite.WriteLineAsync($"Sent: {message}");
                var data = await serverRead.ReadLineAsync();
                await userWrite.WriteLineAsync($"Received: {data}");
                if (data == "exit")
                {
                    break;
                }
            }
        }
    }
}
