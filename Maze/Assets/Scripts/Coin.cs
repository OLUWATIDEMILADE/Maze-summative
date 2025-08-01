using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider), typeof(Renderer), typeof(XRBaseInteractable))]
public class Coin : MonoBehaviour
{
    public static int coinCount = 0;
    public static int totalCoins = 12;

    public AudioClip coinSound;

    public float spinSpeed = 45f;
    public Color glowColor = Color.yellow;
    public float glowIntensity = 2f;

    private Renderer coinRenderer;

    void Start()
    {
        coinRenderer = GetComponent<Renderer>();
        EnableGlow();

        // Hook into XR interaction event
        GetComponent<XRBaseInteractable>().selectEntered.AddListener(OnCoinSelected);
    }

    void Update()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.World);
    }

    private void OnCoinSelected(SelectEnterEventArgs args)
    {
        coinCount++;
        Debug.Log($"[COIN] Collected: {coinCount}/{totalCoins}");

        if (coinSound)
            AudioSource.PlayClipAtPoint(coinSound, transform.position);

        if (GameManager.Instance)
            GameManager.Instance.CollectCoin();

        Destroy(gameObject);
    }

    void EnableGlow()
    {
        if (coinRenderer != null && coinRenderer.material.HasProperty("_EmissionColor"))
        {
            coinRenderer.material.EnableKeyword("_EMISSION");
            coinRenderer.material.SetColor("_EmissionColor", glowColor * glowIntensity);
        }
    }
}
