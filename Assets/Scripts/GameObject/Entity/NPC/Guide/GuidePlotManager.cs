using Flower;
using UnityEngine;

public class GuidePlotManager : NPCPlotManager
{
    [SerializeField] private GameObject logoDisplayer;

    protected override void Awake()
    {
        base.Awake();
        plotFilePath = "Plot/Guide/";
    }

    protected override void Start()
    {
        base.Start();
        plot = FlowerManager.Instance.CreateFlowerSystem("guide", false);
        plot.ReadTextFromResource(plotFilePath + plotInfo[currentPlotIndex].name);
    }

    protected override void Update()
    {
        base.Update();
        GuideTasksAssign();
        IntroPlotRewardLogic();
    }

    protected override void NPCTalk()
    {
        base.NPCTalk();
    }

    private void GuideTasksAssign()
    {
        if (plot.isStop)
        {
            switch (currentPlotIndex)
            {
                case 1:
                    if (!isTaskAssigned)
                    {
                        InstantiateTaskPrefab(0);
                        isTaskAssigned = true;
                        if (currentPlotIndex < plotInfo.Count - 1)
                            currentPlotIndex++;
                    }
                    break;
                case 4:
                    if (!isTaskAssigned)
                    {
                        InstantiateTaskPrefab(1);
                        isTaskAssigned = true;
                    }
                    break;
                case 7:
                    if (MotorManager.instance.motor.anim.GetBool("Move"))
                    {
                        logoDisplayer.SetActive(true);

                        if (currentPlotIndex < plotInfo.Count - 1)
                        {
                            currentPlotIndex++;
                            isTaskAssigned = false;
                        }
                    }else
                        isTaskAssigned = true;
                    break;
                default:
                    break;
            }
        }
    }

    private void IntroPlotRewardLogic()
    {
        if (currentPlotIndex < 7)
            MotorManager.instance.motorGO.SetActive(false);
        else
            if (MotorManager.instance.motorGO.activeSelf == false)
            MotorManager.instance.motorGO.SetActive(true);
    }
}
