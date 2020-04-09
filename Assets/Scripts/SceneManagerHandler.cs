using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.SceneManagement;


public class SceneManagerHandler : MonoBehaviour
{
    /// <summary>
    /// method for generating the scene
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void toGameScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
