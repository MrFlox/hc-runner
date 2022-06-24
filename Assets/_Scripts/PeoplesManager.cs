using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System;
using UnityEngine.EventSystems;

public class PeoplesManager : MonoBehaviour
{
    #region Serialized Params
    [SerializeField] List<RunnerController> guys = new List<RunnerController>();
    [SerializeField] RunnersCounter rCounter;
    [SerializeField] ActiveModificatorLabel activeModifierText;
    [SerializeField] float runSpeed = 30;
    [SerializeField] ParticleSystem particlePrefab;
    [SerializeField] GameObject centralPosition;
    [SerializeField] private float[] xOffsets;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float yUsersOffset = -3.17f;
    [Header("Central Force")]
    [SerializeField] float centeralForce = 100;
    [SerializeField] private GameObject camTarget;
    #endregion

    #region Private variables
    bool endGame;
    SkinnedMeshRenderer prefabMeshRenderer;
    Rect runnersBound = new();
    Vector3 centralGroupPoint;
    Rigidbody body;
    TestIntegrator ti;
    float lastTime = -1f;
    int activeRunners = 0;
    Vector3[] circlesPositions;
    int lastPeopleCount = 0;
    #endregion

    public RoadSide roadSide;
    public static bool DRAW_BOUDNS = true;
    public static PeoplesManager instance;

    #region Public Methods
    public void ApplyGates(Gate gate)
    {
        switch (gate.type)
        {
            case GatesType.Addition:
                setPeopleCount(activeRunners + gate.value);
                break;
            case GatesType.Substraction:
                int newValue = activeRunners - gate.value;
                if (newValue < 0) newValue = 0;
                setPeopleCount(newValue);
                break;
            case GatesType.Multiply:
                setPeopleCount(activeRunners * gate.value);
                break;
            case GatesType.Division:
                setPeopleCount(activeRunners / gate.value);
                break;
        }
        showActiveModifierText(gate.ToString());
    }
    public void remove(GameObject gameObject)
    {
        Vector3 pos = gameObject.transform.position;
        pos.y += prefabMeshRenderer.bounds.size.y;
        gameObject.SetActive(false);
        lastPeopleCount = getVisibleActiveGuysCount();
        rCounter.setCount(lastPeopleCount);
        ParticleSystem p = Instantiate(particlePrefab);
        p.transform.parent = transform;
        p.transform.position = pos;
        p.Play();
    }

    public void addPeople(int peopleCount)
    {
        int newRunnersCount = activeRunners;
        activeRunners += peopleCount;
        for (int i = newRunnersCount; i <= activeRunners; i++)
        {
            if (i < guys.Count)
            {
                RunnerController r = guys[i].GetComponent<RunnerController>();
                r.gameObject.SetActive(true);
            }
        }
        rCounter.setCount(activeRunners);
    }

    public void addPeopleFrom0(int newRunnersCount)
    {
        for (int i = 0; i < newRunnersCount; i++)
        {
            if (i < guys.Count)
            {
                RunnerController r = guys[i].GetComponent<RunnerController>();
                r.gameObject.SetActive(true);
                GameObject g = r.gameObject;
                Vector3 targetPos = g.transform.localPosition;
                Vector3 finalScale = g.transform.localScale;
                g.transform.localPosition = Vector3.zero;
                iTween.MoveTo(g, iTween.Hash(
                    "position", targetPos,
                    "islocal", true,
                    "time", 1f));
            }
        }
        rCounter.setCount(newRunnersCount);
    }

    public void newLevel(GameObject gameObject)
    {
        LevelManager.Instance.activeLevelIndex++;
        saveProgress();
        reloadLevel();
    }

    #endregion

    #region Private Methods
    void saveProgress()
    {
        PlayerPrefs.SetInt("ActiveLevel", LevelManager.Instance.activeLevelIndex);
    }
    //Обнулить местоположение объектов
    void reloadLevel()
    {
        transform.position = Vector3.zero;
        setPeopleCount(1);
        GatesManager.Instance.showAll();
        GameManager.Instance.UpdateGameState(GameState.StartMenu);
    }

    void showActiveModifierText(string value)
    {
        activeModifierText.gameObject.SetActive(true);
        activeModifierText.flyWithText(value);
    }

    void setPeopleCount(int v)
    {
        foreach (RunnerController r in guys)
        {
            r.gameObject.SetActive(false);
        }
        activeRunners = 0;

        addPeople(v);
    }

    int getVisibleActiveGuysCount()
    {
        int result = 0;

        for (int i = 0; i < guys.Count; i++)
        {
            if (guys[i].gameObject.activeSelf) result++;
        }

        return result;
    }

    private void Awake()
    {
        Time.timeScale = 1;
        instance = this;
        PlayerPrefs.SetInt("ActiveLevel", 0);
        LevelManager.Instance.setLevel(PlayerPrefs.GetInt("ActiveLevel"));

        GameManager.OnGameStateChanged += onChangeState;

        body = GetComponent<Rigidbody>();

        prefabMeshRenderer = prefab.GetComponentInChildren<SkinnedMeshRenderer>();
        float yOffset = prefabMeshRenderer.bounds.size.y / 2;

        GameObject[] objs = AddObjectsCircles(transform, prefab);

        for (int i = 0; i < objs.Length; i++)
        {
            var r = objs[i].GetComponent<RunnerController>();
            guys.Add(r);
            r.gameObject.SetActive(false);
            setYOffsetPosition(prefabMeshRenderer, objs[i], yOffset);

        }
        xOffsets = new float[guys.Count];
        int counter = guys.Count;
        for (var i = 0; i < xOffsets.Length; i++)
        {
            xOffsets[i] = guys[i].transform.position.x;
            guys[i].transform.position = new Vector3(guys[i].transform.position.x, yUsersOffset, guys[i].transform.position.z);
        }

        addPeople(1);
        lastPeopleCount = activeRunners;
    }

    private void onChangeState(GameState state)
    {
        if (state == GameState.GamePlay)
        {
            rCounter.gameObject.SetActive(true);
        }
        if (state == GameState.EndGame)
        {

        }
    }

    private static void setYOffsetPosition(SkinnedMeshRenderer mr, GameObject obj, float yOffset)
    {
        Transform subObj = obj.transform.GetChild(0);
        Vector3 lpos = subObj.localPosition;
        lpos.y = yOffset;
        subObj.localPosition = lpos;
    }

    GameObject[] AddObjectsHex(Transform transform, GameObject prefab, float objectSizeMultiplicatorOffset)
        => ti.AddObjects(89, transform, prefab, objectSizeMultiplicatorOffset);

    GameObject[] AddObjectsCircles(Transform transform, GameObject prefab)
    {
        List<GameObject> result = new List<GameObject>();
        circlesPositions = Utils.generateRadialPositions(Vector3.zero, 7);

        for (int i = 0; i < 80; i++)
        {
            var inst = Instantiate(prefab);
            inst.transform.parent = transform;
            inst.transform.localPosition = circlesPositions[i] * .8f;
            result.Add(inst);
        }

        return result.ToArray();
    }

    void getCentralPoint()
    {
        int usersOnStage = getVisibleActiveGuysCount();
        float[] _x = new float[usersOnStage];
        float[] _y = new float[usersOnStage];

        int counter = 0;
        for (var i = 0; i < guys.Count; i++)
        {
            GameObject g = guys[i].gameObject;
            if (g.activeSelf)
            {
                _x[counter] = g.transform.position.x;
                _y[counter] = g.transform.position.z;
                counter++;
            }

        }

        runnersBound.Set(_x.Min(), _y.Min(), _x.Max() - _x.Min(), _y.Max() - _y.Min());
        centralGroupPoint.Set(runnersBound.xMin + runnersBound.width / 2, -0.51f * 2, runnersBound.yMin + runnersBound.height / 2);

        camTarget.transform.position = transform.position;
    }

    private void OnDrawGizmos()
    {
        if (DRAW_BOUDNS)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 4.0f);
            Gizmos.DrawWireCube(transform.position, new Vector3(runnersBound.width, 10, runnersBound.height));
        }
    }

    bool isItTimeForRearrangement()
    {
        float timeDiff = Time.time - lastTime;

        if (timeDiff > 2.0f)
        {
            // Debug.Log(timeDiff);
            lastTime = Time.time;
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameState.GamePlay) return;

        bool pointerOverUI = EventSystem.current.IsPointerOverGameObject();
        if (pointerOverUI) return;
        getCentralPoint();
        applyCentralForce();
        if (!RunnerInputController.instance.touch) return;

        Vector3 tp = RunnerInputController.instance.targetPoint;
        float newPosX = tp.x;
        if (newPosX < RunnerInputController.instance.minX)
            newPosX = RunnerInputController.instance.minX;
        if (newPosX + runnersBound.width > RunnerInputController.instance.maxX)
            newPosX = RunnerInputController.instance.maxX - runnersBound.width;

        tp.x = newPosX + runnersBound.width / 2;

        Vector3 newPoint = tp;

        newPoint.z = transform.position.z + 30;
        Vector3 direction = newPoint - transform.position;
        direction = direction.normalized;
        transform.Translate(direction * Time.deltaTime * runSpeed);

        float cp = RunnerInputController.instance.minX + (RunnerInputController.instance.maxX - RunnerInputController.instance.minX) / 2;

        roadSide = transform.position.x >= cp ? RoadSide.Right : RoadSide.Left;
    }

    private void applyCentralForce()
    {
        foreach (RunnerController g in guys)
        {
            if (g.gameObject.activeSelf)
            {
                Vector3 target = centralPosition.transform.position - g.transform.position;
                g.GetComponent<Rigidbody>().AddForce(target.normalized * Time.deltaTime * centeralForce);
            }
        }
    }
    #endregion
}

public enum RoadSide
{
    Left,
    Right
}