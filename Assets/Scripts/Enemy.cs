using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private Color damagedColor;
    [SerializeField] private float timeToNormal = 1f;
    private SpriteRenderer sprite;
    private bool isColorChanged;
    private Color normalColor;
    private Coroutine lastReturnToNormalCoroutine;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        normalColor = sprite.color;
    }

    public void TakeDamage()
    {
        Debug.Log("Damage Taken");

        if (isColorChanged)
        {
            StopCoroutine(lastReturnToNormalCoroutine);
        }

        sprite.color = damagedColor;
        isColorChanged = true;

        lastReturnToNormalCoroutine = StartCoroutine(ReturnToNormal());
    }

    IEnumerator ReturnToNormal()
    {
        yield return new WaitForSeconds(timeToNormal);
        sprite.color = normalColor;
        isColorChanged = false;
    }
}
