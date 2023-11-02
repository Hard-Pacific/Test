namespace NetworkChatTests;

public class NetworkChatTests
{
    [Test]
    public void StandartTest()
    {
        var server = File.OpenRead("ServerInput.txt");
        var client = File.OpenRead("ClientInput.txt");
        var output = File.OpenWrite("Output.txt");

        Task.Run(() => NetworkChat.Server.Run(8082, new StreamReader(server) as TextReader, new StreamWriter(output) as TextWriter));

        var result = NetworkChat.Client.Run(8082, "localhost", new StreamReader(client) as TextReader, new StreamWriter(output) as TextWriter);
        result.Wait();
    }
}