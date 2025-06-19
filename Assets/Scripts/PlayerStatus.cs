public class PlayerStatus
{
    private readonly float ExperienceGainRate = 1.1f;

    public int HP { get; set; } = 100;
    public int MaxHP { get; private set; } = 100;
    public int SP { get; set; } = 50;
    public int MaxSP { get; private set; } = 50;
    public int Level { get; private set; } = 1;
    public int Experience { get; private set; } = 0;
    public int MaxExperience { get; private set; } = 100;

    public void AddExperience(int amount)
    {
        this.Experience += amount;

        if (this.Experience >= this.MaxExperience)
        {
            this.LevelUp();
        }
    }

    private void LevelUp()
    {
        this.Level++;
        this.Experience %= this.MaxExperience;
        this.MaxExperience = (int)(this.MaxExperience * this.ExperienceGainRate);

        this.MaxHP = (int)(this.MaxHP * this.ExperienceGainRate);
        this.MaxSP = (int)(this.MaxSP * this.ExperienceGainRate);
        this.HP = this.MaxHP;
        this.SP = this.MaxSP;
    }
}
