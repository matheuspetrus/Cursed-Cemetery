using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CursedCemetery.Scripts.Systens
{
    public class LevelLoader : MonoBehaviour
    {
        [Header("Objects")] [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Slider slider;
        [SerializeField] private Text _progressText;
    
        public void LoadLevel(int sceneIndex)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
        }
    
        // loading screen system
        private IEnumerator LoadAsynchronously(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);

                slider.value = progress;
                _progressText.text = progress * 100 + "%";

                yield return null;
            }
        }
    }
}
