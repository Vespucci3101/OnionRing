using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public EventSystem eventSystem;

    public GameObject buttonPopUp;
    public GameObject firstItemGameObject;
    public GameObject secondItemGameObject;
    public GameObject thirdItemGameObject;
    public GameObject confirmationQuitMenu;
    public GameObject items;
    public GameObject AConfirmation;
    public GameObject BQuit;

    public TMP_Text firstItemName;
    public TMP_Text firstItemLevel;
    public TMP_Text firstItemDamage;
    public TMP_Text firstItemCrit;
    public TMP_Text firstItemPrice;

    public TMP_Text secondItemName;
    public TMP_Text secondItemLevel;
    public TMP_Text secondItemDamage;
    public TMP_Text secondItemCrit;
    public TMP_Text secondItemPrice;

    public TMP_Text thirdItemName;
    public TMP_Text thirdItemLevel;
    public TMP_Text thirdItemDamage;
    public TMP_Text thirdItemCrit;
    public TMP_Text thirdItemPrice;

    public Button firstItemButton;
    public Button secondItemButton;
    public Button thirdItemButton;

    public TMP_Text playerMoney;

    public GameObject firstItemSoldOut;
    public GameObject secondItemSoldOut;
    public GameObject thirdItemSoldOut;

    bool isInConfirmationMenu = false;

    List<Attack> attacksList;

    Attack firstItem;
    Attack secondItem;
    Attack thirdItem;
        
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        FillAttacksList();

        InitializeItems();

        playerStats = PlayerStats.GetInstance();

        if (playerStats == null) return;

        playerMoney.text = playerStats.playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        playerMoney.text = playerStats.playerScore.ToString();

        if (!isInConfirmationMenu && Input.GetButtonDown("B"))
        {
            isInConfirmationMenu = true;
            confirmationQuitMenu.SetActive(true);
            items.SetActive(false);
            AConfirmation.SetActive(false);
            BQuit.SetActive(false);
            firstItemSoldOut.SetActive(false);
            secondItemSoldOut.SetActive(false);
            thirdItemSoldOut.SetActive(false);

            eventSystem.SetSelectedGameObject(buttonPopUp);
        }
    }

    void FillAttacksList()
    {
        attacksList = new List<Attack>();

        attacksList.Add(new AxeThrow());
        attacksList.Add(new BladeOfChaos());
        attacksList.Add(new Bomb());
        attacksList.Add(new Boomerang());
        attacksList.Add(new BoomTetris());
        attacksList.Add(new BOY());
        attacksList.Add(new BubbleBeam());
        attacksList.Add(new Cape());
        attacksList.Add(new ChargeShot());
        attacksList.Add(new Chef());
        attacksList.Add(new DuckHuntGun());
        attacksList.Add(new Dunk());
        attacksList.Add(new Fireball());
        attacksList.Add(new FlameBlast());
        attacksList.Add(new GreenMissile());
        attacksList.Add(new HammerSpin());
        attacksList.Add(new Judge());
        attacksList.Add(new LeafShield());
        attacksList.Add(new MasterSword());
        attacksList.Add(new PayLoan());
        attacksList.Add(new Pultergust());
        attacksList.Add(new RaiseTaxes());
        attacksList.Add(new RemoteBomb());
        attacksList.Add(new Slap());
        attacksList.Add(new SpinDash());
        attacksList.Add(new Stone());
        attacksList.Add(new SuperJumpPunch());
        attacksList.Add(new SuperSonic());
        attacksList.Add(new Vacuum());
        attacksList.Add(new ZeroLaser());
    }
    void InitializeItems()
    {
        firstItem = attacksList[Random.Range(0, attacksList.Count)];
        secondItem = attacksList[Random.Range(0, attacksList.Count)];
        thirdItem = attacksList[Random.Range(0, attacksList.Count)];

        firstItemName.text = firstItem.name;
        firstItemLevel.text = firstItem.difficulty;
        firstItemLevel.color = GetItemDifficultyColor(firstItem);
        firstItemDamage.text = "Damage : " + firstItem.baseDamage.ToString();
        firstItemCrit.text = "Crit damage : " + firstItem.criticalDamage.ToString();
        firstItemPrice.text = GetItemPrice(firstItem).ToString();

        secondItemName.text = secondItem.name;
        secondItemLevel.text = secondItem.difficulty;
        secondItemLevel.color = GetItemDifficultyColor(secondItem);
        secondItemDamage.text = "Damage : " + secondItem.baseDamage.ToString();
        secondItemCrit.text = "Crit damage : " + secondItem.criticalDamage.ToString();
        secondItemPrice.text = GetItemPrice(secondItem).ToString();

        thirdItemName.text = thirdItem.name;
        thirdItemLevel.text = thirdItem.difficulty;
        thirdItemLevel.color = GetItemDifficultyColor(thirdItem);
        thirdItemDamage.text = "Damage : " + thirdItem.baseDamage.ToString();
        thirdItemCrit.text = "Crit damage : " + thirdItem.criticalDamage.ToString();
        thirdItemPrice.text = GetItemPrice(thirdItem).ToString();
    }

    int GetItemPrice(Attack item)
    {
        if (item.baseDamage < 100)
        {
            return 3000;
        }
        else if (item.baseDamage < 200)
        {
            return 10000;
        }
        
        return 20000;
    }

    Color GetItemDifficultyColor(Attack item)
    {
        if (item.difficulty == "Easy")
        {
            return Color.green;
        }
        else if (item.difficulty == "Medium")
        {
            return Color.yellow;
        }
        else if (item.difficulty == "Hard")
        {
            return Color.red;
        }
        return Color.black;
    }

    public void BuyItem()
    {
        Attack item = null;
        Button selectedButton = null;
        GameObject nextSelectedObject = null;
        GameObject itemSoldOut = null;
        switch (eventSystem.currentSelectedGameObject.name)
        {
            case "Item1":
                item = firstItem;
                itemSoldOut = firstItemSoldOut;
                selectedButton = firstItemButton;
                nextSelectedObject = secondItemGameObject;
                break;

            case "Item2":
                item = secondItem;
                itemSoldOut = secondItemSoldOut;
                selectedButton = secondItemButton;
                nextSelectedObject = firstItemGameObject;
                break;

            case "Item3":
                item = thirdItem;
                itemSoldOut = thirdItemSoldOut;
                selectedButton = thirdItemButton;
                nextSelectedObject = secondItemGameObject;
                break;
        }

        if (item == null || selectedButton == null || nextSelectedObject == null || itemSoldOut == null) return;

        int itemPrice = GetItemPrice(item);
        if (playerStats.playerScore < itemPrice) return;

        playerStats.playerScore -= itemPrice;

        SetItemAsPlayerAttack(item);

        selectedButton.interactable = false;
        eventSystem.SetSelectedGameObject(nextSelectedObject);
        itemSoldOut.SetActive(true);
    }

    public void QuitShop()
    {
        isInConfirmationMenu = false;
        // Map scene
        SceneManager.LoadScene(3);
    }

    public void ReturnToShopItems()
    {
        isInConfirmationMenu = false;
        confirmationQuitMenu.SetActive(false);
        items.SetActive(true);
        AConfirmation.SetActive(true);
        BQuit.SetActive(true);

        if (!firstItemButton.IsInteractable()) firstItemSoldOut.SetActive(true);
        if (!secondItemButton.IsInteractable()) secondItemSoldOut.SetActive(true);
        if (!thirdItemButton.IsInteractable()) thirdItemSoldOut.SetActive(true);

        eventSystem.SetSelectedGameObject(firstItemGameObject);
    }

    void SetItemAsPlayerAttack(Attack attack)
    {
        if (playerStats.attacks == null) return;

        bool attackChanged = false;
        for (int i = 0; i < playerStats.attacks.Length; i++)
        {
            if (attackChanged) return;
            if (playerStats.attacks[i] == null)
            {
                playerStats.attacks[i] = attack;
                attackChanged = true;
            }
        }

        if (attackChanged) return;

        // Replacing an attack randomly for now
        playerStats.attacks[Random.Range(0, playerStats.attacks.Length)] = attack;
    }
}
