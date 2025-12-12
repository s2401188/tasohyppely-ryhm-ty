using UnityEngine;

public class BossSpriteController : MonoBehaviour
{
    public BossPhaseController phaseController;

    public SpriteRenderer bodyRenderer;
    public Sprite phase1Sprite;
    public Sprite fakePhaseSprite;
    public Sprite truePhaseSprite;

    public GameObject wings;
    public GameObject clothes;
    public GameObject Miekka;

    BossPhase lastPhase;

    void Start()
    {
        lastPhase = phaseController.currentPhase;
        UpdateVisuals();
    }

    void Update()
    {
        if (phaseController.currentPhase != lastPhase)
        {
            lastPhase = phaseController.currentPhase;
            UpdateVisuals();
        }
    }

    void UpdateVisuals()
    {
        if (lastPhase == BossPhase.Phase1)
        {
            bodyRenderer.sprite = phase1Sprite;
            wings.SetActive(false);
            clothes.SetActive(false);
        }
        else if (lastPhase == BossPhase.FakePhase)
        {
            bodyRenderer.sprite = fakePhaseSprite;
            wings.SetActive(false);
            clothes.SetActive(false);
        }
        else if (lastPhase == BossPhase.TruePhase)
        {
            bodyRenderer.sprite = truePhaseSprite;
            wings.SetActive(true);
            clothes.SetActive(true);
        }
        else if (lastPhase == BossPhase.Desperation)
        {
            bodyRenderer.sprite = truePhaseSprite;
            wings.SetActive(true);
            clothes.SetActive(true);
            Miekka.SetActive(true); 
        }
        else
        {
            wings.SetActive(false);
            clothes.SetActive(false);
        }
    }
}