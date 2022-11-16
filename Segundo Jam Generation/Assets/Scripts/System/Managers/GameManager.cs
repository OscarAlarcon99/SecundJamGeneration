using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    [Header("Setup")]
    public float currentTime;
    public float oneDayTime;
    public Slider time;
    public bool isNight;
    int index;
    private void Start()
    {
        time = ScenesManager.Instance?.panelUI.GetComponentInChildren<Slider>();
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        Player.Instance.isActive = false;
        ScenesManager.Instance.panelRuler.SetActive(true);
        yield return new WaitUntil(() => !ScenesManager.Instance.panelRuler.activeInHierarchy);
        Player.Instance.isActive = true;
        ScenesManager.Instance.panelUI.SetActive(true);
        Cursor.visible = false;
        StartCoroutine(CountUp());
       
    }

    public IEnumerator Winner()
    {
        Cursor.visible = true;
        SoundManager.Instance.PauseAllSounds(true);
        SoundManager.Instance.PlayNewSound("Winner");
        Player.Instance.isActive = false;
        yield return new WaitForSeconds(5f);
        ScenesManager.Instance.panelWinner.SetActive(true);
        yield return new WaitUntil(() => !ScenesManager.Instance.panelWinner.activeInHierarchy);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Boot");
    }
    public IEnumerator Losser()
    {
        Cursor.visible = true;
        SoundManager.Instance.PauseAllSounds(true);
        SoundManager.Instance.PlayNewSound("Losser");
        Player.Instance.isActive = false;
        yield return new WaitForSeconds(5f);
        ScenesManager.Instance.panelLosser.SetActive(true);
        yield return new WaitUntil(() => !ScenesManager.Instance.panelLosser.activeInHierarchy);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Boot");

    }

    // Update is called once per frame
    void Update()
    {
        if (isNight)
        {
            if (index == 0)
            {
                StartCoroutine(CountDown());
            }

            if (index == 1)
            {
                StartCoroutine(CountUp());
            }

            isNight = false;
        }

        if (Input.GetKeyDown(KeyCode.P) && Player.Instance.isActive || Input.GetKeyDown(KeyCode.Escape) && Player.Instance.isActive)
        {
            ScenesManager.Instance?.Pause();
        }
    }
    private IEnumerator CountDown()
    {
        while (currentTime > 0)
        {
            if (time != null)
            {
                time.value = currentTime;
            }
            
            currentTime -= Time.deltaTime;
            yield return new WaitForSecondsRealtime(1f);
            ChangePostProcessingProgressive();
        }
        index = 1;
        isNight = true;
    }

    private IEnumerator CountUp()
    {

        while (currentTime < oneDayTime)
        {
            if (time != null)
            {
                time.value = currentTime;
            }
            currentTime += Time.deltaTime;
           
            yield return new WaitForSecondsRealtime(1f);
            ChangePostProcessingProgressive();
        }

        index = 0;
        isNight = true;
    }

    public void ChangePostProcessingProgressive()
    {
        if (index == 0)
        {
          
            if (currentTime > oneDayTime/2 )
            {
                Debug.Log("Atardecer");
                ChangePostProcessingProgressive();
                return;
            }

            if (currentTime > oneDayTime / 3)
            {
                Debug.Log("Es de noche");
                ChangePostProcessingProgressive();
                return;
            }
        }

        if (index == 1)
        {

            if (currentTime < oneDayTime / 3)
            {
                Debug.Log("no se que es");
                ChangePostProcessingProgressive();
                return;
            }
            
            if (currentTime < oneDayTime / 2)
            {
                ChangePostProcessingProgressive();
                return;
            }
        }
    }
}
