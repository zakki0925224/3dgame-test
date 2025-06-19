using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public UIDocument UIDocument;
    public GameObject PlayerObject;

    private Player Player;

    private Slider HPBar;
    private Slider SPBar;

    private void Start()
    {
        this.Player = this.PlayerObject.GetComponent<Player>();

        var root = this.UIDocument.rootVisualElement;
        this.HPBar = root.Q<Slider>("HPBar");
        this.SPBar = root.Q<Slider>("SPBar");
    }

    private void Update()
    {
        if (this.Player != null)
        {
            this.HPBar.value = (float)this.Player.PlayerStatus.HP;
            this.HPBar.highValue = (float)this.Player.PlayerStatus.MaxHP;

            this.SPBar.value = (float)this.Player.PlayerStatus.SP;
            this.SPBar.highValue = (float)this.Player.PlayerStatus.MaxSP;
        }
    }
}
