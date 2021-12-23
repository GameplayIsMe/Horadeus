using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI inst;

    public TextMeshProUGUI arrowCountText;

    [SerializeField]
    private Image crosshairImage;
    [SerializeField]
    private Image CantShoot;
    [SerializeField]
    private Image DebugMenu;

    public void Init() {
        inst = this;
    }

    public void UpdatePlayerInventoryHUD()
    {
        ItemData arrowItem = Game.inst.player.inventory.GetItem(ItemType.Arrow);
        arrowCountText.text = "Arrows: " + arrowItem.count;
    }

    public void EnableCrosshair(bool enable)
    {
        crosshairImage.enabled = enable;
    }

    public void EnableCantShoot(bool enable)
    {
        CantShoot.enabled = enable;
    }

    public void EnableDebugMenu(bool enable)
    {
        DebugMenu.enabled = enable;
    }
}
