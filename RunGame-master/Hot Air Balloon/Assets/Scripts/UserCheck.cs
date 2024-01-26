using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UserCheck : MonoBehaviour
{
    static public string addScoreURL = "http://localhost/playerInfo/addscore.php?";
    static public string displayURL = "http://localhost/playerInfo/display.php";

    static public IEnumerator CreateUser()
    {
        // DataManager���� ���� �̸�, ��й�ȣ�� �����´�.
        string curName = DataManager.instance.userName;
        string curPassword = DataManager.instance.password;

        // displayURL�� �ִ� �̸����� �� �����´�
        UnityWebRequest hs_get = UnityWebRequest.Get(displayURL);
        yield return hs_get.SendWebRequest(); // �񵿱������� ��û�� ����
        if (hs_get.error != null)
        {
            Debug.Log("There was an error");
        }
        else
        {
            bool hasSameName = false;
            string dataText = hs_get.downloadHandler.text;
            MatchCollection mc = Regex.Matches(dataText, @"_");
            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                for (int i = 0; i < mc.Count; i += 3)
                {
                    string dbName = splitData[i];
                    if (dbName.Length != curName.Length) // �ٸ� �̸�
                        continue;

                    if (curName == dbName)
                    {
                        hasSameName = true;
                        yield return false;
                    }
                }
            }

            if (!hasSameName)
            {
                // �����ͺ��̽��� �߰��Ѵ�.
                int data1 = (mc.Count / 2) + 1;
                string data2 = curName;
                string data3 = curPassword;
                int data4 = 0;

                WWWForm form = new WWWForm();
                form.AddField("data1", data1);
                form.AddField("data2", data2);
                form.AddField("data3", data3);
                form.AddField("data4", data4);

                UnityWebRequest www = UnityWebRequest.Post(addScoreURL, form);
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Insert Error");
                    Debug.Log(data1);
                    Debug.Log(data2);
                    Debug.Log(data3);
                    Debug.Log(data4);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }

        yield return true;
    }

    static public IEnumerator CheckSameName()
    {
        // DataManager���� ���� �̸�, ��й�ȣ�� �����´�.
        string curName = DataManager.instance.userName;
        string curPassword = DataManager.instance.password;

        // displayURL�� �ִ� �̸����� �� �����´�
        UnityWebRequest hs_get = UnityWebRequest.Get(displayURL);
        yield return hs_get.SendWebRequest(); // �񵿱������� ��û�� ����

        if (hs_get.result == UnityWebRequest.Result.ConnectionError || hs_get.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("There was an error: " + hs_get.error);
        }
        else
        {
            string dataText = hs_get.downloadHandler.text;
            MatchCollection mc = Regex.Matches(dataText, @"_");
            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                for (int i = 0; i < mc.Count; i += 3)
                {
                    string dbName = splitData[i];
                    if (dbName.Length != curName.Length) // �ٸ� �̸�
                        continue;

                    if (curName == dbName)
                    {
                        string dbPassword = splitData[i + 1];
                        if (curPassword == dbPassword)
                        {
                            Debug.Log("Same name and password found in the database.");
                            DataManager.instance.successLogin = true;
                            // �α��� ���� �� DB�� ����� �ְ� ����� GameManager�� highestScore�� �����
                            DataManager.instance.myHighestScore = int.Parse(splitData[i + 2]);
                            Debug.Log(DataManager.instance.myHighestScore);
                            yield return true;
                        }
                    }
                }
            }
        }

        yield return false;
    }
}
