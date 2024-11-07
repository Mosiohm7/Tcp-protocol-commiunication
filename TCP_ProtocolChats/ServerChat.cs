using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TCP_ProtocolChats
{
    internal class ServerChat
    {
        static void Main(string[] args)
        {
            string serverIP = "127.0.0.1"; // آی‌پی سرور
            int port = 443;

            TcpClient client = new TcpClient();
            client.Connect(serverIP, port);
            Console.WriteLine("Connected to the server.");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            string message = "";

            while (true)
            {
                // ارسال پیام به سرور
                Console.Write("You: ");
                message = Console.ReadLine();
                buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);

                // پایان دادن به چت در صورت ارسال پیام "exit"
                if (message.ToLower() == "exit") break;

                // دریافت پاسخ از سرور
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Server: " + message);

                if (message.ToLower() == "exit") break;
            }

            client.Close();
            Console.WriteLine("Disconnected from server.");
        }
    }
}
