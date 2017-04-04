using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Effects
{
    class GoToSceneAction : OnFreezeEndAction
    {
        public string scene;
        public void Act()
        {
            SceneManager.LoadScene(scene);
        }
    }
}
