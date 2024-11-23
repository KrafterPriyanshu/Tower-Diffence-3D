using UnityEngine;
using UnityEngine.UI;

public class BaseInteraction : MonoBehaviour
{
    public GameObject towerPrefab;
    public bool isOccupied = false;

    public GameObject towerPlacementPanel; // Panel for placing towers
    public Button placeTowerButton;

    public Material highlightMaterial;
    private Material originalMaterial;

    private Renderer baseRenderer;

    public static BaseInteraction currentlySelectedBase;

    void Start()
    {
        baseRenderer = GetComponent<Renderer>();
        originalMaterial = baseRenderer.material;

        if (placeTowerButton != null)
        {
            placeTowerButton.onClick.RemoveAllListeners();
            placeTowerButton.onClick.AddListener(() =>
            {
                currentlySelectedBase?.PlaceTower();
            });
        }
    }

    private void OnMouseDown()
    {
        if (isOccupied)
            return;

        // Close the upgrade panel if it's open
        TowerUpgradeUI upgradeUI = FindObjectOfType<TowerUpgradeUI>();
        if (upgradeUI != null)
        {
            upgradeUI.CloseUpgradeMenu();
        }

        // Handle base selection and ensure only one base is selected at a time
        if (currentlySelectedBase != null && currentlySelectedBase != this)
        {
            currentlySelectedBase.DeHighlightBase();
        }

        HighlightBase();
        currentlySelectedBase = this;

        // Open the placement panel
        if (towerPlacementPanel != null)
        {
            towerPlacementPanel.SetActive(true);
        }
    }

    public void PlaceTower()
    {
        if (towerPrefab != null && GameManager.Instance.gold >= 50)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            GameManager.Instance.UpdateGold(-50);
            isOccupied = true;

            DeHighlightBase();
            CloseTowerPlacementPanel();
        }
        else
        {
            Debug.Log("Not enough gold or towerPrefab is null.");
        }
    }

    public void HighlightBase()
    {
        if (highlightMaterial != null)
        {
            baseRenderer.material = highlightMaterial;
        }
    }

    public void DeHighlightBase()
    {
        if (baseRenderer != null && originalMaterial != null)
        {
            baseRenderer.material = originalMaterial;
        }

        if (towerPlacementPanel != null)
        {
            towerPlacementPanel.SetActive(false);
        }

        if (BaseInteraction.currentlySelectedBase == this)
        {
            BaseInteraction.currentlySelectedBase = null;
        }
    }

    void CloseTowerPlacementPanel()
    {
        if (towerPlacementPanel != null)
        {
            towerPlacementPanel.SetActive(false);
        }
    }
}
