
using System.Collections.Generic;
using UnityEngine;

public class WorldItemSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bookPrefab;
    [SerializeField] private GameObject keyPrefab;

    [Header("Libros - Zona de entrada")]
    [SerializeField]
    private Vector3[] entranceBookPositions =
    {
        new Vector3(-7f, 1.2f, -7f),
        new Vector3(-7f, 1.2f, -2f),
        new Vector3(2f, 1.2f, -5f),
        new Vector3(7f, 1.2f, -2f),
        new Vector3(0f, 1.2f, -4f)
    };

    [Header("Libros - Zona interior")]
    [SerializeField]
    private Vector3[] innerBookPositions =
    {
        new Vector3(-7f, 1.2f, 7f),
        new Vector3(-2f, 1.2f, 3f),
        new Vector3(2f, 1.2f, 7f),
        new Vector3(7f, 1.2f, 3f),
        new Vector3(0f, 1.2f, 5f)
    };

    [Header("Llaves - Zona de entrada")]
    [SerializeField]
    private Vector3[] entranceKeyPositions =
    {
        new Vector3(7f, 1.2f, -8f),
        new Vector3(-6f, 1.2f, -4f),
        new Vector3(2f, 1.2f, -2f)
    };

    [Header("Llaves - Zona interior")]
    [SerializeField]
    private Vector3[] innerKeyPositions =
    {
        new Vector3(-7f, 1.2f, 4f),
        new Vector3(7f, 1.2f, 8f),
        new Vector3(0f, 1.2f, 8f)
    };

    private void Start()
    {
        // Generar dos libros antes de la puerta
        SpawnRandom(bookPrefab, entranceBookPositions, 2);

        // Generar dos libros despues de la puerta
        SpawnRandom(bookPrefab, innerBookPositions, 2);

        // Una llave accesible antes de la puerta
        SpawnRandom(keyPrefab, entranceKeyPositions, 1);

        // Otra llave en la zona interior
        SpawnRandom(keyPrefab, innerKeyPositions, 1);
    }

    private void SpawnRandom(
        GameObject prefab,
        Vector3[] positions,
        int amount)
    {
        if (prefab == null || positions == null ||
            positions.Length < amount)
        {
            Debug.LogError(
                "WorldItemSpawner: faltan prefabs o posiciones."
            );
            return;
        }

        // Copiar posiciones disponibles
        List<Vector3> availablePositions =
            new List<Vector3>(positions);

        for (int i = 0; i < amount; i++)
        {
            // Elegir una posicion aleatoria
            int randomIndex = Random.Range(
                0,
                availablePositions.Count
            );

            Vector3 selectedPosition =
                availablePositions[randomIndex];

            // Crear el objeto en esa posicion
            Instantiate(
                prefab,
                selectedPosition,
                Quaternion.identity,
                transform
            );

            // Evitar repetir la misma posicion
            availablePositions.RemoveAt(randomIndex);
        }
    }
}
