using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public class Gameassets : MonoBehaviour
    {
        private static GameAssets instance = null;

      
        public static GameAssets Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameAssets();
                }
                return instance;
            }

        }

        public Transform pfDamagePopup;
    }
}
