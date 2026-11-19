
using UnityEngine;
using TMPro;

public class InventoryHUD : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private TMP_Text booksText;
    [SerializeField] private TMP_Text keysText;

    private void OnEnable()
    {
        if (inventory == null)
            return;

        inventory.InventoryChanged += UpdateCounters;
        UpdateCounters();
    }

    private void OnDisable()
    {
        if (inventory != null)
            inventory.InventoryChanged -= UpdateCounters;
    }

    private void UpdateCounters()
    {
        if (inventory == null ||
            booksText == null ||
            keysText == null)
            return;

        booksText.text =
            $"Libros: {inventory.Books}/{inventory.RequiredBooks}";

        keysText.text =
            $"Llaves: {inventory.Keys}/{inventory.RequiredKeys}";
    }
}

