using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

// 에디터에서 디버그, 테스트와 관련된 작업을 할 수 있도록 도와줍니다.

namespace DebugTestEditor
{

	public class DebugEditor
	{

		#region Field

		const string PREFS_IsClearCache_Auto = "IsClearCache_Auto";
		public static bool IsClearCache_Auto
		{
			get { return EditorPrefs.GetBool(PREFS_IsClearCache_Auto, false); }
			set { EditorPrefs.SetBool(PREFS_IsClearCache_Auto, value); }
		}

		#endregion


		#region Caching.

		// 애셋번들 제거.
		[MenuItem("Debug Test/Caching/Clear Cache")]
		public static void Caching_ClearCache()
		{
			if (Caching.ClearCache())
			{
				EditorUtility.DisplayDialog("알림", "캐시가 삭제되었습니다.", "확인");
			}
			else
			{
				EditorUtility.DisplayDialog("오류", "캐시 삭제에 실패했습니다.", "확인");
			}
		}

		// 애셋번들 제거 자동화.
		[MenuItem("Debug Test/Caching/Clear Cache_Auto")]
		public static void Validate_Caching_ClearCache_Auto()
		{
			IsClearCache_Auto = !IsClearCache_Auto;

			Menu.SetChecked("Debug Test/Caching/Clear Cache_Auto", IsClearCache_Auto);

			if (IsClearCache_Auto == true)
			{
				EditorUtility.DisplayDialog("알림", "\"애셋번들 자동 제거\"가 자동으로 활성화 되었습니다.", "확인");
			}
			else
			{
				EditorUtility.DisplayDialog("알림", "\"애셋번들 자동 제거\"가 비활성화 되었습니다.", "확인");
			}
		}

		// 애셋번들 제거 자동화. (게임 재생 했을 경우 실행).
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void FirstSceneLoad()
		{
			if (IsClearCache_Auto == true)
			{
				Caching_ClearCache();

				//UnityEngine.SceneManagement.SceneManager.LoadScene(EditorSceneManager.GetActiveScene().name); // 특정씬 열기.
			}
		}

		#endregion


		#region PlayerPrefs.

		// PlayerPrefs 제거.
		[MenuItem("Debug Test/PlayerPrefs/Delete All")]
		public static void PlayerPrefs_DeleteAll()
		{
			PlayerPrefs.DeleteAll();

			EditorUtility.DisplayDialog("알림", "PlayerPrefs 삭제되었습니다.", "확인");
		}

		#endregion


		#region Create.

		[MenuItem("GameObject/UI/DummyUICanvas")]
		public static void Create_GameObject_DummyUICanvas()
		{
			GameObject testUIObj = GameObject.Instantiate(Resources.Load<GameObject>("TestUI"));
			testUIObj.name = "TestUI";
		}

		#endregion

	}

}
