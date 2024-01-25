using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chatting_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("서버 콘솔창\n\n");

            TcpListener server = new TcpListener(IPAddress.Any, 9999);

            server.Start();

            // 클라이언트 객체를 만들어 9999에 연결한 clinet를 받아온다.
            // 클라이언트가 접속할 때까지 서버는 해당 구문에서 블락됨.
            TcpClient client = server.AcceptTcpClient();

            // Socket은 byte[] 형식으로 데이터를 주고 받음
            byte[] byteData = new byte[1024];

            // 클라이언트가 write한 데이터를 읽어옴
            client.GetStream().Read(byteData, 0, byteData.Length);

            // 출력을 위해 byteData를 string으로 변환
            Console.WriteLine(Encoding.Default.GetString(byteData));

            server.Stop();
        }
    }
}
