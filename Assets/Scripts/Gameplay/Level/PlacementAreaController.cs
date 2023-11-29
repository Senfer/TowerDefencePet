using Assets.Scripts.Gameplay.States;
using UnityEngine;

public class PlacementAreaController : MonoBehaviour
{
    private bool _isHighlighted;

    public Color ActiveColor;
    public Color PlacingColor;
    public Color NormalColor;
    public Color OccupiedColor;

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
                AreaMesh.material.color = PlacingColor;
                if (Input.GetMouseButtonUp(0) && GameUIController.InstanceExists)
                {
                    OccupyPlacement();
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
        Instantiate(towerInstance, transform);
        AreaMesh.material.color = OccupiedColor;
        IsOccupied = true;
        _isHighlighted = false;
    }
}
