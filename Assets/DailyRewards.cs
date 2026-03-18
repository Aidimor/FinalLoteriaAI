using System;
using UnityEngine;

public class DailyRewards : MonoBehaviour
{
    public int rewardAmount = 100;
    public double hoursToWait = 24;

    private const string LAST_CLAIM_KEY = "LAST_DAILY_REWARD";
    public bool _erase;

    // Verificar si ya puede reclamar
    public bool CanClaim()
    {
        if (!PlayerPrefs.HasKey(LAST_CLAIM_KEY))
            return true;

        string lastClaimString = PlayerPrefs.GetString(LAST_CLAIM_KEY);
        DateTime lastClaimTime = DateTime.Parse(lastClaimString);

        double hoursPassed = (DateTime.Now - lastClaimTime).TotalHours;

        return hoursPassed >= hoursToWait;
    }

    // Reclamar recompensa
    public void ClaimReward()
    {
        if (!_erase)
        {
            if (!CanClaim())
            {
                Debug.Log("Aún no disponible");
                return;
            }
        }

        GetComponent<ControladorCarta>()._onDailyReward = true;

        // DAR RECOMPENSA
        GetComponent<PesoController>().ChestBigRewardChest();
        GetComponent<ControladorCarta>()._chessAnimator.SetTrigger("Chest");

        //  GUARDAR FECHA (ESTO ES LO QUE TE FALTA)
        PlayerPrefs.SetString(LAST_CLAIM_KEY, DateTime.Now.ToString());
        PlayerPrefs.Save();

        Debug.Log("Recompensa diaria otorgada");
    }

    // Tiempo restante
    public TimeSpan TimeRemaining()
    {
        if (!PlayerPrefs.HasKey(LAST_CLAIM_KEY))
            return TimeSpan.Zero;

        DateTime lastClaimTime = DateTime.Parse(PlayerPrefs.GetString(LAST_CLAIM_KEY));
        DateTime nextClaimTime = lastClaimTime.AddHours(hoursToWait);

        return nextClaimTime - DateTime.Now;
    }
}