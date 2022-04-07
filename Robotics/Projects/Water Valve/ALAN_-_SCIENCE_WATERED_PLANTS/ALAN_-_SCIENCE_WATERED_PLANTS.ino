#include<LiquidCrystal.h> 
LiquidCrystal lcd(12,11,5,4,3,2);

int RELAY=7;   //D7
int RELAY2=8;   // D8
int SENSOR=16;  //A2

void setup() {
  pinMode(RELAY, OUTPUT);
  pinMode(RELAY2, OUTPUT);
  
  pinMode(SENSOR, INPUT);

  lcd.begin(16,2);
  lcd.print("PRUEBA");
  
}

void loop() {
  int  leer = analogRead(SENSOR);
  int humedad = map(leer, 0, 950,0,100);
  
  String valor="DRYNESS "+String(humedad)+ " %";
  //String valor = String(leer);
 
  lcd.begin(16,2);
  lcd.print(valor);
  delay(500);
  
 (humedad<60) ? digitalWrite(RELAY, LOW) : digitalWrite(RELAY,HIGH);
 (humedad<60) ? digitalWrite(RELAY2, HIGH) : digitalWrite(RELAY2,LOW);
}

