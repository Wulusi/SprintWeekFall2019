using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image UIBar;
    public Health stat;

    private void Awake()
    {
        stat = GetComponentInParent<Health>();
    }
    // Update is called once per frame
    void Update()
    {
        UIBar.fillAmount = stat.currentHealth / stat.maxHealth;
    }
}
