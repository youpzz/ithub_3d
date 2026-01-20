using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChanger : MonoBehaviour
{
    public static EnemyChanger Instance;

    [Header("Имена")]

    public string[] names;



    [Space(10)]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int amountToSpawn = 5;

    [SerializeField] private Vector3 spawnArea = new Vector3(10, 0, 10);
    [Range(0.5f, 2.5f)]
    [SerializeField] private float maxCubeScale;
    [Range(0.5f, 1.5f)]
    [SerializeField] private float minCubeScale;

    [SerializeField] private float Spacing = 3f;
    [Space(5)]
    [SerializeField] private GameObject[] cubes;

    [Space(15)]
    [Header("Система Врагов")]

    private Enemy[] enemies;

    [Header("UI")]

    [SerializeField] private Button healthButton;
    [SerializeField] private Button levelButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button bossButton;

    [Space(5)]
    [SerializeField] private TMP_InputField inputField;

    private string inputText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnCubes();
        HandleButtons();
    }


    void HandleButtons()
    {
        inputField.onValueChanged.AddListener(GetText);

        resetButton.onClick.AddListener(Reset);
        bossButton.onClick.AddListener(MakeBoss);
        levelButton.onClick.AddListener(ShowLevelEnemies);
        healthButton.onClick.AddListener(ShowHealthEnemies);
    }


    void SpawnCubes()
    {
        cubes = new GameObject[amountToSpawn];
        enemies = new Enemy[amountToSpawn];
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), spawnArea.y, Random.Range(-spawnArea.z, spawnArea.z));
            float scale = Random.Range(minCubeScale, maxCubeScale);
            GameObject spawnedCube = Instantiate(cubePrefab, randomPosition, Quaternion.identity);
            spawnedCube.transform.localScale = Vector3.one * scale;
            cubes[i] = spawnedCube;
            enemies[i] = spawnedCube.GetComponent<Enemy>();
            Debug.Log($"Создан куб с размером {scale}");
        }
        ShuffleCubes();

    }

    void ShuffleCubes()
    {
        for (int i = cubes.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = cubes[i];
            cubes[i] = cubes[j];
            cubes[j] = temp;
        }

        Vector3 startPos = new Vector3(-10, spawnArea.y, 0);
        for (int i = 0; i < cubes.Length; i++)
        {
            Vector3 position = startPos + new Vector3(i * Spacing, 0, 0);
            cubes[i].transform.position = position;
            Debug.Log($"Куб {i} перемещён в {position}");
        }
    }



    //

    void ShowHealthEnemies()
    {
        float healthValue = float.Parse(inputText);
        for (int i = 0; i < enemies.Length; i++) if (enemies[i].GetHealth() <= healthValue) enemies[i].gameObject.SetActive(false);
    }

    void ShowLevelEnemies()
    {
        int level = int.Parse(inputText);
        for (int i = 0; i < enemies.Length; i++) if (enemies[i].GetLevel() != level) enemies[i].gameObject.SetActive(false);
    }

    void MakeBoss()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetName() == inputText)
            {
                enemies[i].ChangeLevel('+', 1);
                enemies[i].ChangeHealth('*', 3);
                Debug.Log($"{enemies[i].gameObject} | {enemies[i].GetName()} -> boss | Здоровье: {enemies[i].GetHealth()} , Уровень: {enemies[i].GetLevel()}");
                enemies[i].ChangeName("boss");
            }
        }
    }

    void Reset()
    {
        for (int i = 0; i < enemies.Length; i++) { enemies[i].gameObject.SetActive(true); enemies[i].Reset(); }
    }


    void GetText(string text) => inputText = text;
}
