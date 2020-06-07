using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private RectTransform m_spawnArea;
    [SerializeField] private Transform m_speedUp;

    [SerializeField] private Transform m_cut;
    [SerializeField] private Transform m_random;
    [SerializeField] private Transform m_clock;
    [SerializeField] private Transform m_portal;
    [SerializeField] private Transform m_inviz;

    const float chanceSpawn = 5;

   

    private void TrySpawn(Transform target)
    {
        if (target.gameObject.activeSelf == false)
        {
            if (Random.Range(0, 100) <= chanceSpawn)
            {
                target.gameObject.SetActive(true);

                Spawn(target);
            }
        }
    }
    public void Spawn(Transform transform)
    {
        var x = Random.Range(m_spawnArea.rect.xMin, m_spawnArea.rect.xMax);
        var y = Random.Range(m_spawnArea.rect.yMin, m_spawnArea.rect.yMax);

        transform.gameObject.transform.position = new Vector3(x, y, 0);
    }
    public void TrySpawnSomething()
    {
        TrySpawn(m_speedUp);
        TrySpawn(m_random);
        TrySpawn(m_cut);
        TrySpawn(m_clock);
        TrySpawn(m_portal);
        TrySpawn(m_inviz);
    }

}

