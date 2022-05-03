using UnityEngine;

// ScriptableObject хранящий статы игрока и отслеживающий их корректность
// Реализовано через SO для снижения связанности.

[CreateAssetMenu(fileName = "playerStats", menuName = "SO/Player Stats", order = 10)]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] private IntVariableSO health;
    [SerializeField] private int initialHealth = 100;
    [Space]
    [SerializeField] private IntVariableSO exp;
    [SerializeField] private int initialExp = 0;
    [Space]
    [SerializeField] private string playerName;

    private bool isDead = false;

    public int Health => health.Value;
    public int Exp => exp.Value;


    public void OnEnable()
    {
        health.Value = initialHealth;
        exp.Value = initialExp;
        isDead = false;
    }


    // при смерти игрока вызывает соотв. событие
    public void Damage(int damage)
    {
        if (damage < 0)
        {
            Debug.Log("Use Heal() for healing insted of Damage()");
        }
        
        if (!isDead)
        {
            health.Value -= damage;

            if (health.Value == 0)
            {
                isDead = true;
                Events.onPlayerDeath.Invoke(playerName);
            }
        }
    }


    public void Heal(int healing)
    {
        if (healing < 0)
        {
            Debug.Log("Use Damage() for hurting insted of Heal()");
        }

        if (!isDead)
        {
            health.Value += healing;
        }
    }


    public void GiveExp(int expToGive)
    {
        if (!isDead)
        {
            exp.Value += expToGive;
        }
    }


    public void LoseExp(int expToLose)
    {
        if (!isDead)
        {
            exp.Value -= expToLose;
        }
    }
}
