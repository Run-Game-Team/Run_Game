using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

using System; //
using System.Collections;
using System.Security.Cryptography; //
using System.Text.RegularExpressions; //
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking; //
using UnityEngine.UI;
using System.Runtime.CompilerServices; //

public class SceneMove : MonoBehaviour
{
    public int sceneNum;
    public bool hasCheckCondition = true; // 체크 해야할 조건을 가지고 있는지
    public GameObject tutorialView;
    private string secretKey = "mySecretKey";
    public string addScoreURL = "http://localhost/playerInfo/addscore.php?";
    public string displayURL = "http://localhost/playerInfo/display.php";
    public Text nameTextInput;

    public void NextScene()
    {
        if (hasCheckCondition) // 체크 해야할 조건이 있으면
        {
            if (GetComponent<UserNameCheck>().TextCheck())
            {
                // DataManager에서 이름을 가져온다.
                string curName = DataManager.instance.userName;

                // displayURL에 있는 이름들을 다 가져온다
                UnityWebRequest hs_get = UnityWebRequest.Get(displayURL);
                hs_get.SendWebRequest();
                if (hs_get.error != null)
                {
                    Debug.Log("There was an error");
                }
                else
                {
                    string dataText = hs_get.downloadHandler.text;
                    MatchCollection mc = Regex.Matches(dataText, @"_");
                    if (mc.Count > 0)
                    {
                        string[] splitData = Regex.Split(dataText, @"_");
                        bool hasSameName = false;
                        for (int i = 0; i < mc.Count; i++)
                        {
                            string dbName = splitData[i];
                            if (dbName.Length != curName.Length) // 다른 이름
                                continue;

                            if (curName == dbName)
                            {
                                Debug.Log("동일한 이름을 가진 유저가 존재합니다.");
                                hasSameName = true;
                            }
                        }

                        // 같은 이름을 가진 사람이 없으면
                        if (!hasSameName)
                        {
                            // 데이터베이스에 추가한다.
                            int data1 = mc.Count + 1;
                            string data2 = curName;
                            string data3 = curName; // 이건 나중에 비밀번호로 바꿔야 할듯
                            int data4 = 0;

                            WWWForm form = new WWWForm();
                            form.AddField("data1", data1);
                            form.AddField("data2", data2);
                            form.AddField("data3", data3);
                            form.AddField("data4", data4);

                            UnityWebRequest www = UnityWebRequest.Post(addScoreURL, form);
                            www.SendWebRequest();

                            if (www.result != UnityWebRequest.Result.Success)
                            {
                                Debug.LogError("Insert Error");
                            }
                            else
                            {
                                Debug.Log(www.downloadHandler.text);
                            }
                        }
                    }
                }

                if (PlayerPrefs.GetInt("seeTutorial") == 1) // 튜토리얼을 더 이상 표시되지 않도록 체크 해놨다면
                    SceneManager.LoadScene(sceneNum); // 씬을 넘긴다
                else // 튜토리얼을 보지 않은 경우 입력창을 닫고 튜토리얼 창을 띄운다
                {
                    gameObject.GetComponentInParent<Canvas>().enabled = false;
                    tutorialView.SetActive(true);
                }
            }
        }
        else
            SceneManager.LoadScene(sceneNum);

        // 씬 이동 전에 걸어둔 일시정지 상태를 해제
        Time.timeScale = 1;
    }
}
