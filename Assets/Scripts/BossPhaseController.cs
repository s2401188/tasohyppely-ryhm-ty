using UnityEngine;

public enum BossPhase
{
    Phase1,
    FakePhase,
    TruePhase,
    Desperation,
    Ending
}

public class BossPhaseController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool canTakeDamage = true;
    public BossPhase currentPhase = BossPhase.Phase1;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (!canTakeDamage) return;
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
    }

    public void SetInvincible(bool value)
    {
        canTakeDamage = !value;
    }

    public void ChangePhase(BossPhase phase)
    {
        currentPhase = phase;
    }
}
