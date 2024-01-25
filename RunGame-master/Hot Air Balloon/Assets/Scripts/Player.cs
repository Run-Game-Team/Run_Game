using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance = null;

    public Text lifeText;

    public float maxHP = 100;
    public float currentHP;
    public int life = 1;
    public int level = 1;

    public bool isDead = false;
    public bool isUnbeat = false; // 피격x
    public bool isInvincibility = false; // 무적

    private SpriteRenderer playerSprite;

    // 레벨
    // 현재 경험치, 필요 경험치

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        playerSprite = GetComponent<SpriteRenderer>();

        currentHP = maxHP;

        // 체력이 지속적으로 감소
        StartCoroutine("HPDown");
    }

    public void LevelUp()
    {
        level++;
        EffectManager.instance.StartCoroutine(EffectManager.instance.PlayForNSeconds(Enum.Particle.levelUp, transform.position, 5f));
        SoundManager.instance.PlayOnce(SoundManager.instance.levelUp);
        GetComponent<PlayerUI>().LevelUpdate(); // 현재 레벨을 UI에 반영
    }

    public void Resurrection()
    {
        if (life > 0 && isDead)
        {
            life--;
            lifeText.text = "x " + life.ToString();
            PlayerMove.instance.anim.SetBool("isDead", false);
            GetComponent<PlayerUI>().isCoroutineStarted = false; // 부활 시 코루틴도 다시 실행될 수 있도록

            StartCoroutine("AlphaBlink");
            isDead = false;
            // 부활 이펙트 표시
            EffectManager.instance.StartCoroutine(EffectManager.instance.PlayForNSeconds(Enum.Particle.resurrection, transform.position, 1.5f));
            currentHP = maxHP;
            StartCoroutine("HPDown");
        }
        else
            return;
    }

    // 에어를 먹었을 때 체력 회복
    public void HPUp(int recovery)
    {
        Debug.Log("회복량: " + recovery);
        currentHP += recovery;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void GetDamage(int damage)
    {
        Debug.Log("받은 피해량: " + damage);
        StartCoroutine("AlphaBlink");
        currentHP -= damage;
        if (currentHP < 0)
            currentHP = 0;
    }

    IEnumerator HPDown()
    {
        while (currentHP > 0)
        {
            yield return new WaitForSeconds(0.1f * Time.timeScale); // 배속의 영향을 받지 않음
            currentHP -= 0.4f;
            if (currentHP <= 0)
            {                
                GetComponentInChildren<Magnet>().MagnetFiledDisable(); // 자석의 효과를 끔
                AlphaBlinkStop();               
                isDead = true;
            }
        }
    }

    // 알파값 깜빡임 처리
    IEnumerator AlphaBlink()
    {
        int coolTime = 0;

        isUnbeat = true; // 알파값 깜빡이는 동안 무적(충돌체 off)
        while(coolTime < 11)
        {
            if (coolTime == 0)
                playerSprite.color = new Color32(255, 255, 255, 255);
            else if (coolTime % 2 == 0)
                playerSprite.color = new Color32(255, 255, 255, 90);
            else
                playerSprite.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);
            coolTime++;
        }
        isUnbeat = false;

        playerSprite.color = new Color32(255, 255, 255, 255);
    }

    public void AlphaBlinkStop()
    {
        playerSprite.color = new Color32(255, 255, 255, 255);
        StopCoroutine("AlphaBlink");
    }
}
