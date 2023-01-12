using UnityEngine;
using System.Collections;

namespace Pathfinding {
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour {
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		IAstarAI ai;
		public float followRange = 10;
		bool startedFollowing = false;

		void OnEnable () {
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;
		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update () {
			if (ai != null) {
				//list of tags in priority order
				string[] priorityTags = new string[] {"Turret", "Player", "Beacon"};

				//iterate through the tags in priority order
				for (int i = 0; i < priorityTags.Length; i++) {
					//get all objects with this tag in the scene
					GameObject[] targets = GameObject.FindGameObjectsWithTag(priorityTags[i]);

					//iterate through the targets
					for (int j = 0; j < targets.Length; j++) {
						float distance = Vector2.Distance(targets[j].transform.position, transform.position);
						if (distance <= followRange) {
							ai.destination = targets[j].transform.position;
							return; //stop checking other targets once one is found
						}
					}
				}
			}
		}



	}
}
