[System.Serializable]
public class GameDate
{
   // 是否再来一次游戏
   public static bool IsAgainGame = false;

   private bool _isFirstGame;
   private bool _isMusicOn;
   private int[] _bestScoreArr;
   private int _selectSkin;
   private bool[] _skinUnlocked;
   private int _diamondCount;

   public void SetIsFirstGame(bool isFirstGame)
   {
      _isFirstGame = isFirstGame;
   }

   public void SetIsMusicOn(bool isMusicOn)
   {
      _isMusicOn = isMusicOn;
   }

   public void SetBestScoreArr(int[] bestScoreArr)
   {
      _bestScoreArr = bestScoreArr;
   }

   public void SetSelectSkin(int selectSkin)
   {
      _selectSkin = selectSkin;
   }

   public void SetSkinUnlocked(bool[] skinUnlocked)
   {
      _skinUnlocked = skinUnlocked;
   }

   public void SetDiamondCount(int diamondCount)
   {
      _diamondCount = diamondCount;
   }

   // get

   public bool GetIsFirstGame()
   {
      return _isFirstGame;
   }

   public bool GetIsMusicOn()
   {
      return _isMusicOn;
   }

   public int[] GetBestScoreArr()
   {
      return _bestScoreArr;
   }

   public int GetSelectSkin()
   {
      return _selectSkin;
   }

   public bool[] GetSkinUnlocked()
   {
      return _skinUnlocked;
   }

   public int GetDiamondCount()
   {
      return _diamondCount;
   }

}
