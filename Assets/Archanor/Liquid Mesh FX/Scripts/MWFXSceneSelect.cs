using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MWFXSceneSelect : MonoBehaviour
{
    public void LoadSceneMWFXFlowing()
    {
        SceneManager.LoadScene("MWFXFlowing");
    }
    public void LoadSceneMWFXMissiles()
    {
        SceneManager.LoadScene("MWFXMissiles");
    }
    public void LoadSceneMWFXExplosions()
    {
        SceneManager.LoadScene("MWFXExplosions");
    }
    public void LoadSceneMWFXSprays()
    {
        SceneManager.LoadScene("MWFXSprays");
    }
    public void LoadSceneMWFXDirectional()
    {
        SceneManager.LoadScene("MWFXDirectional");
    }
}