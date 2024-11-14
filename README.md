# Unity_2024_Coding


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
### Read multiple data from arduino
```csharp
void Update(){
    string data = arduinoCommunicator.ReceivedData;
    Debug.Log("Data from Arduino: " + data);
    if (!string.IsNullOrEmpty(data)){
      // Debug.Log("Data from Arduino: " + data);
      int[] numbers = System.Array.ConvertAll(data.Split('>'), int.Parse);
      if (numbers[0] == 7 && numbers[1] == 1){
        transform.Translate(0.01f, 0, 0);
        //DO STUFF HERE
      }
      data = "";
    }
  }
  ```
