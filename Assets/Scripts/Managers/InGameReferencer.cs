using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameReferencer : MonoBehaviour {
    static public ArchieveOfStats ArchieveOfStats;
    static public ObjectPooler ObjectPooler;
    static public GameManager GameManager;
    static public ActionManager ActionManager;

    #region Editor reference properities
    public ArchieveOfStats _ArchieveOfStats;
    public ObjectPooler _ObjectPooler;
    public GameManager _GameManager;
    public ActionManager _ActionManager;
    #endregion

    void OnEnable() {
        ArchieveOfStats = _ArchieveOfStats;
        ObjectPooler = _ObjectPooler;
        GameManager = _GameManager;
        ActionManager = _ActionManager;
    }
}
