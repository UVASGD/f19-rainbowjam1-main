using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreen : MonoBehaviour
{
    public static TransitionScreen instance;

    Animator anim;
    int fade_hash, start_fade_hash;

    public GameEvent FadeInStart, FadeInDone, FadeOutStart, FadeOutDone;

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        anim = GetComponent<Animator>();
        fade_hash = Animator.StringToHash("Fade");
        start_fade_hash = Animator.StringToHash("StartFade");
        DontDestroyOnLoad(gameObject);
    }

    public void StartFade(float fade_time=1f)
    {
        StartCoroutine(Fade(fade_time));
    }

    IEnumerator Fade(float fade_time)
    {
        FadeOutStart?.Invoke();
        FadeOut();
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        FadeOutDone?.Invoke();
        yield return new WaitForSeconds(fade_time);
        FadeInStart?.Invoke();
        FadeIn();
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        FadeInDone?.Invoke();
    }

    void FadeIn()
    {
        anim.SetBool(fade_hash, false);
        anim.SetTrigger(start_fade_hash);
    }

    void FadeOut()
    {
        anim.SetBool(fade_hash, true);
        anim.SetTrigger(start_fade_hash);
    }
}
