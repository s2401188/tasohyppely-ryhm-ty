using UnityEngine;

public class BossMusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public BossPhaseController boss;

    public float phase1LoopStart = 6f;
    public float phase1LoopEnd = 84f;
    public float fakePhaseDialogueStart = 84f;
    public float fakePhaseAttackStart = 104f;
    public float fakePhaseEnd = 145f;

    public float truePhaseDialogueStart = 145f;
    public float truePhaseLoopStart = 166f;
    public float truePhaseLoopEnd = 225f;

    public float desperationDialogueStart = 225f;
    public float desperationAttackStart = 247f;
    public float desperationTier2Start = 289f;
    public float endingStart = 319f;

    public int fakePhaseHPThreshold = 70;
    public int desperationHPThreshold = 1;

    bool hpReadyFake;
    bool hpReadyDesperation;

    void Start()
    {
        musicSource.time = phase1LoopStart;
        musicSource.Play();
        boss.ChangePhase(BossPhase.Phase1);
        boss.SetInvincible(false);
    }

    void Update()
    {
        float t = musicSource.time;

        if (boss.currentPhase == BossPhase.Phase1)
        {
            if (!hpReadyFake && boss.currentHealth <= fakePhaseHPThreshold)
            {
                hpReadyFake = true;
                boss.SetInvincible(true);
            }

            if (t >= phase1LoopEnd && t < fakePhaseDialogueStart)
            {
                if (!hpReadyFake)
                {
                    musicSource.time = phase1LoopStart;
                }
                else
                {
                    musicSource.time = fakePhaseDialogueStart;
                }
            }
            else if (t >= fakePhaseDialogueStart && t < fakePhaseAttackStart)
            {
                boss.ChangePhase(BossPhase.FakePhase);
            }
        }
        else if (boss.currentPhase == BossPhase.FakePhase)
        {
            boss.SetInvincible(true);
            if (t >= fakePhaseEnd && t < truePhaseDialogueStart)
            {
                musicSource.time = truePhaseDialogueStart;
            }
            else if (t >= truePhaseDialogueStart && t < truePhaseLoopStart)
            {
                boss.ChangePhase(BossPhase.TruePhase);
                boss.SetInvincible(true);
            }
            else if (t >= truePhaseLoopStart && t < truePhaseLoopEnd)
            {
                boss.SetInvincible(false);
            }
        }
        else if (boss.currentPhase == BossPhase.TruePhase)
        {
            if (!hpReadyDesperation && boss.currentHealth <= desperationHPThreshold)
            {
                hpReadyDesperation = true;
                boss.SetInvincible(true);
            }

            if (t >= truePhaseLoopEnd && t < desperationDialogueStart)
            {
                if (!hpReadyDesperation)
                {
                    musicSource.time = truePhaseLoopStart;
                }
                else
                {
                    musicSource.time = desperationDialogueStart;
                }
            }
            else if (t >= desperationDialogueStart && t < desperationAttackStart)
            {
                boss.ChangePhase(BossPhase.Desperation);
                boss.SetInvincible(true);
            }
        }
        else if (boss.currentPhase == BossPhase.Desperation)
        {
            boss.SetInvincible(true);
            if (t >= desperationAttackStart && t < endingStart)
            {
            }
            else if (t >= endingStart)
            {
                boss.ChangePhase(BossPhase.Ending);
                boss.SetInvincible(true);
            }
        }
        else if (boss.currentPhase == BossPhase.Ending)
        {
        }
    }
}
