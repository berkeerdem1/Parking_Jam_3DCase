using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string sceneName;
    public GameObject ConfettiNextLevel;
    public Transform ConfettiPos;
    private bool confettiCreated = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("car");
        if (cars.Length == 0)
        {
            StartCoroutine(wait());
        }
    }
    public void SceneNext(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    IEnumerator wait()
    {
        if (!confettiCreated)
        {
            Instantiate(ConfettiNextLevel, ConfettiPos.transform.position, Quaternion.identity);
            confettiCreated = true;
        }
        yield return new WaitForSeconds(3f);
        SceneNext(sceneName);
    }
}
