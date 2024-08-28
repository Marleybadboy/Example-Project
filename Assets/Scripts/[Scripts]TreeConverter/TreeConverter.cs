using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;


namespace HCC.Core.Trees
{
    public class TreeConverter : MonoBehaviour
    {
        #region Fields

        [Title("Add on Terrain")]
        [SerializeField] private Terrain _terrainObject;
        [SerializeField] private Transform _parentObject;

        private TreePrototype[] _treePrototypeData;
        #endregion

        #region Properties
        #endregion

        #region Functions

        // Start is called before the first frame update
        void Start()
        {

        }
        #endregion

        #region Methods
        [Button("Convert")]
        private void Convert() 
        {
            if (_terrainObject == null) return;

            TreeInstance[] tree = _terrainObject.terrainData.treeInstances;

            _treePrototypeData = _terrainObject.terrainData.treePrototypes;

            for(int i = 0; i < tree.Length; i++) 
            {
                CreateTreePrefab(tree[i]);
            }

            _terrainObject.terrainData.treeInstances = new TreeInstance[0];


        }

        [Button("Destroy Objects")]
        private void DestroyTreePrefabs() 
        {
            if (_parentObject.childCount <= 0) return;

            for(int i = 0; i < _parentObject.childCount ; i++) 
            { 
                Transform child = _parentObject.GetChild(i); 

                Destroy(child.gameObject);
            }
        
        }

        private void CreateTreePrefab(TreeInstance treeInstance) 
        { 

            float3 worldPos = Vector3.Scale(treeInstance.position,_terrainObject.terrainData.size) + _terrainObject.transform.position;

            GameObject prefabTree = _treePrototypeData[treeInstance.prototypeIndex].prefab;

            GameObject treeObject = Instantiate(prefabTree, worldPos, Quaternion.identity);

            treeObject.transform.SetParent(_parentObject, true);

            treeObject.transform.localScale = Vector3.Scale(treeObject.transform.localScale, new float3(treeInstance.widthScale,treeInstance.heightScale, treeInstance.widthScale));
        
        
        }
        #endregion
    }
}
