using UnityEngine;

public class AnimObjBehaviour : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject[] animObjects;

    [Range(11, 20)]
    [SerializeField] private float animDistance = 12;

    private float cam2objDistance;
    private Animator[] anim;

    private void Start()
    {
        anim = new Animator[animObjects.Length];

        for (int i=0; i<animObjects.Length; i++)
        {
            anim[i] = animObjects[i].GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < animObjects.Length; i++)
        {
            cam2objDistance = Mathf.Abs(cam.transform.position.x - anim[i].transform.position.x);

            if (cam2objDistance <= animDistance)
            {
                if (!anim[i].GetBool("Action"))
                    anim[i].SetBool("Action", true);
            }
            else
            {
                if (anim[i].GetBool("Action"))
                    anim[i].SetBool("Action", false);
            }
        }
    }
}
