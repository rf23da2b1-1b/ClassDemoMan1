using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoMan1
{
    public class Server
    {
        private const int PORT = 10100;
        private readonly Random rnd = new Random(DateTime.Now.Microsecond);


        public void Start()
        {
            // definerer server med port nummer
            TcpListener server = new TcpListener(IPAddress.Any, PORT);
            server.Start();
            Console.WriteLine("Server startet på port " + PORT);

            while (true)
            {
                // venter på en klient 
                TcpClient socket = server.AcceptTcpClient();

                Task.Run(
                    () =>
                    {
                        TcpClient tempsocket = socket;
                        DoOneClient(tempsocket);
                    }
                    );

            }
            //server.Stop();
        }

        private void DoOneClient(TcpClient socket)
        {
            Console.WriteLine($"Min egen (IP, port) = {socket.Client.LocalEndPoint}");
            Console.WriteLine($"Accepteret client (IP, port) = {socket.Client.RemoteEndPoint}");


            // åbner for tekst strenge
            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream());

            DoServerUdenJson(sr, sw);

            sr?.Close();
            sw?.Close();
        }

        private void DoServerUdenJson(StreamReader sr, StreamWriter sw)
        {
            // læser linje 1 fra klient
            string cmd = sr.ReadLine();
            Console.WriteLine("Modtaget: " + cmd);
            if (!(cmd == "Random" || cmd == "Add" || cmd == "Subtract" ))
            {
                // fejl
                sw.WriteLine("Fejl understøtter ikke " + cmd);
                sw.Flush();
                return; // lukker forbindelsen
            }

            // skriver linje 1 tilbage
            sw.WriteLine("Input numbers");
            sw.Flush();

            // læser linje 2 fra klient
            string talStr = sr.ReadLine();
            Console.WriteLine("Modtaget: " + talStr);
            // konverterer til to tal
            try
            {
                string[] strs = talStr.Split(" ");
                int tal1 = int.Parse(strs[0]);
                int tal2 = int.Parse(strs[1]);

                int result = 0;
                switch (cmd)
                {
                    case "Random":
                        {
                            result = rnd.Next(tal1,tal2); break;
                        }
                    case "Add":
                        {
                            result = tal1 + tal2; break;
                        }
                    case "Subtract":
                        {
                            result = tal1 - tal2; break;
                        }

                }


                // skriver linje tilbage - stadig ekko
                sw.WriteLine(result);
                sw.Flush();

            }
            catch (Exception ex)
            {
                // fejl
                sw.WriteLine("Fejl Der er ikke 2 tal ");
                sw.Flush();
            }


        }

        

    }
}
