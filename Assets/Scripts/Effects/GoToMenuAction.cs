using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Effects
{
    class GoToMenuAction : OnFreezeEndAction
    {
        public void Act()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
