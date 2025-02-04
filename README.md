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

  ### Full simpleReader code to read from arduino in a gameObject
  ```csharp
  using UnityEngine;

public class SimpleDataReader : MonoBehaviour
{
  private ArduinoCommunicator arduinoCommunicator;
  void Start()
  {
    arduinoCommunicator = GameObject.FindFirstObjectByType<ArduinoCommunicator>();
  }

  // Update is called once per frame
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
      if (numbers[0] == 0 && numbers[1] == 1){
        transform.Translate(-0.01f, 0, 0);
        //DO STUFF HERE
      }
      data = "";
    }
  }
}
```
```csharp
using UnityEngine;

public class MoveAt45Degrees : MonoBehaviour
{
    // Set your desired speed in the Inspector.
    public float speed = 5f;

    void Update()
    {
        // Calculate the angle in radians (45° in radians)
        float angleInRadians = 45f * Mathf.Deg2Rad;
        
        // Create the direction vector using cosine and sine.
        // This results in a normalized vector pointing 45° upward from the horizontal.
        Vector3 direction = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0f);
        
        // Move the GameObject by adjusting its position.
        // Multiplying by Time.deltaTime makes the movement frame rate independent.
        transform.position += direction * speed * Time.deltaTime;
    }
}
```