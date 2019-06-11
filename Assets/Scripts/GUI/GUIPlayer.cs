using UnityEngine.UI;
[System.Serializable]
public class GUIPlayer
{
    public Image VidaImage;
    public Image ManaImage;
    public Image ExpImage;
    public Text txtLvl;
    public Text VidaText;
    public Text ManaText;
    public Text ExpText;


    public void SetVida(float vida, float vidaMax) {
        VidaImage.fillAmount = vida / vidaMax;
        VidaText.text = string.Format("{0}/{1}",vida,vidaMax);
    }

    public void SetMana(float mana, float manaMax)
    {
        ManaImage.fillAmount = mana / manaMax;
        ManaText.text = string.Format("{0}/{1}", mana, manaMax);
    }

    public void SetExp(int exp, int expMax){
        ExpImage.fillAmount = (float)exp/(float)expMax;
        ExpText.text = string.Format("{0}/{1}",exp,expMax);
    }

    public void SetLvl(int lvl){
        txtLvl.text = "Lvl: "+lvl;
    }

}
