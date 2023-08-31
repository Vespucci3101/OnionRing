using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BattlePhase : MonoBehaviour
{
    GameObject neutralCirclesUI;
    ShowHideUI perfectUI;
    ShowHideUI goodUI;
    ShowHideUI badUI;
    GameObject playerScoreUI;
    GameObject attackPhaseUI;
    GameObject attackDescriptionUI;
    GameObject defencePhaseUI;
    GameObject enemyUI;
    GameObject playerUI;
    GameObject battlePhaseUI;
    GameObject battlePhase;
    GameObject endingScreen;
    GameObject startScreen;

    EventSystem eventSystem;

    public TMP_Text endingScreenText;

    public TMP_Text[] attackButtonsText;

    public ButtonsCreator buttonsCreator;

    public PlayerScore playerScore;

    public TMP_Text enemyName;

    public Slider enemyHealthSlider;
    public Slider playerHealthSlider;

    public TMP_Text enemyHealthDisplay;
    public TMP_Text playerHealthDisplay;

    public bool isInBattlePhase = false;
    public bool isPlayingSong = true;

    public Image enemyAvatar;

    PlayerStats playerStats;

    Player player;
    Enemy enemy;

    float timeElapsed;
    float lerpDuration = 50f;

    Image enemySliderFillAreaImage;
    Image playerSliderFillAreaImage;

    bool isBattleEnded = false;

    void Start()
    {
        neutralCirclesUI = GameObject.Find("NeutralUICircles");
        perfectUI = GameObject.Find("PerfectUI").transform.GetChild(0).GetComponent<ShowHideUI>();
        goodUI = GameObject.Find("GoodUI").transform.GetChild(0).GetComponent<ShowHideUI>();
        badUI = GameObject.Find("BadUI").transform.GetChild(0).GetComponent<ShowHideUI>();
        playerScoreUI = GameObject.Find("PlayerScore");
        attackPhaseUI = GameObject.Find("AttackUI");
        attackDescriptionUI = GameObject.Find("AttackDescription");
        defencePhaseUI = GameObject.Find("DefenseUI");
        enemyUI = GameObject.Find("Enemy");
        playerUI = GameObject.Find("You");
        battlePhaseUI = GameObject.Find("BattlePhaseUI");
        battlePhase = GameObject.Find("BattlePhase");
        endingScreen = GameObject.Find("BattleEndingScreen");
        startScreen = GameObject.Find("BattlePhaseStart");
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        buttonsCreator = GameObject.Find("NeutralUICircles").GetComponent<ButtonsCreator>();

        playerStats = PlayerStats.GetInstance();

        battlePhaseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayingSong) return;
        timeElapsed += Time.deltaTime;
        UpdateEnemyHealthBar(timeElapsed);
        UpdatePlayerHealthBar(timeElapsed);
        CheckSelectedAttackButton();

        if (isBattleEnded) return;

        if (player.currentHealt <= 0 || enemy.currentHealt <= 0)
        {
            isBattleEnded = true;
            StartCoroutine(StopBattle(player.currentHealt > 0));
        }
    }

    void CheckSelectedAttackButton()
    {
        if (!attackPhaseUI.activeInHierarchy) return;

        GameObject selectedButton = eventSystem.currentSelectedGameObject;

        if (selectedButton == null) return;

        string attackDamage = "";
        string attackCrit = "";
        string attackTotal = "";
        string attackName = "";
        switch(selectedButton.name)
        {
            case "Attack1":
                if (playerStats.attacks[0] == null) break;
                attackDamage = "Damage : " + playerStats.attacks[0].baseDamage.ToString();
                attackCrit = "Critical : " + playerStats.attacks[0].criticalDamage.ToString();
                attackTotal = "Total : " + (playerStats.attacks[0].baseDamage + playerStats.attacks[0].criticalDamage).ToString();
                attackName = playerStats.attacks[0].name.ToString();
                break;

            case "Attack2":
                if (playerStats.attacks[1] == null) break;
                attackDamage = "Damage : " + playerStats.attacks[1].baseDamage.ToString();
                attackCrit = "Critical : " + playerStats.attacks[1].criticalDamage.ToString();
                attackTotal = "Total : " + (playerStats.attacks[1].baseDamage + playerStats.attacks[1].criticalDamage).ToString();
                attackName = playerStats.attacks[1].name.ToString();
                break;

            case "Attack3":
                if (playerStats.attacks[2] == null) break;
                attackDamage = "Damage : " + playerStats.attacks[2].baseDamage.ToString();
                attackCrit = "Critical : " + playerStats.attacks[2].criticalDamage.ToString();
                attackTotal = "Total : " + (playerStats.attacks[2].baseDamage + playerStats.attacks[2].criticalDamage).ToString();
                attackName = playerStats.attacks[2].name.ToString();
                break;

            case "Attack4":
                if (playerStats.attacks[3] == null) break;
                attackDamage = "Damage : " + playerStats.attacks[3].baseDamage.ToString();
                attackCrit = "Critical : " + playerStats.attacks[3].criticalDamage.ToString();
                attackTotal = "Total : " + (playerStats.attacks[3].baseDamage + playerStats.attacks[3].criticalDamage).ToString();
                attackName = playerStats.attacks[3].name.ToString();
                break;
        }

        attackDescriptionUI.gameObject.transform.Find("AttackName").GetComponent<TMP_Text>().text = attackName;
        attackDescriptionUI.gameObject.transform.Find("Damage").GetComponent<TMP_Text>().text = attackDamage;
        attackDescriptionUI.gameObject.transform.Find("Critical").GetComponent<TMP_Text>().text = attackCrit;
        attackDescriptionUI.gameObject.transform.Find("Total").GetComponent<TMP_Text>().text = attackTotal;
    }

    void UpdateEnemyHealthBar(float timeElapsed)
    {
        if (enemy == null || timeElapsed > lerpDuration) return;

        enemyHealthSlider.value = Mathf.Lerp(enemyHealthSlider.value, enemy.currentHealt, timeElapsed / lerpDuration);

        ChangeSliderFillColor(enemySliderFillAreaImage, enemyHealthSlider.value, enemy.health);

        if (enemy.currentHealt <= 0) enemy.currentHealt = 0;

        float shownCurrentHealth = Mathf.Floor(Mathf.Lerp(enemyHealthSlider.value, enemy.currentHealt, timeElapsed / lerpDuration));
        enemyHealthDisplay.text = shownCurrentHealth + " / " + enemy.health;
    }

    void UpdatePlayerHealthBar(float timeElapsed)
    {
        if (player == null || timeElapsed > lerpDuration) return;

        playerHealthSlider.value = Mathf.Lerp(playerHealthSlider.value, player.currentHealt, timeElapsed / lerpDuration);

        ChangeSliderFillColor(playerSliderFillAreaImage, playerHealthSlider.value, player.health);

        if (player.currentHealt <= 0) player.currentHealt = 0;

        float shownCurrentHealth = Mathf.Floor(Mathf.Lerp(playerHealthSlider.value, player.currentHealt, timeElapsed / lerpDuration));
        playerHealthDisplay.text = shownCurrentHealth + " / " + player.health;
    }

    void ChangeSliderFillColor(Image sliderFillAreaImage, float sliderValue, float sliderMaxValue)
    {
        if (sliderValue <= sliderMaxValue / 4)
        {
            sliderFillAreaImage.color = Color.red;
        }
        else if (sliderValue <= sliderMaxValue / 2)
        {
            sliderFillAreaImage.color = Color.yellow;
        }
    }

    void InitializeEnemy()
    {
        Map map = Map.GetMapInstance();
        if (map == null) return;

        enemy = map.currentEnemy;
        enemy.buttonsCreator = buttonsCreator;
        enemy.battlePhase = this;
        enemy.InitializeAttacks();

        enemyAvatar.sprite = enemy.avatar;
        enemyAvatar.rectTransform.sizeDelta = enemy.avatarSize;
        enemyAvatar.rectTransform.position += enemy.avatarShift; 

        enemyName.text = enemy.name;

        enemyHealthSlider.minValue = 0;
        enemyHealthSlider.maxValue = enemy.health;
        enemyHealthSlider.value = enemy.currentHealt;

        enemySliderFillAreaImage = enemyHealthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();

        enemyHealthDisplay.text = enemy.currentHealt + " / " + enemy.health;
    }

    void InitializePlayer()
    {
        playerStats.InitializeAttacks(buttonsCreator, this);
        player = playerStats.player;
        playerHealthSlider.minValue = 0;
        playerHealthSlider.maxValue = player.health;
        playerHealthSlider.value = player.currentHealt;

        playerSliderFillAreaImage = playerHealthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();

        playerHealthDisplay.text = player.currentHealt + " / " + player.health;
    }

    void ResetScoresCounter()
    {
        PlayerScore.perfectCounter = 0;
        PlayerScore.goodOrBetterCounter = 0;
        PlayerScore.buttonCounter = 0;
    }

    public void StartBattle()
    {
        KeepPlayerScore();

        neutralCirclesUI.SetActive(false);
        perfectUI.HideUI();
        goodUI.HideUI();
        badUI.HideUI();
        playerScoreUI.SetActive(false);
        endingScreen.SetActive(false);
        attackPhaseUI.SetActive(false);
        attackDescriptionUI.SetActive(false);
        enemyUI.SetActive(false);
        defencePhaseUI.SetActive(false);
        playerUI.SetActive(false);
        

        battlePhase.SetActive(true);
        battlePhaseUI.SetActive(true);
        StartCoroutine(StartBattleUI());
        

        UpdateAttackButtons();
        isInBattlePhase = true;

        ResetScoresCounter();

        InitializeEnemy();
        InitializePlayer();
    }

    IEnumerator StartBattleUI()
    {
        startScreen.SetActive(true);
        
        yield return new WaitForSeconds(5f);

        startScreen.SetActive(false);
        playerUI.SetActive(true);
        enemyUI.SetActive(true);
        attackPhaseUI.SetActive(true);
        attackDescriptionUI.SetActive(true);
    }

    public void ReturnToAttackPhase()
    {
        neutralCirclesUI.SetActive(false);
        perfectUI.HideUI();
        goodUI.HideUI();
        badUI.HideUI();
        playerScoreUI.SetActive(false);
        defencePhaseUI.SetActive(false);
        endingScreen.SetActive(false);

        battlePhase.SetActive(true);
        battlePhaseUI.SetActive(true);
        StartCoroutine(ShowcharactersHealth(true));

        UpdateAttackButtons();
        isInBattlePhase = true;
        timeElapsed = 0;
    }

    public void ReturnToDefencePhase()
    {
        neutralCirclesUI.SetActive(false);
        perfectUI.HideUI();
        goodUI.HideUI();
        badUI.HideUI();
        playerScoreUI.SetActive(false);
        attackPhaseUI.SetActive(false);
        attackDescriptionUI.SetActive(false);
        endingScreen.SetActive(false);

        battlePhase.SetActive(true);
        battlePhaseUI.SetActive(true);
        StartCoroutine(ShowcharactersHealth(false));

        isInBattlePhase = true;
        timeElapsed = 0;
        
        StartCoroutine(EnemyChoosingAttack());
    }

    IEnumerator ShowcharactersHealth(bool isAttacking)
    {
        enemyUI.SetActive(true);
        playerUI.SetActive(true);

        yield return new WaitForSeconds(5f);

        if (isAttacking)
        {
            attackPhaseUI.SetActive(true);
            attackDescriptionUI.SetActive(true);
        }
    }

    IEnumerator EnemyChoosingAttack()
    {
        yield return new WaitForSeconds(3f);

        defencePhaseUI.SetActive(true);

        yield return new WaitForSeconds(3f);

        EnablePlayMode();
        
        // May add intelligent attack choosing, but for very later
        StartCoroutine(enemy.attacks[Random.Range(0, 4)].Use(player, false));
    }

    public void DealDamageToCharacter(float damage, float criticalDamage, HealthCharacter target, bool isAttacking)
    {
        float perfectPourcentage = PlayerScore.perfectCounter / PlayerScore.buttonCounter;
        float goodOrBetterPourcentage = PlayerScore.goodOrBetterCounter / PlayerScore.buttonCounter;

        if (isAttacking)
        {
            damage = damage * goodOrBetterPourcentage + criticalDamage * perfectPourcentage;
        }
        else
        {
            damage = damage * (1 - goodOrBetterPourcentage) + criticalDamage * (1 - perfectPourcentage);
        }

        target.TakeDamage(damage);

        ResetScoresCounter();
    }

    void EnablePlayMode()
    {
        neutralCirclesUI.SetActive(true);

        attackPhaseUI.SetActive(false);
        attackDescriptionUI.SetActive(false);
        defencePhaseUI.SetActive(false);
        enemyUI.SetActive(false);
        playerUI.SetActive(false);

        isInBattlePhase = false;
    }

    void UpdateAttackButtons()
    {
        for (int i = 0; i < attackButtonsText.Length; i++)
        {
            if (playerStats.attacks[i] == null) continue;

            attackButtonsText[i].text = playerStats.attacks[i].name;

            if (attackButtonsText[i].text == "")
            {
                attackButtonsText[i].text = "-";
            }
        }
    }

    public void TriggerFirstAttack()
    {
        if (playerStats.attacks[0].name == "") return;
        EnablePlayMode();
        StartCoroutine(playerStats.attacks[0].Use(enemy, true));
    }

    public void TriggerSecondAttack()
    {
        if (playerStats.attacks[1].name == "") return;
        EnablePlayMode();
        StartCoroutine(playerStats.attacks[1].Use(enemy, true));
    }

    public void TriggerThirdAttack()
    {
        if (playerStats.attacks[2].name == "") return;
        EnablePlayMode();
        StartCoroutine(playerStats.attacks[2].Use(enemy, true));
    }

    public void TriggerFourthAttack()
    {
        if (playerStats.attacks[3].name == "") return;
        EnablePlayMode();
        StartCoroutine(playerStats.attacks[3].Use(enemy, true));
    }

    IEnumerator StopBattle(bool won)
    {
        neutralCirclesUI.SetActive(false);
        perfectUI.HideUI();
        goodUI.HideUI();
        badUI.HideUI();
        playerScoreUI.SetActive(false);
        attackPhaseUI.SetActive(false);
        attackDescriptionUI.SetActive(false);
        defencePhaseUI.SetActive(false);

        battlePhase.SetActive(true);
        enemyUI.SetActive(true);
        playerUI.SetActive(true);

        yield return new WaitForSeconds(5f);

        enemyUI.SetActive(false);
        playerUI.SetActive(false);
        
        endingScreen.SetActive(true);
        endingScreenText.text = won ? "You win !" : "You lose !";

        yield return new WaitForSeconds(5f);

        // load map scene
        SceneManager.LoadScene(3);
    }

    void KeepPlayerScore()
    {
        playerStats.playerScore += playerScore.playerScore;
    }
}
