using System;
using UnityEngine;

public class CoinBot : MonoBehaviour
{
    [Serializable]
    public class DropObject
    {
        [SerializeField] private GameObject _GO;
        [SerializeField] private float _dropInterval;
        private float _counter = 0;

        public DropObject() { }
        public GameObject GetGO() { return _GO; }
        public float GetDropInterval() { return _dropInterval; }
        public float GetCounter() { return _counter; }
        public void SetCounter(float cntr) { _counter = cntr; }
    }


    [SerializeField] private float speed = 5;
    private Vector2 startPoint = new Vector2(-12, 4);

    [SerializeField] private Transform dropSource;
    [SerializeField] private DropObject[] dropItems;

    private float minDropX = 0, maxDropX = 0;
    private bool onCollission = false;
    private int createdCoins = 0;


    private void Start()
    {
        transform.position = new Vector3(startPoint.x, startPoint.y, transform.position.z);
        maxDropX = Global.GetGroundPrefabSizeX() * (Global.GetLevelSize() - 1);
    }


    void Update()
    {
        // airplan flying
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // droping
        if (transform.position.x >= minDropX && transform.position.x <= maxDropX && !onCollission)
        {
            for (int i=0; i<dropItems.Length; i++)
            {
                dropItems[i].SetCounter(dropItems[i].GetCounter() + Time.deltaTime);

                if (dropItems[i].GetCounter() >= dropItems[i].GetDropInterval())
                {
                    dropSource.transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                    Instantiate(dropItems[i].GetGO(), dropSource.transform.position, transform.rotation);
                    dropItems[i].SetCounter(0);

                    if (dropItems[i].GetGO().name == "Coin") createdCoins++;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onCollission = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onCollission = false;
    }

    public int GetCreatedCoins()
    {
        return createdCoins;
    }
}
