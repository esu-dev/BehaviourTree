using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using BehaviourTreeLib;

public class CommonBehaviour
{
    public abstract class CommonAction : ActionClass
    {
        public override void Start() { }

        public override void Update() { }

        public override void Stop() { }
    }

    public class WaitOneFrame : CommonAction
    {
        float _counter;

        public override void Start()
        {
            _counter = 0;
        }

        public override void Update()
        {
            if (_counter++ > 0)
            {
                base.Finish(NodeState.True);
            }
        }

        public override void Stop()
        {

        }
    }

    public class WaitSecond : CommonAction
    {
        BT_Input<float> minTime;
        BT_Input<float> maxTime;
        float _waitTime;
        float _counter;

        public override void Start()
        {
            _waitTime = Random.Range(minTime.value, maxTime.value);
            _counter = 0;
        }

        public override void Update()
        {
            _counter += Time.deltaTime;
            if (_counter >= _waitTime)
            {
                base.Finish(NodeState.True);
            }
        }

        public override void Stop()
        {

        }
    }

    public class IsSmaller : ConditionClass
    {
        BT_Input<float> a;
        BT_Input<float> b;


        public override bool Execute()
        {
            return a.value < b.value;
        }
    }
}
