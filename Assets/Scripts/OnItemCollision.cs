using UnityEngine;

public class OnItemCollision : MonoBehaviour
{
    private CarScript carScript;

    public void SetCar(CarScript _carScript)
    { carScript = _carScript; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        carScript.OnItemCollisionStatic(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        carScript.OnItemCollisionDynamic(collision);
    }
}