using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractable))]
public class CoinInteractable : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<XRBaseInteractable>().selectEntered.AddListener(OnSelected);
    }

    private void OnDisable()
    {
        GetComponent<XRBaseInteractable>().selectEntered.RemoveListener(OnSelected);
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        Coin.coinCount++;
        GameManager.Instance.CollectCoin();
        Destroy(gameObject);
    }
}
