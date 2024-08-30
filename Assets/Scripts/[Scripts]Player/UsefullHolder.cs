using System;
using UnityEngine;

public class UsefullHolder : MonoBehaviour
{
    #region Fields
    public enum UsefullState {Item, Empty}

    [SerializeField] private UsefullItems[] _usefullItems;
    #endregion

    #region Properties
    public UsefullState State { get; set; }
    #endregion

    #region Functions

   // Start is called before the first frame update
    void Start()
    {
        ChangeState(UsefullState.Empty,0);
    }

    #endregion

    #region Methods
    private void ChangeState(UsefullState state, int index) 
    { 
        switch (state) 
        { 
            case UsefullState.Item:
            ActiveUsefull(index);
            break;

            case UsefullState.Empty:
            DisactiveAll(); 
            break;

            default:
            DisactiveAll();
            break;
        }
        State = state;

    }

    public void AssignByNumericKey(int index) 
    { 
        if(index < _usefullItems.Length) { ChangeState(UsefullState.Empty, index); return; }

        ChangeState(UsefullState.Item, index - 1);    
    }

    private void DisactiveAll() 
    { 
        for(int i = 0;  i < _usefullItems.Length; i++) 
        {
            _usefullItems[i].ActiveObject = false;
        }
    
    }

    public void ActiveUsefull(int index) 
    { 
        for(int i = 0; i < _usefullItems.Length; i++) 
        { 
            if(i == index) 
            {
                _usefullItems[i].ActiveObject = true;
                continue;
            }

            _usefullItems[i].ActiveObject = false;
        }
    
    }
    #endregion

    #region Structs
    [Serializable]
    internal struct UsefullItems 
    {
        [SerializeField] private GameObject _usefullItem;
        
        public bool ActiveObject { set => _usefullItem.SetActive(value); }
    }
    #endregion
}
