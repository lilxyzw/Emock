using System.Collections.Generic;

namespace jp.lilxyzw.emock
{
    public static class EmockManager
    {
        public static List<EmockAnimator> animators = new();
        public static EmockController controller;

        public static void Update()
        {
            if (controller) controller.ManagedUpdate();
        }

        public static void LateUpdate()
        {
            foreach (var animator in animators)
                animator.ManagedLateUpdate();
        }
    }
}
