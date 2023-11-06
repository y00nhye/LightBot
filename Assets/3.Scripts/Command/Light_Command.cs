using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Command : Command
{
    public Animator lightAni;
    private Light lightIntensity;

    public List<Animator> lightAniOn = new List<Animator>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            lightAni = other.GetComponentInParent<Animator>();
            lightIntensity = other.GetComponentInChildren<Light>();
        }
    }

    public override void Action(bool isWall, bool isJump, bool isJumpDown)
    {
        isReady = false;
        playerAni.SetBool("GO", false);
        playerAni.SetBool("JUMP", false);

        StartCoroutine(Action_co());
    }

    public override IEnumerator Action_co()
    {
        if (lightAni != null)
        {
            if (lightIntensity.intensity == 0) //light ÄÑ±â
            {
                lightAni.SetBool("LIGHT", true);
                lightAniOn.Add(lightAni);

                GameManager.Instance.currentLight++;

                yield return new WaitForSeconds(1f);
            }
            else
            {
                lightAni.SetBool("LIGHT", false); //light ²ô±â
                lightAniOn.Remove(lightAni);

                GameManager.Instance.currentLight--;

                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        isReady = true;
    }

    public void LightReset()
    {
        for (int i = 0; i < lightAniOn.Count; i++)
        {
            lightAniOn[i].SetBool("LIGHT", false);
            lightAniOn[i].GetComponentInChildren<Light>().intensity = 0;
        }

        lightAniOn = new List<Animator>();
    }
}
