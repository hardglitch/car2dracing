using UnityEngine;

public class MakeLevel : MonoBehaviour
{

    private void Awake()
    {
        //1. full black out
        for (int i = 0; i < 4; i++)
        {
            Transform levelName1 = transform.Find("Background Level " + (i + 1).ToString());
            if (levelName1 && levelName1.CompareTag("Background"))
                levelName1.gameObject.SetActive(false);

            Transform levelName2 = gameObject.transform.Find("Level " + (i + 1).ToString());
            if (levelName2 && levelName2.CompareTag("Background"))
                levelName2.gameObject.SetActive(false);
        }
    }


    private void Start()
    {
        //2. make background and foreground for level
        transform.Find("Background Level " + Global.GetLevel().ToString()).gameObject.SetActive(true);
        transform.Find("Ground Level " + Global.GetLevel().ToString()).gameObject.SetActive(true);
    }
}
