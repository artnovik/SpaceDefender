using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
	[SerializeField]
	private GameObject _destroyedVersion;

	private void OnMouseDown()
	{
		Instantiate(_destroyedVersion, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
