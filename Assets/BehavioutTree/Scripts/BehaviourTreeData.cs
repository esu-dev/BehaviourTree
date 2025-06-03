using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/BehaviourTree")]
public class BehaviourTreeData : ScriptableObject
{
    [SerializeField]
    List<NodeData> _nodeDataList = new List<NodeData>();

    [SerializeField]
    List<NodeData> _nodeDataListForLoad = new List<NodeData>();

    [SerializeField]
    List<string> _blackbaordFieldTextList = new List<string>();

    GameObject _target;
    UnityAction<NodeData> _stateChangeCallback;

    public List<NodeData> GetNodeDataListForLoad()
    {
        return _nodeDataListForLoad;
    }

    public List<string> GetBlackboardFieldTextList()
    {
        return _blackbaordFieldTextList;
    }

    public NodeData FindNodeDataByIDForLoad(string id)
    {
        return _nodeDataListForLoad.Find(x => x.id == id);
    }

    public void AddNodeData(NodeData nodeData)
    {
        _nodeDataList.Add(nodeData);
    }

    public void AddBlackBoardFieldText(string blackboardFieldText)
    {
        _blackbaordFieldTextList.Add(blackboardFieldText);
    }

    public void AllClear()
    {
        _nodeDataList.Clear();
        _blackbaordFieldTextList.Clear();
    }

    // ���[�g�m�[�h��擪�ɂ���
    public void MakeRootFirst()
    {
        NodeData root = _nodeDataList.Find(x => x.nodeTypeID == NodeTypeID.RootNode);
        _nodeDataList.Remove(root);
        _nodeDataList.Insert(0, root);
    }

    public void SortAllChild()
    {
        foreach (NodeData nodeData in _nodeDataList)
        {
            nodeData.outputIDList.Sort((a, b) => (int)(FindNodeDataByID(a).position.y - FindNodeDataByID(b).position.y));
        }
    }

    public void AddOutputID(string fromID, string toID)
    {
        FindNodeDataByID(fromID).outputIDList.Add(toID);
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    /// <summary>
    /// �m�[�h�̏�Ԃ��ω������Ƃ��̃R�[���o�b�N��ݒ肷��
    /// </summary>
    /// <param name="stateChangeCallback"></param>
    public void SetStateChangedCallback(UnityAction<NodeData> stateChangeCallback)
    {
        _stateChangeCallback = (NodeData nodeData) => stateChangeCallback(nodeData);
    }

    /// <summary>
    /// �m�[�h�̏�Ԃ�ύX����
    /// </summary>
    /// <param name="id"></param>
    /// <param name="state"></param>
    public void ChangeState(string id, NodeState state, GameObject target)
    {
        // �Ώۂ�AI���قȂ�ꍇ�͉������Ȃ�
        if (target != _target)
        {
            return;
        }

        NodeData nodeData = FindNodeDataByIDForLoad(id);
        nodeData.state = state;

        _stateChangeCallback?.Invoke(nodeData);
    }

    public void ResetNodeStateDataForLoad()
    {
        for (int i = 0; i < _nodeDataListForLoad.Count(); i++)
        {
            _nodeDataListForLoad[i].state = NodeState.Waiting;
        }
    }

    public void CopyDataForLoad()
    {
        _nodeDataListForLoad.Clear();

        // ���s���p�̃f�[�^�𕡐�
        foreach (NodeData nodeData in _nodeDataList)
        {
            _nodeDataListForLoad.Add(nodeData.Copy());
        }
    }

    public NodeData FindNodeDataByID(string id)
    {
        return _nodeDataList.Find(x => x.id == id);
    }
}

public enum NodeTypeID
{
    RootNode,
    Selector,
    WeightedRandomSelector = 10,
    Sequencer = 2,
    SimpleParallel,
    Repeater,
    LoopNode = 20,
    InverseDecorater = 5,
    ConditionNode,
    LoopConditionNode,
    ActionNode,
    FunctionNode
}

public enum NodeState
{
    Waiting,
    Running,
    True,
    False,
    Completed,
    Warning
}

[System.Serializable]
public class NodeData
{
    public string typeName;
    public NodeTypeID nodeTypeID;
    public string id;
    public string parameterJSON;
    public Rect position;
    public NodeState state;

    public List<string> outputIDList = new List<string>();

    public NodeData Copy()
    {
        NodeData newNodeData = new NodeData();
        newNodeData.typeName = typeName;
        newNodeData.nodeTypeID = nodeTypeID;
        newNodeData.id = id;
        newNodeData.parameterJSON = parameterJSON;
        newNodeData.position = position;
        newNodeData.state = state;
        newNodeData.outputIDList = new List<string>(outputIDList);

        return newNodeData;
    }
}

[System.Serializable]
public class NodeParameter
{
    public string conditionNodeType;
    public string actionName;

    public string actionClassName;

    public ArgumentData[] inputs;
    public ArgumentData output;

    public int[] inputs_int;
    public float[] argument;
    public bool[] inputs_bool;

    public string[] inputNames;
    public string outputName;

    [System.Serializable]
    public class ArgumentData
    {
        public string value;
        public string variableName;
        public string assetPath;
        public bool isDynamic;
    }
}
