using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] protected FoodData m_foodData;
    [SerializeField] protected CustomerData m_customerData;
    [SerializeField] protected CustomerHud m_customerHud;
    [SerializeField] protected SpriteRenderer m_customerRenderer;

    private float timeLeftTillAngry;
    public float TimeLeftTillAngry => timeLeftTillAngry;

    protected bool reacted; // Lazy boolean go brrrrr;

    private enum LineType {
        MalePositive,
        MaleNegative,
        MaleTimer,
        FemalePositive,
        FemaleNegative,
        FemaleTimer
    } Dictionary<LineType, string[]> lineBank;

    protected virtual void Start() {
        lineBank = new Dictionary<LineType, string[]>();
        lineBank[LineType.MalePositive] = new[] { "MaleP1", "MaleP2", "MaleP3", "MaleP4" };
        lineBank[LineType.MaleNegative] = new[] { "MaleN1", "MaleN2" };
        lineBank[LineType.MaleTimer] = new[] { "MaleT1", "MaleT2" };
        lineBank[LineType.FemalePositive] = new[] { "FemaleP1", "FemaleP2", "FemaleP3", "FemaleP4" };
        lineBank[LineType.FemaleNegative] = new[] { "FemaleN1", "FemaleN2" };
        lineBank[LineType.FemaleTimer] = new[] { "FemaleT1", "FemaleT2" };

        timeLeftTillAngry = m_customerData.timeTillAngry * Random.Range(0.75f, 1.2f);
    }

    public CustomerData CustomerData
    {
        get { return m_customerData; }
        set { m_customerData = value; }
    }
    public FoodData FoodChoice
    {
        get { return m_foodData; }
        set { m_foodData = value; }
    }

    private void Update()
    {
        m_customerRenderer.sprite = m_customerData.customerSprite;
        timeLeftTillAngry -= Time.deltaTime * 2;

        if (timeLeftTillAngry <= 0 && !reacted)
        {
            reacted = true;
            StartCoroutine(ReactToFood(null));
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var foodProj = collision.GetComponent<FoodProjectile>();
        if (foodProj != null) 
        {
            foreach (Collider2D comp in GetComponents<Collider2D>()) Destroy(comp);
            if (!reacted) {
                StartCoroutine(ReactToFood(foodProj));
                reacted = true;
            }
        }
    }
    protected virtual IEnumerator ReactToFood(FoodProjectile foodProj)
    {
        if (foodProj != null && foodProj.foodData.name == m_foodData.name) 
        {
            // Increment points to pointsystem based on m_foodData.value
            PlayVoiceline(true);
            m_customerHud.Satisfied();
            Score.Instance.SCORE += foodProj.foodData.value;
        }
        else
        {
            // Decrement points to pointsystem based on m_foodData.value
            if (foodProj != null) PlayVoiceline(false);
            else PlayVoiceline();
            m_customerHud.Unsatisfied();
            Score.Instance.SCORE -= m_foodData.value;
        }

        // Poof after 0.9 seconds of satisfication
        yield return new WaitForSeconds(0.9f);
        while (transform.localScale != Vector3.zero) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 0.15f);
            yield return null;
        } var parSystem = GetComponentInChildren<ParticleSystem>();
        var main = parSystem.main;
        main.startColor = GetComponentInChildren<SpriteRenderer>().sprite.texture.GetPixel(50, 50);
        parSystem.Play();
        Destroy(gameObject, main.startLifetime.constant);
    }

    // Version without argument is for timer;
    protected void PlayVoiceline() {
        var type = m_customerData.isMale ? LineType.MaleTimer : LineType.FemaleTimer;
        AudioManager.Instance.PlaySFX(lineBank[type][Random.Range(0, lineBank[type].Length)], 0.15f);
    }
    // Version with bool is for satisfied/unsatisfied
    protected void PlayVoiceline(bool satisfied) {
        var type = LineType.MalePositive;
        if (m_customerData.isMale) {
            type = satisfied ? LineType.MalePositive : LineType.MaleNegative;
        } else {
            type = satisfied ? LineType.FemalePositive : LineType.FemaleNegative;
        } AudioManager.Instance.PlaySFX(lineBank[type][Random.Range(0, lineBank[type].Length)], 0.1f);
    }
}
