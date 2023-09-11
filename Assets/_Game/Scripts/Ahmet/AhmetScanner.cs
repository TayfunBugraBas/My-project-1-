using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Ahmet
{
    [RequireComponent(typeof(LineRenderer))]
    public class AhmetScanner : MonoBehaviour
    {
        private InputAction _fire;
        private InputAction _changeRadius;

        private List<VisualEffect> _vfxList = new List<VisualEffect>();
        private Dictionary<Contructor, VisualEffect> _vfxDict = new Dictionary<Contructor, VisualEffect>();


        private VisualEffect _currentVFX;
        private Texture2D _texture;
        private bool _createNewVFX;

        private LineRenderer _lineRenderer;
        
        private const string REJECT_LAYER_NAME = "PointReject";
        private const string PLAYER_TAG = "Player";
        private const string TEXTURE_NAME = "PositionsTexture";
        private const string RESOLUTION_PARAMETER_NAME = "Resolution"; 
        
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] public VisualEffect _vfxPrefab;
        [SerializeField] private GameObject _vfxContainer;
        [SerializeField] private Transform _castPoint;
        [SerializeField] private float _radius = 10f;
        [SerializeField] private float _maxRadius = 10f;
        [SerializeField] private float _minRadius = 1f;
        [SerializeField] private int _pointsPerScan = 100;
        [SerializeField] private float _range = 10f;
        [SerializeField, ColorUsage(true, true)] public Color _defaultColor;
        [SerializeField, ColorUsage(true, true)] public Color _tag1Color;
        [SerializeField] public Color _tag2Color;

        [SerializeField] private int resolution = 16000;

        [Header("Experimental TEST")] [SerializeField, ColorUsage(true, true)]
        private Color lastColor;

        private void Start()
        {
            // Get InputAction from PlayerInput
            _fire = playerInput.actions["Fire"];
            _changeRadius = playerInput.actions["Scroll"];
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.enabled = false;
            _createNewVFX = true;
            //CreateNewVisualEffect();
            //ApplyPositions();
        }
        
        private void FixedUpdate()
        {
            Scan();
            ChangeRadius();
        }
        
        /// <summary>
        /// Change to lidar radius
        /// </summary>
        private void ChangeRadius()
        {
            if (_changeRadius.triggered)
            {
                _radius = Mathf.Clamp(_radius + _changeRadius.ReadValue<float>() * Time.deltaTime, _minRadius, _maxRadius);
            }
        }
        
        private void ApplyPositions(Contructor contructor)
        {
        
            Vector3[] pos = contructor.colorPos.ToArray();
        
        
            Vector3 vfxPos = _currentVFX.transform.position;
        
        
            Vector3 transformPos = transform.position;
        
        
            int loopLength = _texture.width * _texture.height;
            int posListLen = pos.Length;

            for (int i = 0; i < loopLength; i++)
            {
                Color data;

                if (i < posListLen - 1)
                {
                    data = new Color(pos[i].x - vfxPos.x, pos[i].y - vfxPos.y, pos[i].z - vfxPos.z, 1);
                }
                else
                {
                    data = new Color(0, 0, 0, 0);
                }
                contructor.color[i] = data;
            }
        
        
            _texture.SetPixels(contructor.color);
            _texture.Apply();
        
        
            _currentVFX.SetTexture(TEXTURE_NAME, _texture);
            _currentVFX.Reinit();
        }

        [ContextMenu("Change Color")]
        public void ChangeColorVFX(string objTag, Contructor contructor)
        {
            _createNewVFX = true;
            CreateNewVisualEffect(objTag, contructor);
            _currentVFX.SetVector4("ParticleColor", contructor.constructorColor);
            ApplyPositions(contructor);

        }
        
        private void CreateNewVisualEffect(string objTag, Contructor contructor) 
        {
            if (!_createNewVFX) return;
            
            if (_vfxDict.ContainsKey(contructor))
            {
                if (_vfxDict.TryGetValue(contructor, out var value))
                {
                    _currentVFX = value;
                }
            }
            else
            {
                //_vfxList.Add(_currentVFX);

                _currentVFX = Instantiate(_vfxPrefab, transform.position, Quaternion.identity, _vfxContainer.transform);
                
                _currentVFX.SetUInt(RESOLUTION_PARAMETER_NAME, (uint)resolution);
                
                _texture = new Texture2D(resolution, resolution, TextureFormat.RGBAFloat, false);
                
                contructor.color = new Color[resolution * resolution];

                contructor.colorPos.Clear();
                
                _vfxDict.Add(contructor, _currentVFX);

                _createNewVFX = false;
            }
        }

        private string lastScanTag = string.Empty;
        
        private void Scan()
        {
            // only call if button is pressed
            if (_fire.IsPressed())
            {
                for (int i = 0; i < _pointsPerScan; i++)
                {
                
                    Vector3 randomPoint = Random.insideUnitSphere * _radius;
                    randomPoint += _castPoint.position;

                
                    Vector3 dir = (randomPoint - transform.position).normalized;

                
                    if (Physics.Raycast(transform.position, dir, out RaycastHit hit, _range, _layerMask))
                    {
                        var constructor = hit.transform.GetComponent<Contructor>();
                        
                        /*
                        if (!hit.transform.CompareTag(lastScanTag))
                        {
                            lastScanTag = hit.transform.tag;
                            ChangeColorVFX(lastScanTag);
                            ApplyPositions();
                            _positionList.Clear();
                        }
                        */
                        
                        Debug.DrawRay(transform.position, dir * hit.distance, Color.green);
                    
                        if (constructor.colorPos.Count < resolution * resolution)
                        {
                      
                            constructor.colorPos.Add(hit.point);
                            _lineRenderer.enabled = true;
                            _lineRenderer.SetPositions(new[]
                            {
                                transform.position,
                                hit.point
                            });
                       
                            Debug.Log(hit.transform.tag);
                            ChangeColorVFX(hit.transform.tag, constructor);

                        }
                    
                        else
                        {
                            ChangeColorVFX(hit.transform.tag, constructor);
                            break;
                        }
                    } 
                    else
                    {
                        Debug.DrawRay(transform.position, dir * _range, Color.red);
                    }
                } 
            } 
            else
            {
                _lineRenderer.enabled = false;
            }
        }


    }
}