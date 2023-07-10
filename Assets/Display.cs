using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Display : MonoBehaviour
{
    private Label counterLabel;
    private int vel;
    // Start is called before the first frame update
    void Start()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        counterLabel = rootVisualElement.Q<Label>("ActVelocity");
    }

    // Update is called once per frame
    void Update()
    {
        vel = GameObject.Find("Spacecraft").GetComponent<Propagator>().velocity;
        counterLabel.text = $"{vel} km/s";
    }
}
