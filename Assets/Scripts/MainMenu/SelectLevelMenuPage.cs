using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.MainMenu
{
    public class SelectLevelMenuPage : BaseMainMenuPage
    {
        public string LevelSceneNameFormat;

        public void SelectLevel(int levelNumber)
        {
            if (LevelSceneNameFormat != null && LevelSceneNameFormat.Length > 0)
            {
                SceneManager.LoadScene(string.Format(LevelSceneNameFormat, levelNumber));
            }
        }
	}
}
