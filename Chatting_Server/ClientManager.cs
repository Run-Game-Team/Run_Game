using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chatting_Server
{
    internal class ClientManager
    {
        public static ConcurrentDictionary<int, ClientData> clientDic = new ConcurrentDictionary<int, ClientData>();
        public event Action<string, string> messageParsingAction = null;


        public void AddClient(TcpClient newClient)
        {
            ClientData currentClient = new ClientData(newClient);            

            try
            {
                currentClient.tcpClient.GetStream().BeginRead(
                currentClient.readByteData,
                0,
                currentClient.readByteData.Length,
                new AsyncCallback(DataReceived),
                currentClient);

                clientDic.TryAdd(currentClient.clientNumber, currentClient);
            }
            catch (Exception e)
            {
            }
        }

        private void DataReceived(IAsyncResult asyncResult)
        {
            // 콜백으로 받아온 Data를 ClientData로 형변환
            ClientData client = asyncResult.AsyncState as ClientData;

            try
            {
                int bytesRead = client.tcpClient.GetStream().EndRead(asyncResult);
                if (bytesRead > 0)
                {

                    // 문자열로 넘어온 데이터를 파싱해서 출력
                    string strData = Encoding.UTF8.GetString(client.readByteData, 0, bytesRead);

                    Console.WriteLine(strData);

                    // 비동기서버는 while문을 돌리지 않고 콜백메서드에서 다시 읽으라고 비동기 명령을 내린다.
                    client.tcpClient.GetStream().BeginRead(
                        client.readByteData,
                        0,
                        client.readByteData.Length,
                        new AsyncCallback(DataReceived),
                        client);

                    if (string.IsNullOrEmpty(client.clientName))
                    {
                        string userName = "test";
                        client.clientName = userName;
                    }

                    //if (messageParsingAction != null)
                    //{
                    //    messageParsingAction.BeginInvoke(client.clientName, strData, null, null);
                    //}
                }
                //else
                //    Console.WriteLine("{0}번 플레이어의 연결이 끊어졌습니다.", client.clientNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}번 플레이어의 연결이 끊어졌습니다.", client.clientNumber);
            }
        }
    }
}
