using UnityEngine;

public class TriggerWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && GameManager.instance.keysCollected >= GameManager.instance.totalKeys)
        {
            GameManager.instance.WinGame();
            Debug.Log("Game won!");
        }
    }
}
