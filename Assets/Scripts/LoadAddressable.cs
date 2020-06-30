using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Tutorial.ScriptableObjects;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Tutorial
{
    public class LoadAddressable : MonoBehaviour
    {
        [SerializeField]
        AssetReference reference;

        Item myItem;

        // Start is called before the first frame update
        void Start()
        {
            AsyncOperationHandle<Item> x = reference.LoadAssetAsync<Item>();
            x.Completed += OnLoadItem;
        }

        void OnLoadItem(AsyncOperationHandle<Item> item)
        {
            myItem = item.Result;
            Debug.Log(myItem.itemName);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}