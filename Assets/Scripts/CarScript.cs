using UnityEngine;

public class CarScript : MonoBehaviour
{
    private Hud _objHud;
    private Player _player;
    private SfxManager _sfxManager;
    private Scoreboard _scoreBoard;

    public void SetHud(Hud hud)
    { _objHud = hud; }

    public void SetPlayer(Player player)
    { this._player = player; }

    public void SetSfxManager(SfxManager sfx)
    { _sfxManager = sfx; }

    public void SetScoreboard(Scoreboard scoreBoard)
    { this._scoreBoard = scoreBoard; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MaxX"))  // Finish
        {
            GetComponent<Competitor>().SetIsFinished(true);
            _scoreBoard.PushPlaceholderValue(gameObject.name);

            //Finish Scene
            if (gameObject.layer == LayerMask.NameToLayer("Player")) _objHud.Finish();
        }

        OnItemCollisionDynamic(collision);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnItemCollisionStatic(collision);
    }


    public void OnItemCollisionStatic(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            _player.SetCoins(+1);
            _objHud.ShowCoinsUI();
            _sfxManager.PlayCoinSfx();
        }

        if (!collision.gameObject.CompareTag("HP")) return;
        Destroy(collision.gameObject);
        _player.RecountHealth(+1);
    }


    public void OnItemCollisionDynamic(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            _player.SetCoins(+1);
            _objHud.ShowCoinsUI();
            _sfxManager.PlayCoinSfx();
        }

        if (!collision.gameObject.CompareTag("HP")) return;
        Destroy(collision.gameObject);
        _player.RecountHealth(+1);
    }
}
