using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{ //collect the key and play sound
    public void Interact()
    {
        GameManager.instance.CollectKey();
        AudioManager.Instance.PlaySFX("key");
        Debug.Log("Key collected");
        Destroy(gameObject);
    }
}
