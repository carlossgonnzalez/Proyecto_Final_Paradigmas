
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;

    [Header("Jugador")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private PlayerInteraction playerInteraction;

    private enum GameState
    {
        Playing,
        Won,
        Lost
    }

    private GameState currentState = GameState.Playing;

    private void Awake()
    {
        // Ocultar los paneles al comenzar
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        if (defeatPanel != null)
            defeatPanel.SetActive(false);
    }

    private void OnEnable()
    {
        // Escuchar el evento de muerte
        if (playerHealth != null)
        {
            playerHealth.Died += Lose;
        }
    }

    private void OnDisable()
    {
        // Cancelar la suscripcion al evento
        if (playerHealth != null)
        {
            playerHealth.Died -= Lose;
        }
    }

    public void Win()
    {
        if (currentState != GameState.Playing)
            return;

        currentState = GameState.Won;

        Debug.Log("¡HAS ESCAPADO!");

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        EndGame();
    }

    private void Lose()
    {
        if (currentState != GameState.Playing)
            return;

        currentState = GameState.Lost;

        Debug.Log("GAME OVER - El jugador ha muerto.");

        if (defeatPanel != null)
            defeatPanel.SetActive(true);

        EndGame();
    }

    private void EndGame()
    {
        // Desactivar los controles del jugador
        if (playerMovement != null)
            playerMovement.enabled = false;

        if (playerCamera != null)
            playerCamera.enabled = false;

        if (playerInteraction != null)
            playerInteraction.enabled = false;

        // Liberar el cursor para usar los botones
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}
