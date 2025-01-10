using UnityEngine;

public class SyrupObj : MonoBehaviour
{
    [SerializeField] private Transform _posToGlass;
    [SerializeField] private RunSyrup _runSyrup;
    [SerializeField] private SyrupType _syrupType;

    [Header("Visual")]
    [SerializeField] private ParticleSystem _pourSyrupParticles;
    [SerializeField] private Color _colorSyrop;

    private GlassObj _currentGlass;

    private void Start()
    {
        EventsGame.OnSyrupPourStart += GlassFreezyToMove;
        EventsGame.OnSyrupPourEnd += GlassFreeMoveActive;

        _runSyrup.Init(this);
    }

    private void OnDisable()
    {
        EventsGame.OnSyrupPourStart -= GlassFreezyToMove;
        EventsGame.OnSyrupPourEnd -= GlassFreeMoveActive;
    }

    public void SetGlass(GlassObj glass)
    {
        if (_currentGlass == null)
        {
            _currentGlass = glass;
        }
    }

    public void ExitGlass(GlassObj glass)
    {
        if (_currentGlass != null && _currentGlass == glass)
        {
            _currentGlass = null;
        }
    }

    public Vector2 GetPosToGlass()
    {
        return _posToGlass.position;
    }

    public void RunSyrup()
    {
        if (_currentGlass != null)
        {
            _pourSyrupParticles.Play();
            _currentGlass.AddWater(_colorSyrop, _syrupType);
        }
    }

    private void GlassFreezyToMove()
    {
        if (_currentGlass != null)
        {
            _currentGlass.isReadyToMove = false;
        }
    }

    private void GlassFreeMoveActive()
    {
        if (_currentGlass != null)
        {
            _currentGlass.isReadyToMove = true;
        }
    }
}
