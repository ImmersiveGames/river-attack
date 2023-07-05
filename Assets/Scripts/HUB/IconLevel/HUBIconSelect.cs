using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(HUBIconMaster))]
public class HUBIconSelect : MonoBehaviour
{

    private Animator animator;
    private HUBIconMaster hubIconMaster;
    private HUBMaster hubMaster;

    [SerializeField]
    public bool isSelected;

    private void OnEnable()
    {
        SetInitialReference();
        hubMaster.EventAllOffFocusElement += IconeOffFocus;
    }

    private void SetInitialReference()
    {
        animator = GetComponent<Animator>();
        hubIconMaster = GetComponent<HUBIconMaster>();
        hubMaster = HUBMaster.Instance;
    }

    public void IconeOnFocus(Levels selectedLevel, Vector3 pos)
    {
        hubMaster.CallEventOnFocusElement(selectedLevel, pos);
        animator.SetBool("IconUnSelect", false);
        // checo se este icone foi selecionado
        if (hubIconMaster.MyLevel == selectedLevel)
        {
            // se eu ja estou selecionado então deselecionar
            if (isSelected)
                UnSelectIcon();
            else
                SelectIcon(selectedLevel, pos);
        }
    }

    private void SelectIcon(Levels selectedLevel, Vector3 pos)
    {
        isSelected = true;
        animator.SetTrigger("IconSelect");
        hubMaster.CallEventAllOffFocusElement(selectedLevel, pos);
    }

    private void UnSelectIcon()
    {
        isSelected = false;
        animator.SetBool("IconUnSelect", true);
        GameManager.Instance.SetActualLevel(null);
        hubMaster.CallEventOffFocusElement();
    }

    public void IconeOffFocus(Levels selectedLevel, Vector3 pos)
    {
        if (hubIconMaster.MyLevel != selectedLevel)
        {
            isSelected = false;
            animator.SetBool("IconUnSelect", true);
        }
    }

    private void OnDisable()
    {
        hubMaster.EventAllOffFocusElement -= IconeOffFocus;
    }
}
