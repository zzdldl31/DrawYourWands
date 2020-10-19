using DYW;
using UnityEngine;
using UnityEngine.UI;

public class FloatingCanvas : MonoBehaviour
{
    public Transform cam;
    public Healthbar healthbar;
    public Text scoreboard, textCenter;
    public PlayerData playerData;

    public int killCount = 0;

    private void Start()
    {
        healthbar.maximumHealth = playerData.maxHp;
        healthbar.health = playerData.maxHp;
        Enemy.OnAnyEnemyDie += () => AddKillCount();

        PlayerData.inst.OnDamaged += (dmg) => OnPlayerDamaged(dmg);
        PlayerData.inst.OnHpUpdated += (value) => healthbar.SetHealth(value);
        PlayerData.inst.OnDied += () => OnPlayerDied();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = cam.position + cam.forward * 1f;
        transform.LookAt(cam);
    }

    private void OnPlayerDamaged(int dmg)
    {
        healthbar.TakeDamage(dmg);
        GameManager.Instance.Pulse(0.01f, 150, 15*dmg);
    }

    private void OnPlayerDied()
    {
        healthbar.healthPerSecond = 0;
        GameManager.Instance.ChangeGameState(0);
    }

    private void AddKillCount()
    {
        scoreboard.text = $"Kills: {++playerData.killCount}";
    }

    public void PrepareForGameStart()
    {
        healthbar.GainHealth(healthbar.maximumHealth);
        SetTextCenter("");
        MoveScoreBoard(428);
        healthbar.gameObject.SetActive(true);
    }

    public void SetForGameOver()
    {
        SetTextCenter("Game Over");
        MoveScoreBoard(-150);
        healthbar.gameObject.SetActive(false);
    }

    private void SetTextCenter(string str)
    {
        textCenter.text = str;
    }

    public void MoveScoreBoard(int ypos)
    {
        Vector3 curpos = scoreboard.transform.localPosition;
        curpos.y = ypos;
        scoreboard.transform.localPosition = curpos;
    }

}
