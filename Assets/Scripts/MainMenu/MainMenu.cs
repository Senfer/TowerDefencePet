using Assets.Scripts.MainMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private IMenuPage _currentPage;

    public BaseMainMenuPage MainMenuPage;
    public SelectLevelMenuPage SelectLevelMenuPage;

    private void Awake()
    {
        ChangePage(MainMenuPage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowLevelSelectMenu()
    {
        ChangePage(SelectLevelMenuPage);
    }

    public void ShowMainMenu()
    {
        ChangePage(MainMenuPage);
    }

    private void ChangePage(IMenuPage destination)
    {
        HideCurrentPage();
        ActivateNewPage(destination);
    }

    private void HideCurrentPage()
    {
        if (_currentPage != null)
        {
            _currentPage.Hide();
        }
    }

    private void ActivateNewPage(IMenuPage newPage)
    {
        _currentPage = newPage;
        _currentPage.Show();
    }
}
