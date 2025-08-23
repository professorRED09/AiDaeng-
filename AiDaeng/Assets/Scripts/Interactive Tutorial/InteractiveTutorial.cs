using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractiveTutorial : MonoBehaviour
{
    [Header("Intro")]
    [SerializeField] TMP_Text _narrativeText;
    [SerializeField] TMP_Text _smallNarrativeText;
    [SerializeField] GameObject _interactTutorialPage;
    public string tutorial1Text;

    public NPC2 tutorialNPC;
    public MixturePanel mixturePanel;
    private PlayerController1 player;

    public Items[] processItem;
        
    public TutorialState currentState;
    public IntroTutorialState introState = new IntroTutorialState();
    public NPCOrderTutorialState npcOrderState = new NPCOrderTutorialState();
    public MakeNarmObTutorialState makeNarmObState = new MakeNarmObTutorialState();
    public GiveNarmObTutorialState giveNarmObState = new GiveNarmObTutorialState();

    private int _introTextIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(StartTutorial), 3.5f);
    }

    void StartTutorial()
    {
        currentState = introState;
        currentState.EnterState(this);

        tutorialNPC = FindObjectOfType<NPC2>();
        player = FindObjectOfType<PlayerController1>();

        _smallNarrativeText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }        
    }

    public void ChangeIntroText()
    {
        ++_introTextIndex;
        if(_introTextIndex == 1)
        {
            SetNarrativeText("JUST GIVE ME THE REASON");
        }
        else if(_introTextIndex == 2)
        {
            SetNarrativeText("TO GIVE MY HEART BEATING");
        }
        else
        {
            LetPlayerPlay();
            Time.timeScale = 1f;
        }
    }

    public void SwitchState(TutorialState m_state)
    {
        currentState = m_state;
        currentState.EnterState(this);
    }

    public void SetNarrativeText(string m_text)
    {
        _narrativeText.text = m_text; 
    }

    public void SetSmallNarrativeText(string m_text)
    {
        _smallNarrativeText.text = m_text;
    }

    public void LetPlayerPlay()
    {
        //_interactTutorialPage.SetActive(false);
        _smallNarrativeText.gameObject.SetActive(true);
        _interactTutorialPage.transform.GetChild(0).gameObject.SetActive(false);
        _interactTutorialPage.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void InstructPlayer()
    {
        //_interactTutorialPage.SetActive(true);
        _smallNarrativeText.gameObject.SetActive(false);
        _interactTutorialPage.transform.GetChild(0).gameObject.SetActive(true);
        _interactTutorialPage.transform.GetChild(1).gameObject.SetActive(true);
    }
}
