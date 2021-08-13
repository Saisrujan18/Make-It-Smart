using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeAnimation : MonoBehaviour
{
    private Animator UpgradeAnimation;
    public IEnumerator playAnimation()
    {
        UpgradeAnimation = GetComponent<Animator>();
        UpgradeAnimation.Play("Upgrade Animation");
        while (UpgradeAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
            yield return null;
        Destroy(gameObject);
    }
}
