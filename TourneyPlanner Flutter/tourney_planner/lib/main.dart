import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:tourney_planner/src/blocs/team_bloc.dart';
import 'package:tourney_planner/src/blocs/tournament_bloc.dart';
import 'package:tourney_planner/src/locators/setup_locator.dart';
import 'package:tourney_planner/src/services/firebase_message_service.dart';
import 'src/app.dart';
import 'src/controllers/settings_controller.dart';
import 'src/services/settings_service.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  setupLocators();
  await Firebase.initializeApp();

  debugPrint(await getDeviceToken());

  FirebaseMessaging.onMessage.listen((RemoteMessage message) {
      debugPrint('Got a message whilst in the foreground!');
      debugPrint('Message data: ${message.data}');

      if (message.notification != null) {
        debugPrint('Message also contained a notification: ${message.notification}');
      }
    });
    
  FirebaseMessaging.instance.subscribeToTopic("matchup-update");
  FirebaseMessaging.onBackgroundMessage(_firebaseMessagingBackgroundHandler);
  // FirebaseMessaging.onBackgroundMessage((message) => )
  // Set up the SettingsController, which will glue user settings to multiple
  // Flutter Widgets.
  final settingsController = SettingsController(SettingsService());

  // Load the user's preferred theme while the splash screen is displayed.
  // This prevents a sudden theme change when the app is first displayed.
  await settingsController.loadSettings();

  // Run the app and pass in the SettingsController. The app listens to the
  // SettingsController for changes, then passes it further down to the
  // SettingsView.
  runApp( MultiBlocProvider(
    providers: [
      BlocProvider<TeamBloc>(
        create: (context) => TeamBloc()
      ),
      BlocProvider<TournamentBloc>(
        create: (context) => TournamentBloc()
      ),
    ], 
    child: MyApp(settingsController: settingsController))
    );
}

@pragma('vm:entry-point')
Future<void> _firebaseMessagingBackgroundHandler(RemoteMessage message) async {
  // If you're going to use other Firebase services in the background, such as Firestore,
  // make sure you call initializeApp before using other Firebase services.
  debugPrint("Handling a background message: ${message.messageId}");
  debugPrint("Handling a background message: ${message.data}");
  debugPrint("Handling a background message: ${message.notification}");
}