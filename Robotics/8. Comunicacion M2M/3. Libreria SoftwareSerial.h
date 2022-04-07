// La velocidad depende del modelo de ESP-0 siendo habituales 9600 y 115200
const int baudRate = 9600;
 
#include "SoftwareSerial.h"
SoftwareSerial softSerial(2, 3); // RX, TX
 
void setup()
{
   Serial.begin(baudRate);
   softSerial.begin(baudRate);
}
 
void loop()
{
   // En monitor serial lo que envio ESP-01 por serial
   if (softSerial.available())
   {
      Serial.print((char)softSerial.read());
   }

   // Mostrar en ESP-01 lo que se envio por monitor serial
   if (Serial.available())
   {
      softSerial.print((char)Serial.read());
   }
}
