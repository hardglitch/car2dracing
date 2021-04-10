using UnityEngine;

public class AnimObjBehaviour : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject[] animObjects;

    [Range(11, 20)]
    [SerializeField] private float animDistance = 12;

    private float _cam2ObjDistance;
    private Animator[] _anim;

    private void Start()
    {
        _anim = new Animator[animObjects.Length];

        for (var i=0; i<animObjects.Length; i++)
        {
            _anim[i] = animObjects[i].GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < animObjects.Length; i++)
        {
            _cam2ObjDistance = Mathf.Abs(cam.transform.position.x - _anim[i].transform.position.x);

            if (_cam2ObjDistance <= animDistance)
            {
                if (!_anim[i].GetBool("Action"))
                    _anim[i].SetBool("Action", true);
            }
            else
            {
                if (_anim[i].GetBool("Action"))
                    _anim[i].SetBool("Action", false);
            }
        }
    }
}
