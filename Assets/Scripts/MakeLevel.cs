using UnityEngine;

public class MakeLevel : MonoBehaviour
{
    private void Awake()
    {
        //1. full black out
        for (var i = 0; i < 4; i++)
        {
            var levelName1 = transform.Find("Background Level " + (i + 1).ToString());
            if (levelName1 && levelName1.CompareTag("Background"))
                levelName1.gameObject.SetActive(false);

            var levelName2 = gameObject.transform.Find("Level " + (i + 1).ToString());
            if (levelName2 && levelName2.CompareTag("Background"))
                levelName2.gameObject.SetActive(false);
        }
    }


    private void Start()
    {
        //2. make background and foreground for level
        transform.Find("Background Level " + Global.Level.ToString()).gameObject.SetActive(true);
        transform.Find("Ground Level " + Global.Level.ToString()).gameObject.SetActive(true);
    }
}
