using UnityEngine;

class Global : MonoBehaviour
{
    [SerializeField] GameObject groundPrefab;
    private static float lengthX = 0;

    private static int level = 1, maxLevel = 4;
    private static int levelSize = 50;  // Level Length = (levelSize) Screens
    private static int hero = 1, car = 1, maxCar = 6;

    private void Awake()
    {
        lengthX = groundPrefab.transform.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public static float GetGroundPrefabSizeX()
    {
        return lengthX;
    }
    public static void SetLevel(int lvl)
    { level = lvl; }

    public static int GetLevel()
    { return level; }

    public static int GetMaxLevel()
    { return maxLevel; }

    public static void SetMaxLevel(int lvl)
    { maxLevel = lvl; }

    public static void SetHero(int hro)
    { hero = hro; }

    public static int GetHero()
    { return hero; }

    public static void SetCar(int cr)
    { car = cr; }

    public static int GetCar()
    { return car; }

    public static void SetMaxCar(int cr)
    { maxCar = cr; }

    public static int GetMaxCar()
    { return maxCar; }

    public static int GetLevelSize()
    { return levelSize; }

    public static void SetLevelSize(int size)
    { levelSize = size; }
}