using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Items : ScriptableObject
{
    [SerializeField]
    private string _id;
    public string _ID { get { return _id; } }
    [SerializeField]
    private string _itemName;
    public Sprite ItemIcon;

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        _id = AssetDatabase.AssetPathToGUID(path);
    }
}

public interface IItemContainer
{
    int ItemCount(string itemID);
    Items RemoveItems(string itemID);
    bool RemoveItem(Items items);
    bool AddItem(Items items);
    bool IsFull();

}
