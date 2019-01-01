using Assets.Scripts.Utils;
using GameEngine.Factory;
using GameEngine.Factory.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Renderer.Entities.Factory
{
    public class ComponentHandler: MonoBehaviour
    {
        public Transform child;

        public void Initialize(FactoryEntity entity)
        {
            var putInto = entity.GetComponent<PutInto>();
            var generator = entity.GetComponent<Generator>();
            if (putInto != null)
                putInto.RaiseRotatedEvent += HandleRotation;
            if(generator != null)
            {
                generator.OnProgressMade += HandleProgressMade;
            }

            this.child = transform.Find("Ressource");
        }

        protected virtual void HandleRotation(object sender, EventArgs args)
        {
            this.transform.rotation = OrientationConverter.ToQuaternion(((PutInto)sender).Target);
        }

        protected virtual void HandleProgressMade(object sender, EventArgs args)
        {
            var progress = ((IProgressableComponent)sender).ProgressPercent / 100f;
            child.localScale = new Vector3(progress, progress, progress);
        }
    }
}
