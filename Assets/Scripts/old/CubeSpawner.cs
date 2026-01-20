using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int amountToSpawn = 5;

    [SerializeField] private Vector3 spawnArea = new Vector3(10, 0, 10);
    [Range(0.5f, 2.5f)]
    [SerializeField] private float maxCubeScale;
    [Range(0.5f, 1.5f)]
    [SerializeField] private float minCubeScale;

    [SerializeField] private float sortSpacing = 3f;
    [Space(5)]
    [SerializeField] private GameObject[] cubes;


    void Start() => SpawnCubes();

    void Update() {if (Input.GetKeyDown(KeyCode.F)) BubbleSortCubes();
}

    void SpawnCubes()
    {
        cubes = new GameObject[amountToSpawn];
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), spawnArea.y, Random.Range(-spawnArea.z, spawnArea.z));
            float scale = Random.Range(minCubeScale, maxCubeScale);
            GameObject spawnedCube = Instantiate(cubePrefab, randomPosition, Quaternion.identity);
            spawnedCube.transform.localScale = Vector3.one * scale;
            cubes[i] = spawnedCube;
            Debug.Log($"Создан куб с размером {scale}");
        }
        
    }

    void BubbleSortCubes()
    {
        Debug.Log("Сортиров-очка");
        for (int i = 0; i < cubes.Length - 1; i++)
        {
            for (int a = 0; a < cubes.Length - i - 1; a++)
            {
                float fisrtSize = cubes[a].transform.localScale.x;
                float secondSize = cubes[a + 1].transform.localScale.x;

                if (fisrtSize < secondSize)
                {
                    GameObject cube_ = cubes[a];
                    cubes[a] = cubes[a + 1];
                    cubes[a + 1] = cube_;
                }
            }
        }

        Vector3 startPos = new Vector3(-10, spawnArea.y, 0);
        for (int i = 0; i < cubes.Length; i++)
        {
            Vector3 position = startPos + new Vector3(i * sortSpacing, 0, 0);
            cubes[i].transform.position = position;
            Debug.Log($"Куб {i} перемещён в {position}");
        }
    }
}
