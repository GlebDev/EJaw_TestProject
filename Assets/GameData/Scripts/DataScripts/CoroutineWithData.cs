﻿using System.Collections;
using UnityEngine;

namespace CoroutineData{
	// class assistant to work with Coroutines
	public class CoroutineWithData {
		public Coroutine coroutine { get; private set; }
		public object result;
		private IEnumerator target;
		public CoroutineWithData(MonoBehaviour owner, IEnumerator target) {
			this.target = target;
			this.coroutine = owner.StartCoroutine(Run());
		}

		private IEnumerator Run() {
			while(target.MoveNext()) {
				result = target.Current;
				yield return result;
			}
		}
	}
}
