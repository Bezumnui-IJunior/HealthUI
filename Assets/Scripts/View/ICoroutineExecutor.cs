using System.Collections;
using UnityEngine;

namespace View
{
    public interface ICoroutineExecutor
    {
        public Coroutine StartCoroutine(IEnumerator enumerator);
        public void StopCoroutine(Coroutine coroutine);
    }
}