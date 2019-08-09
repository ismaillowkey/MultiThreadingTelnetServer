using System;
using System.Net;

namespace TelnetServer
{
    class Program
    {
        private static Server s;

        static void Main(string[] args)
        {
            s = new Server(IPAddress.Any);
            s.ClientConnected += clientConnected;
            s.ClientDisconnected += clientDisconnected;
            s.ConnectionBlocked += connectionBlocked;
            s.MessageReceived += messageReceived;
            s.start();

            Console.WriteLine("SERVER STARTED: " + DateTime.Now);

            char read = Console.ReadKey(true).KeyChar;

            do
            {
                if (read == 'b')
                {
                    s.sendMessageToAll(Console.ReadLine());
                }
            } while ((read = Console.ReadKey(true).KeyChar) != 'q');

            s.stop();
        }

        private static void clientConnected(Client c)
        {
            Console.WriteLine("CONNECTED: " + c);

            s.sendMessageToClient(c, "Telnet Server" + Server.END_LINE );
        }

        private static void clientDisconnected(Client c)
        {
            Console.WriteLine("DISCONNECTED: " + c);
        }

        private static void connectionBlocked(IPEndPoint ep)
        {
            Console.WriteLine(string.Format("BLOCKED: {0}:{1} at {2}", ep.Address, ep.Port, DateTime.Now));
        }

        private static void messageReceived(Client c, string message)
        {
            //if (c.getCurrentStatus() != EClientStatus.LoggedIn)
            //{
            //    handleLogin(c, message);
            //    return;
            //}

            c.setStatus(EClientStatus.LoggedIn);

            Console.WriteLine("MESSAGE: " + message);

            //if (message == "kickmyass" || message == "logout" ||
            //    message == "exit")
            //{
            //    s.kickClient(c);
            //    s.sendMessageToClient(c, Server.END_LINE + Server.CURSOR);
            //}

            //else if (message == "clear")
            //{
            //    s.clearClientScreen(c);
            //    s.sendMessageToClient(c, Server.CURSOR);
            //}

            //else
            //    s.sendMessageToClient(c, Server.END_LINE + Server.CURSOR);
        }

        
    }
}