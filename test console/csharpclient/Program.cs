using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace csharpclient
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
            clientSocket.Connect("127.0.0.1", 8000);
            Console.Write("Message: "); 
            String text = Console.ReadLine();
            NetworkStream serverStream = clientSocket.GetStream();
            //serverStream.SetLength(1024);
            byte[] outStream = System.Text.Encoding.UTF8.GetBytes(text);
            serverStream.Write(outStream, 0, text.Length);
            serverStream.Flush();

            byte[] inStream = new byte[1024];
            serverStream.Read(inStream, 0, 1024);
            string returndata = System.Text.Encoding.UTF8.GetString(inStream).Trim();
            Console.WriteLine("Reply: " + returndata);
        }
    }
}
