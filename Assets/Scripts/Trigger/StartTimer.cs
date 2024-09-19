using UnityEngine;

public class StartTimer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.StartTimer();
                Debug.Log("Timer started for Player.");
            }
        }
    }
}
