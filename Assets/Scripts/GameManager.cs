using HapaMagic;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int playerHealth = 20;
    private int playerEggs;
    private int difficulty = 5;
    public int eggCost;
    public int eggDecayValue = 5;
    public int eggIncrementValue = 5;
    public int eggDecayTime;
    public TMP_Text eggText;

    public OptionsManager OptionsManager { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public DeckManager DeckManager { get; private set; }
    public HandManager handManager { get; private set; }

    public bool PlayingCard = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"Setting Instance to {this}");
            InitializeManagers();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        StartCoroutine(DecayEggCost());
    }

    private void InitializeManagers()
    {
        OptionsManager = GetComponentInChildren<OptionsManager>();
        AudioManager = GetComponentInChildren<AudioManager>();
        DeckManager = GetComponentInChildren<DeckManager>();

        if (OptionsManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
            if (prefab == null)
            {
                Debug.Log($"OptionsManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                OptionsManager = GetComponentInChildren<OptionsManager>();
            }
        }
        if (AudioManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            if (prefab == null)
            {
                Debug.Log($"AudioManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                AudioManager = GetComponentInChildren<AudioManager>();
            }
        }
        if (DeckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager");
            if (prefab == null)
            {
                Debug.Log($"DeckManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                DeckManager = GetComponentInChildren<DeckManager>();
            }
        }
    }

    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    public int PlayerEggs
    {
        get { return playerEggs; }
        set { playerEggs = value; }
    }

    public int Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }
    public IEnumerator DecayEggCost()
    {
        while (true)
        {
            yield return new WaitForSeconds(eggDecayTime);
            if (eggCost > 10)
            {
                eggCost -= eggDecayValue;
                UpdateEggCostNum();
            }
        }
    }

    private void IncreaseEggCost()
    {
        eggCost += eggIncrementValue;
        UpdateEggCostNum();
    }

    public void UpdateEggCostNum()
    {
        eggText.text = eggCost.ToString();
    }

    public bool PayToDraw()
    {
        if (eggCost > playerEggs && DeckManager.DrawCard())
        {
            playerEggs -= eggCost;
            IncreaseEggCost();
            return true;
        } else
        {
            Debug.Log("Can't afford it!!");
            return false;
        }
    }
}
