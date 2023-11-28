using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{

    public string unitName;
    public Renderer bodyRenderer;

    public CharacterController cc;

    public Color selectedColor;
    public Color hoverColor;
    Color defaultColor;
    float moveSpeed = 5;
    public Animator animator;

    bool hover = false;
    public bool selected = false;
    bool hasTarget = false;

    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = bodyRenderer.material.color;

        GameManager.SharedInstance.units.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.SharedInstance.units.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 amountToMove = Vector3.zero;
        if (hasTarget)
        {
            Vector3 vectorToTarget = (target - transform.position).normalized;

            float step = 5 * Time.deltaTime;
            Vector3 rotateTowardsVector = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
            rotateTowardsVector.y = 0;
            transform.forward = rotateTowardsVector;

            Vector3 amountToMove = vectorToTarget * moveSpeed * Time.deltaTime;
            cc.Move(amountToMove);

            animator.SetFloat("speed", amountToMove.magnitude);
            
        }
    }

    private void OnMouseDown()
    {
        GameManager.SharedInstance.SelectUnit(this);
        SetUnitColor();
        //bodyRenderer.material.color = selectedColor;
    }

    private void OnMouseEnter()
    {
        hover = true;
        SetUnitColor();
        //bodyRenderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        hover = false;
        SetUnitColor();
        //bodyRenderer.material.color = defaultColor;
    }

    public void SetUnitColor()
    {
        if (selected)
        {
            bodyRenderer.material.color = selectedColor;
        }
        else if (hover)
        {
            bodyRenderer.material.color = hoverColor;
        }
        else
        {
            bodyRenderer.material.color = defaultColor;
        }
    }

    public void SetTarget(Vector3 t)
    {
        target = t;
        hasTarget = true;
    }

}
