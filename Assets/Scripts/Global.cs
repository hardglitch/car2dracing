public static class Global
{
    private static int _level = 5,
                       _maxLevel = 7,
                       _hero = 1,
                       _car = 1,
                       _maxCar = 6;
    
    private static float _lengthX = 19.20f;              // 1920px
    public static int LevelSize => 100;

    public static float GroundPrefabSizeX
    {
        get => _lengthX;
        set
        {
            value = UnityEngine.Mathf.Abs(value);
            if (value > 100) value = 100;
            _lengthX = value;
        }
    }

    public static int Level
    {
        get => _level;
        set
        {
            value = UnityEngine.Mathf.Abs(value);
            if (value > 100) value = 100;
            _level = value;
        }
    }

    public static int MaxLevel
    {
        get => _maxLevel;
        set
        {
            value = UnityEngine.Mathf.Abs(value);
            if (value > 100) value = 100;
            _maxLevel = value;
        }
    }

    public static int Hero
    {
        get => _hero;
        set
        {
            value = UnityEngine.Mathf.Abs(value);
            if (value > 100) value = 100;
            _hero = value;
        }
    }

    public static int Car
    {
        get => _car;
        set
        {
            value = UnityEngine.Mathf.Abs(value);
            if (value > 100) value = 100;
            _car = value;
        }
    }

    public static int MaxCar
    {
        get => _maxCar;
        set
        {
            value = UnityEngine.Mathf.Abs(value);
            if (value > 100) value = 100;
            _maxCar = value;
        }
    }
}