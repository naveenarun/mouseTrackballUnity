The Unity project in Polarized Slats can be opened by going to the Assets folder and opening progress.unity. This should work right out of the box. You need a polarized screen with 1920x1080 resolution to run it.

The MATLAB code is in the trackball code folder. You should run MotionSensorFl.m and MotionSensor.m to set up the trackball reader. The trackball reader is an optical mouse connected to an Arduino (with pin connections determined by Francisco Luongo). Running varunCode.m should create a UDP instance and send UDP packets containing mouse information. These UDP packets can be read by the udpServerSample script in the Unity project.

(If you run varunCode.m and there's an error, MATLAB will have opened up a UDP port but not closed it, making it impossible to run the program again. This means you'll have to either close the port manually or restart MATLAB.)
