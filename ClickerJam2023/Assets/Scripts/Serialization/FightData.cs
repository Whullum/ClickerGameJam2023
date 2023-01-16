[System.Serializable]
public class FightData
{
    public int WaveEnemyCount;
    public int WaveCurrentEnemyHealth;

    public FightData(int waveEnemyCount, int waveCurrentEnemyHealth)
    {
        WaveEnemyCount = waveEnemyCount;
        WaveCurrentEnemyHealth = waveCurrentEnemyHealth;
    }
}
