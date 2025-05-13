# Pomodoro Timer  
## What is Pomodoro technique?  
The Pomodoro Technique is a time management method developed by Francesco Cirillo in the late 1980s. It uses a timer to break work into intervals, traditionally 25 minutes in length, separated by short breaks.  

Key Steps:  
- Choose a task to work on.
- Set a timer for 25 minutes (one “Pomodoro”).
- Work on the task until the timer rings.
- Take a short break (5 minutes).
- After four Pomodoros, take a longer break (15–30 minutes).

Purpose:  
The technique aims to improve focus, productivity, and mental agility by encouraging sustained concentration and regular rest.  

## Pomodoro Timer function   
During normal operation, the minutes are counted with a red background as shown below.  
![](./images/red.png)

When the break time starts, it turns light blue.  
![](./images/blue.png)

Even at the edge of the screen, the color changes with a picotone so that you can tell when it is time to take a break without having to gaze at it.  
If you want to pause the Pomodoro timer, click on it and it will turn green.  
While green, the timer stops and does not count down.  
![](./images/green.png)  

## Settings
In the configuration file, you can set the length of time for long breaks in minutes.  
If the setting is inappropriate or missing, the default value will be 30 minutes.  
To change the setting, see the included PomodoroTimer.dll.config.  

## Development  
- C#  
- Windows Presentation Foundation(WPF)
- .NET Framework 8.0