using UnityEngine;

public class TowerUpgradeUI : MonoBehaviour
{
    public GameObject upgradePanel;
    public TMPro.TextMeshProUGUI towerInfoText;
    public TMPro.TextMeshProUGUI upgradeCostText;
    public GameObject towerPlacementPanel;
    public BaseInteraction[] baseScripts = new BaseInteraction[3];

    private Tower selectedTower;

    public void OpenUpgradeMenu(Tower tower)
    {
        selectedTower = tower;

        Debug.Log($"Opening upgrade menu for tower: {tower.name}");

        UpdateUI();

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);
            towerPlacementPanel.SetActive(false);
            for (int i = 0; i < baseScripts.Length; i++)
            {
                baseScripts[i].DeHighlightBase();
            }
        }
        else
        {
            Debug.LogError("Upgrade panel is not assigned.");
        }
    }

    public void CloseUpgradeMenu()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }

    public void UpgradeTower()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeTower();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (selectedTower != null)
        {
            towerInfoText.text = $"Level: {selectedTower.Level}\nRange: {selectedTower.range}\nFire Rate: {selectedTower.fireRate}";
            upgradeCostText.text = $"Upgrade Cost: {selectedTower.UpgradeCost} Gold";
        }
    }
}
