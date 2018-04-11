using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LaunchMeshScript : MonoBehaviour {
    Mesh mesh;
    public float meshWidth;
    public int resolution = 25;

    private float g;
    public float angle;
    public float vel;
    private float radianAngle;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        g = Mathf.Abs(Physics2D.gravity.y);
    }
   
	public void RedrawArc (float vel, float angle) {
        this.angle = angle;
        this.vel = vel;
        MakeMesh(CalculateArcArray());
	}

    public void ClearMesh ()
    {
        mesh.Clear();
    }

    // Generate mesh triangles from arc vertices given
    void MakeMesh(Vector3[] arcVerts)
    {
        mesh.Clear();

        Vector3[] vertices = new Vector3[(resolution + 1) * 2];
        int[] triangles = new int[resolution * 6 * 2];

        for (int i = 0; i <= resolution; i++)
        {
            vertices[i * 2] = new Vector3(meshWidth * .5f, arcVerts[i].y, arcVerts[i].x);
            vertices[i * 2 + 1] = new Vector3(meshWidth * -.5f, arcVerts[i].y, arcVerts[i].x);

            if (i != resolution)
            {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                triangles[i * 12 + 6] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2;
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    // Calculate an array of arc vertices for the angle
    Vector3[] CalculateArcArray ()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t);
        }

        return arcArray;
    }

    // Calculate individual point coords given specific params
    Vector3 CalculateArcPoint(float t)
    {
        float x = t * vel * Mathf.Cos(radianAngle);
        float y = t * vel * Mathf.Sin(radianAngle) - .5f * g * t * t;
        return new Vector3(x, y);
    }
}
