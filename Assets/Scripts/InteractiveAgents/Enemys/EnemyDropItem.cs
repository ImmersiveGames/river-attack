using UnityEngine;
using Utils.Variables;

public class EnemyDropItem : MonoBehaviour
{

    [SerializeField]
    private ListDropItens itensAvariables;
    [SerializeField]
    private FloatReference timeToAutoDestroy;
    [SerializeField, Tooltip("Se o minimo for diferente de 0 o valor é aleatorio entre min e max."),MinMaxRange(0, 1)]
    private FloatRanged dropChance;

    private EnemyMaster enemyMaster;
    private GameObject itemDrop;

    private void OnEnable()
    {
        SetinitialReferences();
        enemyMaster.EventDestroyEnemy += DropItem;
    }

    private void SetinitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
    }
    //TODO: implementar Dropar mais de 1 item e pois precisa alterar a poição;
    private void DropItem()
    {
        if (dropChance.maxValue > 0 && itensAvariables != null)
        {
            float checkChance = (dropChance.minValue != 0) ? Random.Range(dropChance.minValue, dropChance.maxValue) : dropChance.maxValue;
            float sortRange = Random.value;
            //Debug.Log("Sorteio 1 - Chance: "+ checkChance + " Sorteio: " + sortRange);
            if (sortRange <= checkChance)
            {
                //Debug.Log("Vai Dropar um item");
                sortRange = Random.value;
                ItensDrop dropItem = itensAvariables.TakeRandomItem(sortRange);
                if (dropItem.item != null)
                {
                    //Debug.Log("Dropou o item: " + dropItem.item.name);
                    itemDrop = Instantiate(dropItem.item, this.transform.position, Quaternion.identity);
                    if (timeToAutoDestroy.Value > 0)
                        Invoke("DestroyDrop", timeToAutoDestroy.Value);
                }
            }
        }
    }

    private void DestroyDrop()
    {
        Destroy(itemDrop);
    }

    private void OnDisable()
    {
        enemyMaster.EventDestroyEnemy -= DropItem;
    }
}
