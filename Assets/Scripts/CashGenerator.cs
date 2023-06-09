using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashGenerator : MonoBehaviour
{
    [SerializeField] int cashAmount;
    [SerializeField] float cashInterval;
    [SerializeField] AudioClip cashSound;
    [SerializeField] float cashVolume = 0.7f;
    [SerializeField] string animatorTrigger;
    Animator animator;


    GameManager gm;
    AudioPlayer audioplayer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioplayer = FindObjectOfType<AudioPlayer>();
        gm = FindObjectOfType<GameManager>();
        StartCoroutine(GiveCash());
    }

    // Update is called once per frame
    IEnumerator GiveCash()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(cashInterval);
            gm.AddCash(cashAmount);
            FindObjectOfType<UIScript>().UpdateMoneyText();
            if (animator != null && animatorTrigger != null) animator.SetTrigger(animatorTrigger);
            audioplayer.PlayClip(cashSound, cashVolume);
            if (!gm.stagePlaying) {
                yield return new WaitUntil(() => gm.stagePlaying);
                yield return new WaitForSeconds(Random.Range(0, 1.5f));
            }
        }
    }
}
