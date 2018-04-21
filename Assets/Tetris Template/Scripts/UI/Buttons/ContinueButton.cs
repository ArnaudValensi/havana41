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
        keeper.CreateSession();
        GameObject.FindObjectOfType<Managers>().Awake();

        yield return null;

        Managers.Audio.PlayUIClick();
        Managers.Game.SetState(typeof(GamePlayState));
        NotificationManager.Instance.FireNotification(EventNotification.GameStart);
    }
}
