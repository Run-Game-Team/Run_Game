using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chatting_Server
{
    internal class MyServer
    {
        ClientManager _clientManager = null;
        Thread connectCheckThread = null;

        public MyServer()
        {
            _clientManager = new ClientManager();
            //_clientManager.messageParsingAction += MessageParsing;
            //Task serverStart = Task.Run(() =>
            //{
            //    ServerRun();
            //});
            ServerRun();

            connectCheckThread = new Thread(ConnectCheckLoop);
            connectCheckThread.Start();
        }

        // 하트비트 스레드
        private async void ConnectCheckLoop()
        {
            while (true)
            {
                foreach (var item in ClientManager.clientDic)
                {
                    try
                    {
                        string sendStringData = "관리자<TEST>";
                        byte[] sendByteData = Encoding.UTF8.GetBytes(sendStringData);

                        await item.Value.tcpClient.GetStream().WriteAsync(sendByteData, 0, sendByteData.Length);
                    }
                    catch (Exception e)
                    {
                        RemoveClient(item.Value);
                    }
                }

                await Task.Delay(1000);
            }
        }

        // 클라이언트의 접속종료가 감지됐을 때 clientDic에서 해당 클라이언트를 제거     
        private void RemoveClient(ClientData client)
        {
            ClientData result = null;
            ClientManager.clientDic.TryRemove(client.clientNumber, out result);
        }

        private void ServerRun()
        {
            // 서버 포트 설정 및 시작
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Any, 9999));
            listener.Start();
            Console.WriteLine("서버를 시작합니다."); ;

            // 클라이언트의 연결 요청 대기
            while(true)
            {
                Task<TcpClient> acceptTask = listener.AcceptTcpClientAsync();

                acceptTask.Wait();

                TcpClient newClient = acceptTask.Result;

                _clientManager.AddClient(newClient);

                foreach (var item in ClientManager.clientDic)
                {
                    NetworkStream stream = item.Value.tcpClient.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes("test");

                    // 메시지 전송
                    stream.Write(data, 0, data.Length);
                }
            }
        }
    }
}
