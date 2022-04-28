using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorTree
{
    public abstract class NodeTree : MonoBehaviour
    {
        private Node root_ = null;

        protected void Start()
        {
            root_ = SetupTree();

        }

        private void Update()
        {
            if(root_ != null)
                root_.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}

