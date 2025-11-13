# Unity_2025_Coding


### Files and packages related to experimental making

Check out the [XR - UNITY Documentation](https://www.icloud.com/freeform/0dcJW4AkDT0uDyV_DSHVgAQbg#XR_-_UNITY) for more information.

```arduino
int sen = 0;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  sen = analogRead(A0);
  sen = map(sen, 23, 990, 0, 1000);
  sen = constrain(sen, 0, 1000);
  Serial.print("0>");
  Serial.print(sen);
  Serial.println("<");
  delay(20);

}
```
