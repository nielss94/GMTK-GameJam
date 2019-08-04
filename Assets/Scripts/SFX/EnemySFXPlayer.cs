using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFXPlayer : SFXPlayer
{
  public void PlayImpact()
  {
    Play(audioClips[0]);
  }
}
