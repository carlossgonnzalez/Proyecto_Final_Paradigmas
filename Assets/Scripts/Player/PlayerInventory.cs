
using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Objetivos")]
    [SerializeField] private int requiredBooks = 4;
    [SerializeField] private int requiredKeys = 2;

    public int Books { get; private set; }
    public int Keys { get; private set; }

    public int RequiredBooks => requiredBooks;
    public int RequiredKeys => requiredKeys;

    public event Action InventoryChanged;

    public void AddBook()
    {
        Books++;
        InventoryChanged?.Invoke();
    }

    public void AddKey()
    {
        Keys++;
        InventoryChanged?.Invoke();
    }
}
