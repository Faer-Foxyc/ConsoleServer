using System;
using System.Text;
using System.Net.Sockets;

namespace ConsoleServer
{
    internal class ClientObject
    {
        public TcpClient Client;
        public ClientObject(TcpClient client)
        {
            Client = client;
        }
        public void Process()
        {
            NetworkStream networkStream = null;
            try
            {
                networkStream = Client.GetStream();
                byte [] buffer = new byte[1024]; // Буфер для получаемых данных
                while (true)
                {
                    // Получаем сообщение
                    StringBuilder stringBuilder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = networkStream.Read(buffer, 0, buffer.Length);
                        stringBuilder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                    } 
                    while (networkStream.DataAvailable);
                    string message = stringBuilder.ToString();
                    Console.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (networkStream != null)
                {
                    networkStream.Close();
                }
                if (Client != null)
                {
                    Client.Close();
                }
            }
        }
    }
}
