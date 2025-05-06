using System.Collections.Generic;
using UnityEngine;
using Flower;
using TMPro;
using System.Linq;

[System.Serializable]
public struct PlotInfo
{
    public string name;
    public int type;//0 common; 1 task start; 2 task process; 3 task end
}
public class NPCPlotManager : MonoBehaviour
{
    private NPCInterface npc;
    #region ChatBox
    [Header("ChatBox")]
    [SerializeField] private GameObject hint;
    [SerializeField] private GameObject chat;
    #endregion
    [SerializeField] public List<PlotInfo> plotInfo;
    [SerializeField] protected List<GameObject> task;
    public Task currentTask;
    protected FlowerSystem plot;

    protected TextMeshProUGUI text;

    private bool isPlayerDetected;
    private bool isPlayerInteract;

    public int currentPlotIndex;
    protected string plotFilePath = "";

    public bool isTaskAssigned;
    protected virtual void Awake()
    {
        npc = GetComponent<NPCInterface>();
        text = chat.GetComponentInChildren<TextMeshProUGUI>();

        isPlayerDetected = false;
        isPlayerInteract = false;
        isTaskAssigned = false;

        currentPlotIndex = 0;
    }

    protected virtual void Start()
    {
        hint.SetActive(false);
        chat.SetActive(false);

    }

    protected virtual void Update()
    {
        NPCInteraction();
    }

    private void NPCInteraction()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);

        isPlayerDetected = colliders.Any(hit => hit.GetComponent<Player>());

        if (!isPlayerInteract)
            hint.SetActive(isPlayerDetected);
        else
            hint.SetActive(false);

        if (isPlayerDetected && Input.GetKeyDown(KeyCode.T))
        {
            isPlayerInteract = true;
            chat.SetActive(isPlayerDetected);
            if(!NPCManager.instance.isPeace)
                NPCManager.instance.isPeace = true;
            NPCTalk();
        }
        else if (!isPlayerDetected)
        {
            chat.SetActive(false);
            if (NPCManager.instance.isPeace)
                NPCManager.instance.isPeace = false;
            isPlayerInteract = false;
        }

        text.SetText(plot.text);
    }

    protected virtual void NPCTalk()
    {
        if (!plot.isStop)
            plot.Next();
        else
        {
            UpdatePlotLogic();

            plot.ReadTextFromResource(plotFilePath + plotInfo[currentPlotIndex].name);
            plot.Resume();
            plot.Next();
        }
    }

    protected void InstantiateTaskPrefab(int _index)
    {
        GameObject taskGO = Instantiate(this.task[_index], ItemManager.instance.itemTransform);
        currentTask = taskGO.GetComponent<Task>();
        currentTask.client = this;
    }

    private void UpdatePlotLogic()
    {
        if (currentPlotIndex < plotInfo.Count - 1)
        {
            switch (plotInfo[currentPlotIndex].type)
            {
                case 0:
                    currentPlotIndex++;
                    break;
                case 1:
                    currentPlotIndex++;
                    isTaskAssigned = true;
                    break;
                case 2:
                    if (!isTaskAssigned)
                        currentPlotIndex++;
                    break;
                case 3:
                    currentPlotIndex++;
                    break;
            }
        }
    }
}