public interface IntegrityInterface
{
    float integrity { get; }
    void TakeDamage(float dam, PhotonPlayer pView);
    void Die(PhotonPlayer pView);
}
