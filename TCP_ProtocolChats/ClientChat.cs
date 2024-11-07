using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace TCP_ProtocolChats
{
    internal class ClientChat
    {
        static void Main(string[] args)
        {
             
            
            int port = 443; // پورت مورد نظر برای ارتباط

            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Server started... Waiting for connection...");

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client connected.");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            string message = "";

            while (true)
            {
                // دریافت پیام از کلاینت
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Client: " + message);

                // پایان دادن به چت در صورت دریافت پیام "exit"
                if (message.ToLower() == "exit") break;

                // ارسال پاسخ به کلاینت
                Console.Write("You: ");
                message = Console.ReadLine();
                buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);

                if (message.ToLower() == "exit") break;
            }

            client.Close();
            server.Stop();
            Console.WriteLine("Server stopped.");
        }
    }
}

