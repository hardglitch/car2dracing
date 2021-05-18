//This must be on "CarPrefab" (on Car and on every Wheel) in Unity

using UnityEngine;

public class Collisions : MonoBehaviour
{
    internal Car CarObj { get; set; }
    internal Hud HudObj { get; set; }
    internal Scoreboard ScoreboardObj { get; set; }
    internal SfxManager SfxManagerObj { get; set; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MaxX"))  // Finish
        {
            ScoreboardObj.PushPlaceholderValue(gameObject.name);

            //Finish Scene
            if (gameObject.layer == LayerMask.NameToLayer("Player") && !CarObj.IsFinished)
            {
                CarObj.IsFinished = true;
                HudObj.Finish();
            }
        }

        OnItemCollision(collision);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnItemCollision(collision);
    }


    private void OnItemCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            CarObj.Coins += 1;
            if (!CarObj.CompetitorMode) HudObj.ShowCoinsUI();
            if (SfxManagerObj != null) SfxManagerObj.PlayCoinSfx();
        }

        if (!collision.gameObject.CompareTag($"HP")) return;
        Destroy(collision.gameObject);
        if (CarObj != null) CarObj.RecountHealth(+1);
    }
    
    private void OnItemCollision(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            CarObj.Coins += 1;
            if (!CarObj.CompetitorMode) HudObj.ShowCoinsUI();
            SfxManagerObj.PlayCoinSfx();
        }

        if (!collision.gameObject.CompareTag($"HP")) return;
        Destroy(collision.gameObject);
        CarObj.RecountHealth(+1);
    }

}
