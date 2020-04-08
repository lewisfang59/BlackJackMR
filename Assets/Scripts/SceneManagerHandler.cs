using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.SceneManagement;


public class SceneManagerHandler : MonoBehaviour
{
    public void toGameScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
