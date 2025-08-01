using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public static ExitDoor Instance;

    public GameObject door;          // The door GameObject to animate/open
    public GameObject lockGlow;      // Visual effect for the lock

    private bool unlocked = false;

    void Awake()
    {
        // Setup singleton instance
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UnlockDoor()
    {
        if (unlocked) return;  // prevent unlocking multiple times

        unlocked = true;

        // Example: disable lock glow and open door animation
        if (lockGlow != null)
            lockGlow.SetActive(false);

        // You can add animation or change door state here
        if (door != null)
        {
            
            if (col != null) col.enabled = false;

            // Or move the door upwards as a simple open effect
            door.transform.Translate(Vector3.up * 3f);
        }

        Debug.Log("[ExitDoor] Door unlocked!");
    }
}
