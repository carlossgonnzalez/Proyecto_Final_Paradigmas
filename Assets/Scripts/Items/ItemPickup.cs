
using UnityEngine;

public enum CollectibleType
{
    Book,
    Key
}

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private CollectibleType itemType;

    public void Interact(GameObject interactor)
    {
        Debug.Log("Intentando recoger: " + gameObject.name);
        if (!interactor.TryGetComponent(
            out PlayerInventory inventory))
        {
            return;
        }

        if (itemType == CollectibleType.Book)
            inventory.AddBook();
        else
            inventory.AddKey();

        gameObject.SetActive(false);
    }
}
