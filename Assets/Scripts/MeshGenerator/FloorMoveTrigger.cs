using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMoveTrigger : MonoBehaviour
{
    public MeshGenerator meshGenerator;
    public MeshGeneratorPart2 meshGeneratorPart2;

    bool isPart1;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        isPart1 = false;

        rb = GetComponent<Rigidbody>();
        rb.position = new Vector3(meshGenerator.xSize / 2, 3, meshGenerator.zSize * 0.30f);
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
                meshGenerator.GetComponent<Rigidbody>().position += new Vector3(0, 0, meshGenerator.zSize * 2);
                meshGenerator.vertices = new Vector3[(meshGenerator.xSize + 1) * (meshGenerator.zSize + 1)];
                meshGenerator.triangles = new int[meshGenerator.xSize * meshGenerator.zSize * 6];
            }
            else
            {
                meshGeneratorPart2.GetComponent<Rigidbody>().position += new Vector3(0, 0, meshGenerator.zSize * 2);
                meshGeneratorPart2.vertices = new Vector3[(meshGeneratorPart2.xSize + 1) * (meshGeneratorPart2.zSize + 1)];
                meshGeneratorPart2.triangles = new int[meshGeneratorPart2.xSize * meshGeneratorPart2.zSize * 6];
            }

            rb.position += new Vector3(0, 0, meshGenerator.zSize);
            isPart1 = !isPart1;
        }
    }
}
