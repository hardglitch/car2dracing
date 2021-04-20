using UnityEngine;

public class MakeLevel : MonoBehaviour
{
    private void Awake()
    {
        //1. full black out
        for (var i = 0; i < Global.MaxLevel; i++)
        {
            var levelName1 = transform.Find("Background Level " + (i + 1));
            if (levelName1 && levelName1.CompareTag("Background"))
                levelName1.gameObject.SetActive(false);

            var levelName2 = gameObject.transform.Find("Ground Level " + (i + 1));
            if (levelName2 && levelName2.CompareTag("Ground"))
                levelName2.gameObject.SetActive(false);
        }
    }


    private void Start()
    {
        //2. make background and foreground for level
        var bgLevel = transform.Find("Background Level " + Global.Level);
        if (bgLevel) bgLevel.gameObject.SetActive(true);

        var gLevel = transform.Find("Ground Level " + Global.Level);
        if (gLevel) gLevel.gameObject.SetActive(true);
    }
}
