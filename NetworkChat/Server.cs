using System.Net.Sockets;
using System.Net;

namespace NetworkChat;

public static class Server
{
    public static async Task Run(int port, TextReader userRead, TextWriter userWrite)
    {

        var listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        using var socket = await listener.AcceptSocketAsync();
        await userWrite.WriteLineAsync("Ready to talk");
        await userWrite.WriteLineAsync("Сonnection established");

        /// <summary>
        /// NetworkStream, StreamReader, and StreamWriter objects are created.
        /// NetworkStream represents a network stream for reading and writing data.
        /// StreamReader is used to read data from NetworkStream, and StreamWriter is used to write data to NetworkStream.
        /// </summary>
        using var stream = new NetworkStream(socket);
        using var clientRead = new StreamReader(stream);
        using var clientWrite = new StreamWriter(stream);

        while (true)
        {
            var data = await clientRead.ReadLineAsync();
            if (data == "exit")
            {
                break;
            }
            /// <summary>
            /// A response message is sent to the client using the WriteLine Async method at the clientWrite object.
            /// The FlushAsync method is used to reset the buffer and send data to the client immediately.
            /// Then information about the sent message is recorded using the WriteLineAsync method at the userWrite object.
            /// </summary>
            await userWrite.WriteLineAsync($"Received: {data}");
            var response = userRead.ReadLine();
            await clientWrite.WriteLineAsync(response);
            await clientWrite.FlushAsync();
            await userWrite.WriteLineAsync($"Sent: {response}");
            if (response == "exit")
            {
                break;
            }
        }

        socket.Close();
    }
}
