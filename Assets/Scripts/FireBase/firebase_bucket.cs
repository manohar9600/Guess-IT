using UnityEngine;
using Firebase;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;
public class firebase_bucket : MonoBehaviour {

    Firebase.Storage.StorageReference storage_ref;
    public GameObject GameManager;

    int err_trails = 0;
    void Start(){
        Firebase.Storage.FirebaseStorage storage = 
                                Firebase.Storage.FirebaseStorage.DefaultInstance;

        storage_ref = storage.GetReferenceFromUrl("gs://guess-it-adcd9.appspot.com");
    }
    public void get_next(string folder_name, string file_, string file_ext,
                        int start_at, int ind, int ind_num)
    {
        string file_name = file_ + file_ext;
        Debug.Log(file_name);
        // Points to "images"
        Firebase.Storage.StorageReference folder_ref = storage_ref.Child(folder_name);

        // Points to "images/space.jpg"
        // Note that you can use variables to create child values
        Firebase.Storage.StorageReference file_ref = folder_ref.Child(file_name);

        const long maxAllowedSize = 1 * 1024 * 1024;
        Texture2D tex = new Texture2D(2048, 2048);
        file_ref.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
                if(this.err_trails < 5 && start_at+1 < 3){
                    err_trails += 1;
                    if(file_ext == ".png"){
                        get_next(folder_name, file_, ".jpg", start_at, ind, ind_num);
                    }else{
                        GameManager.GetComponent<ShuffleLogic>().load_next(start_at);
                    }
                }else{
                    if (this.err_trails >= 5){
                        Debug.Log("Having Connection Issues");
                    }
                }
            }
            else
            {
                this.err_trails = 0;
                byte[] fileContents = task.Result;
                Debug.Log("Finished downloading!");
                tex.LoadImage(fileContents);
                var test_sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height),
                                            new Vector2());
                GameManager.GetComponent<ShuffleLogic>().
                            receive_sprite(test_sprite, file_, start_at, ind, ind_num);
                if(start_at+1 < 3){
                    GameManager.GetComponent<ShuffleLogic>().
                            load_next(start_at+1);
                }
            }
        });

    }
}
