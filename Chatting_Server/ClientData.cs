using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chatting_Server
{
    // 연결이 확인된 클라이언트를 넣어줄 클래스
    internal class ClientData
    {
        // readByteData는 stream 데이터를 읽어오 객체
        public TcpClient tcpClient { get; set; }
        public byte[] readByteData { get; set; }
        public string clientName { get; set; }
        public int clientNumber { get; set; }

        public ClientData(TcpClient client)
        {
            tcpClient = client;
            readByteData = new byte[1024];

            // 127.0.0.1:9999가 있을 때 포트번호 직전에 있는 번호를 클라이언트 번호로 지정함.
            string clientEndPoint = client.Client.LocalEndPoint.ToString();
            char[] point = { ',', ':' };

            string[] splitedData = clientEndPoint.Split(point);
            this.clientNumber = int.Parse(splitedData[0].Split('.')[3]);

            Console.WriteLine("{0}번 사용자 접속 성공", clientNumber);
        }
    }
}
