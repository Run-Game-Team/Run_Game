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
        // DataManager에서 현재 이름, 비밀번호를 가져온다.
        string curName = DataManager.instance.userName;
        string curPassword = DataManager.instance.password;

        // displayURL에 있는 이름들을 다 가져온다
        UnityWebRequest hs_get = UnityWebRequest.Get(displayURL);
        yield return hs_get.SendWebRequest(); // 비동기적으로 요청을 보냄
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
                    if (dbName.Length != curName.Length) // 다른 이름
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
                // 데이터베이스에 추가한다.
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
        // DataManager에서 현재 이름, 비밀번호를 가져온다.
        string curName = DataManager.instance.userName;
        string curPassword = DataManager.instance.password;

        // displayURL에 있는 이름들을 다 가져온다
        UnityWebRequest hs_get = UnityWebRequest.Get(displayURL);
        yield return hs_get.SendWebRequest(); // 비동기적으로 요청을 보냄

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
                    if (dbName.Length != curName.Length) // 다른 이름
                        continue;

                    if (curName == dbName)
                    {
                        string dbPassword = splitData[i + 1];
                        if (curPassword == dbPassword)
                        {
                            Debug.Log("Same name and password found in the database.");
                            DataManager.instance.successLogin = true;
                            // 로그인 했을 때 DB에 저장된 최고 기록을 GameManager의 highestScore에 기록함
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
