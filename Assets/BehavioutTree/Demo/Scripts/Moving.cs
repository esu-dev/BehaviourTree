using UnityEngine;
using BehaviourTreeLib;

public class Moving : MonoBehaviour
{
    [SerializeField]
    BehaviourTreeAI _behaviourTreeAI;

    private void Start()
    {
        _behaviourTreeAI.CreateTree();
    }

    class MoveAction : ActionClass
    {
        BT_Input<float> _velocityX;
        BT_Input<float> _velocityY;
        BT_Input<float> _time;
        float _timer;

        public override void Start()
        {

        }

        public override void Update()
        {
            if (_timer >= _time.value)
            {
                base.Finish(NodeState.True);
            }

            base.TargetObject.transform.position += new Vector3(_velocityX.value, _velocityY.value, 0) * Time.deltaTime;

            _timer += Time.deltaTime;
        }

        public override void Stop()
        {

        }
    }

    class RotateAction : ActionClass
    {
        BT_Input<float> _angularVelocity;
        BT_Input<float> _time;
        float _timer;

        public override void Start()
        {

        }

        public override void Update()
        {
            if (_timer >= _time.value)
            {
                base.Finish(NodeState.True);
            }

            base.TargetObject.transform.Rotate(new Vector3(0, 0, _angularVelocity.value) * Time.deltaTime);

            _timer += Time.deltaTime;
        }

        public override void Stop()
        {

        }
    }
}
