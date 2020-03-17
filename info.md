# Netdaemon - MotionSnapshot for Discord

Send snapshots of all your cameras to Discord when your alarm triggers.

## Install

With HACS (0.23+).

## Configuration in motionsnapshot.yaml

``` 
motion_snapshot:
  class: MotionSnapshot

  ## Fill in with your own entities:
  Alarm: alarm_control_panel.house # change to your alarm
  FilePath: /config/www/{camera}_latest.jpg # the path where you want to store the snapshots, must include "/{camera}_latest.jpg"
  DiscordNotifier: discord # your notify service for discord, eg "notify.discord" - just write "discord"
  DiscordChannel: 123456789 # the channel id of your choice
  Cameras: # change to your camera/-s
    - camera.front_door
    - camera.garage
    - camera.yard
