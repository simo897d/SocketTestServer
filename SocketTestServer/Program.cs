using System;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Threading;

namespace SocketTestTing {
    public class Program {


        static void Main(string[] args) {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12222);
            listenerSocket.Bind(ipEnd);

            int clientNo = 0;
            while (true) {
                listenerSocket.Listen(0);
                Socket clientSocket = listenerSocket.Accept();


                Thread clientThread;
                clientThread = new Thread(() => ClientConnection(clientSocket, clientNo));
                clientThread.Start();
                clientNo++;
            }


        }
        private static void ClientConnection(Socket clientSocket, int clientNumber) {
            byte[] Buffer = new byte[clientSocket.SendBufferSize];
            int readByte;

            do {

                readByte = clientSocket.Receive(Buffer);

                byte[] rdata = new byte[readByte];

                Array.Copy(Buffer, rdata, readByte);
                Console.WriteLine(clientNumber.ToString() + " " + System.Text.Encoding.UTF8.GetString(rdata));
            } while (readByte > 0);

            Console.WriteLine("Bruger forlod");
            Console.ReadKey();
        }
    }
}

