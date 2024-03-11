using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpawnManager : MonoBehaviour
{
    #region variables
    Queue<GameObject> queue;
    [SerializeField]
    public GameObject[] prefab;
    [SerializeField]
    private Transform basePlayer;
    [SerializeField]
    private Transform baseEnemy;
    int i = 0;
    [SerializeField]
    private Image loadingBar;
    bool spawning = false;
    [SerializeField]
    private Image[] loadingIcons;
    private Player _player;

#endregion
    private void Start()
    {
        queue = new Queue<GameObject>();
        _player = GameObject.Find("GameManagers").GetComponent<Player>();

    }

    private IEnumerator waitForSpawn(GameObject character)
    {
        spawning = true;
        float wait = character.GetComponent<Character>().data.spawnTime;
        for(int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(wait / 4);
            loadingBar.fillAmount += 0.25f;
        }
        GameObject go = Instantiate(character, basePlayer.position - new Vector3(-8, 5, 0), transform.rotation * Quaternion.Euler(0f, 90f, 0f));
        go.tag = "Player";
        go.GetComponent<Character>().tagToHit = "enemy";
        go.GetComponent<Character>().layerToIgnore = "Player";
        yield return new WaitForSeconds(wait / 4);
       
        loadingBar.fillAmount = 0;
        spawning = false;
        CheckQueue();
    }
    public void AddQueue(int i)
    {
        if (prefab[i].GetComponent<Character>().data.goldToBuy > _player.Gold) return;
        _player.DecreaseGold(prefab[i]);
        if (queue.Count < 5)
        {
            this.i = i;
            queue.Enqueue(prefab[i]);
            int j = queue.Count;
            j = Mathf.Clamp(j, 0, 5);
            loadingIcons[j - 1].color = Color.red;
            CheckQueue();
        }
        
    }
    private void CheckQueue()
    {
        if (queue.Count > 0 && spawning == false)
        {
           // int j = queue.Count;

            GameObject character = queue.Dequeue();
            loadingIcons[queue.Count].color = Color.white;
            StartCoroutine(waitForSpawn(character));
        }
    }
   
}
