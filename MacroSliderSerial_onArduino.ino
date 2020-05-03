/*
 Name:		MacroSliderSerial.ino
 Created:	1/31/2020 11:41:43 AM
 Author:	fkollar
*/

/*
Step              Lenght
100*Full  1 mm
Full      0,01 mm       10µm
1/2       0,005 mm      5 µm
1/4       0,0025 mm     2,5 µm
1/8       0,00125 mm    1,25 µm
1/16      0,000625 mm   0,625 µm
1/32      0,0003125 mm  0,3125 µm

MODE0	MODE1	MODE2	Microstep Resolution
--------------------------------------------
Low	    Low	    Low	    Full step
High	Low	    Low	    Half step
Low	    High	Low	    1/4 step
High	High	Low	    1/8 step
Low	    Low	    High	1/16 step
High	Low	    High	1/32 step
Low	    High	High	1/32 step
High	High	High	1/32 step


Wiring:

:::::::::::::::::::::::::::::::
::::::::::::IR LED:::::::::::::
:::::::::::::::::::::::::::::::
:: IR LED       Arduino Nano ::
:::::::::::::::::::::::::::::::
:: Anode(+)---RESISTOR---D5  ::
:: Cathode(-)-----------GND  ::
:::::::::::::::::::::::::::::::

:::::::::::::::::::::::::::::::
:::::::::HOME SWITCH:::::::::::
:::::::::::::::::::::::::::::::
:: IR LED       Arduino Nano ::
:::::::::::::::::::::::::::::::
::    PIN1------------D0     ::
::    PIN2------------GND    ::
:::::::::::::::::::::::::::::::

:::::::::::::::::::::::::::::::
::::::::::::JOYSTICK:::::::::::
:::::::::::::::::::::::::::::::
:: JOYSTICK     Arduino Nano ::
:::::::::::::::::::::::::::::::
::   UP-------------D8       ::
::   DOWN-----------D9       ::
::   LEFT-----------D6       ::
::   RIGHT----------D7       ::
::   SELECT---------D10      ::
::   COMMON---------GND      ::
:::::::::::::::::::::::::::::::

:::::::::::::::::::::::::::::::
:::::::::::DRV8825:::::::::::::
:::::::::::::::::::::::::::::::
:: DRV8825      Arduino Nano ::
:::::::::::::::::::::::::::::::
::   ENABLE-----NC           ::
::   M0---------D11          ::
::   M1---------D12          ::
::   M2---------D13          ::
::   RESET------Arduino VCC  ::
::   SLEEP------Arduino VCC  ::
::   STEP-------D3           ::
::   DIR--------D2           ::
::   VMOT-------12V+         ::
::   GND--------12V-         ::
::   B2---------NEMA17_B2    ::
::   B1---------NEMA17_B1    ::
::   A1---------NEMA17_A1    ::
::   A2---------NEMA17_A2    ::
::   FAULT------NC           ::
::   GND--------Arduino GND  ::
:::::::::::::::::::::::::::::::

:::::::::::::::::::::::::::::::
::::::::LCD Display I2C::::::::
:::::::::::::::::::::::::::::::
:: LCD          Arduino Nano ::
:::::::::::::::::::::::::::::::
::   GND-------------GND     ::
::   VCC-------------5V      ::
::   SDA-------------A4      ::
::   SCL-------------A5      ::
:::::::::::::::::::::::::::::::

*/
#define home_switch 4   // Pin 0 connected to Home Switch (MicroSwitch)
#define dir_pin 2
#define step_pin 3
//#define sleep_pin 4     // Pin 4 connected to DRV8825 (Stepper Motor Driver)
#define LEDpin 5        //Nikon IR remote LED PIN is D6
#define M0 11
#define M1 12
#define M2 13
#define stepsPerRevolution 1


//Define LCD
#include <LiquidCrystal.h>
#include <Wire.h> 
#include <LiquidCrystal_I2C.h>
LiquidCrystal_I2C lcd(0x27, 20, 4);  // set the LCD address to 0x27 for a 16 chars and 2 line display

//Input & Button Logic
const int numOfInputs = 5;
const int inputPins[numOfInputs] = { 6,7,8,9,10 };
int inputState[numOfInputs];
int lastInputState[numOfInputs] = { LOW,LOW,LOW,LOW,LOW };
bool inputFlags[numOfInputs] = { LOW,LOW,LOW,LOW,LOW };
long lastDebounceTime[numOfInputs] = { 0,0,0,0,0 };
long debounceDelay = 5;

//Define special characters
byte aa[8] = {
  B00010,
  B00100,
  B01110,
  B00001,
  B01111,
  B10001,
  B01111,
  B00000
};
byte ee[8] = {
  B00010,
  B00100,
  B01110,
  B10001,
  B11111,
  B10000,
  B01110,
  B00000
};
byte ii[8] = {
  B00010,
  B00100,
  B00000,
  B00100,
  B00100,
  B00100,
  B00100,
  B00000
};
byte oo[8] = {
  B00101,
  B01010,
  B01110,
  B10001,
  B10001,
  B10001,
  B01110,
  B00000
};
byte ooo[8] = {
  B00010,
  B00100,
  B01110,
  B10001,
  B10001,
  B10001,
  B01110,
  B00000
};
byte uu[8] = {
  B01010,
  B00000,
  B10001,
  B10001,
  B10001,
  B10001,
  B01110,
  B00000
};
byte full[8] = {
  B11111,
  B11111,
  B11111,
  B11111,
  B11111,
  B11111,
  B11111,
  B11111
};

//LCD Menu Logic
const int numOfScreens = 7;
int numberToStep = 6;
int currentScreen = 0;
String screens[numOfScreens][2] = 
{
    {"K\002p darabsz\001m","db"},
    {"Motor l\002ptet\002s", "step"},
    {"Step szorz\005","szorz\005"},
    {"Delay a k\002p el\004tt","m\001sodperc"},
    {"Delay a k\002p ut\001n","m\001sodperc"},
    {"El\004tol\001s", "step"},
    {"\007\007\007Motor ind\003t\001sa\007\007\007","\007\007\007\007\007S.T.A.R.T.\007\007\007\007\007"}
};

float multiply = 1;         //Step multiplier
String Step;                //Actual Step value in String (Full, 1/2, 1/4, 1/8, 1/16, 1/32)
int steps = 0;              //Actual Step value
int inputData[10] = {};     //IsTakeAShotSet, IsStartPositionSet, IsSetZeroSet, NumberOfSnaps,SettleTime, HoldTime,StartPositionValue,DataMotorMicroStep, DataMotorSpeed, Istakeshots;

void setup() 
{
    Serial.begin(115200);

    for (int i = 0; i < numOfInputs; i++) 
    {
        pinMode(inputPins[i], INPUT);
        digitalWrite(inputPins[i], HIGH); // pull-up 20k
    }

    //Motor setup
    pinMode(M0, OUTPUT);     //DRV8825 Microstep selection M0 PIN set as OUTPUT
    pinMode(M1, OUTPUT);     //DRV8825 Microstep selection M1 PIN set as OUTPUT
    pinMode(M2, OUTPUT);     //DRV8825 Microstep selection M2 PIN set as OUTPUT
    pinMode(step_pin, OUTPUT);//DRV8825 STEP PIN set as OUTPUT //controls the mirosteps of the motor. Each HIGH pulse sent to this pin steps the motor by number of microsteps set by Microstep Selection Pins. The faster the pulses, the faster the motor will rotate.
    pinMode(dir_pin, OUTPUT); //DRV8825 DIRECTION PIN set as OUTPUT // controls the spinning direction of the motor. Pulling it HIGH drives the motor clockwise and pulling it LOW drives the motor counterclockwise.

    pinMode(LEDpin, OUTPUT);            //Nikon IR LED remote trigger set as output
    pinMode(home_switch, INPUT_PULLUP); //Slider home switch set as input

    lcd.init();
    lcd.createChar(1, aa); // á karakter
    lcd.createChar(2, ee); // é karakter
    lcd.createChar(3, ii); // í karakter
    lcd.createChar(4, oo); // õ karakter
    lcd.createChar(5, ooo); // ó karakter
    lcd.createChar(6, uu); // ü karakter
    lcd.createChar(7, full); // full karakter
    
    //Welcome screen
    lcd.backlight();
    lcd.clear();
    lcd.print("    Macro Slider");
    lcd.setCursor(0, 1);
    lcd.print("       v2.0");
    lcd.setCursor(0, 2);
    lcd.print("      SERIAL");
    delay(3000);

    setZero(); //Set Zero Point
}

void loop() 
{
    SerialCommunication();
    setInputFlags();
    resolveInputFlags();
}
//---------------------------------------------------------------------------------------------------------------------------------------
void setInputFlags() 
{
    for (int i = 0; i < numOfInputs; i++) 
    {
        int reading = digitalRead(inputPins[i]);
        if (reading != lastInputState[i]) 
        {
            lastDebounceTime[i] = millis();
        }
        if ((millis() - lastDebounceTime[i]) > debounceDelay) 
        {
            if (reading != inputState[i]) 
            {
                inputState[i] = reading;
                if (inputState[i] == HIGH) 
                {
                    inputFlags[i] = HIGH;
                }
            }
        }
        lastInputState[i] = reading;
    }
}
//---------------------------------------------------------------------------------------------------------------------------------------
void resolveInputFlags() 
{
    for (int i = 0; i < numOfInputs; i++) 
    {
        if (inputFlags[i] == HIGH) 
        {
            inputAction(i);
            inputFlags[i] = LOW;
            printScreen();
        }
    }
}
//---------------------------------------------------------------------------------------------------------------------------------------
void inputAction(int input) 
{
    if (input == 0) 
    {
        if (currentScreen == 0)
        {
            currentScreen = numOfScreens - 1;
        }
        else
        {
            currentScreen--;
        }
    }

    else if (input == 1)
    {
        if (currentScreen == numOfScreens - 1) 
        {
            currentScreen = 0;
        }
        else 
        {
            currentScreen++;
        }
    }

    else if (input == 2)
    {
        parameterChange(0);
    }

    else if (input == 3) 
    {
        parameterChange(1);
    }

    if (currentScreen == 5) 
    {
        if (input == 2) 
        {
            // Set 1/4 Step (Not so noisy)
            digitalWrite(M0, LOW);
            digitalWrite(M1, HIGH);
            digitalWrite(M2, LOW);

            if (steps < 10000) 
            {
                for (int i = 0; i < 100; i++) //  To make sure the Stepper doesn't go beyond the Home Position
                {
                    digitalWrite(dir_pin, LOW);  // (HIGH = anti-clockwise / LOW = clockwise)
                    digitalWrite(step_pin, HIGH);
                    delayMicroseconds(500);
                    digitalWrite(step_pin, LOW);
                    delayMicroseconds(500);
                    steps++;   // Decrease the number of steps taken
                }
            }
        }

        if (input == 3) 
        {
            if (steps > 0)
            {
                for (int i = 0; i < 100; i++) //  To make sure the Stepper doesn't go beyond the Home Position
                {
                    digitalWrite(dir_pin, HIGH);  // (HIGH = anti-clockwise / LOW = clockwise)
                    digitalWrite(step_pin, HIGH);
                    delayMicroseconds(500);
                    digitalWrite(step_pin, LOW);
                    delayMicroseconds(500);
                    steps--;   // Decrease the number of steps taken
                }
            }
        }
    }

    if (input == 4 && currentScreen == 6)       //If CurrentSreen is 6 and Select pushed then Shooting is starts
    {
        run();
    }
}
//---------------------------------------------------------------------------------------------------------------------------------------
//Values change
void parameterChange(int key) 
{

    //1. screen|Number Of Pictures
    if (currentScreen == 0) 
    {
        if (key == 0)                           //If UP pressed
        {
            inputData[3] = inputData[3] + 10;   //If Up pressed NumberOfPics ++
        }
        else if (key == 1)                      //If Down pressed
        {
            if (inputData[3] == 0)              //If NumberOfPics is "0" and Down pressed it doesn't change
            {
                inputData[3] = inputData[3];
            }
            else 
            {
                inputData[3] = inputData[3] - 10; //If Down pressed and the value is bigger than "0" then NumberOfPics -10
            }
        }
    }

    //2. screen|StepNumber
    if (currentScreen == 1) 
    {
        if (key == 0)                           //If UP pressed
        {
            if (inputData[7] == 5)
            {
                inputData[7] = 0;               //If UP pressed and the value is "5" then StepNumber will be "0"
            }
            else 
            {
                inputData[7]++;                 //If Up pressed StepNumber ++
            }
        }
        else if (key == 1)                      //If Down pressed
        {
            if (inputData[7] == 0)
            { 
                inputData[7] = 5;               //If DOWN pressed and the value is "0" then StepNumber will be "5"
            }
            else 
            {
                inputData[7]--;                 //If Down pressed StepNumber --
            }
        }
    }

    stepNumber(inputData[7]);

    //3. screen|Multiply
    if (currentScreen == 2) 
    {
        if (key == 0)                           //If UP pressed Multiply ++
        {
            multiply++;
        }
        else if (key == 1)                      //If Down pressed
        {
            if (multiply == 1)                  //If Multiply is "1" and Down pressed it doesn't change
            {
                multiply = multiply;
            }
            else 
            {
                multiply--;                     //If Down pressed and the value is bigger than "1" then Multiply --
            }
        }
    }

    //4. screen| Settle Time
    if (currentScreen == 3) 
    {
        if (key == 0)                           //If UP pressed Settle Time ++
        {
            inputData[4]++;
        }
        else if (key == 1)                      //If Down pressed
        {
            if (inputData[4] == 0)              //If Settle Time is "0" and Down pressed it doesn't change
            {
                inputData[4] = inputData[4];
            }
            else 
            {
                inputData[4]--;                 //If Down pressed and the value is bigger than "0" then Settle Time --
            }
        }
    }

    //5. screen| Hold Time
    if (currentScreen == 4) 
    {
        if (key == 0)                           //If UP pressed Hold Time ++
        {
            inputData[5]++;
        }
        else if (key == 1)                      //If Down pressed
        {
            if (inputData[5] == 0)              //If Hold Time is "0" and Down pressed it doesn't change
            {
                inputData[5] = inputData[5];
            }
            else 
            {
                inputData[5]--;                 //If Down pressed and the value is bigger than "0" then Hold Time --
            }
        }
    }
}
//---------------------------------------------------------------------------------------------------------------------------------------
//Print values to LCD
void printScreen() 
{
    lcd.clear();
    lcd.print(screens[currentScreen][0]);
    lcd.setCursor(0, 1);

    if (currentScreen != 6) 
    {
        if (currentScreen == 0) 
        {
            lcd.print(inputData[3]);
        }
        if (currentScreen == 1) 
        {
            lcd.print(Step);
        }
        if (currentScreen == 2) 
        {
            lcd.print(multiply);
        }
        if (currentScreen == 3) 
        {
            lcd.print(inputData[4]);
        }
        if (currentScreen == 4) 
        {
            lcd.print(inputData[5]);
        }
        if (currentScreen == 5) 
        {
            lcd.print(steps);
        }

        lcd.print(" ");
        lcd.print(screens[currentScreen][1]);
    }


    if (currentScreen == 6) 
    {
        lcd.clear();
        lcd.print("\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007");
        lcd.setCursor(0, 1);
        lcd.print(screens[currentScreen][0]);
        lcd.setCursor(0, 2);
        lcd.print(screens[currentScreen][1]);
        lcd.setCursor(0, 3);
        lcd.print("\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007\007");
    }
}
//---------------------------------------------------------------------------------------------------------------------------------------
//Nema17 Stepper motor Step value
void stepNumber(int numberToStep)
{
    if (numberToStep == 0)
    {
        Step = { "Full" };
    }

    if (numberToStep == 1)
    {
        Step = { "1/2" };
    }

    if (numberToStep == 2)
    {
        Step = { "1/4" };
    }

    if (numberToStep == 3)
    {
        Step = { "1/8" };
    }

    if (numberToStep == 4)
    {
        Step = { "1/16" };
    }

    if (numberToStep == 5)
    {
        Step = { "1/32" };
    }
}
//---------------------------------------------------------------------------------------------------------------------------------------
//Nikon IR remote trigger
void takePhoto(void) 
{
    int i;
    for (i = 0; i < 76; i++) 
    {
        digitalWrite(LEDpin, HIGH);
        delayMicroseconds(7);
        digitalWrite(LEDpin, LOW);
        delayMicroseconds(7);
    }

    delay(27);
    delayMicroseconds(810);
   
    for (i = 0; i < 16; i++) 
    {
        digitalWrite(LEDpin, HIGH);
        delayMicroseconds(7);
        digitalWrite(LEDpin, LOW);
        delayMicroseconds(7);
    }
    
    delayMicroseconds(1540);
    
    for (i = 0; i < 16; i++) 
    {
        digitalWrite(LEDpin, HIGH);
        delayMicroseconds(7);
        digitalWrite(LEDpin, LOW);
        delayMicroseconds(7);
    }
    
    delayMicroseconds(3545);
    
    for (i = 0; i < 16; i++) 
    {
        digitalWrite(LEDpin, HIGH);
        delayMicroseconds(7);
        digitalWrite(LEDpin, LOW);
        delayMicroseconds(7);
    }
}
//---------------------------------------------------------------------------------------------------------------------------------------
//Set Step Zero point
void setZero(void) 
{
    lcd.clear();
    lcd.print("    Macro Slider");
    lcd.setCursor(0, 1);
    lcd.print("       v2.0");
    lcd.setCursor(0, 2);
    lcd.print("   Nulla poz\003ci\005");
    lcd.setCursor(0, 3);
    lcd.print("     be\001ll\003t\001sa");

    digitalWrite(M0, LOW);
    digitalWrite(M1, HIGH);
    digitalWrite(M2, LOW);

    while (digitalRead(home_switch))   // Do this until the switch is activated 
    {  
        digitalWrite(dir_pin, HIGH);      // (HIGH = anti-clockwise / LOW = clockwise)
        digitalWrite(step_pin, HIGH);
        delayMicroseconds(500);                     // Delay to slow down speed of Stepper
        digitalWrite(step_pin, LOW);
        delayMicroseconds(500);
    }

    while (!digitalRead(home_switch))  // Do this until the switch is deactivated
    {
        digitalWrite(dir_pin, LOW);
        digitalWrite(step_pin, HIGH);
        delayMicroseconds(1000);                       // More delay to slow even more while moving away from switch
        digitalWrite(step_pin, LOW);
        delayMicroseconds(1000);
    }

    lcd.clear();
    lcd.print("    Macro Slider");
    lcd.setCursor(0, 1);
    lcd.print("       v2.0");
    lcd.setCursor(0, 2);
    lcd.print("   Nulla poz\003ci\005");
    lcd.setCursor(0, 3);
    lcd.print("     be\001ll\003tva");
    delay(2000);

    steps = 0;  // Reset position variable to zero
}
//---------------------------------------------------------------------------------------------------------------------------------------
void MicrostepSelection(int Step_select) 
{
    switch (Step_select) 
    {
     case 0:    // Full STEP
        digitalWrite(M0, LOW);
        digitalWrite(M1, LOW);
        digitalWrite(M2, LOW);
        break;

     case 1:    // 1/2 STEP      
        digitalWrite(M0, HIGH);
        digitalWrite(M1, LOW);
        digitalWrite(M2, LOW);
        break;

     case 2:    //  1/4 STEP
        digitalWrite(M0, LOW);
        digitalWrite(M1, HIGH);
        digitalWrite(M2, LOW);
        break;

     case 3:    //  1/8 STEP
        digitalWrite(M0, HIGH);
        digitalWrite(M1, HIGH);
        digitalWrite(M2, LOW);
        break;

     case 4:    //  1/16 
        digitalWrite(M0, LOW);
        digitalWrite(M1, LOW);
        digitalWrite(M2, HIGH);
        break;

     case 5:    //  1/32 STEP
        digitalWrite(M0, HIGH);
        digitalWrite(M1, LOW);
        digitalWrite(M2, HIGH);
        break;
    }
}
//---------------------------------------------------------------------------------------------------------------------------------------
void run(void)
{
MicrostepSelection(inputData[7]); //Beállításra kerül a Step értéke

for (int s = 0; s <= inputData[3]; s++)
{ // megadja hogy hány képet csináljon

    lcd.clear();
    lcd.print("    F\002nyk\002pez\002s");

    lcd.setCursor(0, 1);
    lcd.print("    folyamatban");

    lcd.setCursor(0, 2);
    lcd.print("Elk\002sz\006lt k\002pek: ");

    lcd.setCursor(0, 3);
    lcd.print("       ");
    lcd.print(inputData[3]);
    lcd.print("/");
    lcd.print(s);

    // Set the spinning direction clockwise:
    digitalWrite(dir_pin, LOW);
    // Spin the stepper motor 1 revolution slowly:
    for (int i = 0; i < multiply * stepsPerRevolution; i++)
    {
        // These four lines result in 1 step:
        digitalWrite(step_pin, HIGH);
        delayMicroseconds(1000);
        digitalWrite(step_pin, LOW);
        delayMicroseconds(1000);
    }

    delay(inputData[4] * 1000);
    takePhoto();

    delay(inputData[5] * 1000);
}
//Kiírja, hogy a képek elkészültek
lcd.clear();
lcd.setCursor(0, 1);
lcd.print("      A k\002pek");
lcd.setCursor(0, 2);
lcd.print("    elk\002sz\006ltek");
delay(3000);

setZero(); //Beállítja a nulla pozíciót
}
//---------------------------------------------------------------------------------------------------------------------------------------

void SerialCommunication(void)
{
    //lcd.clear();
//lcd.print("COM PORT ACTIVE");
    if (Serial.available())
    {
        //lcd.clear();
        //lcd.print("COM PORT ACTIVE");
        //String aa[10] = {};
        //expected:     X1;2;3;4;5;6;7;8;9;10
        //inputData[0] = 0; //IsTakeAShotSet
        //inputData[1] = 0; //IsStartPositionSet
        //inputData[2] = 0; //IsSetZeroSet
        //inputData[3] = 0; //NumberOfSnaps
        //inputData[4] = 0; //SettleTime
        //inputData[5] = 0; //HoldTime
        //inputData[6] = 0; //StartPositionValue
        //inputData[7] = 0; //DataMotorMicroStep
        //inputData[8] = 0; //DataMotorSpeed
        //inputData[9] = 0; //Shotstart
        //Serial.println(inputData[0]);
        byte START = Serial.read();
        if (START == 'X')
        {
            String serialData1 = Serial.readStringUntil(';');
            Serial.read(); //next character is comma, so skip it using this
            String serialData2 = Serial.readStringUntil(';');
            Serial.read();
            String serialData3 = Serial.readStringUntil(';');
            Serial.read();
            String serialData4 = Serial.readStringUntil(';');
            Serial.read();
            String serialData5 = Serial.readStringUntil(';');
            Serial.read();
            String serialData6 = Serial.readStringUntil(';');
            Serial.read();
            String serialData7 = Serial.readStringUntil(';');
            Serial.read();
            String serialData8 = Serial.readStringUntil(';');
            Serial.read();
            String serialData9 = Serial.readStringUntil(';');
            Serial.read();
            String serialData10 = Serial.readStringUntil('\0');
            inputData[0] = serialData1.toInt(); //IsTakeAShotSet
            inputData[1] = serialData2.toInt(); //IsStartPositionSet
            inputData[2] = serialData3.toInt(); //IsSetZeroSet
            inputData[3] = serialData4.toInt(); //NumberOfSnaps
            inputData[4] = serialData5.toInt(); //SettleTime
            inputData[5] = serialData6.toInt(); //HoldTime
            inputData[6] = serialData7.toInt(); //StartPositionValue
            inputData[7] = serialData8.toInt(); //DataMotorMicroStep
            inputData[8] = serialData9.toInt(); //DataMotorSpeed
            inputData[9] = serialData10.toInt(); //Shotstart
            
            Serial.println(inputData[0]);
            Serial.println(inputData[1]);
            Serial.println(inputData[2]);
            Serial.println(inputData[3]);
            Serial.println(inputData[4]);
            Serial.println(inputData[5]);
            Serial.println(inputData[6]);
            Serial.println(inputData[7]);
            Serial.println(inputData[8]);
            Serial.println(inputData[9]);
           
            if (inputData[0] == 1)
            {
                takePhoto();
                inputData[0] = 0;
            }
            else if (inputData[1] == 1)
            {
                digitalWrite(M0, LOW);
                digitalWrite(M1, HIGH);
                digitalWrite(M2, LOW);
                int difference = 0;
                difference = inputData[6] - steps;
                
                
                if (difference > 0) 
                {
                    
                        for (int i = 0; i < difference; i++) //  To make sure the Stepper doesn't go beyond the Home Position
                        {
                            digitalWrite(dir_pin, LOW);  // (HIGH = anti-clockwise / LOW = clockwise)
                            digitalWrite(step_pin, HIGH);
                            delayMicroseconds(inputData[8]);
                            digitalWrite(step_pin, LOW);
                            delayMicroseconds(inputData[8]);
                            steps++;   // Decrease the number of steps taken
                        }
                    
                    //inputData[1] = 0;
                }
                else if (difference < 0) 
                {
                    
                        difference = difference * -1;
                        for (int i = 0; i < difference; i++) //  To make sure the Stepper doesn't go beyond the Home Position
                        {
                            digitalWrite(dir_pin, HIGH);  // (HIGH = anti-clockwise / LOW = clockwise)
                            digitalWrite(step_pin, HIGH);
                            delayMicroseconds(inputData[8]);
                            digitalWrite(step_pin, LOW);
                            delayMicroseconds(inputData[8]);
                            steps--;   // Decrease the number of steps taken
                        }
     
                    //inputData[1] = 0;
                }
                inputData[1] = 0;
            }
            else if (inputData[2] == 1)
            {
                setZero();
                inputData[2] = 0;
            }
            else if (inputData[9] == 1)
            {
                run();
                inputData[9] = 0;
            }
            /*
            else if(inputData[9] == 1)
            {
                    //ide jön a program futása
            //Ide jön a Stepselection

            MicrostepSelection(inputData[7]); //Beállításra kerül a Step értéke

            for (int s = 0; s <= inputData[3]; s++)
            { // megadja hogy hány képet csináljon

                lcd.clear();
                lcd.print("    F\002nyk\002pez\002s");

                lcd.setCursor(0, 1);
                lcd.print("    folyamatban");

                lcd.setCursor(0, 2);
                lcd.print("Elk\002sz\006lt k\002pek: ");

                lcd.setCursor(0, 3);
                lcd.print("       ");
                lcd.print(inputData[3]);
                lcd.print("/");
                lcd.print(s);

                // Set the spinning direction clockwise:
                digitalWrite(dir_pin, LOW);
                // Spin the stepper motor 1 revolution slowly:
                for (int i = 0; i < multiply * stepsPerRevolution; i++)
                {
                    // These four lines result in 1 step:
                    digitalWrite(step_pin, HIGH);
                    delayMicroseconds(1000);
                    digitalWrite(step_pin, LOW);
                    delayMicroseconds(1000);
                }

                delay(inputData[4] * 1000);
                takePhoto();

                delay(inputData[5] * 1000);
            }
            //Kiírja, hogy a képek elkészültek
            lcd.clear();
            lcd.setCursor(0, 1);
            lcd.print("      A k\002pek");
            lcd.setCursor(0, 2);
            lcd.print("    elk\002sz\006ltek");
            delay(3000);

            setZero(); //Beállítja a nulla pozíciót
            }
            */
        }
    }

}