using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ColorChanger
    {
        private readonly Image _image;
        private readonly ICoroutineExecutor _coroutineExecutor;
        private readonly WaitForSeconds _delay;
        private Coroutine _coroutine;

        public ColorChanger(ICoroutineExecutor coroutineExecutor, Image image, float delaySeconds)
        {
            _coroutineExecutor = coroutineExecutor;
            _image = image;
            _delay = new WaitForSeconds(delaySeconds);
        }

        public void SetDamageColor(Color color)
        {
            if (_coroutine != null)
                _coroutineExecutor.StopCoroutine(_coroutine);

            _image.color = color;
        }

        public void SetDefaultDelayed(Color color)
        {
            if (_coroutine != null)
                _coroutineExecutor.StopCoroutine(_coroutine);

            _coroutine = _coroutineExecutor.StartCoroutine(SettingDefaultDelayed(color));
        }

        private IEnumerator SettingDefaultDelayed(Color color)
        {
            yield return _delay;

            _image.color = color;
        }
    }
}