public static class Global
{
    private static int _level = 1, _maxLevel = 4;
    private static int _levelSize = 10;                  // Level Length = (levelSize) Screens
    private static int _hero = 1, _car = 1, _maxCar = 6;
    private static float _lengthX = 19.20f;              // 1920px

    public static float GroundPrefabSizeX { get => _lengthX; set => _lengthX = GroundPrefabSizeX; }
    public static int Level { get => _level; set => _level = Level; }
    public static int MaxLevel { get => _maxLevel; set => _maxLevel = MaxLevel; }
    public static int Hero { get => _hero; set => _hero = Hero; }
    public static int Car { get => _car; set => _car = Car; }
    public static int MaxCar { get => _maxCar; set => _maxCar = MaxCar; }
    public static int LevelSize { get => _levelSize; set => _levelSize = LevelSize; }
}