using UnityEngine;

public class CarScript : MonoBehaviour
{
    private HUD objHUD;
    private Player player;
    private SFXManager sfxManager;
    private Scoreboard scoreBoard;

    public void SetHUD(HUD _hud)
    { objHUD = _hud; }

    public void SetPlayer(Player _player)
    { player = _player; }

    public void SetSfxManager(SFXManager _sfx)
    { sfxManager = _sfx; }

    public void SetScoreboard(Scoreboard _scoreBoard)
    { scoreBoard = _scoreBoard; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MaxX"))  // Finish
        {
            GetComponent<Competitor>().SetIsFinished(true);
            scoreBoard.PushPlaceholderValue(gameObject.name);
            //Finish Scene
        }

        OnItemCollisionDynamic(collision);
    }


    public void OnItemCollisionStatic(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            player.SetCoins(+1);
            objHUD.ShowCoinsUI();
            sfxManager.PlayCoinSFX();
        }

        if (collision.gameObject.CompareTag("HP"))
        {
            Destroy(collision.gameObject);
            player.RecountHealth(+1);
        }
    }


    public void OnItemCollisionDynamic(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            objHUD.ShowCoinsUI();
            player.SetCoins(+1);
            sfxManager.PlayCoinSFX();
        }

        if (collision.gameObject.CompareTag("HP"))
        {
            Destroy(collision.gameObject);
            player.RecountHealth(+1);
        }
    }
}
