using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sd : MonoBehaviour {
    public Text text;
    public Image mImage;
    public Button butn;
    public Text text1;

    private AndroidJavaClass jc;
    private AndroidJavaObject jo;
    void Start () {
        
        jc= new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
      
    }


    public  void ss()
    {
        //OpenGallery
        int res = jo.Call<int>("Adda", 56, 55);
        text.text = res.ToString();
        print("调用成功");
        jo.Call("OpenGallery");

    }
    public void GetImagePath(string imagePath)
    {
        if (imagePath == null)
            return;
        text1.text = imagePath;
        StartCoroutine("LoadImage", imagePath);
    }

    private IEnumerator LoadImage(string imagePath)
    {
        WWW www = new WWW("file://" + imagePath);
        text1.text = text1.text + "\n www开始加载";
        yield return www;
        text1.text = text1.text + "\n www加载完成";
        if (www.error == null)
            StartCoroutine("UpdataImage", www.texture);
        else
            text1.text = text1.text + "\n" + www.error;


    }

    private IEnumerator UpdataImage(Texture2D texture)
    {
        text1.text = text1.text + "\n 开始转化为精灵";
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        mImage.sprite = sprite;
        text1.text = text1.text + "\n 转换结束";
        yield return new WaitForSeconds(0.01f);
        Resources.UnloadUnusedAssets();
    }
}
