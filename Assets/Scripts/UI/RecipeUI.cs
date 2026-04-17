using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private KitchenObjectIconUI iconTemplate;

    private void Start()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void UpdateUI(RecipeSO recipeSO)
    {
        text.text = recipeSO.recipeSOName;
        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            KitchenObjectIconUI newIcon = GameObject.Instantiate<KitchenObjectIconUI>(iconTemplate, iconTemplate.transform.parent);
            newIcon.Show(kitchenObjectSO.sprite);
        }
    }
}
