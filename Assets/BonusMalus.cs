using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BonusMalus : MonoBehaviour {

    #region
    enum BonusMalusType
    {
        Null,
        AutoLiner_NOIMPL,
        ScoreBumper,
        SpeedUp_NOIMPL
    }
    #endregion

    [SerializeField] UnityEvent onFire;
    [SerializeField] BonusMalusType type;
    
	public void Fire()
    {
        onFire.Invoke();

        switch (type)
        {
            case BonusMalusType.AutoLiner_NOIMPL:
                break;
            case BonusMalusType.ScoreBumper:
                ScoreBumper();
                break;
            case BonusMalusType.SpeedUp_NOIMPL:
                break;
            case BonusMalusType.Null:
            default:
                break;
        }
    }

    void ScoreBumper()
    {
        ScoreBanner.Instance.AddScore(5000);
    }

}
