using UnityEngine;

public class BossSpriteController : MonoBehaviour
{
    public BossPhaseController phaseController;
    public SpriteRenderer sr;

    public Sprite phase1Sprite;
    public Sprite fakePhaseSprite;
    public Sprite truePhaseSprite;
    public Sprite desperationSprite;

    BossPhase lastPhase;

    void Start()
    {
        lastPhase = phaseController.currentPhase;
        UpdateSprite();
    }

    void Update()
    {
        if (phaseController.currentPhase != lastPhase)
        {
            lastPhase = phaseController.currentPhase;
            UpdateSprite();
        }
    }

    void UpdateSprite()
    {
        if (lastPhase == BossPhase.Phase1) sr.sprite = phase1Sprite;
        else if (lastPhase == BossPhase.FakePhase) sr.sprite = fakePhaseSprite;
        else if (lastPhase == BossPhase.TruePhase) sr.sprite = truePhaseSprite;
        else if (lastPhase == BossPhase.Desperation) sr.sprite = desperationSprite;
    }
}
