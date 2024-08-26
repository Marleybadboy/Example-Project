using System;
using UnityEngine;

namespace HCC.Interfaces
{ 
    public interface IInteractable
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Functions
        #endregion

        #region Methods
        public void OnCollisionEnter(Collision collisionInfo);
        public void OnCollisionExit(Collision collisionInfo);
        public void OnTriggerEnter(Collider other);
        public void OnTriggerExit(Collider other);
        #endregion
    }

}

