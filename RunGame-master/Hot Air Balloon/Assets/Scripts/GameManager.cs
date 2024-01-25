using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    // 게임의 속도 조절을 위한 두 스크립트
    public PlayerMove playerMove;
    public ScrollingObject scrollingObject;

    public GameObject gameResultBoard;
    public Text distanceScore;
    public Text coinScore;
    public Text totalScore;
    public Text totalScore2;
    public Text bestScore;
    public Text UserName;

    private int highestScore = 0; // 게임이 끝난 뒤 최고기록 표시

    const int scorePerCoin = 100;
    const int scorePerDistance = 20;

    public Text coinNum;
    private int coin;

    public bool isPause = false; // 키입력을 무시하는 일시정지 상태
    private bool isGameOver = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // 저장된 데이터를 로드
        DataManager.instance.Load();
        if (DataManager.instance.data != null) // 저장된 데이터가 존재하면
        {
            Player.instance.level = DataManager.instance.data.level;
            highestScore = DataManager.instance.data.highestScore;
            PlayerMove.instance.targetDistance = DataManager.instance.data.targetDistance;
        }
        
        Screen.SetResolution(800, 480, false); // 스크린 해상도 고정
        Application.targetFrameRate = 60; // 게임 프레임을 60으로 설정
    }

    public void GetCoin(int coinValue)
    {
        coin += coinValue;
        coinNum.text = coin.ToString();
    }

    public IEnumerator GameOver()
    {
        // 게임 오버는 한번만 호출됨
        if (!isGameOver)
        {
            isGameOver = true;
        }
        else
            yield break;

        
        EffectManager.instance.StartCoroutine(EffectManager.instance.PlayParticle(Enum.Particle.gameDead, Player.instance.transform.position));
        yield return new WaitForSeconds(1f);
        StopAllCoroutines();

        // 플레이어가 가지고 있는 PlayerMove 스크립트에서 이동한 거리를 불러옴
        int distance = (int)GameObject.Find("Player").GetComponent<PlayerMove>().curDistance;

        // 점수를 계산하고 결과창을 띄움
        distanceScore.text = (distance * scorePerDistance).ToString();
        coinScore.text = (coin * scorePerCoin).ToString();
        totalScore.text = ((distance * scorePerDistance) + (coin * scorePerCoin)).ToString();

        // 최고 기록을 갱신한 경우
        if (int.Parse(totalScore.text) > highestScore)
            highestScore = int.Parse(totalScore.text);

        // 베스트 스코어 보드에도 반영
        UserName.text = DataManager.instance.userName;
        // 닉네임 길이에 따른 폰트 사이즈 조절
        if(UserName.text.Length > 6)
        {
            int fontSizeReduction = (UserName.text.Length - 6) * 2; // 한 글자 늘어날 때마다 폰트 2씩 감소
            UserName.fontSize -= fontSizeReduction;

        }
        totalScore2.text = totalScore.text;
        bestScore.text = highestScore.ToString();
        gameResultBoard.SetActive(true);

        // 데이터 저장
        GameData data = new GameData();
        data.level = Player.instance.level;
        data.highestScore = highestScore;
        data.targetDistance = PlayerMove.instance.targetDistance;        
        DataManager.instance.Save(data);

        Debug.Log("들어옴!");
        Time.timeScale = 0;
    }
}
