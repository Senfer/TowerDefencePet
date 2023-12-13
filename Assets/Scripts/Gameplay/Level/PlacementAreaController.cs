using Assets.Scripts.Gameplay.States;
using UnityEngine;

public class PlacementAreaController : MonoBehaviour
{
    private bool _isHighlighted;

    public Color ActiveColor;
    public Color PlacingColor;
    public Color NormalColor;
    public Color OccupiedColor;
    public Color PlacingInvalidColor;

    public MeshRenderer AreaMesh;

    public bool IsOccupied;
    public bool ReadyForPlacement;

    public void Start()
    {
        if (GameUIController.InstanceExists)
        {
            GameplayManager.Instance.TowerSelected += OnTowerSelected;
            GameplayManager.Instance.TowerSelectionEnded += OnTowerSelectionEnded;
        }
    }

    public void HighlightPlacement()
    {
        if (!_isHighlighted && !IsOccupied)
        {
            AreaMesh.material.color = ActiveColor;
            _isHighlighted = true;
        }
    }

    public void DeactivatePlacementHighlight()
    {
        if (_isHighlighted && !IsOccupied)
        {
            AreaMesh.material.color = NormalColor;
            _isHighlighted = false;
        }
    }

    private void OnMouseOver()
    {
        if (GameplayManager.InstanceExists && GameplayManager.Instance.CurrentState == GameplayState.Building)
        {
            if (GameplayManager.Instance.SelectedTower != null && !IsOccupied)
            {
                if (IsPlacingValid(GameplayManager.Instance.SelectedTower))
                {
                    AreaMesh.material.color = PlacingColor;
                    if (Input.GetMouseButtonUp(0) && GameUIController.InstanceExists)
                    {
                        OccupyPlacement();
                    }
                }
                else
                {
                    AreaMesh.material.color = PlacingInvalidColor;
                }
            }
        }
    }

    private void OnMouseExit()
    {
        if (GameplayManager.InstanceExists && GameplayManager.Instance.CurrentState == GameplayState.Building)
        {
            if (GameUIController.InstanceExists && !IsOccupied)
            {
                if (GameplayManager.Instance.SelectedTower != null)
                {
                    AreaMesh.material.color = ActiveColor;
                }
            }
        }
    }

    private void OnTowerSelected()
    {
        HighlightPlacement();
    }

    private void OnTowerSelectionEnded()
    {
        DeactivatePlacementHighlight();
    }

    private void OccupyPlacement()
    {
        var towerInstance = GameplayManager.Instance.SelectedTower;
        var towerCost = towerInstance.GetComponent<Launcher>().towerData.cost;
        Instantiate(towerInstance, transform);
        AreaMesh.material.color = OccupiedColor;
        IsOccupied = true;
        _isHighlighted = false;
        GameplayManager.Instance.WalletManager.DecreaseCurrency(towerCost);
    }

    private bool IsPlacingValid(GameObject tower)
    {
        return GameplayManager.Instance.WalletManager.CurrencyAmount > tower.GetComponent<Launcher>().towerData.cost;
    }
}
