//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                 *
//   * Facebook: https://goo.gl/5YSrKw											     *
//   * Contact me: https://goo.gl/y5awt4								             *
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using UnityEngine;
using System.Collections;

public class ContinueButton : MonoBehaviour {

    [SerializeField] OnStartKeepArena keeper;

    public void OnClickContinueButton()
    {
        StartCoroutine(Continue());
    }

    IEnumerator Continue()
    {
        var go = keeper.CreateSession();
        
        NotificationManager.Instance.FireNotification(EventNotification.GameStart);
        yield return null;

        go.GetComponentInChildren<Managers>(true).Awake();

        Managers.Audio.PlayUIClick();
        Managers.Game.SetState(typeof(GamePlayState));
    }
}
