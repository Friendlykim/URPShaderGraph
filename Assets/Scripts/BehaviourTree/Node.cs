using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree
{

    //노드 종류
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        protected NodeState state;

        public Node parent;
        protected List<Node> children = new List<Node>();

        //노드의 데이터
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        //부모 Null
        public Node()
        {
            parent = null;
        }

        //각 노드마다 연결함수 실행
        public Node(List<Node> children)
        {
            foreach (Node n in children)
                _Attach(n);
        }

        //부모지정, 노드를 부모에 연결
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        //노드 데이터 지정
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        //노드 데이터 가져오기
        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;
            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }
            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }


}

