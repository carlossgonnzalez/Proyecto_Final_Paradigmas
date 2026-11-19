using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interactor)
    {
        Debug.Log(interactor.name + " ha interactuado con " + gameObject.name);

        // Para esta prueba, el objeto desaparece al interactuar
        gameObject.SetActive(false);
    }
}
