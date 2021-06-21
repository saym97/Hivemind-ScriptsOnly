using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerAttackVisualization : MonoBehaviour
{
    public bool Debug = false;
    private PlayerContext m_player;
    private List<PlayerAttackData> m_playerAttackData;
    private List<GameObject> m_visualizationMeshes;
    private int m_meshResolution = 10;
    private bool m_rotationLocked = false;

    private void Awake()
    {
        m_player = FindObjectOfType<PlayerContext>();

        m_playerAttackData = new List<PlayerAttackData>();
        m_visualizationMeshes = new List<GameObject>();

        m_playerAttackData.Add(m_player.BasicAttackData);
    }

    private void Update()
    {
        if(Debug && !m_rotationLocked) // rotate towards mouse
        {
            Vector3 mouse = m_player.MouseDirection;
            m_visualizationMeshes.SingleOperation((GameObject g) => g.transform.rotation = Quaternion.LookRotation(mouse));
        }
    }

    private void OnEnable()
    {
        CommandDebugMode.OnDebugMode += DebugMode;
        InputEvents.OnInputDeviceChange += MeshRotation;
    }

    private void OnDisable()
    {
        CommandDebugMode.OnDebugMode -= DebugMode;
        InputEvents.OnInputDeviceChange += MeshRotation;
    }

    private void MeshRotation(string scheme)
    {
        if (scheme.Equals("Gamepad"))
        {
            m_rotationLocked = true;
            m_visualizationMeshes.SingleOperation((GameObject g) => g.transform.localRotation = Quaternion.identity);
        }
        else m_rotationLocked = false;
    }

    public void DebugMode(bool mode) 
    {
        if(!Debug && mode) // create stuff
        {
            m_playerAttackData.SingleOperation(CreateFieldOfView);
        }
        else if(Debug && !mode) // destroy stuff
        {
            m_visualizationMeshes.SingleOperation(Destroy);
            m_visualizationMeshes = new List<GameObject>();
        }

        Debug = mode;
    }

    private void CreateFieldOfView(PlayerAttackData attack)
    {
        GameObject meshObj = new GameObject();
        m_visualizationMeshes.Add(meshObj);

        meshObj.transform.SetParent(transform);
        meshObj.transform.localPosition = Vector3.zero;
        meshObj.transform.localRotation = Quaternion.identity;
        meshObj.name = "DebugMesh " + name;

        meshObj.AddComponent<MeshRenderer>().material = attack.DebugMaterial;
        Mesh mesh = new Mesh();
        mesh.name = "Mesh";
        meshObj.AddComponent<MeshFilter>().mesh = mesh;

        float stepAngleSize = attack.SnapAngle / (m_meshResolution - 1);

        List<Vector3> angles = new List<Vector3>();

        for (int i = 0; i < m_meshResolution; i++)
        {
            float angle = transform.eulerAngles.y - attack.SnapAngle / 2 + stepAngleSize * i;
            angles.Add(DirectionFromAngle(angle, true));
        }

        int vertextCount = m_meshResolution + 1;
        Vector3[] vertices = new Vector3[vertextCount];
        int[] indices = new int[(vertextCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 1; i < vertextCount; i++)
        {
            Vector3 vertex = transform.position + angles[i - 1] * attack.Range;
            vertices[i] = transform.InverseTransformPoint(vertex);

            if (i < vertextCount - 1)
            {
                indices[i * 3 - 3] = 0;
                indices[i * 3 + 1 - 3] = i;
                indices[i * 3 + 2 - 3] = i + 1;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateNormals();

        CreateAttackView(attack);
    }

    private void CreateAttackView(PlayerAttackData attack1)
    {
        var attack = (BasicAttackData)attack1;
        GameObject meshObj = new GameObject();
        m_visualizationMeshes.Add(meshObj);

        meshObj.transform.SetParent(transform);
        meshObj.transform.localPosition = Vector3.zero;
        meshObj.transform.localRotation = Quaternion.identity;
        meshObj.name = "DebugMesh " + name;

        meshObj.AddComponent<MeshRenderer>().material = attack.DebugMaterial;
        Mesh mesh = new Mesh();
        mesh.name = "Mesh";
        meshObj.AddComponent<MeshFilter>().mesh = mesh;

        Vector3[] verts = new Vector3[]
        {
            new Vector3(attack.Width/2, 0f, 0f),
            new Vector3(-attack.Width/2, 0f, 0f),
            new Vector3(attack.Width/2, 0f, attack.Range),
            new Vector3(-attack.Width/2, 0f, attack.Range)
        };

        int[] indices = { 0, 1, 2, 1, 3, 2 };

        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
    }

    private Vector3 DirectionFromAngle(float angleDegrees, bool globalAngle)
    {
        if (!globalAngle) angleDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
    }
}