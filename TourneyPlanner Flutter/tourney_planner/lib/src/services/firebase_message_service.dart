import 'dart:io';

import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart';

Future<String> getDeviceToken() async {
  return (await FirebaseMessaging.instance.getToken())!;
}

 Future<void> firebaseMessagingBackgroundHandler(RemoteMessage message) async {
       print("Handling a background message");
 }

 class ForegroundNotificationService {
  final FlutterLocalNotificationsPlugin _localNotification = FlutterLocalNotificationsPlugin();

  void firebaseInit() {
    FirebaseMessaging.onMessage.listen((message) { 
      RemoteNotification? notification = message.notification;
      AndroidNotification? android = message.notification!.android;
      AppleNotification? apple = message.notification!.apple;

      if (Platform.isIOS) {
        
      }
      else if (Platform.isAndroid) {

      }
    });
  }

  void _initLocalNotification(RemoteMessage message) async {
    // var androidInitSettings = message.
  }
 }