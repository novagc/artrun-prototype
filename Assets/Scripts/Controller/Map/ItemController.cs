using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ParticleSystem))]
public class ItemController : MonoBehaviour
{
    public Item Info;

    private Animator _animator;
    private ParticleSystem _ps;

    private bool _took;
    
    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _ps = gameObject.GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_took && other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(Info.Type, PlayerPrefs.GetInt(Info.Type) + 1);
            PlayerPrefs.Save();

            _took = true;
            _animator.SetTrigger("Hiding");
        }
    }

    public void Hiding()
    {
        _ps.Play();
    }

    public void DestroyObject()
    {
        Destroy(Info.MiniMapIcon);
        Destroy(Info.Object);
    }
}

