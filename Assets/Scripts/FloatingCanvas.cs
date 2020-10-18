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

    private void Awake()
    {
        playerData = PlayerData.inst;
    }

    private void Start()
    {
        Enemy.OnAnyEnemyDie += () => AddKillCount();
        PlayerData.inst.OnDamaged += (dmg) => OnPlayerDamaged(dmg);
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
        GameManager.Instance.Pulse(0.01f, 150, 75);
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


    public void SetTextCenter(string str)
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
