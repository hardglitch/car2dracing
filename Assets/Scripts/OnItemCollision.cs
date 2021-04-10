using UnityEngine;

public class OnItemCollision : MonoBehaviour
{
    private CarScript _carScript;

    public void SetCar(CarScript carScript)
    { this._carScript = carScript; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _carScript.OnItemCollisionStatic(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _carScript.OnItemCollisionDynamic(collision);
    }
}