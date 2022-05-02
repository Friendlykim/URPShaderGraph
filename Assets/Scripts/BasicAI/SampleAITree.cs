using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SampleAITree : NodeTree
{

//SampleAITree의 행동트리를 셋업

protected override Node SetupTree()
{
// 노드,시퀸스를 사용해 순서대로 트리를 설정합니다
    Node root = new Selector(new List<Node>
    {
    
    });
    return root;
}

}