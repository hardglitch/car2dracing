using System;
using UnityEngine;

public class CoinBot : MonoBehaviour
{
    [Serializable]
    public class DropObject
    {
        [SerializeField] private GameObject go;
        [SerializeField] private float dropInterval;
        private float _counter;

        public GameObject GetGO() { return go; }
        public float GetDropInterval() { return dropInterval; }
        public float GetCounter() { return _counter; }
        public void SetCounter(float counter) { _counter = counter; }
    }


    [SerializeField] private float speed = 5;
    private readonly Vector2 _startPoint = new Vector2(-12, 4);

    [SerializeField] private Transform dropSource;
    [SerializeField] private DropObject[] dropItems;

    private const float _minDropX = 0;
    private float _maxDropX;
    private bool _onCollision;
    private int _createdCoins;


    private void Start()
    {
        transform.position = new Vector3(_startPoint.x, _startPoint.y, transform.position.z);
        _maxDropX = Global.GroundPrefabSizeX * (Global.LevelSize - 1);
    }


    private void Update()
    {
        // airplane flying
        transform.Translate(Vector3.right * (speed * Time.deltaTime));

        // dropping
        if (!(transform.position.x >= _minDropX) || !(transform.position.x <= _maxDropX) || _onCollision) return;
        foreach (var t in dropItems)
        {
            t.SetCounter(t.GetCounter() + Time.deltaTime);

            if (!(t.GetCounter() >= t.GetDropInterval())) continue;
            dropSource.transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            Instantiate(t.GetGO(), dropSource.transform.position, transform.rotation);
            t.SetCounter(0);

            if (t.GetGO().name == "Coin") _createdCoins++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _onCollision = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _onCollision = false;
    }

    public int GetCreatedCoins()
    {
        return _createdCoins;
    }
}
