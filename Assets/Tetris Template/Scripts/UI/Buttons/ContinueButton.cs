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
    static bool tuto = true;

    public void OnClickContinueButton()
    {
        StartCoroutine(Continue());
    }

    IEnumerator Continue()
    {
        NotificationManager.Instance.FireNotification(EventNotification.GameStart);
        if(tuto)
        {
            yield return new WaitForSeconds(3f);
            yield return new WaitWhile(() => !Input.anyKey);
            tuto = false;
        }

        GetComponent<Animator>().SetTrigger("NextKey");

        var go = keeper.CreateSession();
        
        yield return null;

        go.GetComponentInChildren<Managers>(true).Awake();

        Managers.Audio.PlayUIClick();
        Managers.Game.SetState(typeof(GamePlayState));
    }
}
