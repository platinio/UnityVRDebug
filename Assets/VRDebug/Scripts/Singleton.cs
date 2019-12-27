using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Platinio
{


	/// <summary>
	/// Basic singleton class
	/// </summary>
	public class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		private static T m_instance = null;

		public static T instance
		{
			get
			{
                

				if(m_instance == null)
				{
					
					//get all the singletones
					T[] singletons = GameObject.FindObjectsOfType( typeof(T) ) as T[];

					if(singletons != null)
					{
						if(singletons.Length == 1)
						{

							m_instance = singletons[0];
							return m_instance;
						}

						else if(singletons.Length > 1)
						{
							Debug.LogWarning("You have more thah one " + typeof( T ).Name + " In the scene, you only need one , all the instances will be destroyed for create a new one");

							m_instance = singletons[0];

							for(int n = 1 ; n < singletons.Length ; n++)
							{
								Destroy( singletons[n].gameObject );
							}

							return m_instance;
						}
					}

                    //if the application is quitting dont create more singletons
                    if(ApplicationIsQuitting)
                        return null;

					GameObject go = new GameObject( typeof(T).Name );
					m_instance = go.AddComponent<T>();

				}

				return m_instance;

			}
		}



		public static bool ApplicationIsQuitting = false;
		

		public static bool DestroyOnLoad
		{
			get
			{
				return m_instance.m_destroyOnLoad;
			}
		}


		[SerializeField] protected bool 	m_destroyOnLoad	= true;
		
		protected virtual void Awake()
		{
			if( !ReferenceEquals( (object)instance , (object)gameObject.GetComponent<T>() ))
			{
				//destroy repeat instance
				Destroy(gameObject);
			}
		}

		protected virtual void Start()
		{
            //get the instance
            T singleton = instance;

            if (!DestroyOnLoad && instance.transform.parent != null)
            {
                instance.transform.parent = null;
            }
           

			if(!DestroyOnLoad)
			{
				DontDestroyOnLoad( m_instance );
			}



		}
		

		protected virtual void OnApplicationQuit()
		{
            ApplicationIsQuitting = true;

        }


	}

}
