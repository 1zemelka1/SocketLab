using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace SocketLab
{
    internal class Server
    {
        public static void Main()
        {
            Int32 serverPort = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(localAddr, serverPort);
            server.Start();

            Console.WriteLine("Сервер запущен...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Подключен клиент.");

                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
            }
        }

        static void HandleClient(TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    while (true)
                    {
                        Byte[] bytes = new Byte[2058];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        if (i == 0)
                        {
                            Console.WriteLine("Клиент закрыл соединение.");
                            break;
                        }
                        String data = Encoding.UTF8.GetString(bytes, 0, i);
                        Console.WriteLine("Получен текст: {0}", data);

                        TextSorting processor = new TextSorting();
                        List<string> sortedWords = processor.ProcessTextBlock(data);

                        StringBuilder responseBuilder = new StringBuilder();

                        for (int j = 0; j < sortedWords.Count; j++)
                        {
                            responseBuilder.AppendLine($"{j + 1}. {sortedWords[j]}");
                        }

                        string response = responseBuilder.ToString();

                        Byte[] responseData = Encoding.UTF8.GetBytes(response);
                        stream.Write(responseData, 0, responseData.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при обработке клиента: {0}", ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
