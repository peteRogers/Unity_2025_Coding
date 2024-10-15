using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoleMover : MonoBehaviour{

    Sequence sequence;
    Vector3 originPos;

    void Start(){
        originPos = transform.position;
        MoveUpAndDown();
    }

    void Update(){
       //nothing go on here: wait what now?
    }

    private void MoveUpAndDown(){
        // Create a sequence to chain animations
        sequence = DOTween.Sequence();
        sequence.AppendInterval(Random.Range(0.5f, 4f));
        Vector3 targetPosition = originPos + new Vector3(0, 2, 0);
        sequence.Append(transform.DOMove(targetPosition, 0.5f));
        sequence.AppendInterval(1.0f);
        sequence.Append(transform.DOMove(originPos, 0.3f));
        sequence.OnComplete(OnSequenceComplete);
    }

     private void DownSequence(){
        sequence = DOTween.Sequence();
        Vector3 startPosition = transform.position;
        sequence.Append(transform.DOMove(originPos, startPosition.y/3));
        sequence.OnComplete(OnSequenceComplete);

    }

    private void OnMouseDown(){
        if(transform.position.y > originPos.y){
            Debug.Log("hit");
            sequence.Kill();
            DownSequence();
        }
    }
     
     void OnSequenceComplete(){
        MoveUpAndDown();
     }
}