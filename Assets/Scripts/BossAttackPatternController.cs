using UnityEngine;
using System.Collections;

public class BossAttackPatternController : MonoBehaviour
{
    public BossPhaseController phaseController;
    public BossMovementController movement;
    public Transform fireOrigin;
    public Transform player;
    public GameObject bulletPrefab;
    public GameObject lightningPrefab;
    public GameObject laserPrefab;
    public AudioSource musicSource;

    public float desperationTier2Start = 289f;

    float spiralAngle;

    void Start()
    {
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        while (true)
        {
            if (phaseController.currentPhase == BossPhase.Phase1)
                yield return StartCoroutine(Phase1Loop());
            else if (phaseController.currentPhase == BossPhase.FakePhase)
                yield return StartCoroutine(FakePhaseLoop());
            else if (phaseController.currentPhase == BossPhase.TruePhase)
                yield return StartCoroutine(TruePhaseLoop());
            else if (phaseController.currentPhase == BossPhase.Desperation)
                yield return StartCoroutine(DesperationLoop());
            else
                yield return null;
        }
    }

    IEnumerator Phase1Loop()
    {
        while (phaseController.currentPhase == BossPhase.Phase1)
        {
            int pattern = Random.Range(0, 3);
            if (pattern == 0) yield return StartCoroutine(SpiralBurst(60, 0.03f, 6f));
            else if (pattern == 1) yield return StartCoroutine(RingBurst(24, 5f));
            else yield return StartCoroutine(AimedBurst(5, 0.4f, 7f));
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator FakePhaseLoop()
    {
        while (phaseController.currentPhase == BossPhase.FakePhase)
        {
            yield return StartCoroutine(FakePhasePattern());
        }
    }

    IEnumerator TruePhaseLoop()
    {
        while (phaseController.currentPhase == BossPhase.TruePhase)
        {
            int pattern = Random.Range(0, 3);
            if (pattern == 0) yield return StartCoroutine(SpiralBurst(80, 0.02f, 7f));
            else if (pattern == 1) yield return StartCoroutine(RingBurst(32, 6f));
            else yield return StartCoroutine(LightningPattern());
            yield return new WaitForSeconds(0.7f);
        }
    }

    IEnumerator DesperationLoop()
    {
        while (phaseController.currentPhase == BossPhase.Desperation)
        {
            bool tier2 = musicSource.time >= desperationTier2Start;
            if (!tier2)
            {
                yield return StartCoroutine(SpiralBurst(80, 0.02f, 7f));
                yield return StartCoroutine(RainPattern(30, 0.15f, 5f));
                yield return StartCoroutine(LaserPattern());
            }
            else
            {
                yield return StartCoroutine(SpiralBurst(120, 0.015f, 8f));
                yield return StartCoroutine(DoubleRingBurst(32, 6f));
                yield return StartCoroutine(RainPattern(50, 0.1f, 6f));
                yield return StartCoroutine(LightningPattern());
                yield return StartCoroutine(LaserPattern());
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpiralBurst(int steps, float wait, float speed)
    {
        for (int i = 0; i < steps; i++)
        {
            spiralAngle += 120f * Time.deltaTime;
            float rad = spiralAngle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            SpawnBullet(fireOrigin.position, dir, speed);
            yield return new WaitForSeconds(wait);
        }
    }

    IEnumerator RingBurst(int count, float speed)
    {
        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i;
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            SpawnBullet(fireOrigin.position, dir, speed);
        }
        yield return null;
    }

    IEnumerator DoubleRingBurst(int count, float speed)
    {
        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i;
            float rad1 = angle * Mathf.Deg2Rad;
            float rad2 = rad1 + Mathf.PI / 12f;
            Vector2 dir1 = new Vector2(Mathf.Cos(rad1), Mathf.Sin(rad1));
            Vector2 dir2 = new Vector2(Mathf.Cos(rad2), Mathf.Sin(rad2));
            SpawnBullet(fireOrigin.position, dir1, speed);
            SpawnBullet(fireOrigin.position, dir2, speed);
        }
        yield return null;
    }

    IEnumerator AimedBurst(int shots, float delay, float speed)
    {
        for (int i = 0; i < shots; i++)
        {
            if (player != null)
            {
                Vector2 dir = (player.position - fireOrigin.position).normalized;
                SpawnBullet(fireOrigin.position, dir, speed);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator RainPattern(int count, float delay, float speed)
    {
        movement.GoToCenterTop();
        float left = -6f;
        float right = 6f;
        float y = 5.5f;
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(left, right);
            Vector3 pos = new Vector3(x, y, 0f);
            SpawnBullet(pos, Vector2.down, speed);
            yield return new WaitForSeconds(delay);
        }
        movement.Release();
    }

    IEnumerator LightningPattern()
    {
        movement.GoToCenterTop();
        yield return new WaitForSeconds(0.2f);
        float x = Random.Range(-4f, 4f);
        Vector3 pos = new Vector3(x, -1f, 0f);
        Instantiate(lightningPrefab, pos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        movement.Release();
    }

    IEnumerator LaserPattern()
    {
        movement.GoToCenter();
        yield return new WaitForSeconds(0.3f);
        Instantiate(laserPrefab, fireOrigin.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        movement.Release();
    }

    IEnumerator FakePhasePattern()
    {
        movement.GoToCenterTop();
        yield return StartCoroutine(SpiralBurst(60, 0.03f, 6f));
        yield return StartCoroutine(RainPattern(25, 0.18f, 5f));
        yield return new WaitForSeconds(1f);
        movement.Release();
    }

    void SpawnBullet(Vector3 pos, Vector2 dir, float speed)
    {
        GameObject b = Instantiate(bulletPrefab, pos, Quaternion.identity);
        BossBullet bb = b.GetComponent<BossBullet>();
        bb.speed = speed;
        bb.SetDirection(dir);
    }
}
