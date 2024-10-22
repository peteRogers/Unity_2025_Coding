# Y2_2024_Coding


### Start particleSystem for x seconds

```csharp
 public void StartParticleSystemForFiveSeconds()
    {
        StartCoroutine(PlayParticleSystemForDuration(0.5f));
    }
    IEnumerator PlayParticleSystemForDuration(float duration)
    {
        // Play the ParticleSystem
        particleSystem.Play();

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Stop the ParticleSystem
        particleSystem.Stop();
    }
```