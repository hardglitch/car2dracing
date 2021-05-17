using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private GameObject groundPrefab, spikesPrefab;
    private GameObject _ground, _spikes;
    private Vector3 _startPos, 
                    _spikesPos;

    private void Start()
    {
        _startPos = transform.position;
        Global.GroundPrefabSizeX = groundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;


        for (var i=0; i<Global.LevelSize; i++)
        {
            _startPos = new Vector3(_startPos.x + Global.GroundPrefabSizeX, _startPos.y, _startPos.z);
            _ground = Instantiate(groundPrefab, _startPos, transform.rotation);
            _ground.transform.SetParent(transform);

            if (i % 10 != 0 || i <= 0) continue;
            _spikesPos = new Vector3(_startPos.x, _startPos.y + 0.6f, transform.position.z + 1);
            _spikes = Instantiate(spikesPrefab, _spikesPos, transform.rotation);
            _spikes.transform.SetParent(transform);
        }
    }
}
