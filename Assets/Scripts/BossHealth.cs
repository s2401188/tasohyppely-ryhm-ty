using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHP = 100;
    public int hp = 100;
    public bool invincible;

    public void ResetHP()
    {
        hp = maxHP;
        invincible = false;
    }

    public bool TryTakeDamage(int amount)
    {
        if (invincible) return false;
        hp -= amount;
        if (hp < 0) hp = 0;
        return true;
    }
}
