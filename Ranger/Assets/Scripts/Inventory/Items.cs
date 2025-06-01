#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public enum ItemType
{
    Plant,
    Bug,
    Fish
}

[CreateAssetMenu]
public class Items : ScriptableObject
{
    [SerializeField]
    private string _id;
    public string ID { get { return _id; } }
    [SerializeField]
    private string _itemName;
    public Sprite ItemIcon;

    [SerializeField] private GameObject _itemPrefab;
    public GameObject ItemPrefab => _itemPrefab;

    [SerializeField] private ItemType _itemType;
    public ItemType ItemType => _itemType;
    public int Weight
    {
        get
        {
            switch (_itemType)
            {
                case ItemType.Plant:
                    return 1;
                case ItemType.Bug:
                    return 2;
                case ItemType.Fish:
                    return 3;
                default:
                    return 1;
            }
        }
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        _id = AssetDatabase.AssetPathToGUID(path);
    }
#endif
}