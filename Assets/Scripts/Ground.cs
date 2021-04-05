using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] GameObject groundPrefab, spikesPrefab;
    private GameObject ground, spikes;
    private Vector3 startPos = new Vector3(), 
                    spikesPos = new Vector3();

    private void Start()
    {
        startPos = transform.position;

        for (int i=0; i<Global.GetLevelSize(); i++)
        {
            startPos = new Vector3(startPos.x + Global.GetGroundPrefabSizeX(), startPos.y, startPos.z);
            ground = Instantiate(groundPrefab, startPos, transform.rotation);
            ground.transform.SetParent(transform);

            if (i % 10 == 0 && i > 0)
            {
                spikesPos = new Vector3(startPos.x, startPos.y + 0.7f, transform.position.z);
                spikes = Instantiate(spikesPrefab, spikesPos, transform.rotation);
                spikes.transform.SetParent(transform);
            }
        }
    }
}
