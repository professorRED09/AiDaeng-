using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InteractiveTutorial : MonoBehaviour
{
    [Header("UI Ref")]
    [SerializeField] TMP_Text _narrativeText;
    [SerializeField] TMP_Text _smallNarrativeText;
    [SerializeField] TMP_Text _clickToGoText;
    [SerializeField] Image _panel;
    //public string tutorial1Text;

    public NPC2 tutorialNPC;
    public MixturePanel mixturePanel;
    public PlayerController1 player;
    public ScoreManager scoreManager;

    public Items[] processItem;
        
    public TutorialState currentState;
    public IntroTutorialState introState = new IntroTutorialState();
    public NPCOrderTutorialState npcOrderState = new NPCOrderTutorialState();
    public MakeNarmObTutorialState makeNarmObState = new MakeNarmObTutorialState();
    public GiveNarmObTutorialState giveNarmObState = new GiveNarmObTutorialState();
    public OnNPCRageLeaveTutorialState onNPCRageLeaveState = new OnNPCRageLeaveTutorialState();
    public OnWrongNarmObTutorialState onWrongNarmObState = new OnWrongNarmObTutorialState();
    public OnRightNarmObTutorialState onRightNarmObState = new OnRightNarmObTutorialState();
    public GameRuleTutorialState gameRuleState = new GameRuleTutorialState();

    private int _introTextIndex = 0;

    [SerializeField] private GameObject[] _pointerArray;

    public static InteractiveTutorial instance;
    [SerializeField] private Timer _timer;
    public GameObject PlayAgainUIBox;
    public GameObject TryAgainUIBox;

    private Tween blinkTween;

    private void Awake()
    {
        instance = this;        
    }

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerController1>().enabled = false;
        _timer.unlimitedTimeCheck = true;
        Invoke(nameof(StartTutorial), 3.5f);

        ShowNarrativeText(false);
        ShowSmallNarrativeText(false);        
    }

    void StartTutorial()
    {
        tutorialNPC = FindObjectOfType<NPC2>();
        player = FindObjectOfType<PlayerController1>();

        tutorialNPC.orderTotalForTutorial = 1;

        currentState = introState;
        currentState.EnterState(this);        

        _smallNarrativeText.gameObject.SetActive(false);
        //_clickToGoText.DOFade(0f, 0.5f).OnComplete(() => _narrativeText.DOFade(1f, 0.5f));

        
    }

    public void BlinkingClickToGoText()
    {
        _clickToGoText.gameObject.SetActive(true);
        //// Create a sequence for the blinking effect
        blinkTween = DOTween.Sequence()
            .Append(_clickToGoText.DOFade(0f, 0.5f).SetEase(Ease.Linear)) // Fade out
            .Append(_clickToGoText.DOFade(1f, 0.5f).SetEase(Ease.Linear)) // Fade in
            .SetLoops(-1, LoopType.Restart).SetUpdate(true); ; // Loop indefinitely
        //blinkTween.timeScale = 1f;
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
            SetNarrativeText("ในด่านนี้ เราจะแนะนำวิธีการเล่นพื้นฐานให้กับผู้เล่น");
        }
        else if(_introTextIndex == 2)
        {
            SetNarrativeText("ผู้เล่นจะได้รับบทเป็นแดง ที่จะต้องคอยให้บริการน้ำอบสำหรับสรงน้ำพระพุทธรูปแก่ผู้คน");
        }
        else if (_introTextIndex == 3)
        {
            SetNarrativeText("อย่างในตอนนี้ ก็มีคนมาใช้บริการซุ้มของเราแล้ว มารอดูกันว่าเค้าต้องการน้ำอบแบบไหน");
        }
        else
        {
            ShowNarrativeText(false);
            Time.timeScale = 1f;
        }
    }

    public void ChangeNPCOrderText()
    {
        ++_introTextIndex;
        if (_introTextIndex == 1)
        {
            SetNarrativeText("ในด่านนี้ เราจะแนะนำวิธีการเล่นพื้นฐานให้กับผู้เล่น");
        }
        else if (_introTextIndex == 2)
        {
            SetNarrativeText("ผู้เล่นจะได้รับบทเป็นแดง ที่จะต้องคอยให้บริการน้ำอบสำหรับสรงน้ำพระพุทธรูปแก่ผู้คน");
        }
        else if (_introTextIndex == 3)
        {
            SetNarrativeText("อย่างในตอนนี้ ก็มีคนมาใช้บริการซุ้มของเราแล้ว มารอดูกันว่าเค้าต้องการน้ำอบแบบไหน");
        }
        else
        {
            ShowNarrativeText(false);
            Time.timeScale = 1f;
        }
    }    

    public void HideAllPointer()
    {
        for (int i = 0; i < _pointerArray.Length; i++)
        {
            _pointerArray[i].SetActive(false);
        }        
    }

    public void ShowPointer(int m_pointerIndex)
    {
        for(int i = 0; i < _pointerArray.Length; i++)
        {
            if(i == m_pointerIndex)
            {
                _pointerArray[i].SetActive(true);
            }
            else
            {
                _pointerArray[i].SetActive(false);
            }
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
        _smallNarrativeText.text = "- " + m_text + " -";
    }

    public void ShowNarrativeText(bool isOpen)
    {
        if (isOpen == true)
        {
            Time.timeScale = 0f;
            _panel.gameObject.SetActive(true);
            _panel.DOFade(0.7f, 0.5f).SetUpdate(true); 
            _narrativeText.gameObject.SetActive(true);

            BlinkingClickToGoText();
        }
        else
        {
            _panel.DOFade(0f, 0.5f).OnComplete(() => Time.timeScale = 1f).OnComplete(() => 
                    _panel.gameObject.SetActive(false));

            _narrativeText.gameObject.SetActive(false);
            _clickToGoText.gameObject.SetActive(false);
        }
    }

    public void ShowSmallNarrativeText(bool isOpen)
    {
        if (isOpen == true)
        {
            _smallNarrativeText.transform.DOLocalMoveY(277f, 0.5f);
            _smallNarrativeText.gameObject.SetActive(true);
        }
        else
        {
            _smallNarrativeText.gameObject.SetActive(false);
        }
    }

    public void ShowUIBox(GameObject m_UIBox)
    {
        m_UIBox.SetActive(true);
        m_UIBox.transform.GetChild(0).GetComponent<Image>().DOFade(0.7f, 0.5f).OnComplete(() =>
          m_UIBox.transform.GetChild(1).gameObject.transform.DOLocalMoveX(0, 1f));
    }

    //public void LetPlayerPlay()
    //{
    //    //_interactTutorialPage.SetActive(false);
    //    _smallNarrativeText.gameObject.SetActive(true);
    //    _interactTutorialPage.transform.GetChild(0).gameObject.SetActive(false);
    //    _interactTutorialPage.transform.GetChild(1).gameObject.SetActive(false);
    //}

    //public void InstructPlayer()
    //{
    //    //_interactTutorialPage.SetActive(true);
    //    _smallNarrativeText.gameObject.SetActive(false);
    //    _interactTutorialPage.transform.GetChild(0).gameObject.SetActive(true);
    //    _interactTutorialPage.transform.GetChild(1).gameObject.SetActive(true);
    //}
}
