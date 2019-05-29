using UnityEngine.UI;
[System.Serializable]
public class GUIPlayer
{
    public Image VidaImage;
    public Image ManaImage;
    public Text VidaText;
    public Text ManaText;

    public void SetVida(float vida, float vidaMax) {
        VidaImage.fillAmount = vida / vidaMax;
        VidaText.text = string.Format("{0}/{1}",vida,vidaMax);
    }

    public void SetMana(float mana, float manaMax)
    {
        ManaImage.fillAmount = mana / manaMax;
        ManaText.text = string.Format("{0}/{1}", mana, manaMax);
    }


}
