using System;
using System.Net.Sockets;

namespace SocketClient
{
    internal class Client
    {
        static void Main(string[] args)
        {
            Int32 port = 13000;
            string serverAddr = "127.0.0.1";
            TcpClient client = new TcpClient();
            client.Connect(serverAddr, port);
            NetworkStream stream = client.GetStream();

            try
            {
                while (true)
                {
                    Console.WriteLine("Введите текстовый блок (или пустую строку для завершения):");
                    string userInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        Console.WriteLine("Пустая строка введена. Завершение работы.");
                        break;
                    }

                    Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(userInput);
                    stream.Write(bytes, 0, bytes.Length);

                    bytes = new Byte[2056];
                    int i = stream.Read(bytes, 0, bytes.Length);
                    string data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                    Console.WriteLine("Список слов в алфавитном порядке без повторений:");
                    Console.WriteLine(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при обработке: {0}", ex.Message);
            }
            finally
            {
                Console.WriteLine("Нажмите любую клавишу для закрытия соединения...");
                Console.ReadKey();
                client.Close();
            }
        }
    }
}
