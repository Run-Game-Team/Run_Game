using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ChatOnOff : MonoBehaviour
{
    public Canvas canvasChat;

    bool isConnect = false;
    TcpClient client = null;
    NetworkStream stream;

    private async void Connect()
    {
        try
        {
            client = new TcpClient();
            await Task.Run(() => client.Connect("127.0.0.1", 9999));
            Console.WriteLine("�������� ����.");
            Console.ReadKey();

            stream = client.GetStream();
            
            SendMessage();
            ReceiveMessagesAsync();

        }
        catch (SocketException e)
        {
            Console.WriteLine("���� ����");
        }
        finally
        {
            if(client.Connected)
            {
                client.Close();
            }
        }
    }

    private void SendMessage()
    {
        Console.WriteLine("���� message�� �Է����ּ���");
        string message = "Test";
        byte[] byteData = new byte[message.Length];
        byteData = Encoding.UTF8.GetBytes(message);

        client.GetStream().Write(byteData, 0, byteData.Length);
        Console.WriteLine("���ۼ���");
        Console.ReadKey();
    }

    public void OnClick()
    {
        if (canvasChat.enabled)
        {
            canvasChat.enabled = false;
        }
        else
        {
            if (!isConnect)
            {
                isConnect = true;
                Connect();
            }
            canvasChat.enabled = true;
        }
    }

    private async void ReceiveMessagesAsync()
    {
        try
        {
            while (true)
            {
                byte[] data = new byte[1024];
                int bytesRead = await stream.ReadAsync(data, 0, data.Length);  // �񵿱������� ������ �б�

                if (bytesRead > 0)
                {
                    string message = Encoding.UTF8.GetString(data, 0, bytesRead);
                    Debug.Log($"�����κ��� �޽��� ����: {message}");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("���� �� ����: " + e.Message);
        }
    }

}
