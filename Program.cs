using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleServer
{
    internal class Program
    {
        const int port = 8888;
        static TcpListener tcplistener;
        static void Main(string[] args)
        {
            try
            {
                tcplistener = new TcpListener(IPAddress.Parse("192.168.0.140"), port);
                tcplistener.Start();
                Console.WriteLine("Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = tcplistener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(tcpClient);

                    // Создаем новый поток для обслуживания нового клиента
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (tcplistener != null)
                {
                    tcplistener.Stop();
                }
            }
        }
    }
}
