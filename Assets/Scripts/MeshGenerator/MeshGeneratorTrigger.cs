using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneratorTrigger : MonoBehaviour
{

    public MeshGenerator meshGenerator;
    public MeshGeneratorPart2 meshGeneratorPart2;

    bool isPart1;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        isPart1 = true;

        rb = GetComponent<Rigidbody>();
        rb.position = new Vector3(meshGenerator.xSize / 2, 3, meshGenerator.zSize * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {

            if (isPart1)
            {
                StartCoroutine(meshGeneratorPart2.CreateShape());
            }
            else
            {
                StartCoroutine(meshGenerator.CreateShape());
            }

            rb.position += new Vector3(0, 0, meshGenerator.zSize);
            isPart1 = !isPart1;
        }
    }
}
