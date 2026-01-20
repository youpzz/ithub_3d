using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string enemyName = "";
    [SerializeField] private int health = 10;
    [SerializeField] private int level = 1;



    private int startHealth;
    private int startLevel;
    private string startName;


    void Awake() => SaveDefaults();

    void SaveDefaults()
    {
        RandomizeStats();
        startHealth = health;
        startLevel = level;
        startName = enemyName;
    }

    void RandomizeStats()
    {
        enemyName = EnemyChanger.Instance.names[Random.Range(0, EnemyChanger.Instance.names.Length)];
        health = Random.Range(1, 26);
        level = Random.Range(1, 11);
    }

    


    public float GetHealth() => health;
    public float GetLevel() => level;
    public string GetName() => enemyName;

    public void ChangeHealth(char type, int value)
    {
        switch (type)
        {
            case '+': health += value; break;
            case '-': health -= value; break;
            case '*': health *= value; break;
            case '/': health /= value; break;
        }
    }

    public void ChangeLevel(char type, int value)
    {
        switch (type)
        {
            case '+': level += value; break;
            case '-': level -= value; break;
            case '*': level *= value; break;
            case '/': level /= value; break;
        }
    }

    public void Reset()
    {
        health = startHealth;
        level = startLevel;
        enemyName = startName;
    }


    public void ChangeName(string text) => enemyName = text;
}
