using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor; // 바이너리 파일 포맷을 위한 네임스페이스

// 게임 데이터를 저장, 로드하는 기능
public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;

    public GameData data;

    public string dataPath;
    public string userName; // 파일명을 처음 입력한 유저 이름으로 설정
    public string password; // 파일명을 처음 입력한 유저 비밀번호로 설정
    public string fileExtension = ".dat";
    public bool successLogin = false;
    public int myHighestScore = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;           
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this); // 타이틀화면에서 데이터를 불러오기 때문에 씬이동시 파괴되면 안됨
    }

    // 경로, 파일명 설정
    public void Initialize(string name, string pw)
    {
        userName = name;
        password = pw;
        dataPath = Application.persistentDataPath + userName + fileExtension;        
    }

    public void SetNameAndPassword(string name, string pw)
    {
        userName = name;
        password = pw;
    }

    public void Save(GameData gameData)
    {
        // 바이너리 파일 포맷을 위한 BinaryFormatter 생성
        BinaryFormatter bf = new BinaryFormatter();
        // 데이터 저장을 위한 파일 생성
        FileStream file = File.Create(dataPath);

        data = gameData;

        // BinaryFormatter를 사용해 파일에 데이터 기록
        bf.Serialize(file, data);
        Debug.Log("저장됨");
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(dataPath)) // 입력한 경로에 사용자명.dat 파일이 존재하면
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);

            // GameData 클래스에 파일로부터 읽은 데이터를 기록
            data = (GameData)bf.Deserialize(file);
            Debug.Log("로드됨");
            file.Close();
        }
        else
            data = null; // 저장된 파일이 없을 경우 null로 지정
    }
}
