using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    [Header("Setup")]
    public float currentTime;
    public float maxTime;
    public Slider time;
    public bool reverse;
    public bool canChange;
    public float speed;
    public bool isActive;
    private void Start()
    {
        Player.Instance.isActive = false;
        time = ScenesManager.Instance?.panelUI.GetComponentInChildren<Slider>();
        StartCoroutine(StartGame());
    }
    public IEnumerator StartGame()
    {
        ScenesManager.Instance.panelRuler.SetActive(true);
        yield return new WaitUntil(() => !ScenesManager.Instance.panelRuler.activeInHierarchy);
        ScenesManager.Instance.panelInformative.SetActive(true);
        yield return new WaitForSeconds(2f);
        ScenesManager.Instance.panelInformative.SetActive(false);
        isActive = true;
        Player.Instance.isActive = true;
        ScenesManager.Instance.panelUI.SetActive(true);
        ScenesManager.Instance.playerHealt.SetActive(true);
        Cursor.visible = false;
        StartCoroutine(CountDay());
    }

    public IEnumerator Winner()
    {
        ScenesManager.Instance.panelUI.SetActive(false);
        ScenesManager.Instance.playerHealt.SetActive(false);
        Cursor.visible = true;
        isActive = false;
        SoundManager.Instance.PauseAllSounds(true);
        SoundManager.Instance.PlayNewSound("Winner");
        Player.Instance.isActive = false;
        yield return new WaitForSeconds(5f);
        ScenesManager.Instance.panelWinner.SetActive(true);
        yield return new WaitUntil(() => !ScenesManager.Instance.panelWinner.activeInHierarchy);
        ScenesManager.Instance.RestartMainMenu();
    }
    public IEnumerator Losser()
    {
        ScenesManager.Instance.panelUI.SetActive(false);
        ScenesManager.Instance.playerHealt.SetActive(false); Cursor.visible = true;
        isActive = false;
        SoundManager.Instance.PauseAllSounds(true);
        SoundManager.Instance.PlayNewSound("Losser");
        Player.Instance.isActive = false;
        yield return new WaitForSeconds(5f);
        ScenesManager.Instance.panelLosser.SetActive(true);
        yield return new WaitUntil(() => !ScenesManager.Instance.panelLosser.activeInHierarchy);
        ScenesManager.Instance.RestartMainMenu();
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;

        if (Input.GetKeyDown(KeyCode.P) && Player.Instance.isActive || Input.GetKeyDown(KeyCode.Escape) && Player.Instance.isActive)
        {
            ScenesManager.Instance?.Pause();
        }

        if (canChange)
        {
            currentTime = 0;

            if (time != null)
            {
                time.value = currentTime;
            }

            Player.Instance.vFXController.ChangePostProccesing();
            StartCoroutine(CountDay());
            canChange = false;
        }
    }

    public IEnumerator CountDay()
    {
        while (currentTime < maxTime && isActive)
        {
            if (time != null)
            {
                time.value = currentTime;
            }

            currentTime += speed * Time.deltaTime;
            yield return new WaitForSecondsRealtime(1f);
        }
        
        canChange = true;
    }
}
