using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ClassDemoMan1Json
{
    public class Server
    {
        private const int PORT = 10101;
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

            DoServerMedJson(sr, sw);

            sr?.Close();
            sw?.Close();
        }

        private void DoServerMedJson(StreamReader sr, StreamWriter sw)
        {
            Response resp = new Response();

            Request? req = null;
            // læser json linje fra klient
            try
            {
                string json = sr.ReadLine();
                req = JsonSerializer.Deserialize<Request>(json);
            }
            catch (Exception ex)
            {
                resp.ErrorMessage = ex.Message;
                resp.Ok = false;
            }

            if (req != null)
            {
                switch (req.Method)
                {
                    case "Random":
                        {
                            resp.Result = rnd.Next(req.Tal1, req.Tal2);
                            resp.Ok = true;
                            break;
                        }
                    case "Add":
                        {
                            resp.Result = req.Tal1 + req.Tal2;
                            resp.Ok = true;
                            break;
                        }
                    case "Subtract":
                        {
                            resp.Result = req.Tal1 - req.Tal2;
                            resp.Ok = true;
                            break;
                        }
                    default:
                        {
                            resp.Ok = false;
                            resp.ErrorMessage = $"metoden {req.Method} ikke understøttet";
                            break;
                        }

                }
            }


            // skriver linje tilbage - stadig ekko
            sw.WriteLine(JsonSerializer.Serialize(resp));
            sw.Flush();




        }

    }
}
