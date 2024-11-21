using UnityEngine;

namespace AroundTheWorld.Utilities
{
    public class WaitForSecondsInterruptible : CustomYieldInstruction
    {
        public WaitForSecondsInterruptible(float timer)
        {
            this.timer = timer;
        }

        public override bool keepWaiting => CheckShouldWait();

        private bool CheckShouldWait()
        {
            timer -= Time.deltaTime;
            return timer > 0f && !shouldInterrupt;
        }

        public void Interrupt() => shouldInterrupt = true;

        private bool shouldInterrupt = false;
        private float timer;
    }
}