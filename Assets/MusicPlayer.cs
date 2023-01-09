using UnityEngine;


namespace MyAmazingGame.Components
{
	public enum MusicFiles
    {
        None,
        MenuMusic,
        LevelMusic
    }
	
	
    public class MusicPlayer : MonoBehaviour
    {
		//static instance of the MusicPlayer to check if we should keep or delete new instances
        static MusicPlayer Player_Instance;


        //two audio sources - the MusicPlayer script is attached to a Game Object
		//audio sources are added as children to the Game Object
		//audio sources have "Play On Awake" set to false
		//the audio sources are referenced by adding them in the Eidtor to these audio variables
        public AudioSource _Audio__Menu_Music;
        public AudioSource _Audio__Level_Music;


        private void Start()
        {            
            if (MusicPlayer.Player_Instance == null)
            {
                Debug.Log("MusicPlayer: Start: Initial Music Player: Dont Destroy");
				
				//dont destroy this instance
                DontDestroyOnLoad(this);
				
				//save a reference to this instance in the static variable
                MusicPlayer.Player_Instance = this;

				//track scene changes
                UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
				
                //start music
                this.Start_Stop_Music(MusicFiles.MenuMusic);               
            }
            else
            {
                Debug.Log("MusicPlayer: Start: NOT Initial Music Player: Destroy");
                Destroy(this.gameObject);
            }
        }
       

        void Start_Stop_Music(MusicFiles music_file)
        {
            //this assumes you have additional ui to enabled/disable this preference
            var play_music = PlayerPrefs.GetString("music");            
            if (play_music?.ToLower() == "false")                       
            {
                //stop all music
				this._Audio__Menu_Music.Stop();
				this._Audio__Level_Music.Stop();

                return;
            }



            if (music_file == MusicFiles.MenuMusic)
            {
                //stop others
                this._Audio__Level_Music.Stop();

                //start if not already playing
                if (this._Audio__Menu_Music.isPlaying == false)
                {
                    this._Audio__Menu_Music.Play();
                }
            }
            else if (music_file == MusicFiles.LevelMusic)
            {
                //stop others
                this._Audio__Menu_Music.Stop();

                //start if not already playing
                if (this._Audio__Level_Music.isPlaying == false)
                {
                    this._Audio__Level_Music.Play();
                }
            }       
        }


        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode load_scene_mode)
        {
            Debug.Log("MuscPlayer: OnSceneLoaded");
			
			//play theme music if the scene name is "Play"
            if (scene.name == "Play")
            {
                Start_Stop_Music(MusicFiles.LevelMusic);
            }
            else
            {
                Start_Stop_Music(MusicFiles.MenuMusic);
            }
        }
    }
}